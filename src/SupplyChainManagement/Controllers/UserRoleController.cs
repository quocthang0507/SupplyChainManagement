﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Models;

namespace SupplyChainManagement.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Role()
        {
            return View();
        }

        public IActionResult ChangeRole()
        {
            return View();
        }

        public async Task<IActionResult> UserProfile()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(user);
        }
    }
}
