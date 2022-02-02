using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorFlowly.Models;
using BlazorFlowly.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorFlowly.Pages.Account.Center
{
    public partial class Index
    {
        private CurrentUser _currentUser = new()
        {
            Geographic = new GeographicType {City = new TagType(), Province = new TagType()}
        };

        private IList<ListItemDataType> _fakeList = new List<ListItemDataType>();
        private bool _inputVisible;
        public string InputValue { get; set; }

        [Inject] public IUserService UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await UserService.GetCurrentUserAsync();
        }

        protected void ShowInput()
        {
            _inputVisible = true;
        }

        protected void HandleInputConfirm()
        {
            _inputVisible = false;
        }
    }
}