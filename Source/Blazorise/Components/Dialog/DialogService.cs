using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Components;
namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogService : IDialogService
    {
        internal event Action<DialogReference> OnDialogInstanceAdded;
        internal event Action<DialogReference, DialogResult> OnDialogCloseRequested;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDialogReference Show<T>( Dictionary<string, object> parameters )
        {
            var dialogInstanceId = Guid.NewGuid();
            var dialogReference = new DialogReference( dialogInstanceId, this );

            var dialogContent = new RenderFragment( builder =>
            {
                var i = 0;
                builder.OpenComponent( i++, typeof( T ) );
                foreach ( var parameter in parameters )
                {
                    builder.AddAttribute( i++, parameter.Key, parameter.Value );
                }
                //builder.AddComponentReferenceCapture(1, inst => { dialogReference.InjectDialog(inst); });
                builder.CloseComponent();
            } );

            var dialogInstance = new RenderFragment( builder =>
            {
                builder.OpenComponent<DialogInstance>( 0 );
                builder.SetKey( dialogInstanceId );
                builder.AddAttribute( 1, "Content", dialogContent );
                builder.AddAttribute( 2, "Id", dialogInstanceId );
                builder.CloseComponent();
            } );
            dialogReference.InjectRenderFragment( dialogInstance );
            OnDialogInstanceAdded?.Invoke( dialogReference );
            return dialogReference;
        }

        internal void Close( DialogReference dialogReference )
        {
            Close( dialogReference, DialogResult.Ok<object>( null ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogReference"></param>
        /// <param name="result"></param>
        public void Close( DialogReference dialogReference, DialogResult result )
        {
            OnDialogCloseRequested?.Invoke( dialogReference, result );
        }
    }
}
