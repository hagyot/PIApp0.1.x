using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Bor : Drink
    {
        public Bor(string name, string vvpercent, bool custom, int id)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Bor";
            this.custom = custom;
            this.id = id;
        }
    }
}
