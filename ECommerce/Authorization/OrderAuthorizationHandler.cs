using ECommerce.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Authorization
{
    public class OrderAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, Order>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, ResourceOwnerRequirement requirement, Order resource)
        {
            if (context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value == "Admin")
            {
                context.Succeed(requirement);
            }

            if (context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value == resource.UserId.ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}