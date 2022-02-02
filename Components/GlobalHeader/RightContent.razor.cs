using BlazorFlowly.Models;
using BlazorFlowly.Services;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using AntDesign;

namespace BlazorFlowly.Components
{
    public partial class RightContent
    {
        private CurrentUser _currentUser = new();

        public AvatarMenuItem[] AvatarMenuItems { get; set; } = new AvatarMenuItem[]
        {
            new() { Key = "center", IconType = "user", Option = "Account Center"},
            new() { Key = "setting", IconType = "setting", Option = "Personal settings"},
            new() { IsDivider = true },
            new() { Key = "logout", IconType = "logout", Option = "Log Out"}
        };

        [Inject] protected NavigationManager NavigationManager { get; set; }

        [Inject] protected IUserService UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();
            _currentUser = await UserService.GetCurrentUserAsync();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }

        public void HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                case "center":
                    NavigationManager.NavigateTo("/account/center");
                    break;
                case "setting":
                    NavigationManager.NavigateTo("/account/settings");
                    break;
                case "logout":
                    NavigationManager.NavigateTo("/user/login");
                    break;
            }
        }
    }
}