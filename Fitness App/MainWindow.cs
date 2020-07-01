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
            Title = "Фитнес";

            StackPanel Panel = new StackPanel();

            #region Exercises Complexes
            Button ExercisesComplexes = new Button();
            ExercisesComplexes.Height = 50;
            ExercisesComplexes.Width = 500;
            ExercisesComplexes.Content = "Комплексы упражнений";
            ExercisesComplexes.Click += ProceedToCopmlexes;
            Panel.Children.Add(ExercisesComplexes);

            Button Exit = new Button();
            Exit.Height = 50;
            Exit.Width = 500;
            Exit.Content = "Выход";
            Exit.Click += ExitOnClick;
            Panel.Children.Add(Exit);

            #endregion


            Content = Panel;
        }

        public void ProceedToCopmlexes(object Sender, RoutedEventArgs Args)
        {
            ComplexesForm Form = new ComplexesForm(Methods.SynthesizeComplexes());
            Application.Current.Windows[0].Content = Form.Content;
        }

        public void ExitOnClick(object Sender, RoutedEventArgs Args)
        {
            MessageBoxResult Result = 
                MessageBox.Show("Вы действительно хотите выйти?", 
                "Вопрос:",
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) Application.Current.Shutdown();
        }
    }
}
