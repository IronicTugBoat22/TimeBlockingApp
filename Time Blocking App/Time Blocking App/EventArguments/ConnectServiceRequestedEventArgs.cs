using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Blocking_App.Enums;

namespace Time_Blocking_App.EventArguments
{
    /// <summary>
    /// Contains event ifo for a request to connect a service from the SettingsPage.ConnectServiceRequested event.
    /// </summary>
    public class ConnectServiceRequestedEventArgs : EventArgs
    {
        /// <summary>
        /// The enum value of the service being requested to connect to.
        /// </summary>
        public ConnectedServiceType RequestedService { get; private set; }

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="requestedService"></param>
        public ConnectServiceRequestedEventArgs(ConnectedServiceType requestedService)
        {
            this.RequestedService = requestedService;
        }
    }
}
