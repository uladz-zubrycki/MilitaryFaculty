using System;
using System.Windows;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Commands
{
    /// <summary>
    ///     A CommandBinding subclass that will attach its
    ///     CanExecute and Executed events to the event handling
    ///     methods on the object referenced by its RoutedCommands property.
    ///     Set the attached RoutedCommands property on the element
    ///     whose CommandBindings collection contain CommandContainerBindings.
    ///     If you dynamically create an instance of this class and add it
    ///     to the CommandBindings of an element, you must explicitly set
    ///     its RoutedCommands property.
    /// </summary>
    public class RoutedBinding : CommandBinding
    {
        private RoutedCommands _routedCommands;

        public RoutedCommands RoutedCommands
        {
            get { return _routedCommands; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (_routedCommands != null)
                {
                    throw new InvalidOperationException("Cannot set RoutedCommands more than once.");
                }

                _routedCommands = value;

                CanExecute +=
                    (s, e) =>
                    {
                        var command = e.Command as RoutedCommand;
                        if (command == null)
                        {
                            throw new InvalidOperationException(
                                "Only routed _routedCommands can be used inside RoutedBinding.");
                        }

                        bool handled;
                        e.CanExecute = _routedCommands.CanExecuteCommand(command, e.Parameter, out handled);
                        e.Handled = handled;
                    };

                Executed +=
                    (s, e) =>
                    {
                        var command = e.Command as RoutedCommand;
                        if (command == null)
                        {
                            throw new InvalidOperationException(
                                "Only routed commands can be used inside RoutedBinding.");
                        }

                        bool handled;
                        _routedCommands.ExecuteCommand(command, e.Parameter, out handled);
                        e.Handled = handled;
                    };
            }
        }

        public static readonly DependencyProperty RoutedCommandsProperty =
            DependencyProperty.RegisterAttached(
                name: "RoutedCommands",
                propertyType: typeof(RoutedCommands),
                ownerType: typeof(RoutedBinding),
                defaultMetadata: new UIPropertyMetadata(
                    defaultValue: null,
                    propertyChangedCallback: OnRoutedCommandsChanged));

        public static RoutedCommands GetRoutedCommands(DependencyObject obj)
        {
            return (RoutedCommands)obj.GetValue(RoutedCommandsProperty);
        }

        public static void SetRoutedCommands(DependencyObject obj, RoutedCommands value)
        {
            obj.SetValue(RoutedCommandsProperty, value);
        }

        private static void OnRoutedCommandsChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var routedCommands = e.NewValue as RoutedCommands;

            if (!ConfigureDelayedProcessing(depObj, routedCommands))
            {
                ProcessRoutedCommandsChanged(depObj, routedCommands);
            }
        }

        private static bool ConfigureDelayedProcessing(DependencyObject depObj, RoutedCommands routedCommands)
        {
            var isDelayed = false;

            var elem = new CommonElement(depObj);
            if (elem.IsValid && !elem.IsLoaded)
            {
                RoutedEventHandler handler = null;
                handler =
                    delegate
                    {
                        elem.Loaded -= handler;
                        ProcessRoutedCommandsChanged(depObj, routedCommands);
                    };

                elem.Loaded += handler;
                isDelayed = true;
            }

            return isDelayed;
        }

        private static void ProcessRoutedCommandsChanged(DependencyObject depObj, RoutedCommands commands)
        {
            var cmdBindings = GetCommandBindings(depObj);

            if (cmdBindings == null)
            {
                throw new ArgumentException(
                    "The RoutedBinding.RoutedCommands attached property was set " +
                    "on an element that does not support CommandBindings.");
            }

            foreach (CommandBinding cmdBinding in cmdBindings)
            {
                var csb = cmdBinding as RoutedBinding;
                if (csb != null && csb.RoutedCommands == null)
                {
                    csb.RoutedCommands = commands;
                }
            }
        }

        private static CommandBindingCollection GetCommandBindings(DependencyObject depObj)
        {
            var elem = new CommonElement(depObj);

            return elem.IsValid ? elem.CommandBindings : null;
        }

        #region CommonElement [nested class]

        /// <summary>
        ///     This class makes it easier to write code that works
        ///     with the common members of both the FrameworkElement
        ///     and FrameworkContentElement classes.
        /// </summary>
        private class CommonElement
        {
            public readonly bool IsValid;
            private readonly FrameworkContentElement fce;
            private readonly FrameworkElement fe;

            public CommandBindingCollection CommandBindings
            {
                get
                {
                    Verify();

                    if (fe != null)
                    {
                        return fe.CommandBindings;
                    }

                    return fce.CommandBindings;
                }
            }

            public bool IsLoaded
            {
                get
                {
                    Verify();

                    if (fe != null)
                    {
                        return fe.IsLoaded;
                    }

                    return fce.IsLoaded;
                }
            }

            public CommonElement(DependencyObject depObj)
            {
                fe = depObj as FrameworkElement;
                fce = depObj as FrameworkContentElement;

                IsValid = fe != null || fce != null;
            }

            public event RoutedEventHandler Loaded
            {
                add
                {
                    Verify();

                    if (fe != null)
                    {
                        fe.Loaded += value;
                    }
                    else
                    {
                        fce.Loaded += value;
                    }
                }
                remove
                {
                    Verify();

                    if (fe != null)
                        fe.Loaded -= value;
                    else
                    {
                        fce.Loaded -= value;
                    }
                }
            }

            private void Verify()
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException("Cannot use an invalid CommmonElement.");
                }
            }
        }

        #endregion // CommonElement [nested class]
    }
}