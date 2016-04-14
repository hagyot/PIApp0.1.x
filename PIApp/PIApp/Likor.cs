using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Likor : Drink
    {
        public Likor(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Likőr";
            this.custom = custom;
            this.id = id;
        }
    }
}
