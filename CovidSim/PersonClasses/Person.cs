using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CovidSim.GearClasses;

namespace CovidSim.PersonClasses
{
    /// <summary>
    /// Person class
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Health status
        /// </summary>
        public HealthStatusEnum Health { get; set; }

        /// <summary>
        /// Quarantine status
        /// </summary>
        public QuarantineStatusEnum QuarantineStatus = QuarantineStatusEnum.Normal;

        /// <summary>
        /// Citizen classification
        /// </summary>
        public CitizenClassEnum CitizenClass = CitizenClassEnum.Citizen;

        /// <summary>
        /// Gear the person has available
        /// </summary>
        public List<IGear> Gear { get; set; } = new List<IGear>();
        private readonly Random _randomGenerator = new Random();
        public PersonOptions Options { get; set; }

        public ushort MeetingCount { get; private set; }

        public Person(PersonOptions options = null, bool immunoCompromised = false)
        {
            Options = options ?? new PersonOptions();

            if (!immunoCompromised)
            {
                Gear.Add(new Immunity());
            }
        }

        /// <summary>
        /// Process a meeting between two people
        /// </summary>
        /// <param name="otherPerson">The person to be met with</param>
        public void Meet(Person otherPerson)
        {
            MeetingCount++;
            if (IsContagious == otherPerson.IsContagious)
            {
                //Both healthy or already infected, nothing to do here
                return;
            }

            var quarantineRoll = _randomGenerator.Next(0, 100);

            if (Health == HealthStatusEnum.Symptoms || Health == HealthStatusEnum.SeriouslyIll)
            {
                if (quarantineRoll < Options.QuarantineCompliance.Value)
                {
                    //This person is infected and decided to stay home
                    return;
                }
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
                        // Debug.WriteLine("Disease transmitted!");
                        otherPerson.Health = HealthStatusEnum.Asymptomatic;
                    }
                    // else
                    // {
                    //     Debug.WriteLine("Transmission dodged!");
                    // }
                }
            }
            else
            {
                //Other person infecting this one

                var roll = _randomGenerator.Next(0, 100);
                if (roll > otherPerson.Prevention)
                {
                    roll = _randomGenerator.Next(0, 100);

                    if (roll > Protection)
                    {
                        //This person got infected
                        // Debug.WriteLine("Disease received!");
                        Health = HealthStatusEnum.Asymptomatic;
                    }
                }
                else
                {
                    // Debug.WriteLine("Transmission dodged!");
                }
            }
        }

        /// <summary>
        /// Process person health status at the end of the day
        /// </summary>
        public void EndOfDay()
        {
            // Debug.WriteLine($"This person met: {MeetingCount} people and is {Health}");
            MeetingCount = 0;

            if (IsContagious)
            {
                var roll = _randomGenerator.Next(0, 100);

                switch (Health)
                {
                    case HealthStatusEnum.Asymptomatic when Options.SymptomCountdown > 0:
                        //Symptoms have yet to show
                        Options.SymptomCountdown--;
                        break;
                    //Chance to stay asymptomatic
                    case HealthStatusEnum.Asymptomatic when roll > Options.AsymptomaticProbability.Value:
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

                        break;
                    }
                    case HealthStatusEnum.Asymptomatic:
                        //If after 5 days no symptoms patient becomes healthy again and gains immunity
                        Health = HealthStatusEnum.Immune;
                        break;
                    case HealthStatusEnum.Symptoms when Options.CureCountdown > 0:
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

                        break;
                    }
                    case HealthStatusEnum.Symptoms:
                        Health = HealthStatusEnum.Immune;
                        QuarantineStatus = QuarantineStatusEnum.Normal;
                        break;
                    case HealthStatusEnum.SeriouslyIll:
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

                        break;
                    }
                    case HealthStatusEnum.Healthy:
                    case HealthStatusEnum.Immune:
                    case HealthStatusEnum.Deceased:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Health), "Health enumerator is out of range!");
                }
            }
        }

        /// <summary>
        /// Get person contagiousness status
        /// </summary>
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

        //TODO: Check for better formula
        /// <summary>
        /// Protection calculation based on persons gear including immunity. Based on diminishing returns principle
        /// </summary>
        public double Protection => 100 - Gear.Aggregate(100.0, (current, gear) => current - current * gear.ProtectionModifier.Value / 100);

        /// <summary>
        /// Prevention calculation based on persons gear. Based on diminishing returns principle
        /// </summary>
        public double Prevention => 100 - Gear.Aggregate(100.0, (current, gear) => current - current * gear.PreventionModifier.Value / 100);

        /// <summary>
        /// Health status enumerator
        /// </summary>
        public enum HealthStatusEnum
        {
            Healthy,
            Asymptomatic,
            Symptoms,
            SeriouslyIll,
            Immune,
            Deceased
        }

        /// <summary>
        /// Quarantine status enumerator
        /// </summary>
        public enum QuarantineStatusEnum
        {
            Normal,
            HomeQuarantine,
            Hospital,
            Dead
        }

        /// <summary>
        /// Citizen class enumerator
        /// </summary>
        public enum CitizenClassEnum
        {
            Citizen,
            MedicalStaff,
            Military,
            FirstResponder
        }
    }
}
