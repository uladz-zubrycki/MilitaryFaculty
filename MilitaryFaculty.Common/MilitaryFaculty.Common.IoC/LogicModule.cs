using Ninject.Modules;

namespace MilitaryFaculty.Common.IoC
{
    public class LogicModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // Register services here
        }

        #endregion // Overrides of NinjectModule
    }
}