﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Moq;

namespace Blazorise.Tests.Mocks
{
    internal class MockDateEdit<T> : DateEdit<T>
    {
        public MockDateEdit( Validation validation = null, Expression<Func<T>> dateExpression = null )
        {
            var mockIdGenerator = new Mock<IIdGenerator>();

            mockIdGenerator
                .Setup( r => r.Generate )
                .Returns( Guid.NewGuid().ToString() );

            base.IdGenerator = mockIdGenerator.Object;

            base.ParentValidation = validation;
            base.DateExpression = dateExpression;

            this.OnInitialized();
        }

        public string TextValue
        {
            get { return base.CurrentValueAsString; }
        }

        public string ClickedId { get; private set; }

        public async Task<ParseValue<T>> ParseValueAsync( string value )
        {
            return await base.ParseValueFromStringAsync( value );
        }

        public void OnChange( ChangeEventArgs e )
        {
            base.OnChangeHandler( e );
        }

        private bool OnActivateDatePicker( ElementReference elementRef, string elementId, object options )
        {
            this.ClickedId = elementId;
            return true;
        }
    }
}
