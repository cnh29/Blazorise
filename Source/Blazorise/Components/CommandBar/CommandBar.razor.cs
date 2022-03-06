using System;
using Microsoft.AspNetCore.Components;

namespace Blazorise
{
    public partial class CommandBar: BaseComponent
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
