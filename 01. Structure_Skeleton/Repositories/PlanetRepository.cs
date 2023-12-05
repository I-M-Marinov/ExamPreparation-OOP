namespace PlanetWars.Repositories
{
    using System.Linq;
    using Contracts;
    using PlanetWars.Models.Planets.Contracts;
    using System.Collections.Generic;

    internal class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planets.AsReadOnly();

        public void AddItem(IPlanet model) => planets.Add(model);

        public IPlanet FindByName(string name)
            => planets.FirstOrDefault(p => p.Name == name);

        public bool RemoveItem(string name)
        {
            IPlanet planet = FindByName(name);
            return planets.Remove(planet);
        }
    }
}
