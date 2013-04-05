using System;
using TechTalk.SpecFlow;
using TestProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCalc.Specs
{
    [Binding]
    public class CalculonSteps
    {
        CalcVM calc;

        [Given(@"I started calculon")]
        public void GivenIStartedCalculon()
        {
            calc = new CalcVM();
        }
        
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            calc.Number.Execute(p0);
        }
        
        [Given(@"I press ""(.*)""")]
        public void GivenIPress(string p0)
        {
            calc.Do.Execute(p0);
        }
        
        [When(@"I press ""(.*)""")]
        public void WhenIPress(string p0)
        {
            calc.Do.Execute(p0);
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.AreEqual(p0.ToString(), calc.Output);
        }
    }
}
