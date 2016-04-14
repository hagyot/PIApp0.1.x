using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Longdrink : Drink
    {
        public Longdrink(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Longdrink";
            this.custom = custom;
            this.id = id;
        }
    }
}
