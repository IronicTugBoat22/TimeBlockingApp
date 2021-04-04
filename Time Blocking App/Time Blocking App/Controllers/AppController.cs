using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Blocking_App.Controllers.Navigation;
using Time_Blocking_App.Enums;
using Time_Blocking_App.EventArguments;

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
                case PageTypes.Settings:
                    // Navigate to the home page.
                    this.NavState.GotoSettings();
                    break;
                default:
                    break;
            }
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

            // Set the given page as the root page.
            this.RootPage = rootPage;

            // Navigate to the home page.
            this.NavState.GotoHome();
        }
        #endregion
    }
}
