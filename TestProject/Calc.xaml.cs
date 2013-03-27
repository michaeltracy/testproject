using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Util;
using System.ComponentModel;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for Calc.xaml
    /// </summary>
    public partial class Calc : Window, INotifyPropertyChanged
    {
        public Calc()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string _output;
        public string Output 
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged("Output");
            }
        }

        private RelayCommand _number;
        public ICommand Number
        {
            get
            {
                return _number ?? (_number = new RelayCommand(n => Output += n));
            }
        }

        private RelayCommand _do;
        public ICommand Do
        {
            get
            {
                return _do ?? (_do = new RelayCommand(d => Calculate(d.ToString())));
            }
        }

        private int result = 0;
        public void Calculate(string action)
        {
            int output;
            if (!int.TryParse(Output, out output))
                return;
            result += output;
            switch (action)
            {
                case "=":
                    Output = result.ToString();
                    break;
                case "+":
                    Output = "";
                    break;
                case "-":
                    Output = "-";
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
