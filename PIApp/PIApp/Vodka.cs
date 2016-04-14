using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Vodka : Drink
    {
        public Vodka(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Vodka";
            this.custom = custom;
            this.id = id;
        }
    }
}
