using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Sor : Drink
    {
        public Sor(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Sör";
            this.custom = custom;
    }
    }
}
