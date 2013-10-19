using System.Configuration;
using MilitaryFaculty.DataAccess;
using MilitaryFaculty.DataAccess.Contract;
using Ninject.Modules;

namespace MilitaryFaculty.Common.IoC
{
    public class DataAccessModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
           

            Bind(typeof (IRepository<>)).To(typeof (BaseRepository<>));
        }

        #endregion // Overrides of NinjectModule
    }
}