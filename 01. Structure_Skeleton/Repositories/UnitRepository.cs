using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {

        private readonly List<IMilitaryUnit> units;

        public UnitRepository()
        {
            units = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models  => units;

        public void AddItem(IMilitaryUnit model)
        {
            units.Add(model);
        }
        public IMilitaryUnit FindByName(string unitTypeName)
        {
            return units.FirstOrDefault(w => w.GetType().Name.Equals(unitTypeName));
        }
        public bool RemoveItem(string unitTypeName)
        {
            var unitToRemove = FindByName(unitTypeName);

            if (unitToRemove != null)
            {
                units.Remove(unitToRemove);
                return true;
            }
            return false;
        }
    }
}
