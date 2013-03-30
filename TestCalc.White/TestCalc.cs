using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.WindowStripControls;
using White.Core.UIItems.MenuItems;
using White.Core.UIItems.TreeItems;
using White.Core.UIItems.Finders;

namespace TestCalc.White
{
    [TestClass]
    public class TestCalc
    {
        Application app;
        Window calculon;
        TextBox display;
        Button one;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public virtual void SetUp()
        {
            //app = Application.Launch(@"C:\Users\Mike\Documents\Visual Studio 2010\Projects\TestProject\TestCalc.White\bin\Debug\Calculon.exe");
            app = Application.Launch(AppDomain.CurrentDomain.BaseDirectory + @"\Calculon.exe");
            calculon = app.GetWindow("Calculon");
            Assert.IsNotNull(calculon);
            calculon.WaitWhileBusy();

            display = calculon.Get<TextBox>();
            Assert.IsNotNull(display);
            one = calculon.Get<Button>(SearchCriteria.ByText("1"));
            Assert.IsNotNull(one);
        }

        [TestCleanup]
        public void TearDown()
        {
            app.Close();
        }

        [TestClass]
        public class Numbers : TestCalc
        {
            [TestMethod]
            public void I_hit_1()
            {
                one.Click();

                Assert.AreEqual("1", display.Text);
            }

            [TestMethod]
            public void I_hit_1_then_2()
            {
                one.Click();
                calculon.Get<Button>(SearchCriteria.ByText("2")).Click();

                Assert.AreEqual("12", display.Text);
            }
        }

        [TestClass]
        public class Addition : TestCalc
        {
            Button equals;

            [TestInitialize]
            public override void SetUp()
            {
                base.SetUp();

                equals = calculon.Get<Button>(SearchCriteria.ByText("="));
                Assert.IsNotNull(equals);
            }

            [TestMethod]
            public void Add_1_and_1_equals_2()
            {
                var plus = calculon.Get<Button>(SearchCriteria.ByText("+"));
                Assert.IsNotNull(plus);

                one.Click();
                plus.Click();
                one.Click();
                equals.Click();

                Assert.AreEqual("2", display.Text);
            }

            [TestMethod]
            public void Subtract_1_and_1_equals_0()
            {
                var minus = calculon.Get<Button>(SearchCriteria.ByText("-"));
                Assert.IsNotNull(minus);

                one.Click();
                minus.Click();
                one.Click();
                equals.Click();

                Assert.AreEqual("0", display.Text);
            }
        }
    }
}
