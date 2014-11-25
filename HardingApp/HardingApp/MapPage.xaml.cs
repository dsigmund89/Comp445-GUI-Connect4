using Bing.Maps;
using HardingApp.Common;
using System;
using System.Collections.Generic;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace HardingApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MapPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MapPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            AddPushpins();
        }

        public void AddPushpins()
        {
            // These are all of the buildings on campus.

            // Administration/General Buildings
            AddPushpin(new Location(35.248974, -91.724070), "Pryor-England Center", "Classes Include: Comp Sci, Math, Engineering, and other Sciences", DataLayer);
            AddPushpin(new Location(35.247982, -91.725342), "Benson Auditorium", "Don't be late for Chapel!", DataLayer);
            AddPushpin(new Location(35.248862, -91.725073), "McInteer Bible Building", "Bible Classes are found here", DataLayer);
            AddPushpin(new Location(35.248818, -91.726157), "Hammon Student Center", "Main place students can be found. You will also find your mailbox, the HUB, Chick-fil-a, and other places to eat.", DataLayer);
            AddPushpin(new Location(35.249002, -91.726994), "American Heritage Bldg.", "The Heritage hotel, Cone Chapel, and the Heritage Auditorium can be found here", DataLayer);
            AddPushpin(new Location(35.248185, -91.723967), "Mabee Business Bldg.", "Classes Include: Business", DataLayer);
            AddPushpin(new Location(35.247703, -91.724396), "Lee Bldg.", "Misc.", DataLayer);
            AddPushpin(new Location(35.247291, -91.724139), "Rhodes Field House", "Come cheer on the Bisons and Lady Bisons!", DataLayer);
            AddPushpin(new Location(35.246785, -91.726551), "Ezell Bldg.", "Classes Include: English, Psychology, and History Classes.", DataLayer);
            AddPushpin(new Location(35.246829, -91.727017), "Administration Bldg.", "The Admin Auditorium and important offices can be found here.", DataLayer);
            AddPushpin(new Location(35.246859, -91.727280), "Ganus Bldg.", "More History Classes", DataLayer);
            AddPushpin(new Location(35.248285, -91.727933), "American Studies Bldg.", "Classes Include: English & Literature", DataLayer);
            AddPushpin(new Location(35.248370, -91.728373), "Thornton Education Center", "Classes", DataLayer);
            AddPushpin(new Location(35.248934, -91.729793), "Sears Honors College", "Honor Students can be found here", DataLayer);
            AddPushpin(new Location(35.244449, -91.731451), "Ulrey Performing Arts Ctr.", "Don't miss some really great performances!", DataLayer);
            AddPushpin(new Location(35.244926, -91.728308), "Farrar Health Sciences Ctr.", "Health related classes can be found here", DataLayer);
            AddPushpin(new Location(35.244869, -91.727691), "Nursing Bldg.", "Nursing classes can be found here", DataLayer);
            AddPushpin(new Location(35.244482, -91.726256), "Reynolds Center", "Classes Include: Music and Education", DataLayer);
            AddPushpin(new Location(35.244346, -91.723622), "Ganus Athletic Center", "Come play some Basketball, workout in the gym, or go swimming.", DataLayer);
            AddPushpin(new Location(35.244236, -91.722995), "Jim Citty Football Complex", "Workout", DataLayer);
            AddPushpin(new Location(35.244933, -91.721171), "First Security Stadium", "GO BISONS!!!", DataLayer);
            AddPushpin(new Location(35.247224, -91.726127), "Brackett Library", "Quiet!", DataLayer);
            AddPushpin(new Location(35.247938, -91.726235), "Olen Hendrix Bldg.", "Classes", DataLayer);
            AddPushpin(new Location(35.248379, -91.726484), "Stevens Art & Design Center", "Classes Include: Various art and design classes", DataLayer);
            AddPushpin(new Location(35.248799, -91.727627), "The Caf", "Come get some of Aramark's finest!", DataLayer);

            // Men's Dorms
            AddPushpin(new Location(35.246068, -91.722569), "Cone Dorm", "Men's Upperclassmen dorm, Apartment style rooms.", DataLayer);
            AddPushpin(new Location(35.246072, -91.723492), "Harbin Dorm", "Men's freshmen dorm, community style baths.", DataLayer);
            AddPushpin(new Location(35.246580, -91.723996), "Allen Dorm", "Men's Upperclassmen dorm, community style baths.", DataLayer);
            AddPushpin(new Location(35.246230, -91.725021), "Armstrong Dorm", "Freshmen dorm, suite style rooms.", DataLayer);
            AddPushpin(new Location(35.246252, -91.725761), "Keller", "Men's Upperclassmen dorm, suite style rooms.", DataLayer);
            AddPushpin(new Location(35.246909, -91.725187), "Grad Dorm", "Men's Upperclassmen dorm, suite style rooms.", DataLayer);

            // Women's Dorms
            AddPushpin(new Location(35.246097, -91.728003), "Pryor Hall", "Women's upperclassmen housing", DataLayer);
            AddPushpin(new Location(35.246506, -91.728566), "Shores Hall", "Women's upperclassmen housing", DataLayer);
            AddPushpin(new Location(35.246918, -91.728566), "Stephens Hall", "Women's sophomore housing", DataLayer);
            AddPushpin(new Location(35.247137, -91.728304), "Cathcart Hall", "Women's freshmen housing", DataLayer);
            AddPushpin(new Location(35.247211, -91.727966), "Pattie Cobb Hall", "Women's sophomore housing", DataLayer);
            AddPushpin(new Location(35.247737, -91.728459), "Kendall Hall", "Women's freshmen housing", DataLayer);
            AddPushpin(new Location(35.248039, -91.729489), "Sears Hall", "Women's freshmen housing", DataLayer);
            AddPushpin(new Location(35.247868, -91.730117), "Searcy Hall", "Women's upperclassmen housing", DataLayer);

        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        // Code found here http://blogs.bing.com/maps/2013/06/17/infoboxes-for-native-windows-store-apps/
        // to add a info box to a point on the map.
        private void CloseInfobox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Infobox.Visibility = Visibility.Collapsed;
        }

        public class MetaData
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public void AddPushpin (Location latlong, string title, string description, MapLayer layer)
        {
            Pushpin p = new Pushpin()
            {
                Tag = new MetaData()
                {
                    Title = title,
                    Description = description
                }
            };

            MapLayer.SetPosition(p, latlong);

            p.Tapped += PinTapped;

            layer.Children.Add(p);
        }

        private void PinTapped(object sender, TappedRoutedEventArgs e)
        {
            Pushpin p = sender as Pushpin;
            MetaData m = (MetaData)p.Tag;

            //Ensure there is content to be displayed before modifiying the infobox control
            if (!String.IsNullOrEmpty(m.Title) || !String.IsNullOrEmpty(m.Description))
            {
                Infobox.DataContext = m;

                Infobox.Visibility = Visibility.Visible;

                MapLayer.SetPosition(Infobox, MapLayer.GetPosition(p));
            }
            else
            {
                Infobox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
