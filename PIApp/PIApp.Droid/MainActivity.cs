using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PIApp.Droid
{
	[Activity (Label = "PIApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : TabActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            ActionBar.Hide();
            CreateTab(typeof(PartyActivity), "party", "Party", Resource.Drawable.drinking);
            CreateTab(typeof(StatusActivity), "status", "Státusz", Resource.Drawable.status);
            CreateTab(typeof(TravelActivity), "travel", "Utazás", Resource.Drawable.Icon);
            CreateTab(typeof(StatisticsActivity), "statistics", "Statisztika", Resource.Drawable.stats);
            CreateTab(typeof(SettingsActivity), "settings", "Profil", Resource.Drawable.profile);

            /* az egyes activitylayoutok belseje
             
             <selector xmlns:android="http://schemas.android.com/apk/res/android">
    <item
        android:drawable="@drawable/ic_tab_whats_on_selected"
        android:state_selected="true" />
    <item
        android:drawable="@drawable/ic_tab_whats_on_unselected" />
</selector>
             
             */
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
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }
    }
}


