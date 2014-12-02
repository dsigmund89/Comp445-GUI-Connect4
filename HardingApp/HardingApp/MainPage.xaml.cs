using HardingApp.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HardingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string urlString = "https://pipeline.harding.edu/logout.php";

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            Debug.WriteLine("MainPage - SaveState");
        }

        void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Debug.WriteLine("MainPage - LoadState");

            // Load app data
            Windows.Storage.ApplicationDataContainer roamingSetttings =
                Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSetttings.Values.ContainsKey("welcometext"))
                welcomeTextBlock.Text = roamingSetttings.Values["welcometext"].ToString();
        }

        #region Navigation Helper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void chapelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChapelPage));
        }

        private void mapButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapPage));
        }

        private void emergenciesButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmergenciesPage));
        }

        private void classesButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClassesPage));
        }

        private void gradesButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GradesPage));
        }

        private void currentEventsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SchedulePage));
        }

        private void classifiedButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WhiteboardPage));
        }

        private void cafeteriaButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HardingCafeteriaInfoPage));
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }

        private async void Logout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HtmlDocument doc = new HtmlDocument();
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(new Uri(urlString, UriKind.Absolute));
                doc.LoadHtml(result);
            }

             Windows.Storage.ApplicationDataContainer roamingSettings =
              Windows.Storage.ApplicationData.Current.RoamingSettings;

             if (roamingSettings.Values.ContainsKey("username") && roamingSettings.Values.ContainsKey("password"))
             {
                 roamingSettings.Values["username"] = "";
                 roamingSettings.Values["password"] = "";
             }

            this.Frame.Navigate(typeof(MyLoginPage));
        }

    }
}
