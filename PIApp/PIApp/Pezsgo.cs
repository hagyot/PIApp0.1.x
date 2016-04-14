using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Pezsgo : Drink
    {
        public Pezsgo(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Pezsgő";
            this.custom = custom;
            this.id = id;
        }
    }
}
