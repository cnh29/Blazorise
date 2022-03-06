using System;
using System.Threading.Tasks;

namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDialogReference
    {
        /// <summary>
        /// 
        /// </summary>
        Task<DialogResult> Result { get; }
        /// <summary>
        /// 
        /// </summary>
        void Close();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        void Close( DialogResult result );
    }
}
