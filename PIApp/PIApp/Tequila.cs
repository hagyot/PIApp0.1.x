using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Tequila : Drink
    {
        public Tequila(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Tequila";
            this.custom = custom;
            this.id = id;
        }
    }
}
