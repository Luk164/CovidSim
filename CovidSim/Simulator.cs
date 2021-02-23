using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CovidSim.PersonClasses;

namespace CovidSim
{
    public class Simulator
    {
        private List<Person> Citizens { get; set; } = new List<Person>();
        private List<(Person, IList<Person>)> Matches { get; set; } = new List<(Person, IList<Person>)>();

        public Simulator(uint citizenCount, uint medicalStaffCount, uint firstResponderCount, uint militaryCount)
        {
            //Generate people
            for (uint i = 0; i < citizenCount; i++)
            {
                Citizens.Add(PersonFactory.GenerateCitizen());
            }

            //Generate medicalStaff
            for (uint i = 0; i < medicalStaffCount; i++)
            {
                Citizens.Add(PersonFactory.GenerateMedicalStaff());
            }

            //Generate military
            for (uint i = 0; i < militaryCount; i++)
            {
                Citizens.Add(PersonFactory.GenerateMilitary());
            }

            //Generate firstResponder
            for (uint i = 0; i < firstResponderCount; i++)
            {
                Citizens.Add(PersonFactory.GenerateMilitary());
            }
        }

        public void PrepDay()
        {

        }

        public void Matchmaker()
        {
            Parallel.ForEach(Citizens, person =>
            {
                //Stage1

                //TODO Compute basic meeting amount
                var meetingCount = 100;

                //Generate random indexes to select people to meet with
                var random = new Random();
                var randomIndexList = new HashSet<int>();
                while (randomIndexList.Count < meetingCount)
                {
                    randomIndexList.Add(random.Next(0, Citizens.Count));
                }

                //Use the random indexes to select people to meet
                var peopleToMeet = new List<Person>();
                foreach (var index in randomIndexList)
                {
                    peopleToMeet.Add(Citizens[index]);
                }

                //Stage2 Add extra infected to meet for medical staff
                //TODO Upgrade this to reflect total amount of medical personnel and infected patients in hospitals
                var patientsToMeet = 15;
                var infectedCitizens = Citizens.Where(person1 => person1.IsContagious).ToList();

                if (person.CitizenClass == Person.CitizenClassEnum.MedicalStaff)
                {
                    randomIndexList.Clear();
                    while (randomIndexList.Count < patientsToMeet &&
                           randomIndexList.Count < infectedCitizens.Count)
                    {
                        randomIndexList.Add(random.Next(0, infectedCitizens.Count));
                    }

                    foreach (var index in randomIndexList)
                    {
                        peopleToMeet.Add(infectedCitizens[index]);
                    }
                }

                Matches.Add((person, new List<Person>()));
            });
        }
    }
}
