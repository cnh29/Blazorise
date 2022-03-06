using System;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommandBarItem: BaseComponent
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string PrefixIcon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Parameter] public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public EventCallback<string> OnClickCallback { get; set; }

        private async Task OnClick( string id )
        {
            await OnClickCallback.InvokeAsync( id );
        }
    }
}
