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
        private ushort _day;
        private List<Person> Citizens { get; } = new List<Person>();
        private List<(Person, IList<Person>)> Matches { get; } = new List<(Person, IList<Person>)>();
        public ushort PatientMaxCount { get; set; } = 15;

        public Simulator(uint citizenCount, uint medicalStaffCount, uint firstResponderCount, uint militaryCount, uint infectedCitizenCount)
        {
            //Generate people
            for (uint i = 0; i < citizenCount; i++)
            {
                Citizens.Add(PersonFactory.GenerateCitizen());
            }

            //Generate people
            for (uint i = 0; i < infectedCitizenCount; i++)
            {
                Citizens.Add(PersonFactory.GenerateCitizen(Person.HealthStatusEnum.Symptoms));
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

        public void Day()
        {
            Matchmaker();

            ExecuteAllMeetingsParallel();

            //Run end of day checks
            Parallel.ForEach(Citizens, person =>
            {
                person.EndOfDay();
            });

            Console.WriteLine($"End of day {_day++}");
            Console.WriteLine($"Healthy count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Healthy)}");
            Console.WriteLine($"Asymptomatic count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Asymptomatic)}");
            Console.WriteLine($"Symptoms count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Symptoms)}");
            Console.WriteLine($"Seriously ill count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.SeriouslyIll)}");
            Console.WriteLine($"Immune count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Immune)}");
            Console.WriteLine($"Dead count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Deceased)}");
            Console.WriteLine("#############################################################################################");
        }

        private void ExecuteAllMeetingsParallel()
        {
            Parallel.ForEach(Matches, match =>
            {
                var (person, peopleToMeet) = match;
                foreach (var otherPerson in peopleToMeet)
                {
                    person.Meet(otherPerson);
                }
            });
        }

        public void Matchmaker()
        {
            Parallel.ForEach(Citizens, person =>
            {
                //Generate random indexes to select people to meet with
                var random = new Random();
                var randomIndexList = new HashSet<int>();
                var peopleToMeet = new List<Person>();

                //Stage1
                RandomMeetings(person, random, randomIndexList, peopleToMeet);
                randomIndexList.Clear();
                

                //Stage2 Add extra infected to meet for medical staff

                if (person.CitizenClass == Person.CitizenClassEnum.MedicalStaff)
                {
                    PatientMeetings(person, random, randomIndexList, peopleToMeet);
                }
            });
        }

        public void RandomMeetings(Person person, Random random, HashSet<int> randomIndexList, List<Person> peopleToMeet)
        {
            //TODO Compute basic meeting amount
            var meetingCount = 100;

            while (randomIndexList.Count < meetingCount)
            {
                randomIndexList.Add(random.Next(0, Citizens.Count));
            }

            //Use the random indexes to select people to meet
            
            foreach (var index in randomIndexList)
            {
                peopleToMeet.Add(Citizens[index]);
            }
        }

        public void PatientMeetings(Person person, Random random, HashSet<int> randomIndexList,
            List<Person> peopleToMeet)
        {
            //TODO Upgrade this to reflect total amount of medical personnel and infected patients in hospitals
            var infectedCitizens = Citizens.Where(p => p.IsContagious).ToList();
            while (randomIndexList.Count < PatientMaxCount &&
                   randomIndexList.Count < infectedCitizens.Count)
            {
                randomIndexList.Add(random.Next(0, infectedCitizens.Count));
            }

            foreach (var index in randomIndexList)
            {
                peopleToMeet.Add(infectedCitizens[index]);
            }
        }
    }
}
