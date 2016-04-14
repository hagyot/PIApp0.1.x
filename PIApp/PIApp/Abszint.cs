using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Abszint : Drink
    {
        public Abszint(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Abszint";
            this.custom = custom;
            this.id = id;
    }
    }
}
