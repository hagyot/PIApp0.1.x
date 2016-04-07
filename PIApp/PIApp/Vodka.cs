using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Vodka : Drink
    {
        public Vodka(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Vodka";
            this.custom = custom;
    }
    }
}
