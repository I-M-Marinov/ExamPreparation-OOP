namespace PlanetWars.Models.Planets
{

    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Repositories;
    using Weapons;
    using Utilities.Messages;
    using MilitaryUnits;
    using Contracts;
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Models.MilitaryUnits.Contracts;

    public class Planet : IPlanet
    {
        private WeaponRepository weapons;
        private UnitRepository units;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            weapons = new WeaponRepository();
            units = new UnitRepository();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }

                budget = value;
            }
        }

        public double MilitaryPower => CalculateTotalAmountOfMilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit) => units.AddItem(unit);

        public void AddWeapon(IWeapon weapon) => weapons.AddItem(weapon);

        public string PlanetInfo()
        {
            StringBuilder planetInfo = new StringBuilder();

            string army = !Army.Any() ? "No units"
                                      : string.Join(", ", Army.Select(u => u.GetType().Name));

            string weapons = !Weapons.Any() ? "No weapons"
                                      : string.Join(", ", Weapons.Select(u => u.GetType().Name));

            planetInfo.AppendLine($"Planet: {Name}")
                      .AppendLine($"--Budget: {Budget} billion QUID")
                      .AppendLine($"--Forces: {army}")
                      .AppendLine($"--Combat equipment: {weapons}")
                      .AppendLine($"--Military Power: {MilitaryPower}");

            return planetInfo.ToString().TrimEnd();

        }

        public void Profit(double amount) => Budget += amount;

        public void Spend(double amount)
        {
            if (Budget - amount < 0)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (IMilitaryUnit unit in Army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double CalculateTotalAmountOfMilitaryPower()
        {
            double totalAmount = Army.Sum(u => u.EnduranceLevel) + Weapons.Sum(w => w.DestructionLevel);

            if (Army.Any(w => w.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalAmount *= 1.3;
            }

            if (Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                totalAmount *= 1.45;
            }

            return Math.Round(totalAmount, 3);
        }
    }
}