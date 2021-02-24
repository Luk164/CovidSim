using System;
using System.Collections.Generic;
using System.Linq;
using CovidSim.GearClasses;

namespace CovidSim.PersonClasses
{
    public class Person
    {
        public HealthStatusEnum Health { get; set; }
        public QuarantineStatusEnum QuarantineStatus = QuarantineStatusEnum.Normal;
        public CitizenClassEnum CitizenClass = CitizenClassEnum.Citizen;
        public List<IGear> Gear { get; set; } = new List<IGear>();
        private readonly Random _randomGenerator = new Random();
        public PersonOptions Options { get; set; }

        public Person(PersonOptions options = null)
        {
            Options = options ?? new PersonOptions();
        }

        /// <summary>
        /// Process a meeting between two people
        /// </summary>
        /// <param name="otherPerson">The person to be met with</param>
        public void Meet(Person otherPerson)
        {
            if (IsContagious == otherPerson.IsContagious)
            {
                //Both healthy or already infected, nothing to do here
                return;
            }

            if (IsContagious)
            {
                //Infecting other person

                var roll = _randomGenerator.Next(0, 100);
                if (roll > Prevention)
                {
                    roll = _randomGenerator.Next(0, 100);
                    if (roll > otherPerson.Protection)
                    {
                        //Other person got infected
                        otherPerson.Health = HealthStatusEnum.Asymptomatic;
                    }
                }
            }
            else
            {
                //Other person infection this one

                var roll = _randomGenerator.Next(0, 100);
                if (roll > otherPerson.Prevention)
                {
                    roll = _randomGenerator.Next(0, 100);

                    if (roll > Protection)
                    {
                        //This person got infected
                        Health = HealthStatusEnum.Asymptomatic;
                    }
                }
            }
        }

        public void EndOfDay()
        {
            if (IsContagious)
            {
                var roll = _randomGenerator.Next(0, 100);

                if (Health == HealthStatusEnum.Asymptomatic)
                {
                    if (Options.SymptomCountdown > 0)
                    {
                        //Symptoms have yet to show
                        Options.SymptomCountdown--;
                    }
                    else
                    {
                        //Chance to stay asymptomatic
                        if (roll > Options.AsymptomaticProbability.Value)
                        {
                            //Symptoms show
                            Health = HealthStatusEnum.Symptoms;

                            //Roll for quarantine compliance
                            roll = _randomGenerator.Next(0, 100);
                            if (roll > Options.QuarantineCompliance.Value)
                            {
                                //Person stays at home
                                QuarantineStatus = QuarantineStatusEnum.HomeQuarantine;
                            }
                        }
                        else
                        {
                            //If after 5 days no symptoms patient becomes healthy again
                            Health = HealthStatusEnum.Immune;
                        }
                    }
                }
                else if (Health == HealthStatusEnum.Symptoms)
                {
                    if (Options.CureCountdown > 0)
                    {
                        Options.CureCountdown--;

                        if (roll < Options.EscalatedSymptoms.Value)
                        {
                            //Symptoms escalate for 20% of cases
                            Health = HealthStatusEnum.SeriouslyIll;

                            //Roll for hospital visit
                            roll = _randomGenerator.Next(0, 100);
                            if (roll > Options.QuarantineCompliance.Value)
                            {
                                //Person goes to hospital
                                QuarantineStatus = QuarantineStatusEnum.Hospital;
                            }
                            else
                            {
                                //At least stay home
                                roll = _randomGenerator.Next(0, 100);
                                if (roll > Options.QuarantineCompliance.Value)
                                {
                                    //Person stays at home
                                    QuarantineStatus = QuarantineStatusEnum.HomeQuarantine;
                                }
                            }
                        }
                    }
                    else
                    {
                        Health = HealthStatusEnum.Immune;
                        QuarantineStatus = QuarantineStatusEnum.Normal;
                    }
                }
                else if (Health == HealthStatusEnum.SeriouslyIll)
                {
                    if (Options.CureCountdown > 0)
                    {
                        Options.CureCountdown--;

                        if (roll > Options.DeathRate.Value)
                        {
                            //Patient died
                            Health = HealthStatusEnum.Deceased;
                            QuarantineStatus = QuarantineStatusEnum.Dead;
                        }
                    }
                }
            }
        }

        public bool IsContagious
        {
            get
            {
                switch (Health)
                {
                    case HealthStatusEnum.Asymptomatic:
                    case HealthStatusEnum.Symptoms:
                    case HealthStatusEnum.SeriouslyIll:
                        return true;
                    case HealthStatusEnum.Healthy:
                    case HealthStatusEnum.Immune:
                    case HealthStatusEnum.Deceased:
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        //TODO: Check calculations in unit tests
        public double Protection => 100 - Gear.Aggregate(100.0, (current, gear) => current - current * gear.ProtectionModifier.Value / 100);
        public double Prevention => 100 - Gear.Aggregate(100.0, (current, gear) => current - current * gear.PreventionModifier.Value / 100);

        public enum HealthStatusEnum
        {
            Healthy,
            Asymptomatic,
            Symptoms,
            SeriouslyIll,
            Immune,
            Deceased
        }

        public enum QuarantineStatusEnum
        {
            Normal,
            HomeQuarantine,
            Hospital,
            Dead
        }

        public enum CitizenClassEnum
        {
            Citizen,
            MedicalStaff,
            Military,
            FirstResponder
        }
    }
}
