using System;
using Microsoft.AspNetCore.Components;

namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SelectionButton: BaseComponent
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter] public int SelectedItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter] public EventCallback OnClick { get; set; }
    }
}
