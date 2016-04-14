using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Sor : Drink
    {
        public Sor(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Sör";
            this.custom = custom;
            this.id = id;
        }
    }
}
