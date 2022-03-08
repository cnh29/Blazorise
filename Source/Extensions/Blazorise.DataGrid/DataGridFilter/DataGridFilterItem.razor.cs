using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorise.DataGrid
{
    public partial class DataGridFilterItem<TItem>: BaseComponent
    {

        #region Properties

        [Parameter] public DataGridColumn<TItem> Column { get; set; }
        [Parameter] public EventCallback DidClickRemove { get; set; }
        [Parameter] public EventCallback<KeyboardEventArgs> OnInputEnter { get; set; }
        [Parameter] public EventCallback<FocusEventArgs> OnFocusOut { get; set; }
        [Parameter] public EventCallback OnChange { get; set; }

        #endregion
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task OnValueChanged( object item )
        {
            Column.Filter.SearchValue = item;
            await OnChange.InvokeAsync();
        }

        private async Task OperatorChangedHandler( string value )
        {
            Column.Filter.Operator = value;

            if ( string.IsNullOrEmpty( Column.Filter.ValueAsString ) )
            {
                return;
            }
            await OnChange.InvokeAsync();
        }

        
    }
}
