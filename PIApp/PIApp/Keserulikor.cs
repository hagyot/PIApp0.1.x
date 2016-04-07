using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Keserulikor : Drink
    {
        public Keserulikor(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Keserű likőr";
            this.custom = custom;
    }
    }
}
