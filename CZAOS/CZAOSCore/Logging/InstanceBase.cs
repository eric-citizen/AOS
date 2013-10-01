using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZAOSCore.Logging
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


        /// <summary>
        /// This is the method that will get called inside the derived class when initialization needs to be performed.
        /// </summary>
        /// <returns></returns>
        protected abstract bool OnInitialize();
    }

}
