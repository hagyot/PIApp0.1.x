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
using System.IO;

namespace PIApp.Droid
{
    [Activity]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set content view
            SetContentView(Resource.Layout.settingsLayout);

            //Declare layout objects
            EditText firstname;
            EditText lastname;
            EditText password;
            EditText height;
            EditText weight;
            EditText address;
            EditText ice;
            RadioButton male;
            RadioButton female;
            DatePicker birthday; 
            Button savebutton = FindViewById<Button>(Resource.Id.cmdSave);

            savebutton.Click += delegate {
                firstname = FindViewById<EditText>(Resource.Id.txtFirstname);
                lastname = FindViewById<EditText>(Resource.Id.txtLastname);
                password = FindViewById<EditText>(Resource.Id.txtPassword);
                height = FindViewById<EditText>(Resource.Id.txtHeight);
                weight = FindViewById<EditText>(Resource.Id.txtWeight);
                address = FindViewById<EditText>(Resource.Id.txtAddress);
                ice = FindViewById<EditText>(Resource.Id.txtIce);
                male = FindViewById<RadioButton>(Resource.Id.rbMale);
                female = FindViewById<RadioButton>(Resource.Id.rbFemale);
                birthday = FindViewById<DatePicker>(Resource.Id.dpBirthday);



                if (checkFill(firstname, lastname, password, height, weight, address) == false)
                {
                    ShowAlert("A csillaggal jelölt mezõk kitöltése kötelezõ!");
                }
                else
                {

                    string gender;
         
                    if (male.Checked == true)
                    {
                        gender = "male";
                    }
                    else
                    {
                        gender = "female";
                    }


                    Person user = new Person(firstname.Text, lastname.Text, Convert.ToSingle(height.Text), Convert.ToDouble(weight.Text), address.Text, ice.Text, gender, birthday.DateTime, password.Text);
                    WriteProfileToFile(user);
                    ShowAlert("A profil mentése sikeres volt!");

                }
            };
        }

        private bool checkFill(EditText firstname, EditText lastname, EditText password, EditText height, EditText weight, EditText address)
        {
            //Ezt false-ra kell állítani
            bool filled = false;

            if(firstname.Length() > 0 && lastname.Length() > 0 && password.Length() > 0 && height.Length() > 0 && weight.Length() > 0 && address.Length() > 0)
            {
                filled = true;
            }
            return filled;
        }

        public void ShowAlert(string str)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(str);
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                // write your own set of instructions
            });

            //run the alert in UI thread to display in the screen
            RunOnUiThread(() => {
                alert.Show();
            });
        }

        private void WriteProfileToFile(Person p)
        {

            
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), "profile.piapp");
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
                StreamWriter streamWriter;
            streamWriter = new StreamWriter(filename, true);
            try
            {
                streamWriter.Write(p);
            }
            catch (IOException e)
            {
                ShowAlert("A profil mentése nem sikerült! Hiba:" + e);
            }
            finally
            {
                streamWriter.Close();
            }
        }
    }
}