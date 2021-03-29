using System.Collections.Generic;
using CovidSim.PersonClasses;

namespace CovidSim
{
    public class MeetList
    {
        public Person Person { get; set; }
        public List<Person> PeopleToMeet { get; set; } = new();
    }
}