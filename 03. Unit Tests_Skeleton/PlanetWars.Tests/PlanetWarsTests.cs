using NUnit.Framework;
using PlanetWars2;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PlanetWars2.Tests
{
    public class Tests
    {
        public class PlanetWarsTests
        {
            private Weapon weapon;
            private Weapon weapon2;
            private Planet planet;
            private IReadOnlyList<Weapon> weapons;

            [SetUp]
            public void Setup()
            {
                weapon = new Weapon("Laser", 120.00, 20);
                weapon2 = new Weapon("Missile", 330.00, 40);
                planet = new Planet("Venus", 200);
            }

            [Test]

            public void CheckIfWeaponsConstructorWorksProperly()
            {
                Assert.AreEqual("Laser", weapon.Name);
                Assert.AreEqual(120.00, weapon.Price);
                Assert.AreEqual(20, weapon.DestructionLevel);
            }

            [TestCase(-1)]
            [TestCase(-200)]

            public void CheckIfWeaponPriceIsANegativeNumberThrowsAnException(int price)
            {
                Assert.Throws<ArgumentException>(() => new Weapon("Rifle", price, 10));
            }

            [Test]

            public void CheckIfWeaponIncreasesDestructionLevel()
            {
                weapon.IncreaseDestructionLevel();
                Assert.AreEqual(21,weapon.DestructionLevel);
            }

            [TestCase(10)]
            [TestCase(20)]

            public void CheckIfBoolNuclearIsUpdatedProperly(int destructionLevel)
            {
                weapon.DestructionLevel = destructionLevel;
                Assert.IsTrue(weapon.IsNuclear);
            }

            [TestCase(9)]
            [TestCase(0)]
            public void CheckIfBoolNuclearIsFalseWhenDestructionUnder10(int destructionLevel)
            {
                weapon.DestructionLevel = destructionLevel;
                Assert.IsFalse(weapon.IsNuclear);
            }

            [Test]

            public void CheckIfPlanetsConstructorIsWorkingProperly()
            {
                Assert.AreEqual("Venus",planet.Name);
                Assert.AreEqual(200,planet.Budget);

                planet.AddWeapon(weapon);
                Assert.AreEqual(1,planet.Weapons.Count);
            }

            [TestCase("")]
            [TestCase(null)]

            public void CheckIfNameIsNullOrEmptyThrowsAnException(string planetName)
            {
                Assert.Throws<ArgumentException>(() => new Planet(planetName, 20));
            }

            [TestCase(-5)]
            [TestCase(-200)]

            public void CheckIfBudgetIsBelowZeroIt_ThrowsAnException(int budget)
            {
                Assert.Throws<ArgumentException>(() => new Planet("Uranus", budget));
            }

            [Test]

            public void CheckIfPowerRatioIsCalculatingProperly()
            {
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                Assert.AreEqual(60,planet.MilitaryPowerRatio);
            }

            [Test]

            public void CheckIfProfitMethodIsWorkingProperly()
            {
                planet.Profit(50);
                Assert.AreEqual(250,planet.Budget);
            }

            [Test]

            public void CheckIfSpendFundsMethod_Works_Correctly()
            {
                planet.SpendFunds(25);
                Assert.AreEqual(175, planet.Budget);
            }

            [Test]

            public void Check_IfSpendFundsMethodThrowsWhenBudgetIsNotEnough()
            {
                Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(210));
            }

            [Test]

            public void Check_IfAddWeaponMethodThrowsWhenWeaponAlreadyExists()
            {
                planet.AddWeapon(weapon);
                Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon));
            }


            [Test]

            public void Check_IfAddWeaponMethodWorksCorrectly()
            {
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                Assert.AreEqual(2, planet.Weapons.Count);
            }

            [Test]

            public void CheckIf_RemoveWeapon_RemovesAWeaponFromTheCollectionSuccessfully()
            {
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                planet.RemoveWeapon(weapon.Name);

                Assert.AreEqual(1, planet.Weapons.Count);
            }

            [Test]

            public void CheckIfUpgradeWeapon_WorksProperly()
            {
                planet.AddWeapon(weapon);
                planet.UpgradeWeapon(weapon.Name);

                Assert.AreEqual(21, weapon.DestructionLevel);
            }

            [Test]

            public void CheckIfUpgradeWeaponThrowsIfWeaponNotFound()
            {
                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("Prashka"));
            }

            [Test]

            public void CheckIf_DestructOpponent_WorksProperly()
            {
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                Planet planet2 = new Planet("Uranus", 100);
                Assert.AreEqual("Uranus is destructed!", planet.DestructOpponent(planet2));
            }

            [Test]

            public void CheckIf_DestructOpponent_Throws_IfOpponentIsTooStrong()
            {
                Planet planet2 = new Planet("Uranus", 100);
                planet2.AddWeapon(weapon);
                planet2.AddWeapon(weapon2);

                Assert.Throws<InvalidOperationException>(() => planet.DestructOpponent(planet2));
            }
        }
    }
}
