using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Rum : Drink
    {
        public Rum(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Rum";
            this.custom = custom;
            this.id = id;
        }
    }
}
