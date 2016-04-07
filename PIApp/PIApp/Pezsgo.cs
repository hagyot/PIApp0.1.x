using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Pezsgo : Drink
    {
        public Pezsgo(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Pezsgő";
            this.custom = custom;
    }
    }
}
