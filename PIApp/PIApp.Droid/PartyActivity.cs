using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UIKit;

namespace PIApp.Droid
{
    [Activity]
    public class PartyActivity : Activity
    {
        Party party;
        ImageButton elozoKor;
        ImageButton ujKor;
        ImageButton startParty;
        EditText bulinev;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartyLayout);

            //Define imagebuttons
            elozoKor = FindViewById<ImageButton>(Resource.Id.elozoKor);
            ujKor = FindViewById<ImageButton>(Resource.Id.ujKor);
            startParty = FindViewById<ImageButton>(Resource.Id.startParty);
            bulinev = FindViewById<EditText>(Resource.Id.partyName);

            //Get screen size
            var metrics = Resources.DisplayMetrics;
            //var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

            elozoKor.SetMinimumHeight((heightInDp / 3) - 10);
            ujKor.SetMinimumHeight((heightInDp / 3) - 10);
            startParty.SetMinimumHeight(heightInDp / 3);


            ////////////////////////////////////////////////////////////////////////////
            ///Itt indul a móka
            ///
            startParty.Click += delegate
            {
                if (bulinev.Text == "")
                {
                    ShowAlert("Hiba!", "Kérlek add meg a buli nevét!");
                    bulinev.RequestFocus();
                }
                else
                {
                    string partyname = bulinev.Text;
                    DateTime startparty = new DateTime();
                    startparty = DateTime.Now.ToLocalTime();
                    party = new Party(partyname, startparty);

                    //Felesleges dolgok eltûntetése

                    bulinev.Visibility = ViewStates.Invisible;
                    startParty.Visibility = ViewStates.Invisible;
                    ujKor.Visibility = ViewStates.Visible;
                    elozoKor.Visibility = ViewStates.Visible;
                }
            };
       }
        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
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
