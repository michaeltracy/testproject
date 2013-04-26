using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dexter.Data;
using Dexter.Model;
using Npgsql;
using System.Data;
using Plant.Core;

namespace Dexter.Planter
{
    [TestClass]
    public class TestDexter
    {
        IDbTransaction trans;
        BasePlant plant;
        ContactRepo target;

        [TestInitialize]
        public void SetUp()
        {
            target = new ContactRepo();
            trans = target.StartTransaction();
            var count = target.Count;

            plant = new BasePlant().WithBlueprintsFromAssemblyOf<ContactBlueprint>();
            for (int i = 1; i <= 100; i++) {
                var con = plant.Create<Contact>();
                target.Create(con);
            }
            Assert.AreEqual(count + 100, target.Count);
        }

        [TestCleanup]
        public void TearDown()
        {
            trans.Dispose();
        }

        [TestMethod]
        public void AddContact_should_IncreaseCount()
        {
            var count = target.Count;
            var newGuy = plant.Create<Contact>(new { Name = "Guy Somme", Email = "som@guy.co" });
            target.Create(newGuy);
            Assert.AreEqual(count + 1, target.Count);
        }

        [TestMethod]
        public void RemoveContact_should_DecreaseCount()
        {
            var count = target.Count;
            var first = target.All().First();
            target.Destory(first);
            Assert.AreEqual(count - 1, target.Count);
        }

        [TestMethod]
        public void UpdateContact_should_beUpdated()
        {
            var count = target.Count;
            var last = target.All().Last();
            last.Name = "Not random";
            last.Email = "unique@random.com";

            target.Update(last);
            var updated = target.All().Single(c => c.Id == last.Id);

            Assert.AreEqual(count, target.Count);
            Assert.AreEqual(last.Name, updated.Name);
            Assert.AreEqual(last.Email, updated.Email);
        }
    }
}
