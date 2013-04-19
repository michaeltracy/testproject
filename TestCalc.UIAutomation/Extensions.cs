using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace TestCalc.UIAutomation
{
    /// <summary>
    /// Extensions in this class have been borrowed from:
    /// http://blog.functionalfun.net/2009/06/introduction-to-ui-automation-with.html
    /// and is governed by the Ms-PL
    /// </summary>
    public static class TutorialExtensions
    {
        public static AutomationElement FindChildByProcessId(this AutomationElement @element, int id)
        {
            return @element.FindChildByCondition(new PropertyCondition(AutomationElement.ProcessIdProperty, id));
        }

        public static AutomationElement FindChildByCondition(this AutomationElement @element, Condition condition)
        {
            return @element.FindFirst(TreeScope.Children, condition);
        }

        public static AutomationElement FindDescendentByIdPath(this AutomationElement element, IEnumerable<string> idPath)
        {
            var conditionPath = CreateConditionPathForPropertyValues(AutomationElement.AutomationIdProperty, idPath.Cast<object>());

            return FindDescendentByConditionPath(element, conditionPath);
        }

        public static IEnumerable<Condition> CreateConditionPathForPropertyValues(AutomationProperty property, IEnumerable<object> values)
        {
            var conditions = values.Select(value => new PropertyCondition(property, value));

            return conditions.Cast<Condition>();
        }

        public static AutomationElement FindDescendentByConditionPath(this AutomationElement element, IEnumerable<Condition> conditionPath)
        {
            if (!conditionPath.Any())
            {
                return element;
            }

            var result = conditionPath.Aggregate(
                element,
                (parentElement, nextCondition) => parentElement == null
                                                      ? null
                                                      : parentElement.FindChildByCondition(nextCondition));

            return result;
        }

        public static InvokePattern GetInvokePattern(this AutomationElement element)
        {
            return element.GetPattern<InvokePattern>(InvokePattern.Pattern);
        }

        public static T GetPattern<T>(this AutomationElement element, AutomationPattern pattern) where T : class
        {
            var patternObject = element.GetCurrentPattern(pattern);

            return patternObject as T;
        }

        public static string GetValue(this AutomationElement element)
        {
            var pattern = element.GetPattern<ValuePattern>(ValuePattern.Pattern);

            return pattern.Current.Value;
        }

        public static void SetValue(this AutomationElement element, string value)
        {
            var pattern = element.GetPattern<ValuePattern>(ValuePattern.Pattern);

            pattern.SetValue(value);
        }
    }

    public static class LocalExtensions
    {
        public static Condition And(this Condition @me, Condition you)
        {
            return new AndCondition(@me, you);
        }

        public static Condition Or(this Condition @me, Condition you)
        {
            return new OrCondition(@me, you);
        }
    }
}
