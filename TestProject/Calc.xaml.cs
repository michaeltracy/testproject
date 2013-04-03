using System.Windows;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for Calc.xaml
    /// </summary>
    public partial class Calc : Window
    {
        public Calc()
        {
            InitializeComponent();
            DataContext = new CalcVM();
        }
    }
}
