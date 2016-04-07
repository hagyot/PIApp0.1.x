using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Bor : Drink
    {
        public Bor(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Bor";
            this.custom = custom;
    }
    }
}
