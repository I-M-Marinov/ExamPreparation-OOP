using System;
using PlanetWars.Models.MilitaryUnits.Contracts;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private const int initialEnduranceLevel = 1;
        private int enduranceLevel = initialEnduranceLevel;

        protected MilitaryUnit(double cost)
        {
            Cost = cost;
        }

        public double Cost { get; private set; }

        public int EnduranceLevel
        {
            get => enduranceLevel;
            private set => enduranceLevel = value;
        }


        public void IncreaseEndurance()
        {
            EnduranceLevel += 1;
            if (EnduranceLevel > 20)
            {
                EnduranceLevel = 20;
                throw new ArgumentException("Endurance level cannot exceed 20 power points.");
            }
        }
    }
}
