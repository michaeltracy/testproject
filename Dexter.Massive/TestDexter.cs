using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using Dexter.Data;
using Dexter.Model;
using Massive.PostgreSQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace Dexter.Fringe
{
    [TestClass]
    public class TestDexter
    {
        ContactRepo target;
        IDbTransaction trans;
        static DynamicModel contacts;
        static IEnumerable<dynamic> sampleData;

        [ClassInitialize]
        public static void LoadTestData(TestContext ctx)
        {
            contacts = new DynamicModel("ConnectionString", "contact", "id");
            var current = contacts.All();
            var list = new List<dynamic>();
            for (int i = 1; i <= 100; i++)
            {
                dynamic newCon = new ExpandoObject();
                newCon.name = "Random User";
                newCon.email = "random_" + i + "@example.com";
                list.Add(newCon);
            }
            contacts.Save(list.ToArray());
            sampleData = contacts.All().Except(current);
        }

        [ClassCleanup]
        public static void CleanTestData()
        {
            foreach (var sample in sampleData)
                contacts.Delete(sample.id);
        }

        [TestInitialize]
        public void SetUp()
        {
            target = new ContactRepo(
                new NpgsqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString));
            trans = target.StartTransaction();
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
            target.Create(new Contact { Name = "New Guy", Email = "new@guy.com" });
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
