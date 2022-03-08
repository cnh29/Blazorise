using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorise.DataGrid
{
    public partial class DataGridFilter<TItem>: BaseComponent
    {
        [Parameter] public PropertyInfo[] FilterProperties { get; set; }

        [Parameter] public IEnumerable<DataGridColumn<TItem>> FilterableColumns { get; set; }

        [Parameter] public EventCallback FilterResultsCommand { get; set; }

        private void OnFilterValueChanged(string elementId)
        {
            var column = FilterableColumns
                .FirstOrDefault( x => x.ElementId == elementId );
            column.Filter.IsVisible = true;
            column.Filter.lastFilteredDate = DateTime.Now;

            ShowFilterInput = false;
        }

        private async Task DidRemoveFilterItem(DataGridColumn<TItem> column )
        {
            column.Filter.Clear();
            await OnSearchValueChanged();

        }

        public async Task OnSearchValueChanged()
        {
            await FilterResultsCommand.InvokeAsync( null );
        }

        private bool ShowFilterInput = false;

        private void OnAddFilterClicked()
        {
            ShowFilterInput = true;
        }

        private void ClickOutsideFocus()
        {
            ShowFilterInput = false;
        }

        private async Task OnFilterItemEnter(KeyboardEventArgs arg )
        {
            if ( arg.Key != "Enter" )
            {
                return;

            }
            else
            {
                await FilterResultsCommand.InvokeAsync( null );
            }

        }
    }
}
