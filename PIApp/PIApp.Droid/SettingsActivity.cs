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
        Person up; //user profile - red from file
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

            //////////////////////////////////////////////////////////////////////////


            //Define profile data path
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), "profile.piapp");

            //Declare temp content array
            string[] content = new string[9];

            //Check profile data exist
            if (File.Exists(filename) == true)
            {
                //Store all lines in content array
                content = ReadProfileFromFile(filename);
                //Make Person object from tmp array
                up = new Person(content[0], content[1], Convert.ToSingle(content[5]), Convert.ToDouble(content[6]), content[7], content[8], content[3], Convert.ToDateTime(content[4]), content[2]);

                //Fill the profile form
                firstname.Text = up.getFirstName();
                lastname.Text = up.getLastName();
                password.Text = up.getPassword();
                height.Text = up.getHeight().ToString();
                weight.Text = up.getWeight().ToString();
                ice.Text = up.getIce();
                address.Text = up.getHomeAddress();

                //Declare gender
                string gender = content[3];
                if(gender == "male")
                {
                    male.Checked = true;
                    female.Checked = false;
                }
                else
                {
                    male.Checked = false;
                    female.Checked = true;
                }

                birthday.DateTime = up.getBirthday();

            }

            

            savebutton.Click += delegate {
                

                if (checkFill(firstname, lastname, password, height, weight, address) == false)
                {
                    ShowAlert("Hiba!","A csillaggal jelölt mezõk kitöltése kötelezõ!");
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
                    ShowAlert("Mentés","A profil mentése sikeres volt!");
                    //Restart
                    StartActivity(typeof(MainActivity));

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

        private void WriteProfileToFile(Person p)
        {

            
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), "profile.piapp");
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
                StreamWriter streamWriter = new StreamWriter(filename, true);
            try
            {
                streamWriter.WriteLine(p.getFirstName()); //0
                streamWriter.WriteLine(p.getLastName()); //1
                streamWriter.WriteLine(p.getPassword()); //2
                streamWriter.WriteLine(p.getGender()); //3
                streamWriter.WriteLine(p.getBirthday()); //4
                streamWriter.WriteLine(p.getHeight()); //5
                streamWriter.WriteLine(p.getWeight()); //6
                streamWriter.WriteLine(p.getHomeAddress()); //7
                streamWriter.WriteLine(p.getIce()); //8


            }
            catch (IOException e)
            {
                ShowAlert("Hiba!","A profil mentése nem sikerült! Hiba:" + e);
            }
            finally
            {
                streamWriter.Close();
            }
        }

        private string[] ReadProfileFromFile(string filename)
        {
            string[] content = new string[9];
            StreamReader streamReader = new StreamReader(filename, true);

            try
            {
                for(int i=0; i<9; ++i)
                {
                    content[i] = streamReader.ReadLine();
                }
                
            }
            catch(IOException e)
            {
                ShowAlert("Hiba!","A profil beolvasása nem sikerült! Hiba: " + e);
            }
            finally
            {
                streamReader.Close();
            }

            return content;
        }
    }
}