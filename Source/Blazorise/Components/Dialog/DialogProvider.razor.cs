using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DialogProvider : IDisposable
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private readonly ICollection<DialogReference> _dialogs = new Collection<DialogReference>();

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            ( (DialogService)DialogService ).OnDialogInstanceAdded += AddInstance;
            ( (DialogService)DialogService ).OnDialogCloseRequested += DismissInstance;
            NavigationManager.LocationChanged += LocationChanged;
        }

        private void AddInstance( DialogReference dialog )
        {
            _dialogs.Add( dialog );
            StateHasChanged();
        }

        private void DismissAll()
        {
            _dialogs.ToList().ForEach( r => r.Dismiss( DialogResult.Cancel() ) );
            _dialogs.Clear();
            StateHasChanged();
        }

        private void DismissInstance( DialogReference dialog, DialogResult result )
        {
            dialog.Dismiss( result );
            _dialogs.Remove( dialog );
            StateHasChanged();
        }

        private DialogReference GetDialogReference( Guid id )
        {
            return _dialogs.SingleOrDefault( x => x.Id == id );
        }

        internal void DismissInstance( Guid id, DialogResult result )
        {
            var reference = GetDialogReference( id );
            if ( reference != null )
                DismissInstance( reference, result );
        }

        private void LocationChanged( object sender, LocationChangedEventArgs args )
        {
            DismissAll();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if ( NavigationManager != null )
            {
                NavigationManager.LocationChanged -= LocationChanged;
            }
        }

    }
}
