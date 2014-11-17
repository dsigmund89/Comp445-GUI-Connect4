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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace HardingApp
{
   /// <summary>
   /// A basic page that provides characteristics common to most applications.
   /// </summary>
   public sealed partial class MyLoginPage : Page
   {
       private string urlString = "https://login.harding.edu/login?service=https%3A%2F%2Fpipeline.harding.edu%2F";
       private string ltValue;
       private string executionValue;

       private string loginParams;
       private string logInUserIdString;
       private string logInPasswordString;

       private string welcomeText;

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


      public MyLoginPage()
      {
         this.InitializeComponent();
         this.navigationHelper = new NavigationHelper(this);
         this.navigationHelper.LoadState += navigationHelper_LoadState;
         this.navigationHelper.SaveState += navigationHelper_SaveState;

         GetHiddenValues();
      }

      public async void GetHiddenValues()
      {
          HtmlDocument doc = new HtmlDocument();
          using (var client = new HttpClient())
          {
              var result = await client.GetStringAsync(new Uri(urlString, UriKind.Absolute));
              doc.LoadHtml(result);
              //var test = doc.DocumentNode.Descendants()
              // .Where(n => n.Name == "input");

              var value = from input in doc.DocumentNode.Descendants("input")
                          select input.OuterHtml;

              string temp;
              foreach (string data in value)
              {
                  if (data.Contains("name=\"lt\""))
                  {
                      temp = data;
                      int startPos = temp.LastIndexOf("value=\"") + "value=\"".Length;
                      int length = temp.IndexOf("\">") - startPos;
                      ltValue = temp.Substring(startPos, length);
                  }
                  else if (data.Contains("name=\"execution\""))
                  {
                      temp = data;
                      int startPos = temp.LastIndexOf("value=\"") + "value=\"".Length;
                      int length = temp.IndexOf("\">") - startPos;
                      executionValue = temp.Substring(startPos, length);
                  }
                  temp = "";
              }

              Debug.WriteLine("ltValue = " + ltValue);
              Debug.WriteLine("executionValue = " + executionValue);
              
          }
      }

      private async void VerifyLogin()
      {
          loginParams = "&username=" + logInUserIdString + "&password=" + logInPasswordString
                          + "&lt=" + ltValue + "&execution=" + executionValue + "&_eventId=submit&submit=Log+in";
          string teamResponse = urlString + loginParams;
          Debug.WriteLine(teamResponse);

          HttpClient client = new HttpClient();

          try
          {
              HttpResponseMessage response = await client.PostAsync(new Uri(teamResponse), null);

              response.EnsureSuccessStatusCode();
              string responseBody = await response.Content.ReadAsStringAsync();

              string temp;
              // Check for the Welcome message to see if login was successful
              if (responseBody.Contains("<div id=\"welcome\">"))
              {
                  LoginInfoTextBlock.Text = "";
                  temp = responseBody;
                  int startPos = temp.LastIndexOf("<div id=\"welcome\">") + "<div id=\"welcome\">".Length;
                  int length = temp.IndexOf("<a href=\"/logout.php\"") - startPos;
                  welcomeText = temp.Substring(startPos, length);

                  // Save (to localMachine) the welcomeText to be used elsewhere
                  Windows.Storage.ApplicationDataContainer roamingSettings =
                      Windows.Storage.ApplicationData.Current.RoamingSettings;
                  roamingSettings.Values["welcometext"] = welcomeText;

                  this.Frame.Navigate(typeof(MainPage));
              }
              else
              {
                  LoginInfoTextBlock.Text = "Invaild Credentials";
                  Debug.WriteLine("Didn't Login!");
              }

             // Debug.WriteLine(responseBody);
          }
          catch (System.Net.Http.HttpRequestException e)
          {
              Debug.WriteLine("\nException Caught!");
              Debug.WriteLine("Message :{0} ", e.Message);
          }
      }

      private void Button_Tapped(object sender, TappedRoutedEventArgs e)
      {
          if (UsernameTextBox.Text.Trim() != "" && PasswordBox.Password.Trim() != "")
          {
              logInUserIdString = UsernameTextBox.Text;
              logInPasswordString = PasswordBox.Password;

              VerifyLogin();
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
}
