using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Blocking_App.Controllers.Navigation;
using Time_Blocking_App.Enums;
using Time_Blocking_App.EventArguments;
using Time_Blocking_App.Models.ClickUp;
using Time_Blocking_App.Pages;
using Windows.UI.Xaml.Controls;

namespace Time_Blocking_App.Controllers
{
    /// <summary>
    /// The main controller for the app - handles navigation between app-level processes.
    /// </summary>
    public class AppController : SingletonController<AppController>
    {
        #region Properties
        /// <summary>
        /// The root page for app navigation.
        /// </summary>
        public MainPage RootPage { get; private set; }

        /// <summary>
        /// The current navigation state of the app.
        /// </summary>
        public AppNavigationState NavState { get; set; }

        /// <summary>
        /// Reference to currently displayed page.
        /// </summary>
        public Page CurrentPage { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the app controller class, with default values for properties.
        /// </summary>
        public AppController()
        {
            // Initialize the controller with a main page as the root and the start state for the app navigation.
            this.RootPage = new MainPage();
            this.NavState = new StartState(this);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles a navigation request between app pages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigationRequested(object sender, NavigationRequestedEventArgs e)
        {
            // If this is a back request, go back and exit this method.
            if (e.IsBackRequest)
            {
                this.NavState.GoBack();
                return;
            }

            switch (e.ToPage)
            {
                case PageTypes.Home:
                    // Navigate to the home page.
                    this.NavState.GotoHome();
                    break;
                case PageTypes.TimeBlocks:
                    // Navigate to the time blocks page.
                    this.NavState.GotToTimeBlocks();
                    break;
                case PageTypes.Settings:
                    // Navigate to the settings page.
                    this.NavState.GotoSettings();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles when a navigation between pages is complete. Subscribes to the new page's
        /// events and unsubscribes from the old page's events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigated(object sender, NavigatedEventArgs e)
        {
            // Store a reference to the page as the new current page.
            this.CurrentPage = e.PageNavigatedTo;

            // Subscribe to the new page's events.
            if (e.PageNavigatedTo is HomePage homePage)
            {

            }
            else if (e.PageNavigatedTo is SettingsPage settingsPage)
            {
                settingsPage.ConnectServiceRequested += this.ConnectedServiceRequested;
            }

            // Unsubscribe from the old page's events. This is to clear any handles on the page
            //      so that garbage collection deletes it and prevents a memory leak.
            if (e.PageNavigatedFrom is null)
            {
                // There is no from page (this is the app initialization), so nothing more to do.
                return;
            }
            else if (e.PageNavigatedFrom is HomePage homePageFrom)
            {

            }
            else if (e.PageNavigatedFrom is SettingsPage settingsPageFrom)
            {
                settingsPageFrom.ConnectServiceRequested -= this.ConnectedServiceRequested;
            }
        }

        /// <summary>
        /// Handles when a request is received to initiate connecting an integrated service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectedServiceRequested(object sender, ConnectServiceRequestedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Nailed it");
            //ClickUpAPIWrapper.Instance.StartOAuthAsync();
            ClickUpAPIWrapper.Instance.StartOAuthAsync2();
            //Uri oauthEndpoint = ClickUpAPIWrapper.Instance.GetOauthEndpoint();

            // Navigate to the settings page.
            //this.NavState.GotoSettings();

            //if (this.CurrentPage is SettingsPage settingsPage)
            //{
            //    settingsPage.BeginAuthorizeConnection(oauthEndpoint);
            //}
            //else
            //{
            //    // If for some reason the settings page was not navigated to by the previous call...
            //    throw new Exception("Inconsistent settings state. Expected to be on Settings Page.");
            //}
        }
        #endregion

        #region Methods
        /// <summary>
        /// Intended to be called upon app launch. Initializes app navigation.
        /// </summary>
        /// <param name="rootPage">The root page for this initialization of the app.</param>
        public void StartApp(MainPage rootPage)
        {
            // Subscribe to the root page's events.
            rootPage.NavigationRequested += this.OnNavigationRequested;
            rootPage.Navigated += this.OnNavigated;

            // Set the given page as the root page.
            this.RootPage = rootPage;

            // Navigate to the home page.
            this.NavState.GotoHome();
        }

        public void ResumeAppFromAPIAuth(MainPage rootPage)
        {
            // Subscribe to the root page's events.
            rootPage.NavigationRequested += this.OnNavigationRequested;
            rootPage.Navigated += this.OnNavigated;

            // Set the given page as the root page.
            this.RootPage = rootPage;

            System.Diagnostics.Debug.WriteLine("annnnnd we back!");

            // Finish the Authorization.
            //ClickUpAPIWrapper.Instance.GetOauthTokenAsync();
            // Navigate to the home page.
            //this.NavState.GotoHome();
        }
        #endregion
    }
}
