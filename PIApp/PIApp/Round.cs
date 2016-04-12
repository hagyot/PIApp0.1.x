using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Round
    {
        //Declare fields

        private DateTime drinktime;
        private Drink drink;
        private double quantity; //cl

        public Round(DateTime drinktime, Drink drink, double quantity)
        {
            this.drinktime = drinktime;
            this.drink = drink;
            this.quantity = quantity;
        }

        public DateTime getDrinktime()
        {
            return drinktime;
        }

        public Drink getDrink()
        {
            return drink;
        }
        
        public double getQuantity()
        {
            return quantity;
        }
    }
}
