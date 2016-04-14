using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Konyak : Drink
    {
        public Konyak(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Konyak";
            this.custom = custom;
            this.id = id;
        }
    }
}
