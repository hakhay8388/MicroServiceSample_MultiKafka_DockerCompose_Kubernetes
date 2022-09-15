using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace MicroServiceSample.nCustomDI
{
    public class cUnityServiceProvider : IServiceProvider
    {
        private IUnityContainer m_Montainer;

        public IUnityContainer UnityContainer => m_Montainer;

        public cUnityServiceProvider(IUnityContainer _IUnityContainer)
        {
            m_Montainer = _IUnityContainer;
        }

        #region Implementation of IServiceProvider

        /// <summary>Gets the service object of the specified type.</summary>
        /// <returns>A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type <paramref name="serviceType" />.</returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param>
        public object GetService(Type serviceType)
        {
            //Delegates the GetService to the Containers Resolve method
            return m_Montainer.Resolve(serviceType);
        }

        #endregion
    }
}
