using System;
using PlanetWars.Models.Weapons.Contracts;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        protected Weapon(int destructionLevel, double price)
        {
            DestructionLevel = destructionLevel;
            Price = price;
        }

        private int destructionLevel;
        public double Price { get; private set; }

        public int DestructionLevel
        {
            get => destructionLevel;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Destruction level cannot be zero or negative.");
                }
                if (value > 10)
                {
                    throw new ArgumentException("Destruction level cannot exceed 10 power points.");
                }
                destructionLevel = value;
            }
        }
    }
}
