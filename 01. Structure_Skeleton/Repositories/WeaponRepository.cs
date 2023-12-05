using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> models;

        public WeaponRepository()
        {
            models = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => models;

        public void AddItem(IWeapon model)
        {
            models.Add(model);
        }
        public IWeapon FindByName(string weaponTypeName)
        {
          return models.FirstOrDefault(w => w.GetType().Name.Equals(weaponTypeName));
        }
        public bool RemoveItem(string weaponTypeName)
        {
            var weaponToRemove = FindByName(weaponTypeName);

            if (weaponToRemove != null)
            {
                models.Remove(weaponToRemove);
                return true;
            }
            return false;
        }
    }
}
