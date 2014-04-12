using System;
using System.Windows;
using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Core.Commands
{
    /// <summary>
    ///     A CommandBinding subclass that will attach its
    ///     CanExecute and Executed events to the event handling
    ///     methods on the object referenced by its CommandContainer property.
    ///     Set the attached CommandContainer property on the element
    ///     whose CommandBindings collection contain CommandContainerBindings.
    ///     If you dynamically create an instance of this class and add it
    ///     to the CommandBindings of an element, you must explicitly set
    ///     its CommandContainer property.
    /// </summary>
    public class CommandContainerBinding : CommandBinding
    {
        #region CommandContainer [instance property]

        private ICommandContainer _commandContainer;

        public ICommandContainer CommandContainer
        {
            get { return _commandContainer; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (_commandContainer != null)
                {
                    throw new InvalidOperationException("Cannot set CommandContainer more than once.");
                }

                _commandContainer = value;

                CanExecute += (s, e) =>
                              {
                                  var command = e.Command as RoutedCommand;
                                  if (command == null)
                                  {
                                      throw new InvalidOperationException(
                                          "Only routed commands can be used inside CommandContainerBinding.");
                                  }

                                  bool handled;
                                  e.CanExecute = _commandContainer.CanExecuteCommand(command, e.Parameter, out handled);
                                  e.Handled = handled;
                              };

                Executed += (s, e) =>
                            {
                                var command = e.Command as RoutedCommand;
                                if (command == null)
                                {
                                    throw new InvalidOperationException(
                                        "Only routed commands can be used inside CommandContainerBinding.");
                                }

                                bool handled;
                                _commandContainer.ExecuteCommand(command, e.Parameter, out handled);
                                e.Handled = handled;
                            };
            }
        }

        #endregion // CommandContainer [instance property]

        #region CommandContainer [attached property]

        public static readonly DependencyProperty CommandContainerProperty =
            DependencyProperty.RegisterAttached(
                "CommandContainer",
                typeof (ICommandContainer),
                typeof (CommandContainerBinding),
                new UIPropertyMetadata(null, OnCommandContainerChanged));

        public static ICommandContainer GetCommandContainer(DependencyObject obj)
        {
            return (ICommandContainer) obj.GetValue(CommandContainerProperty);
        }

        public static void SetCommandContainer(DependencyObject obj, ICommandContainer value)
        {
            obj.SetValue(CommandContainerProperty, value);
        }

        private static void OnCommandContainerChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var commandContainer = e.NewValue as ICommandContainer;

            if (!ConfigureDelayedProcessing(depObj, commandContainer))
            {
                ProcessCommandContainerChanged(depObj, commandContainer);
            }
        }

        // This method is necessary when the CommandConatiner attached property is set on an element 
        // in a template, or any other situation in which the element's CommandBindings have not 
        // yet had a chance to be created and added to its CommandBindings collection.
        private static bool ConfigureDelayedProcessing(DependencyObject depObj, ICommandContainer commandContainer)
        {
            var isDelayed = false;

            var elem = new CommonElement(depObj);
            if (elem.IsValid && !elem.IsLoaded)
            {
                RoutedEventHandler handler = null;
                handler = delegate
                          {
                              elem.Loaded -= handler;
                              ProcessCommandContainerChanged(depObj, commandContainer);
                          };

                elem.Loaded += handler;
                isDelayed = true;
            }

            return isDelayed;
        }

        private static void ProcessCommandContainerChanged(DependencyObject depObj, ICommandContainer commandContainer)
        {
            var cmdBindings = GetCommandBindings(depObj);

            if (cmdBindings == null)
            {
                throw new ArgumentException(
                    "The CommandContainerBinding.CommandContainer attached property was set on an element that does not support CommandBindings.");
            }

            foreach (CommandBinding cmdBinding in cmdBindings)
            {
                var csb = cmdBinding as CommandContainerBinding;
                if (csb != null && csb.CommandContainer == null)
                {
                    csb.CommandContainer = commandContainer;
                }
            }
        }

        private static CommandBindingCollection GetCommandBindings(DependencyObject depObj)
        {
            var elem = new CommonElement(depObj);

            return elem.IsValid ? elem.CommandBindings : null;
        }

        #endregion // CommandSink [attached property]

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