﻿using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoDbGenericRepository.Attributes;

namespace SupplyChainManagement.Models
{
    [CollectionName("Roless")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
    }
}
