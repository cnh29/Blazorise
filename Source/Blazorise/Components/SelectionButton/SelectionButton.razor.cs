using System;
using Microsoft.AspNetCore.Components;

namespace Blazorise.Components.SelectionButton
{
    public partial class SelectionButton: BaseComponent
    {
        [Parameter] public int SelectedItems { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }
    }
}
