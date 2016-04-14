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

namespace PIApp.Droid
{
    [Activity(Label = "Drinking")]
    public class Drinking : Activity
    {
        //Declare containers
        string[] quantity;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DrinkPickerLayout);

            //Define containers

            quantity = new string[] {"1cl", "2cl", "3cl", "4cl", "5cl", "1dl", "2dl", "3dl", "4dl", "5dl"};
            ListView lv = FindViewById<ListView>(Resource.Id.lvQuantity);
            Button drink = FindViewById<Button>(Resource.Id.cmdDrink);

            drink.Click += delegate
            {

                Toast.MakeText(this, "Teszt", ToastLength.Short);
            };
            //ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, quantity);
            lv.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, quantity));
        }
    }
}