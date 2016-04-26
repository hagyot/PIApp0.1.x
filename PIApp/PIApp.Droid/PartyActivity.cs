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
using System.IO;

//TODO : A currentnél az italok mentése
//TODO : Az ivás elkészítése
namespace PIApp.Droid
{
    [Activity]
    public class PartyActivity : Activity
    {
        Party party;
        ImageButton elozoKor;
        ImageButton ujKor;
        ImageButton startParty;
        ImageButton endParty;
        EditText bulinev;

        string startpartystr;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PartyLayout);

            //Define imagebuttons
            elozoKor = FindViewById<ImageButton>(Resource.Id.elozoKor);
            ujKor = FindViewById<ImageButton>(Resource.Id.ujKor);
            startParty = FindViewById<ImageButton>(Resource.Id.startParty);
            bulinev = FindViewById<EditText>(Resource.Id.partyName);
            endParty = FindViewById<ImageButton>(Resource.Id.partyEnd);

            //Get screen size
            var metrics = Resources.DisplayMetrics;
            //var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

            elozoKor.SetMinimumHeight((heightInDp / 3) - 10);
            ujKor.SetMinimumHeight((heightInDp / 3) - 10);
            startParty.SetMinimumHeight(heightInDp / 3);


            ////////////////////////////////////////////////////////////////////////////
            ///Itt indul a móka

            //current.piapp meglétének ellenõrzése

            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var currentpiapp = Path.Combine(path.ToString(), "current.piapp");

            if (!File.Exists(currentpiapp))
            {
                //Ha nem létezik
                startParty.Click += delegate
                {
                    if (bulinev.Text == "")
                    {
                        ShowAlert("Hiba!", "Kérlek add meg a buli nevét!");
                        bulinev.RequestFocus();
                    }
                    else
                    {
                        //Változók definiálása
                        string partyname = bulinev.Text;
                        var startparty = DateTime.Now;
                        startpartystr = startparty.ToString("yyyy.MM.dd hh:mm");
                        party = new Party(partyname, startparty);

                        //Kiírandó lista létrehozása

                        List<string> current = new List<string>();

                        current.Add(partyname);
                        current.Add(startpartystr);
                        //A buli kiírása a folyamatban lévõ bulit jelzõ fájlba (current.piapp)
                        //Buli neve, kezdés ideje

                        WriteToFile(current, "current.piapp");



                        //Felesleges dolgok eltûntetése

                        bulinev.Visibility = ViewStates.Gone;
                        startParty.Visibility = ViewStates.Gone;
                        ujKor.Visibility = ViewStates.Visible;
                        elozoKor.Visibility = ViewStates.Visible;
                        endParty.Visibility = ViewStates.Visible;

                    }
                };
            }
            else
            {
                //Ha létezik

                //Beolvassuk a dolgokat fájlból
                List<string> currentpiappbeolvasas = new List<string>();
                currentpiappbeolvasas = ReadFromFile("current.piapp");
                //Ellenõrizzük, hogy a fájl üres-e
                if (currentpiappbeolvasas.Count < 2)
                {
                    DeleteFile("current.piapp");
                    //Restart
                    StartActivity(typeof(MainActivity));

                }
                else
                {
                    //Party példányosítása

                    party = new Party(currentpiappbeolvasas[0], Convert.ToDateTime(currentpiappbeolvasas[1]));

                    //Ellenõrizzük, hogy történt-e már ivás (megvan-e a fájl)
                    string partyname = currentpiappbeolvasas[0];
                    startpartystr = currentpiappbeolvasas[1];
                    var path2 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    var currentdrinks = Path.Combine(path2.ToString(), partyname + "drinks.piapp");

                    if (File.Exists(currentdrinks))
                    {

                        List<string> drinklist = new List<string>();
                        drinklist = ReadPartyFromFile(startpartystr + partyname + "drinks.piapp");
                        ShowAlert("Teszt", drinklist.Count.ToString());

                        //Rendrakás a kijelzõn

                        bulinev.Visibility = ViewStates.Gone;
                        startParty.Visibility = ViewStates.Gone;
                        ujKor.Visibility = ViewStates.Visible;
                        elozoKor.Visibility = ViewStates.Visible;
                        elozoKor.Enabled = true;
                        endParty.Visibility = ViewStates.Visible;
                    }

                    else

                    {
                        //Rendrakás a kijelzõn

                        bulinev.Visibility = ViewStates.Gone;
                        startParty.Visibility = ViewStates.Gone;
                        ujKor.Visibility = ViewStates.Visible;
                        elozoKor.Visibility = ViewStates.Visible;
                        endParty.Visibility = ViewStates.Visible;
                    }



                }
            }

            //Elõzõ kör ismétlése gomb megnyomása
            elozoKor.Click += delegate
            {

            };

            //Új kör gomb megnyomása
            ujKor.Click += delegate
            {
                ShowDrink();
            };

            //Buli vége gomb megnyomása
            endParty.Click += delegate
           {

               ShowYesNo("Buli vége", "Biztosan befejezted a bulizást?");
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
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                // write your own set of instructions
            });

            //run the alert in UI thread to display in the screen
            RunOnUiThread(() =>
            {
                alert.Show();
            });
        }

        public void ShowDrink()
        {
            /*var alert = new AlertDialog.Builder(this);
            alert.SetView(LayoutInflater.Inflate(Resource.Layout.DrinkPickerLayout, null));
            alert.Create().Show();*/

            //Dialog alert = new Dialog(this);
            //alert.SetContentView(StartActivity(typeof(Drinking)));
            StartActivity(typeof(Drinking));
            /*alert.SetContentView(LayoutInflater.Inflate(Resource.Layout.DrinkPickerLayout, null));
            alert.SetTitle("Új kör hozzáadása");
            

            string[] quantity = new string[] { "1cl", "2cl", "3cl", "4cl", "5cl", "1dl", "2dl", "3dl", "4dl", "5dl" };
            View drinkpicker = LayoutInflater.Inflate(Resource.Layout.DrinkPickerLayout, null);
            ListView lv = (ListView)drinkpicker.FindViewById(Resource.Id.lvQuantity);
            Button drink = (Button)drinkpicker.FindViewById(Resource.Id.cmdDrink);

            lv.SetAdapter(new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem2, quantity));
            alert.Show();*/


        }

        public void ShowYesNo(string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Igen", (senderAlert, args) =>
            {
                //Megadjuk az aktuális dátumot az endpartynak, töröljük a currentet, újra megjelenítjük amit kell
                party.setEnd(DateTime.Now.ToLocalTime());
                DeleteFile("current.piapp");
                //Restart
                StartActivity(typeof(MainActivity));


            });
            alert.SetNegativeButton("Nem", (senderAlert, args) =>
            {
                //NOTHING HAPPENS HERE
            });
            alert.Show();
        }

        //Fájlbaírás/////////////////////////////////////////////
        private void WriteToFile(List<string> message, string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);


            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            StreamWriter streamWriter = new StreamWriter(filename, true);
            try
            {
                for (int i = 0; i < message.Count; ++i)
                {
                    streamWriter.WriteLine(message.ElementAt<string>(i));
                }
            }
            catch (IOException e)
            {
                ShowAlert("Hiba!", "A fájlba írás nem sikerült! Hibakód:" + e);
            }
            finally
            {
                streamWriter.Close();
            }
        }

        //Fájlolvasás/////////////////////////////////////////////
        private List<string> ReadFromFile(string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);
            List<string> content = new List<string>();
            StreamReader streamReader = new StreamReader(filename, true);

            try
            {
                 for (int i = 0; i < 2; ++i)
                 {
                     content.Add(streamReader.ReadLine());
                 }
                 

               
            



            }
            catch (IOException e)
            {
                ShowAlert("Hiba!", "A beolvasás nem sikerült! Hibakód: " + e);
            }
            finally
            {
                streamReader.Close();
            }

            return content;
        }

        private List<string> ReadFromPartyFile(string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);

            List<string> content = new List<string>();
            FileStream fs = null;
            if (File.Exists(filename))
            {
                fs = File.Open(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                try
                {
                    while (!sr.EndOfStream)
                    {
                        content.Add(sr.ReadLine());
                        ShowAlert("teszt", sr.ReadLine());
                    }
                }
                catch (IOException ex)
                {
                    ShowAlert("Hiba", "A fájl olvasása nem sikerült! " + ex);
                }
                finally
                {
                    sr.Close();
                    fs.Close();


                }
            }
            else
            {
            }

            return content;
        }

        private void DeleteFile(string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        public List<string> ReadPartyFromFile(string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);
            FileStream fs = null;

            List<string> content = new List<string>();



            return content;
            
        }

    }
}

