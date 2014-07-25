using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MilitaryFaculty.Common;

namespace MilitaryFaculty.Application
{
    public static class GlobalCommands
    {
        private static readonly IDictionary<Type, RoutedCommand> AddCommands =
            new Dictionary<Type, RoutedCommand>();

        private static readonly IDictionary<Type, RoutedCommand> SaveCommands =
            new Dictionary<Type, RoutedCommand>();

        private static readonly IDictionary<Type, RoutedCommand> RemoveCommands =
            new Dictionary<Type, RoutedCommand>();

        private static readonly IDictionary<Type, RoutedCommand> BrowseAddCommands =
           new Dictionary<Type, RoutedCommand>();

        private static readonly IDictionary<Type, RoutedCommand> BrowseDetailsCommands =
          new Dictionary<Type, RoutedCommand>();

        public static readonly RoutedCommand GenerateReport = new RoutedCommand();
        public static readonly RoutedCommand BrowseForward = new RoutedCommand();
        public static readonly RoutedCommand BrowseBack = new RoutedCommand();
        public static readonly RoutedCommand DismissProfessor = new RoutedCommand();

        public static RoutedCommand Add<T>()
        {
            return GetCommand<T>(AddCommands);
        }

        public static RoutedCommand Save<T>()
        {
            return GetCommand<T>(SaveCommands);
        }

        public static RoutedCommand Remove<T>()
        {
            return GetCommand<T>(RemoveCommands);
        }

        public static RoutedCommand BrowseAdd<T>()
        {
            return GetCommand<T>(BrowseAddCommands);
        }

        public static RoutedCommand BrowseDetails<T>()
        {
            return GetCommand<T>(BrowseDetailsCommands);
        }

        public static void CreateCommandsForEntity(Type entityType)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("entityType");
            }

            var perTypeCommandSources = new[]
                                        {
                                            AddCommands,
                                            SaveCommands,
                                            RemoveCommands,
                                            BrowseAddCommands,
                                            BrowseDetailsCommands
                                        };

            perTypeCommandSources.ForEach(source => source[entityType] = new RoutedCommand());
        }

        public static IEnumerable<RoutedCommand> GetCommands()
        {
            Func<IDictionary<Type, RoutedCommand>, IEnumerable<RoutedCommand>>
                getCommands = dict => dict.Values;

            var allTypesCommands = new[]
                                  {
                                      AddCommands,
                                      SaveCommands,
                                      RemoveCommands,
                                      BrowseAddCommands,
                                      BrowseDetailsCommands
                                  };

            var allCommands = allTypesCommands.Select(getCommands)
                                              .SelectMany(x => x)
                                              .Union(new[]
                                                     {
                                                         GenerateReport,
                                                         DismissProfessor,
                                                         BrowseBack,
                                                         BrowseForward
                                                     })
                                              .ToList();

            return allCommands;
        } 

        private static RoutedCommand GetCommand<T>(IDictionary<Type, RoutedCommand> commandSource)
        {
            var type = typeof(T);

            RoutedCommand command;
            if (!commandSource.TryGetValue(type, out command))
            {
                throw new Exception("Register commands for {0}.".f(type.FullName));
            }

            return command;
        }
    }
}