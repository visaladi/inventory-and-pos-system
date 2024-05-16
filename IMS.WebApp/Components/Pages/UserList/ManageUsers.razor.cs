using Microsoft.AspNetCore.Identity;

namespace IMS.WebApp.Components.Pages.UserList
{
    public partial class ManageUsers
    {
        private List<IdentityUser>? users;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            users = UserManager.Users.ToList();
        }

        private void ManageUser(IdentityUser user)
        {
            NavigationManager.NavigateTo($"/manageuser/{user.Id}");
        }

    }
}
