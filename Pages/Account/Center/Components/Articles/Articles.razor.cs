using System.Collections.Generic;
using BlazorFlowly.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorFlowly.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}