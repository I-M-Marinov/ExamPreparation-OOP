using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars2
{
    public class Planet
    {
        private string name;
        private double budget;
        private List<Weapon> weapons;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            weapons = new List<Weapon>();
        }
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid planet Name");
                }
                name = value;
            }
        }

        public double Budget
        {
            get
            {
                return budget;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Budget cannot drop below Zero!");
                }
                budget = value;
            }
        }

        public IReadOnlyCollection<Weapon> Weapons => weapons;

        public double MilitaryPowerRatio => weapons.Sum(d => d.DestructionLevel);

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void SpendFunds(double amount)
        {
            if (amount > Budget)
            {
                throw new InvalidOperationException($"Not enough funds to finalize the deal.");
            }
            Budget -= amount;
        }

        public void AddWeapon(Weapon weapon)
        {
            if (Weapons.Any(x => x.Name == weapon.Name))
            {
                throw new InvalidOperationException($"There is already a {weapon.Name} weapon.");
            }
            weapons.Add(weapon);
        }

        public void RemoveWeapon(string weaponName)
        {
            Weapon weapon = weapons.FirstOrDefault(x => x.Name == weaponName);
            weapons.Remove(weapon);
        }

        public void UpgradeWeapon(string weaponName)
        {
            if (!Weapons.Any(x => x.Name == weaponName))
            {
                throw new InvalidOperationException($"{weaponName} does not exist in the weapon repository of {Name}");
            }

            else
            {
                Weapon weapon = Weapons.FirstOrDefault(x => x.Name == weaponName);
                weapon.IncreaseDestructionLevel();
            }
        }

        public string DestructOpponent(Planet opponent)
        {
            if (opponent.MilitaryPowerRatio >= MilitaryPowerRatio)
            {
                throw new InvalidOperationException($"{opponent.Name} is too strong to declare war to!");
            }

            return $"{opponent.Name} is destructed!";
        }
    }
}
