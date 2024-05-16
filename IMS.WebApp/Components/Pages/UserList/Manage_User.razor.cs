using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IMS.WebApp.Components.Pages.UserList
{
    public partial class Manage_User
    {
        [Parameter]
        public string? UserId { get; set; }

        private IdentityUser user;
        private Claim departmentClaim;
        private bool userHadDepartment = true;
        private ManageUserViewModel viewModel;

        public class ManageUserViewModel
        {
            public string? Email { get; set; }

            [Required]
            public string Department { get; set; }
        }

        protected override async Task OnParametersSetAsync()
        {
            base.OnParametersSet();

            user = UserManager.Users.First(x => x.Id == this.UserId);
            var claims = await UserManager.GetClaimsAsync(user);
            departmentClaim = claims.FirstOrDefault(x => x.Type == "Department");
            if (departmentClaim == null)
            {
                userHadDepartment = false;
                departmentClaim = new Claim("Department", string.Empty);
            }

            viewModel = new ManageUserViewModel
            {
                Email = user.Email,
                Department = departmentClaim.Value
            };

        }

        private async Task SaveUser()
        {
            if (userHadDepartment)
                await UserManager.ReplaceClaimAsync(user, departmentClaim, new Claim("Department", viewModel.Department));
            else
                await UserManager.AddClaimAsync(user, new Claim("Department", viewModel.Department));

            NavigationManager.NavigateTo("/manageusers");
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/manageusers");
        }

    }
}
