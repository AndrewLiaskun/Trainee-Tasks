// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

using BattleShips.UI.Views.Controls;

namespace BattleShips.UI.Views.Behavior
{
    public class AutoScrollHandler : DependencyObject, IDisposable
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(AutoScrollHandler),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.None,
                ItemsSourcePropertyChanged));

        private HistoryListCollectionControl target;

        public AutoScrollHandler(HistoryListCollectionControl target)
        {
            this.target = target;
            var binding = new Binding("ItemsSource") { Source = this.target };
            BindingOperations.SetBinding(this, ItemsSourceProperty, binding);
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public void Dispose()
        {
            BindingOperations.ClearBinding(this, ItemsSourceProperty);
        }

        private static void ItemsSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((AutoScrollHandler)o).ItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }

        private void ItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            var collection = oldValue as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged -= this.CollectionChangedEventHandler;
            }

            collection = newValue as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged += this.CollectionChangedEventHandler;
            }
        }

        private void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add || e.NewItems == null || e.NewItems.Count < 1)
            {
                return;
            }

            this.target.ScrollIntoView(e.NewItems[e.NewItems.Count - 1]);
        }
    }
}