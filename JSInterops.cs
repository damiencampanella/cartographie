using Microsoft.JSInterop;
using System.Threading.Tasks;


namespace BlazorFlowly {
    public static class JSInterops
    {
        private static readonly string _BaseObjectContainer = "window.jsInterops";

        public static ValueTask InitTooltips(IJSRuntime jsRuntime) =>
            jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.init_tooltips");

        public static ValueTask InitPopovers(IJSRuntime jsRuntime) =>
            jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.init_popovers");

        public static ValueTask InitDatePickers(IJSRuntime jsRuntime, string language) =>
            jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.init_datepickers", language);

        public static ValueTask SetActiveTab(IJSRuntime jsRuntime, string tabsId, string tabHref) =>
            jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.set_active_tab", tabsId, tabHref);

        public static ValueTask SetServerTime (IJSRuntime jsRuntime, string hour, string minute) =>
            jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.set_server_time", hour, minute);

        public static ValueTask WindowResize(IJSRuntime jsRuntime) =>
            jsRuntime.InvokeVoidAsync("screenresize");
    }
}
