// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleShips.UI.Commands
{
    public abstract class BaseRelayCommand : ICommand
    {
        private readonly HashSet<string> _observedPropertiesExpressions = new HashSet<string>();
        private bool _isActive;

        public event EventHandler CanExecuteChanged;

        public virtual event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            if (_observedPropertiesExpressions.Contains(propertyExpression.ToString()))
            {
                throw new ArgumentException($"{propertyExpression.ToString()} is already being observed.",
                    nameof(propertyExpression));
            }
            else
            {
                _observedPropertiesExpressions.Add(propertyExpression.ToString());
                PropertyObserver.Observes(propertyExpression, RaiseCanExecuteChanged);
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        protected abstract bool CanExecute(object parameter);

        protected abstract void Execute(object parameter);

        protected virtual void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}