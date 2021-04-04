using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Time_Blocking_App.Enums;
using Time_Blocking_App.EventArguments;
using Time_Blocking_App.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

// Navigation code based on example at https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview#code-example

namespace Time_Blocking_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// List of values to convert between page types and tags.
        /// </summary>
        private readonly List<(PageTypes Tag, Type Page)> _pages = new List<(PageTypes Tag, Type Page)>
        {
            (PageTypes.Home, typeof(HomePage)),
            (PageTypes.Settings, typeof(SettingsPage))
        };

        #region Events
        public delegate void NavigationRequestedHandler(object sender, NavigationRequestedEventArgs e);
        /// <summary>
        /// Raised when the user indicated they want to navigate.
        /// </summary>
        public event NavigationRequestedHandler NavigationRequested;
        private void RaiseNavigationRequested(PageTypes toPage, bool isBackRequest = false)
        {
            // Create the args and call the listening event handlers, if there are any.
            NavigationRequestedEventArgs args = new NavigationRequestedEventArgs(toPage, isBackRequest);
            this.NavigationRequested?.Invoke(this, args);
        }
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles whenever a menu item in the navigation view is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                // Settings does not have a tag, as it is handled separately from the other menu items.
                this.RaiseNavigationRequested(PageTypes.Settings);
            }
            else
            {
                // Get the page enum value associated with the selected nav menu item's tag.
                PageTypes toPage = Enum.Parse<PageTypes>(args.InvokedItemContainer.Tag.ToString());
                this.RaiseNavigationRequested(toPage);
            }
        }

        /// <summary>
        /// Handles whenever back navigation is requested from the navigation view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            this.On_BackRequested();
        }

        /// <summary>
        /// Handles when the content frame fails to navigate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Handles relevant setup when the navigation view has loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // Default to the first menu item (home) when loaded.
            this.NavigationView.SelectedItem = NavigationView.MenuItems[0];

            //this.Navigate(PageTypes.Home, new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());

            // Add keyboard accelerators for backwards navigation.
            var goBack = new KeyboardAccelerator { Key = Windows.System.VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);

            // ALT routes here
            var altLeft = new KeyboardAccelerator
            {
                Key = Windows.System.VirtualKey.Left,
                Modifiers = Windows.System.VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        /// <summary>
        /// Handles logic once the content frame has completed a successful navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            this.NavigationView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                this.NavigationView.SelectedItem = (NavigationViewItem)this.NavigationView.SettingsItem;
                this.NavigationView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                this.NavigationView.SelectedItem = this.NavigationView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag.ToString()));

                this.NavigationView.Header = ((NavigationViewItem)this.NavigationView.SelectedItem)?.Content?.ToString();
            }
        }

        #region Methods
        /// <summary>
        /// Navigates the child page to the requested page.
        /// </summary>
        /// <param name="page">The page to navigate to</param>
        public void Navigate(PageTypes page)
        {
            // Get the page from the list with a tag matching the given page type.
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(page));

            // In theory this shouldn't happen... but just in case...
            if (item.Page == null)
            {
                throw new Exception("unexpected");
            }

            // Tell the content frame inside of the navigation view to navigate pages.
            this.ContentFrame.Navigate(item.Page);
        }

        /// <summary>
        /// Navigates the child page to the requested page.
        /// </summary>
        /// <param name="page">The page to navigate to</param>
        public void Navigate(PageTypes page, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            // Get the page from the list with a tag matching the given page type.
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(page));

            // In theory this shouldn't happen... but just in case...
            if (item.Page == null)
            {
                throw new Exception("unexpected");
            }

            // Tell the content frame inside of the navigation view to navigate pages.
            this.ContentFrame.Navigate(item.Page, null, transitionInfo);
        }

        /// <summary>
        /// Navigates to the previous child page.
        /// </summary>
        public void NavigateBack()
        {
            this.ContentFrame.GoBack();
        }
        #endregion

        #region Helper Methods
        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            this.On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!this.ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (this.NavigationView.IsPaneOpen &&
                (this.NavigationView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 this.NavigationView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            string pageTypeString = this.ContentFrame.BackStack.First().SourcePageType.Name.ToString().Replace("Page", String.Empty);
            PageTypes backToPage = Enum.Parse<PageTypes>(pageTypeString);
            this.RaiseNavigationRequested(backToPage, true);
            return true;
        }
        #endregion
    }
}
