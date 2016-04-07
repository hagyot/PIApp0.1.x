using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Vermut : Drink
    {
        public Vermut(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Vermut";
            this.custom = custom;
    }
    }
}
