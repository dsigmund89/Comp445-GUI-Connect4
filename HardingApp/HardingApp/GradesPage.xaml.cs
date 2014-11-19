using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Http;
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
using HtmlAgilityPack;

namespace HardingApp
{
   /// <summary>
   /// A basic page that provides characteristics common to most applications.
   /// </summary>
   public sealed partial class GradesPage : Page
   {

      private NavigationHelper navigationHelper;
      private ObservableDictionary defaultViewModel = new ObservableDictionary();
      private string urlString = "https://pipeline.harding.edu/student/";

      
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


      public GradesPage()
      {
         this.InitializeComponent();
         this.navigationHelper = new NavigationHelper(this);
         this.navigationHelper.LoadState += navigationHelper_LoadState;
         this.navigationHelper.SaveState += navigationHelper_SaveState;
         GetGradesInfo();
      }

      private async void GetGradesInfo()
      {

         using (var client = new HttpClient())
         {
            HtmlDocument doc;
            doc = new HtmlDocument();
            var result = await client.GetStringAsync(new Uri(urlString, UriKind.Absolute));
            doc.LoadHtml(result);
            var value = doc.DocumentNode.Descendants("div")
               .Where(
                  x => x.Attributes.Contains("id") && x.Attributes["id"].Value.Trim().ToString().Equals("studentgrades"))
               .SelectMany(x => x.Descendants("table").SelectMany(y => y.Descendants("tr").Skip(1)
                  .Select(r =>
                  {
                     var linkNode = r.Descendants("td").Select(b => b.InnerText.Trim()).ToArray();
                     return new Grades
                     {
                        Title = linkNode[0].ToString(),
                        Teacher = linkNode[1].ToString(),
                        Midterm = linkNode[2].ToString(),
                        Final = linkNode[3].ToString()
                     };
                  }
                  ))).ToList();
      
            var currentSemesterVar = doc.DocumentNode.Descendants("div")
               .Where(x => x.Attributes.Contains("id")
                           && x.Attributes["id"].Value.Trim().ToString().Equals("studentgrades"))
               .Select(x => x.Descendants("h3").Select(y => y.InnerText.Trim())).FirstOrDefault();


            foreach (var item in currentSemesterVar)
            {
               CurrentSemesterTextBlock.Text = item.ToString();
            }
            GradesListView.ItemsSource = value;
         }
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
   }

   public class Grades
   {
      public string Title { get; set; }
      public string Teacher { get; set; }
      public string Midterm { get; set; }
      public string Final { get; set; }
   }
}