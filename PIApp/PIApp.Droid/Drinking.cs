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
using static Android.Widget.AdapterView;

namespace PIApp.Droid
{
    [Activity(Label = "Drinking")]
    public class Drinking : Activity
    {
        //Declare containers
        string[] quantity;
        string[] type;
        List<Drink> drinks = new List<Drink>();
        List<string> partydata = new List<string>();
        string partyname;
        string partydate;
        List<string> drinkname;

        //TODO: A kör elkészítése, kiírása fájlba stb.
        //TODO: Betöltéskor ellenõrizni, hogy van-e már kör ebben a buliban


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DrinkPickerLayout);
            ActionBar.Hide();
            //Define containers

            quantity = new string[] {"1cl", "2cl", "3cl", "4cl", "5cl", "1dl", "2dl", "3dl", "4dl", "5dl"};
            type = new string[] { "Abszint", "Bor", "Brandy", "Cider, Radler", "Gin", "Keserû likõr", "Koktéllikõr", "Konyak", "Likõr", "Longdrink", "Pálinka", "Pezsgõ", "Rum", "Sör", "Tequila", "Vermuth", "Vodka", "Whisky"};
            ListView lv = FindViewById<ListView>(Resource.Id.lvQuantity);
            ListView tp = FindViewById<ListView>(Resource.Id.lvDrinkType);
            ListView bn = FindViewById<ListView>(Resource.Id.lvBrandType);
            Button drink = FindViewById<Button>(Resource.Id.cmdDrink);
            Button cancel = FindViewById<Button>(Resource.Id.cmdCancel);

            //Load drinks
            AddDrink("abszint");
            AddDrink("bor");
            AddDrink("brandy");
            AddDrink("ciderradler");
            AddDrink("gin");
            AddDrink("keseru");
            AddDrink("koktellikor");
            AddDrink("konyak");
            AddDrink("likor");
            AddDrink("longdrink");
            AddDrink("palinka");

            //Load partydata

            partydata = ReadPartyData("current.piapp");
            partyname = partydata[0];
            partydate = partydata[1];


            drink.Click += delegate
            {


                
                    string drinking;
                    //Alkohol típusa
                    long[] id = tp.GetCheckItemIds();
                    string dtype = type[Convert.ToInt16(id[0])];
                    //Alkohol márkája
                    id = bn.GetCheckItemIds();
                    string btype = drinkname[Convert.ToInt16(id[0])];
                    //Fogyasztott mennyiség
                    id = lv.GetCheckItemIds();
                     string qtype = quantity[Convert.ToInt16(id[0])];
                    //Alkohol százaléka
                    string vvpercent = "";
                    
                    int i = 0;
                    while(drinks[i].name.Equals(btype))
                    {
                        if (drinks[i].name.Equals(btype))
                        {
                            vvpercent = drinks[i].vvpercent;
                        }
                        ++i;
                    }
                    //Fogyasztás ideje
                    var drinktime = DateTime.Now;
                    string drinktimestr = drinktime.ToString("yyyy.MM.dd hh:mm");

                    //Összefûzés stringgé

                    drinking = dtype + ";" + btype + ";" + qtype + ";" + vvpercent + ";" + drinktimestr;

                    //Kiírás fájlba

                   WriteDrink(drinking, partydate + partyname + "drinks.piapp");

               

            };

            cancel.Click += delegate
            {

                Finish();
            };

            tp.ItemClick += delegate
            {
                long[] id = tp.GetCheckItemIds();
                int ind = Convert.ToInt16(id[0]);
                drinkname = new List<string>();
                for (int i = 0; i < drinks.Count; ++i)
                {
                    if (drinks[i].id == ind)
                    {
                        drinkname.Add(drinks[i].name);
                    }
                }
                //Populate drinklist
                bn.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SelectDialogSingleChoice, drinkname));



            };
            //Populate lists
            lv.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SelectDialogSingleChoice, quantity));
            tp.SetAdapter(new ArrayAdapter<String>(this, Android.Resource.Layout.SelectDialogSingleChoice, type));
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

        public void ShowAlertAndFinish(string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                Finish();
            });

            //run the alert in UI thread to display in the screen
            RunOnUiThread(() => {
                alert.Show();
            });
        }
        public void AddDrink(string drinktype)
        {
            int resid;

            switch (drinktype)
            {
                case "abszint":
                    resid = Resource.Raw.abszint;
                    break;
                case "bor":
                    resid = Resource.Raw.bor;
                    break;
                case "brandy":
                    resid = Resource.Raw.brandy;
                    break;
                case "ciderradler":
                    resid = Resource.Raw.ciderradler;
                    break;
                case "gin":
                    resid = Resource.Raw.gin;
                    break;
                case "keseru":
                    resid = Resource.Raw.keseru;
                    break;
                case "koktellikor":
                    resid = Resource.Raw.koktellikor;
                    break;
                case "konyak":
                    resid = Resource.Raw.konyak;
                    break;
                case "likor":
                    resid = Resource.Raw.likor;
                    break;
                case "longdrink":
                    resid = Resource.Raw.longdrink;
                    break;
                case "palinka":
                    resid = Resource.Raw.palinka;
                    break;
                default:
                    resid = 0;
                    break;
            }

            Stream beolvasstream = Resources.OpenRawResource(resid);
            StreamReader streamReader = new StreamReader(beolvasstream, true);

            int counter = 1;
            List<string> drinkname = new List<string>();
            List<string> drinkvv = new List<string>();
            string line;
            int linelength;
            int countdrinks;
            try
            {
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    if (counter % 2 == 1)
                    {
                        drinkname.Add(line);
                        counter++;
                    }
                    else
                    {
                        linelength = line.Length;
                        drinkvv.Add(line.Substring(0, linelength - 1));
                        counter++;
                    }

                }

            }
            catch (IOException ex)
            {
                ShowAlert("Hiba!", ex.ToString());
            }
            finally
            {
                streamReader.Close();
                countdrinks = drinkname.Count;
                /////////Add to container

                for (int i = 0; i < countdrinks; ++i)
                {
                    switch (drinktype)
                    {
                        case "abszint":
                            drinks.Add(new Abszint(drinkname[i], drinkvv[i], false, 0));
                            break;
                        case "bor":
                            drinks.Add(new Bor(drinkname[i], drinkvv[i], false, 1));
                            break;
                        case "brandy":
                            drinks.Add(new Brandy(drinkname[i], drinkvv[i], false, 2));
                            break;
                        case "ciderradler":
                            drinks.Add(new Ciderradler(drinkname[i], drinkvv[i], false, 3));
                            break;
                        case "gin":
                            drinks.Add(new Gin(drinkname[i], drinkvv[i], false, 4));
                            break;
                        case "keseru":
                            drinks.Add(new Keserulikor(drinkname[i], drinkvv[i], false, 5));
                            break;
                        case "koktellikor":
                            drinks.Add(new Koktellikor(drinkname[i], drinkvv[i], false, 6));
                            break;
                        case "konyak":
                            drinks.Add(new Konyak(drinkname[i], drinkvv[i], false, 7));
                            break;
                        case "likor":
                            drinks.Add(new Likor(drinkname[i], drinkvv[i], false, 8));
                            break;
                        case "longdrink":
                            drinks.Add(new Longdrink(drinkname[i], drinkvv[i], false, 9));
                            break;
                        case "palinka":
                            drinks.Add(new Palinka(drinkname[i], drinkvv[i], false, 10));
                            break;
                        default:
                            ShowAlert("Hiba!", drinkname + " " + drinkvv);
                            break;
                    }
                }
            }
        }

        private List<string> ReadPartyData(string fname)
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

        private void WriteDrink(string message, string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);
            List<string> filecontent = ReadFromPartyFile(partydate + partyname + "drinks.piapp");
            filecontent.Add(message);
            

            StreamWriter streamWriter = new StreamWriter(filename, true);
            try
            {
                    streamWriter.WriteLine(message);
            }
            catch (IOException e)
            {
                ShowAlert("Hiba!", "A fájlba írás nem sikerült! Hibakód:" + e);
            }
            finally
            {
                streamWriter.Close();
            }





            //Esemény kiírása, activity bezárása
            ShowAlertAndFinish("Kör hozzáadása", "A kör elmentve!");
        }

        private List<string> ReadFromPartyFile(string fname)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path.ToString(), fname);

            List<string> content = new List<string>();
            if (File.Exists(filename))
            {
                StreamReader streamReader = new StreamReader(filename, true);

                try
                {
                    while (!streamReader.EndOfStream)
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
            }
            return content;
        }
    }
}