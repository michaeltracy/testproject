using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

namespace TestCalc.UIAutomation
{
    [TestClass]
    public class TestCalc
    {
        Process process;
        CalcScreen calc;

        [TestInitialize]
        public virtual void SetUp()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\Calculon.exe";
            process = Process.Start(new ProcessStartInfo(path));
            process.WaitForInputIdle();
            Thread.Sleep(10);

            calc = new CalcScreen(process.Id);
        }

        [TestCleanup]
        public virtual void TearDown()
        {
            // just use the close button to shut it down
            calc.Close();
            process.Close();
        }

        [TestClass]
        public class Numbers : TestCalc
        {
            [TestMethod]
            public void Invoke_1()
            {
                calc.OneButton.GetInvokePattern().Invoke();
                var actual = calc.Display.GetValue();
                Assert.AreEqual("1", actual);
            }

            [TestMethod]
            public void Invoke_2()
            {
                calc.TwoButton.GetInvokePattern().Invoke();
                var actual = calc.Display.GetValue();
                Assert.AreEqual("2", actual);
            }

            [TestMethod]
            public void Invoke_1_then_2()
            {
                calc.OneButton.GetInvokePattern().Invoke();
                calc.TwoButton.GetInvokePattern().Invoke();
                var actual = calc.Display.GetValue();
                Assert.AreEqual("12", actual);
            }
        }

        [TestClass]
        public class Addition : TestCalc
        {
            [TestInitialize]
            public override void SetUp()
            {
                base.SetUp();
                // start math going...
                calc.OneButton.GetInvokePattern().Invoke();
                calc.PlusButton.GetInvokePattern().Invoke();
            }

            [TestMethod]
            public void One_plus_one_shouldBe_2()
            {
                calc.OneButton.GetInvokePattern().Invoke();
                calc.EqualsButton.GetInvokePattern().Invoke();
                var actual = calc.Display.GetValue();
                Assert.AreEqual("2", actual);
            }

            [TestMethod]
            public void One_plus_two_shouldBe_3()
            {
                calc.TwoButton.GetInvokePattern().Invoke();
                calc.EqualsButton.GetInvokePattern().Invoke();
                var actual = calc.Display.GetValue();
                Assert.AreEqual("3", actual);
            }
        }
    }
}
