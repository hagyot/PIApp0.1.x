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
                        string startpartystr = startparty.ToString("yyyy.MM.dd hh:mm");
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
                if (currentpiappbeolvasas.Count <2 )
                {
                    DeleteFile("current.piapp");
                    //Restart
                    StartActivity(typeof(MainActivity));

                }
                else
                {
                    //Party példányosítása

                    party = new Party(currentpiappbeolvasas[0], Convert.ToDateTime(currentpiappbeolvasas[1]));

                    //Rendrakás a kijelzõn

                    bulinev.Visibility = ViewStates.Gone;
                    startParty.Visibility = ViewStates.Gone;
                    ujKor.Visibility = ViewStates.Visible;
                    elozoKor.Visibility = ViewStates.Visible;
                    endParty.Visibility = ViewStates.Visible;
                }
            }

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
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                // write your own set of instructions
            });

            //run the alert in UI thread to display in the screen
            RunOnUiThread(() => {
                alert.Show();
            });
        }

        
        public void ShowYesNo(string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Igen", (senderAlert, args) => {
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
                for(int i=0; i<message.Count; ++i)
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

        private void DeleteFile(string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);

            if (File.Exists(filename))
               {
                   File.Delete(filename);
               }
        }




    }
}

