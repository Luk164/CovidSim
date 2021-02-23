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
        private IList<Person> _citizens = new List<Person>();

        public Simulator(uint citizenCount, uint medicalStaffCount, uint firstResponderCount, uint militaryCount)
        {
            //Generate people
            for (uint i = 0; i < citizenCount; i++)
            {
                _citizens.Add(PersonFactory.GenerateCitizen());
            }

            //Generate medicalStaff
            for (uint i = 0; i < medicalStaffCount; i++)
            {
                _citizens.Add(PersonFactory.GenerateMedicalStaff());
            }

            //Generate military
            for (uint i = 0; i < militaryCount; i++)
            {
                _citizens.Add(PersonFactory.GenerateMilitary());
            }
        }

        public void Prep()
        {

        }
    }
}
