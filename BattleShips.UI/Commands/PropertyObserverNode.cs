// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.UI.Commands
{
    internal class PropertyObserverNode
    {
        private readonly Action _action;
        private INotifyPropertyChanged _inpcObject;

        public PropertyObserverNode(PropertyInfo propertyInfo, Action action)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            _action = () =>
            {
                action?.Invoke();
                if (Next == null) return;
                Next.UnsubscribeListener();
                GenerateNextNode();
            };
        }

        public PropertyInfo PropertyInfo { get; }

        public PropertyObserverNode Next { get; set; }

        public void SubscribeListenerFor(INotifyPropertyChanged inpcObject)
        {
            _inpcObject = inpcObject;
            _inpcObject.PropertyChanged += OnPropertyChanged;

            if (Next != null) GenerateNextNode();
        }

        private void GenerateNextNode()
        {
            var nextProperty = PropertyInfo.GetValue(_inpcObject);
            if (nextProperty == null) return;
            if (!(nextProperty is INotifyPropertyChanged nextInpcObject))
                throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that " +
                                                    $"owns '{Next.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");

            Next.SubscribeListenerFor(nextInpcObject);
        }

        private void UnsubscribeListener()
        {
            if (_inpcObject != null)
                _inpcObject.PropertyChanged -= OnPropertyChanged;

            Next?.UnsubscribeListener();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName == PropertyInfo.Name || string.IsNullOrEmpty(e?.PropertyName))
            {
                _action?.Invoke();
            }
        }
    }
}