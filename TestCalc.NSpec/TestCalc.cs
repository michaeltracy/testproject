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

        void describe_math()
        {
            before = () => calc.Number.Execute("1");
            context["1 plus 1"] = () =>
            {
                act = () =>
                {
                    calc.Do.Execute("+");
                    calc.Number.Execute("1");
                    calc.Do.Execute("=");
                };
                it["should be 2"] = () => calc.Output.should_be("2");
            };

            context["1 minus 1"] = () =>
            {
                act = () =>
                {
                    calc.Do.Execute("-");
                    calc.Number.Execute("1");
                    calc.Do.Execute("=");
                };
                it["should be 0"] = () => calc.Output.should_be("0");
            };
        }
    }
}
