using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    public class Person
    {

        //Declare fields
        private String firstname;
        private String lastname;
        private Single height;
        private Double weight;
        private String homeaddress;
        private String ice;
        private String gender;
        private DateTime birthday; //year, month, day
        private String password;

        //Define constructor
        public Person(String firstname, String lastname, Single height, Double weight, String homeaddress, String ice, String gender, DateTime birthday, String password)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.height = height;
            this.weight = weight;
            this.homeaddress = homeaddress;
            this.ice = ice;
            this.gender = gender;
            this.birthday = birthday;
            this.password = password;
        }

        //Define getters
        public String getFirstName()
        {
            return firstname;
        }

        public String getPassword()
        {
            return password;
        }

        public String getLastName()
        {
            return lastname;
        }

        public Single getHeight()
        {
            return height;
        }

        public Double getWeight()
        {
            return weight;
        }

        public String getHomeAddress()
        {
            return homeaddress;
        }

        public String getIce()
        {
            return ice;
        }

        public String getGender()
        {
            return gender;
        }

        public DateTime getBirthday()
        {
            return birthday;
        }


    }
}
