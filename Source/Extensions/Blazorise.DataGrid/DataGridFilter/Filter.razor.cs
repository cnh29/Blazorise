using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Blazorise.DataGrid
{
    public partial class Filter: BaseComponent
    {
        private string DefaultValue = "eq";

        [Parameter] public Type DataType { get; set; }
        [Parameter] public string Operator { get; set; }
        [Parameter] public EventCallback<string> OperatorChanged { get; set; }

        private bool isNumber
        {
            get
            {
                return NumericTypes.Contains( DataType );
            }
        }

        private bool isDate
        {
            get
            {
                return DateTypes.Contains( DataType );
            }
        }

        /// <summary>
        /// This should be in a separate utility class.
        /// </summary>
        internal static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),
            typeof(double),
            typeof(decimal),
            typeof(long),
            typeof(short),
            typeof(sbyte),
            typeof(byte),
            typeof(ulong),
            typeof(ushort),
            typeof(uint),
            typeof(float)
        };

        // <summary>
        /// This should be in a separate utility class.
        /// </summary>
        internal static readonly HashSet<Type> DateTypes = new HashSet<Type>
        {
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(DateTimeKind)
        };



    }
}
