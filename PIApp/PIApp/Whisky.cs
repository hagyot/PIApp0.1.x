using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Whisky : Drink
    {
        public Whisky(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Whisky";
            this.custom = custom;
    }
    }
}
