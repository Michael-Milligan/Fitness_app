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
            //Sorting the exercises
            Methods.RewriteExercises(Methods.SynthesizeComplexes());
            new Application().Run(new MainWindow());
        }

        public MainWindow()
        {
            Title = "Fitness";

            StackPanel Panel = new StackPanel();

            #region Exercises Complexes
            Button ExercisesComplexes = new Button();
            ExercisesComplexes.Height = 50;
            ExercisesComplexes.Width = 500;
            ExercisesComplexes.Content = "Complexes of exercises";
            ExercisesComplexes.Click += ProceedToCopmlexes;
            Panel.Children.Add(ExercisesComplexes);

            Button Exit = new Button();
            Exit.Height = 50;
            Exit.Width = 500;
            Exit.Content = "Exit";
            Exit.Click += ExitOnClick;
            Panel.Children.Add(Exit);

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

        public void ExitOnClick(object Sender, RoutedEventArgs Args)
        {
            MessageBoxResult Result = 
                MessageBox.Show("Do you really want to quit?", 
                "Question",
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) Application.Current.Shutdown();
        }
    }
}
