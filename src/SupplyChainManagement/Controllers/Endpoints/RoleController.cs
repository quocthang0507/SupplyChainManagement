﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Models;
using SupplyChainManagement.Models.AccountViewModels;
using SupplyChainManagement.Models.CRUD;
using SupplyChainManagement.Services;

namespace SupplyChainManagement.Controllers.Endpoints
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRoles _roles;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IRoles roles)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roles = roles;
        }

        /// <summary>
        /// Lấy danh sách các vai trò
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            await _roles.GenerateRolesFromPagesAsync();

            List<ApplicationRole> Items = _roleManager.Roles.ToList();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Lấy vai trò theo người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoleByApplicationUserId([FromRoute] string id)
        {
            await _roles.GenerateRolesFromPagesAsync();
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles.ToList();
            List<UserRoleViewModel> Items = new();
            int count = 1;
            foreach (var item in roles)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, item.Name);
                Items.Add(new UserRoleViewModel { CounterId = count, ApplicationUserId = id, RoleName = item.Name, IsHaveAccess = isInRole });
                count++;
            }

            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        /// <summary>
        /// Cập nhật vai trò của người dùng
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserRole([FromBody] CrudViewModel<UserRoleViewModel> payload)
        {
            UserRoleViewModel userRole = payload.value;
            if (userRole != null)
            {
                var user = await _userManager.FindByIdAsync(userRole.ApplicationUserId);
                if (user != null)
                {
                    if (userRole.IsHaveAccess)
                    {
                        await _userManager.AddToRoleAsync(user, userRole.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                    }
                }
                return Ok(userRole);
            }
            return BadRequest();
        }
    }
}
