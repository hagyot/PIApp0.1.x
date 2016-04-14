using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Palinka : Drink
    {
        public Palinka(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Pálinka";
            this.custom = custom;
            this.id = id;
        }
    }
}
