using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Party
    {

        //Declare fields
        private List<Round> rounds = new List<Round>();
        private string partyname;
        private DateTime start;
        private DateTime end;


        public Party(string partyname, DateTime start)
        {
            this.partyname = partyname;
            this.start = start;
        }

        public void addRound(Round r)
        {
            rounds.Add(r);
        }

        public int getRounds()
        {
            return rounds.Count;
        }

        public DateTime getStart()
        {
            return start;
        }

        public DateTime getEnd()
        {
            return end;
        }

        public void setEnd(DateTime d)
        {
            end = d;
        }
    }
}
