using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blazorise.DataGrid
{
    /// <summary>
    /// Context for editors in datagrid filter section.
    /// </summary>
    public class FilterContext<TItem>
    {
        #region Members

        private event FilterChangedEventHandler SearchValueChanged;

        public delegate void FilterChangedEventHandler( object value );

        private Type dataType
        {
            get
            {
                return Field == null ? typeof( object ) : typeof( TItem ).GetNestedType( Field );
            }
        }

        private bool isNumber
        {
            get
            {
                return NumericTypes.Contains( dataType );
            }
        }

        internal DateTime lastFilteredDate;

        /// <summary>
        /// This should be in a separate utility class.
        /// </summary>
        internal static readonly HashSet<Type> NumericTypes = new()
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

        #endregion

        #region Methods

        public void Subscribe( FilterChangedEventHandler listener )
        {
            SearchValueChanged += listener;
        }

        public void Unsubscribe( FilterChangedEventHandler listener )
        {
            SearchValueChanged -= listener;
        }

        internal void Clear()
        {
            IsVisible = false;
            SearchValue = null;
        }

        

        public void TriggerFilterChange( object value )
        {
            SearchValue = value;
            SearchValueChanged?.Invoke( value );
        }

        internal string ValueAsString => SearchValue switch
        {
            DateTime _ when dataType == typeof( DateTime ) => string.Format( "yyyy-MM-dd", SearchValue ),
            DateTime _ when dataType == typeof( DateTime? ) => string.Format( "yyyy-MM-dd", SearchValue ),
            _ => SearchValue?.ToString()?.ToLower(),
        };

        public Func<TItem, bool> GenerateFilterFunction()
        {
            // short circuit
            if ( SearchValue == null )
                return new Func<TItem, bool>( x => true );

            var parameter = Expression.Parameter( typeof( TItem ), "x" );

            Expression comparison = Expression.Empty();

            if ( dataType == typeof( string ) )
            {
                var field = Expression.Property( parameter, typeof( TItem ).GetProperty( Field ) );
                var valueString = SearchValue?.ToString();

                switch ( Operator )
                {
                    case "contains":
                        comparison = Expression.Call( field, dataType.GetMethod( "Contains", new[] { dataType } ), Expression.Constant( valueString ) );
                        break;
                    case "eq":
                        comparison = Expression.MakeBinary( ExpressionType.Equal, field, Expression.Constant( valueString ) );
                        break;
                    case "startswith":
                        comparison = Expression.Call( field, dataType.GetMethod( "StartsWith", new[] { dataType } ), Expression.Constant( valueString ) );
                        break;
                    case "endswith":
                        comparison = Expression.Call( field, dataType.GetMethod( "EndsWith", new[] { dataType } ), Expression.Constant( valueString ) );
                        break;
                    default:
                        return new Func<TItem, bool>( x => true );
                }
            }
            else if ( isNumber )
            {
                var field = Expression.Convert( Expression.Property( parameter, typeof( TItem ).GetProperty( Field ) ), typeof( double ) );
                var valueNumber = SearchValue == null ? 0 : Convert.ToDouble( SearchValue );

                switch ( Operator )
                {
                    case "eq":
                        comparison = Expression.MakeBinary( ExpressionType.Equal, field, Expression.Constant( valueNumber ) );
                        break;
                    case "ne":
                        comparison = Expression.MakeBinary( ExpressionType.NotEqual, field, Expression.Constant( valueNumber ) );
                        break;
                    case "gt":
                        comparison = Expression.MakeBinary( ExpressionType.GreaterThan, field, Expression.Constant( valueNumber ) );
                        break;
                    case "ge":
                        comparison = Expression.MakeBinary( ExpressionType.GreaterThanOrEqual, field, Expression.Constant( valueNumber ) );
                        break;
                    case "lt":
                        comparison = Expression.MakeBinary( ExpressionType.LessThan, field, Expression.Constant( valueNumber ) );
                        break;
                    case "le":
                        comparison = Expression.MakeBinary( ExpressionType.LessThanOrEqual, field, Expression.Constant( valueNumber ) );
                        break;
                    default:
                        return new Func<TItem, bool>( x => true );
                }
            }
            else if ( dataType == typeof( bool ) )
            {
                var field = Expression.Property( parameter, typeof( TItem ).GetProperty( Field ) );
                var valueAsBoolean = Convert.ToBoolean( SearchValue );
                comparison = Expression.MakeBinary( ExpressionType.Equal, field, Expression.Constant( valueAsBoolean ) );
            }
            else if ( dataType == typeof( DateTime ) )
            {
                var field = Expression.Property( parameter, typeof( TItem ).GetProperty( Field ) );
                var valueAsDate = Convert.ToDateTime( SearchValue );
                switch ( Operator )
                {
                    case "eq":
                        comparison = Expression.MakeBinary( ExpressionType.Equal, field, Expression.Constant( valueAsDate ) );
                        break;
                    case "ne":
                        comparison = Expression.MakeBinary( ExpressionType.NotEqual, field, Expression.Constant( valueAsDate ) );
                        break;
                    case "gt":
                        comparison = Expression.MakeBinary( ExpressionType.GreaterThan, field, Expression.Constant( valueAsDate ) );
                        break;
                    case "ge":
                        comparison = Expression.MakeBinary( ExpressionType.GreaterThanOrEqual, field, Expression.Constant( valueAsDate ) );
                        break;
                    case "lt":
                        comparison = Expression.MakeBinary( ExpressionType.LessThan, field, Expression.Constant( valueAsDate ) );
                        break;
                    case "le":
                        comparison = Expression.MakeBinary( ExpressionType.LessThanOrEqual, field, Expression.Constant( valueAsDate ) );
                        break;
                    default:
                        return new Func<TItem, bool>( x => true );
                }
            }
            else
            {
                return new Func<TItem, bool>( x => true );
            }

            var ex = Expression.Lambda<Func<TItem, bool>>( comparison, parameter );

            return ex.Compile();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filter value.
        /// </summary>
        public object SearchValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Operator { get; set; } = "eq";

        /// <summary>
        /// 
        /// </summary>
        internal bool IsVisible { get; set; }

        #endregion
    }
}