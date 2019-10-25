using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebIdentity.Extensions.ProfileService
{
    public class ProfileDataService : IProfileService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public ProfileDataService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var userClaims = await GetClaimsByUser(user);

            var roles = await GetRolesByUser(user);
            roles.ToList().ForEach(r => userClaims.Add(r));

            userClaims.Add(new Claim(IdentityClaimTypes.Role, "Admin")); // TODO: change it, it's just a test

            var userRoleClaims = await GetRoleClaimsByUser(user);
            userRoleClaims.ToList().ForEach(ur => userClaims.Add(ur));

            context.IssuedClaims.AddRange(userClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }

        private async Task<IList<Claim>> GetClaimsByUser(IdentityUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        private async Task<IList<Claim>> GetRolesByUser(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            foreach (var role in roles)
                claims.Add(new Claim(IdentityClaimTypes.Role, role));

            return claims;
        }

        private async Task<IList<Claim>> GetRoleClaimsByUser(IdentityUser user)
        {
            var claims = new List<Claim>();

            if (_roleManager.SupportsRoleClaims)
            {
                var roleNames = await _userManager.GetRolesAsync(user);

                foreach (var roleName in roleNames)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role is null)
                        continue;

                    claims.AddRange(await _roleManager.GetClaimsAsync(role));
                }
            }

            return claims;
        }
    }

    public static class IdentityClaimTypes
    {
        public const string Role = "role";
    }
}