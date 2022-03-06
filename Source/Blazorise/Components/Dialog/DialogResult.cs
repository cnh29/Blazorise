using System;
namespace Blazorise
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Cancelled { get; }

        /// <summary>
        /// 
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancelled"></param>
        protected internal DialogResult( object data, bool cancelled )
        {
            Cancelled = cancelled;
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static DialogResult Ok<T>( T result ) => new DialogResult( result, false );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DialogResult Cancel() => new DialogResult( default, true );
    }
}
