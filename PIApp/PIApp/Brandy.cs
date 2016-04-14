using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Brandy : Drink
    {
        public Brandy(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Brandy";
            this.custom = custom;
            this.id = id;
        }
    }
}
