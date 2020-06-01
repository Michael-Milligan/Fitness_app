using System;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class MainWindow : Window
    {
        public static string Path = "Exercises.txt";

        [STAThread]
        static void Main()
        {
            new Application().Run(new MainWindow());
        }

        public MainWindow()
        {
            Title = "Fitness";

            StackPanel Panel = new StackPanel();

            #region Exercises Complexes
            Button ExercisesComplexes = new Button();
            ExercisesComplexes.Content = "Complexes of exercises";
            ExercisesComplexes.Click += ProceedToCopmlexes;
            Panel.Children.Add(ExercisesComplexes);

            #endregion


            Content = Panel;
        }

        public void ProceedToCopmlexes(object Sender, RoutedEventArgs Args)
        {
            ComplexesForm Form = new ComplexesForm(Methods.SynthesizeComplexes());
            Form.Show();
            Application.Current.MainWindow = Form;
            Application.Current.Windows[0].Close();
        }
    }
}
