using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CovidSim
{
    public class Person
    {
        public HealthStatus Health { get; set; }
        public List<IGear> Gear { get; set; }

        public Person()
        {

        }

        public void meet(Person otherPerson)
        {
            if (IsContagious == otherPerson.IsContagious)
            {
                //Both healthy or already infected, nothing to do here
                return;
            }
            else
            {
                if (IsContagious)
                {
                    //Infecting other person

                    var randomGenerator = new Random();

                    var roll = randomGenerator.Next(0, 100);
                    // var protection = 
                }
            }
        }

        public bool IsContagious
        {
            get
            {
                switch (Health)
                {
                    case HealthStatus.Asymptomatic:
                    case HealthStatus.Symptoms:
                    case HealthStatus.Infected:
                        return true;
                    case HealthStatus.Healthy:
                    case HealthStatus.Survived:
                    case HealthStatus.VaccineSecondDose:
                    case HealthStatus.Deceased:
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public double Protection => Gear.Aggregate(100.0, (current, gear) => current - current * gear.ProtectionModifier.Value / 100);
        public double Prevention => Gear.Aggregate(100.0, (current, gear) => current - current * gear.PreventionModifier.Value / 100);

        public enum HealthStatus
        {
            Healthy,
            Asymptomatic,
            Symptoms,
            Infected,
            Survived,
            VaccineSecondDose,
            Deceased
        }

        public enum QuarantineStatus
        {
            Normal,
            HomeQuarantine,
            Hospital
        }

        public enum CitizenClass
        {
            Citizen,
            MedicalStaff,
            Military,
            FirstResponder
        }
    }
}
