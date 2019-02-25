using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using RiceShop.Clb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    //get user from db (in my case this is by email)
                    var user = await _userManager.FindByNameAsync(context.Subject.Identity.Name);
                    if (user != null)
                    {
                        var claims = _userManager.GetClaimsAsync(user);
                        if (claims.Result.Count() > 0)
                            context.IssuedClaims.AddRange(claims.Result);
                    }
                }
                else
                {
                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    //where and subject was set to my user id.
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value))
                    {
                        //get user from db (find user by user id)
                        var user = await _userManager.FindByIdAsync(userId.Value);

                        // issue the claims for the user
                        if (user != null)
                        {

                            var roles = await _userManager.GetRolesAsync(user);
                            if (roles.Count > 0 && roles != null)
                            {
                                foreach (var role in roles) context.IssuedClaims.Add(new Claim("Role", role));
                               
                            }
                            //userinfo add
                            context.IssuedClaims.Add(new Claim("Name", user.UserName));
                            context.IssuedClaims.Add(new Claim("Email", user.Email));

                            var claims = _userManager.GetClaimsAsync(user);
                            if (claims != null)
                                //set issued claims to return
                                context.IssuedClaims.AddRange(claims.Result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {

                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    var user = await _userManager.FindByIdAsync(userId.Value);

                    if (user != null)
                    {
                        if (user.UserName != null)
                        {
                            // context.IsActive = user.IsActive;
                            context.IsActive = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
