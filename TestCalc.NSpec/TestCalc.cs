using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSpec;
using TestProject;

namespace TestCalc.NSpec
{
    class TestCalc : nspec
    {
        CalcVM calc;

        void before_each()
        {
            calc = new CalcVM();
        }

        void describe_pushing_buttons() {
            context["when I push 1"] = () =>
            {
                before = () => calc.Number.Execute("1");
                it["should display '1'"] = () => calc.Output.should_be("1");

                context["then I push 2"] = () => {
                    before = () => calc.Number.Execute("2");
                    it["should display 12"] = () => calc.Output.should_be("12");

                    context["then I push equals"] = () =>
                    {
                        before = () => calc.Do.Execute("=");
                        it["should still show 12"] = () => calc.Output.should_be("12");
                    };
                };
            };
        }
    }
}
