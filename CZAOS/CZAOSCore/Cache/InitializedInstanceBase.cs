using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CZAOSCore
{
    /// <summary>
    /// The singleton pattern where initialization is required once and only once is fairly common.
    /// This base class provides an easy way to safely re-use the initialization patterns.
    /// </summary>
    public abstract class InitializedInstanceBase
    {
        protected bool isInitialized = false;
        private bool _initializing = false;
        private object _locker = new object();
        private ICache _provider;        

        public bool Initialized
        {
            get { return isInitialized; }
        }

        /// <summary>
        /// The actual initialization method that should be invoked by outside callers
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {            

            if (!isInitialized)
            {

                lock (_locker)
                {
                    // Double check inside lock
                    if (!isInitialized && !_initializing)
                    {


                        _initializing = true;

                        // Wrap with try/catch so we don't get trapped inside lock if something goes wrong
                        try
                        {
                            // Call our derived actual init method
                            isInitialized = OnInitialize();
                        }
                        catch
                        {
                            isInitialized = false;
                        }

                        _initializing = false;
                    }
                }
            }

            return isInitialized;
        }

        public ICache Provider
        {
            get
            {
                if (_provider == null)
                {
                    lock (_locker)
                    {
                        Configure();
                    }
                }

                Initialize();

                return _provider;
            }
            protected set
            {
                _provider = value;
            }
        }

        public void Configure()
        {
            //Integral.Providers.WCFCacheServer, Integral.Providers KT.CMSCache.IISWebCache, KT
            //KT.Config.CustomProvider cp = Providers.GetProvider(Config.CustomProvider.ProviderType.CacheProvider); // new Config.CustomProvider(Config.CustomProvider.ProviderType.CacheProvider);            
            //CMSCacheProvider
           // string providerName = ""; // System.Configuration.ConfigurationManager.AppSettings.Get("CMSCacheProvider");
            string providerName = "CZAOSCore.IISWebCache, CZAOSCore";         
            //_provider = Activator.CreateInstance(Type.GetType("KT.CMSCache.IISWebCache, KT")) as ICache;
            _provider = Activator.CreateInstance(Type.GetType(providerName)) as ICache;
            //cp

        }

        //private KT.Config.CustomProviderManager Providers
        //{
        //    get
        //    {
        //        string strConfig = System.Configuration.ConfigurationManager.AppSettings.Get("ProviderSectionName");
        //        return System.Configuration.ConfigurationManager.GetSection(strConfig) as KT.Config.CustomProviderManager;
        //    }
        //}

        /// <summary>
        /// This is the method that will get called inside the derived class when initialization needs to be performed.
        /// </summary>
        /// <returns></returns>
        protected abstract bool OnInitialize();
    }
}