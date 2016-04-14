using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace PIApp.Droid
{
	[Activity (Label = "PIApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : TabActivity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            ActionBar.Hide();

            //Create tabs
            CreateTab(typeof(PartyActivity), "party", "Buli", Resource.Drawable.drinking);
            CreateTab(typeof(StatusActivity), "status", "Én", Resource.Drawable.status);
            CreateTab(typeof(TravelActivity), "travel", "Taxi", Resource.Drawable.Icon);
            CreateTab(typeof(StatisticsActivity), "statistics", "Bulik", Resource.Drawable.stats);
            CreateTab(typeof(SettingsActivity), "settings", "Profil", Resource.Drawable.profile);
             

            //Define profile data location
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), "profile.piapp");

            //Check profile data exist
            if (checkFileExist(filename) == false)
            {
                ShowAlert("Profil készítése","Üdvözöllek! Kérlek készítsd el a profilodat!");
                //A settings tab meghívása
                TabHost.SetCurrentTabByTag("settings");
                //A többi tab letiltása amíg a settings folyik
                TabHost.TabWidget.GetChildTabViewAt(0).Enabled = false;
                TabHost.TabWidget.GetChildTabViewAt(1).Enabled = false;
                TabHost.TabWidget.GetChildTabViewAt(2).Enabled = false;
                TabHost.TabWidget.GetChildTabViewAt(3).Enabled = false;
            }
            
            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button> (Resource.Id.myButton);

            //button.Click += delegate {
            //button.Text = string.Format ("{0} clicks!", count++);
            //};
        }

        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);

            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            //spec.SetIndicator(label, drawableIcon);
            spec.SetIndicator("", drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }

        private bool checkFileExist(string filenamewithpath)
        {
            bool exist = File.Exists(filenamewithpath);
            return exist;
        }

        public void ShowAlert(string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                // write your own set of instructions
            });

            //run the alert in UI thread to display in the screen
            RunOnUiThread(() => {
                alert.Show();
            });
        }


    }
}


