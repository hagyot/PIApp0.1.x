using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Vermut : Drink
    {
        public Vermut(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Vermut";
            this.custom = custom;
            this.id = id;
        }
    }
}
