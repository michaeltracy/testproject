using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace TestCalc.UIAutomation
{
    public class CalcScreen
    {
        private AutomationElement _mainWindow;
        public AutomationElement Display { get; set; }
        public AutomationElement OneButton { get; set;}
        public AutomationElement TwoButton { get; set;}
        public AutomationElement PlusButton { get; set;}
        public AutomationElement EqualsButton { get; set;}

        public CalcScreen(int processId)
        {
            _mainWindow = AutomationElement.RootElement.FindChildByProcessId(processId);
            Display = _mainWindow.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
            OneButton = _mainWindow.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                .And(new PropertyCondition(AutomationElement.NameProperty, "1")));
            TwoButton = _mainWindow.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                .And(new PropertyCondition(AutomationElement.NameProperty, "2")));
            PlusButton = _mainWindow.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                .And(new PropertyCondition(AutomationElement.NameProperty, "+")));
            EqualsButton = _mainWindow.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                .And(new PropertyCondition(AutomationElement.NameProperty, "=")));
        }

        public void Close()
        {
            var titleBar = _mainWindow.FindChildByCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TitleBar));
            var closeButton = titleBar.FindChildByCondition(
                new PropertyCondition(AutomationElement.NameProperty, "Close"));
            closeButton.GetInvokePattern().Invoke();
        }
    }
}
