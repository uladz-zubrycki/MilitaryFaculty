using System;

namespace MilitaryFaculty.Presentation.Extensions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        ///     Get service of specified type.
        /// </summary>
        /// <typeparam name="T">Type of requested service.</typeparam>
        /// <param name="serviceProvider">Provides access to services.</param>
        /// <returns>Service of requested type, if exists; otherwise null.</returns>
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            return (T) serviceProvider.GetService(typeof (T));
        }
    }
}