using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Keserulikor : Drink
    {
        public Keserulikor(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Keserű likőr";
            this.custom = custom;
            this.id = id;
        }
    }
}
