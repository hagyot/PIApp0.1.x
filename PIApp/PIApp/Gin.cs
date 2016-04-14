using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Gin : Drink
    {
        public Gin(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Gin";
            this.custom = custom;
            this.id = id;
        }
    }
}
