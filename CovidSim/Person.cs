using System;
using System.Collections.Generic;
using System.Text;

namespace CovidSim
{
    public class Person
    {
        public HealthStatus Health { get; set; }

        public void meet(Person otherPerson)
        {

        }

        public bool IsContagious => Health != HealthStatus.Healthy || Health != HealthStatus.Survived || Health != HealthStatus.Deceased;

        public enum HealthStatus
        {
            Healthy,
            Asymptomatic,
            Symptoms,
            Infected,
            Survived,
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
