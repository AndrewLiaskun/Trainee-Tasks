// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.UI.Commands
{
    public class GenericRelayCommand<TObject> : BaseRelayCommand
    {
        private readonly Action<TObject> _executeMethod;
        private Func<TObject, bool> _canExecuteMethod;

        public GenericRelayCommand(Action<TObject> executeMethod)
                : this(executeMethod, (o) => true)
        {
        }

        public GenericRelayCommand(Action<TObject> executeMethod, Func<TObject, bool> canExecuteMethod)
            : base()
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod));

            TypeInfo genericTypeInfo = typeof(TObject).GetTypeInfo();

            // DelegateCommand allows object or Nullable<>.
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericTypeInfo.IsValueType)
            {
                if ((!genericTypeInfo.IsGenericType) || (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
                {
                    throw new InvalidCastException();
                }
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void Execute(TObject parameter)
        {
            _executeMethod(parameter);
        }

        public bool CanExecute(TObject parameter)
        {
            return _canExecuteMethod(parameter);
        }

        public GenericRelayCommand<TObject> ObservesProperty<TType>(Expression<Func<TType>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        public GenericRelayCommand<TObject> ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            Expression<Func<TObject, bool>> expression = Expression.Lambda<Func<TObject, bool>>(canExecuteExpression.Body, Expression.Parameter(typeof(TObject), "o"));
            _canExecuteMethod = expression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }

        protected override void Execute(object parameter)
        {
            Execute((TObject)parameter);
        }

        protected override bool CanExecute(object parameter)
        {
            return CanExecute((TObject)parameter);
        }
    }
}