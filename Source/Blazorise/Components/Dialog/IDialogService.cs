using System;
using System.Collections.Generic;
using System.Threading;

namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDialogReference Show<T>( Dictionary<string, object> parameters );
    }
}
