using System;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazorise.Components.CommandBar
{
    public partial class CommandBarItem: BaseComponent
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string PrefixIcon { get; set; }
        [Parameter] public string Id { get; set; }

        [Parameter] public bool IsEnabled { get; set; } = true;

        [Parameter] public EventCallback<string> OnClickCallback { get; set; }

        private async Task OnClick( string id )
        {
            await OnClickCallback.InvokeAsync( id );
        }
    }
}
