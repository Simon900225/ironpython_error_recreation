using GSAB.RSProduction.Framework.Scripting;
using System.Windows;

namespace Recreate_python_error
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            script.Text = @"
import System
from System import *
import clr
from System.Linq import *
clr.ImportExtensions(System.Linq)


a = range(1,10)

a.ElementAt(5)
";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var python = new PythonHost();
            var result = python.Execute(script.Text);
            resultBox.Text = result.ToString();
        }
    }
}
