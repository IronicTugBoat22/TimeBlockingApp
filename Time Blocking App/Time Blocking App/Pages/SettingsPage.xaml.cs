using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Time_Blocking_App.Enums;
using Time_Blocking_App.EventArguments;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Time_Blocking_App.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        #region Events
        public delegate void ConnectServiceRequestedHandler(object sender, ConnectServiceRequestedEventArgs e);
        /// <summary>
        /// Raised when the user indicated they want to connect a service.
        /// </summary>
        public event ConnectServiceRequestedHandler ConnectServiceRequested;
        private void RaiseConnectServiceRequested(ConnectedServiceType serviceType)
        {
            // Create the args and call the listening event handlers, if there are any.
            ConnectServiceRequestedEventArgs args = new ConnectServiceRequestedEventArgs(serviceType);
            this.ConnectServiceRequested?.Invoke(this, args);
        }
        #endregion

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles when the user clicks 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectClickUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Raise a request to connect the ClickUp service.
            this.RaiseConnectServiceRequested(ConnectedServiceType.ClickUp);
        }
    }
}
