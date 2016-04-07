using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Likor : Drink
    {
        public Likor(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Likőr";
            this.custom = custom;
    }
    }
}
