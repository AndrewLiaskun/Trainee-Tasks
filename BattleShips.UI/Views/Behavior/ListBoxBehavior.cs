// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using BattleShips.UI.Views.Controls;

namespace BattleShips.UI.Views.Behavior
{
    public static class ListBoxBehavior
    {
        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.RegisterAttached(
            "AutoScroll",
            typeof(bool),
            typeof(HistoryListCollectionControl),
            new PropertyMetadata(false));

        public static readonly DependencyProperty AutoScrollHandlerProperty =
            DependencyProperty.RegisterAttached(
                "AutoScrollHandler",
                typeof(AutoScrollHandler),
                typeof(HistoryListCollectionControl));

        public static bool GetAutoScroll(HistoryListCollectionControl instance)
        {
            return (bool)instance.GetValue(AutoScrollProperty);
        }

        public static void SetAutoScroll(HistoryListCollectionControl instance, bool value)
        {
            AutoScrollHandler OldHandler = (AutoScrollHandler)instance.GetValue(AutoScrollHandlerProperty);
            if (OldHandler != null)
            {
                OldHandler.Dispose();
                instance.SetValue(AutoScrollHandlerProperty, null);
            }

            instance.SetValue(AutoScrollProperty, value);
            if (value)
            {
                instance.SetValue(AutoScrollHandlerProperty, new AutoScrollHandler(instance));
            }
        }
    }
}