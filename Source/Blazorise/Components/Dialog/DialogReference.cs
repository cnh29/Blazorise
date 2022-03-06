using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogReference : IDialogReference
    {
        private readonly DialogService _dialogService;
        internal Guid Id;
        private readonly TaskCompletionSource<DialogResult> _resultCompletion = new TaskCompletionSource<DialogResult>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogInstanceId"></param>
        /// <param name="dialogService"></param>
        public DialogReference( Guid dialogInstanceId, DialogService dialogService )
        {
            Id = dialogInstanceId;
            _dialogService = dialogService;
        }

        internal RenderFragment RenderFragment { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Task<DialogResult> Result => _resultCompletion.Task;

        internal void InjectRenderFragment( RenderFragment rf )
        {
            RenderFragment = rf;
        }

        internal void Dismiss( DialogResult result )
        {
            _resultCompletion.TrySetResult( result );
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            _dialogService.Close( this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void Close( DialogResult result )
        {
            _dialogService.Close( this, result );
        }
    }
}
