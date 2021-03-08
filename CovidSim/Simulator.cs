using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CovidSim.PersonClasses;

namespace CovidSim
{
    /// <summary>
    /// Primary simulator class
    /// </summary>
    public class Simulator
    {
        private ushort _day;
        private List<Person> Citizens { get; } = new List<Person>();
        private List<MeetList> Matches { get; } = new List<MeetList>();

        /// <summary>
        /// Max amount of patients for one member of medical staff to handle
        /// </summary>
        public ushort PatientMaxCount { get; set; } = 15;

        /// <summary>
        /// Meeting count for a single citizen/day
        /// </summary>
        public ushort MeetingCount { get; set; } = 20;

        /// <summary>
        /// Primary simulator class constructor
        /// </summary>
        /// <param name="citizenCount">Ordinary citizen count</param>
        /// <param name="medicalStaffCount">Medical staff count</param>
        /// <param name="firstResponderCount">First responder count</param>
        /// <param name="militaryCount">Military personnel count</param>
        /// <param name="infectedCitizenCount">Infected citizen count</param>
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

            Console.WriteLine($"Starting state {_day++}");
            Console.WriteLine($"Healthy count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Healthy)}");
            Console.WriteLine($"Asymptomatic count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Asymptomatic)}");
            Console.WriteLine($"Symptoms count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Symptoms)}");
            Console.WriteLine($"Seriously ill count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.SeriouslyIll)}");
            Console.WriteLine($"Immune count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Immune)}");
            Console.WriteLine($"Dead count: {Citizens.Count(person => person.Health == Person.HealthStatusEnum.Deceased)}");
            Console.WriteLine("#############################################################################################");
        }

        /// <summary>
        /// Simulate one day
        /// </summary>
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

        /// <summary>
        /// Executes meetings between person classes defined in Matches list
        /// </summary>
        private void ExecuteAllMeetingsParallel()
        {
            Parallel.ForEach(Matches, match =>
            {
                foreach (var otherPerson in match.PeopleToMeet)
                {
                    match.Person.Meet(otherPerson);
                }
            });
        }

        /// <summary>
        /// Generate meetings for Matches list
        /// </summary>
        public void Matchmaker()
        {
            Parallel.ForEach(Citizens, person =>
            {
                //Generate random indexes to select people to meet with
                var random = new Random();
                var randomIndexList = new HashSet<int>();
                var peopleToMeet = new List<Person>();

                //Stage1
                RandomMeetings(person, randomIndexList, peopleToMeet, random);
                randomIndexList.Clear();
                

                //Stage2 Add extra infected to meet for medical staff

                // if (person.CitizenClass == Person.CitizenClassEnum.MedicalStaff)
                // {
                //     PatientMeetings(person, random, randomIndexList, peopleToMeet);
                // }

                Matches.Add(new MeetList{Person = person, PeopleToMeet = peopleToMeet.ToList()});
            });
        }

        /// <summary>
        /// Generate random meetings
        /// </summary>
        /// <param name="person">Person for whom these meetings are generated</param>
        /// <param name="randomIndexList">Index list of people to meet</param>
        /// <param name="peopleToMeet">List of people to meet to which meetings are added</param>
        /// <param name="random">Random generator to increase efficiency</param>
        public void RandomMeetings(Person person, HashSet<int> randomIndexList, List<Person> peopleToMeet,
            Random random = null)
        {
            random ??= new Random();

            //TODO Compute basic meeting amount

            while (randomIndexList.Count < MeetingCount)
            {
                randomIndexList.Add(random.Next(0, Citizens.Count));
            }

            //Use the random indexes to select people to meet
            
            foreach (var index in randomIndexList)
            {
                peopleToMeet.Add(Citizens[index]);
            }
        }

        /// <summary>
        /// Generate patient-doctor meetings
        /// </summary>
        /// <param name="person">Person for whom these meetings are generated. Should be a member of medical-staff</param>
        /// <param name="randomIndexList">Index list of people to meet</param>
        /// <param name="peopleToMeet">List of people to meet to which meetings are added</param>
        /// <param name="random">Random generator to increase efficiency</param>
        public void PatientMeetings(Person person, HashSet<int> randomIndexList,
            List<Person> peopleToMeet, Random random = null)
        {
            random ??= new Random();

            if (person.CitizenClass != Person.CitizenClassEnum.MedicalStaff)
            {
                throw new WarningException("Warning! Doctor-patient meeting generated for non-medical staff!");
            }

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
