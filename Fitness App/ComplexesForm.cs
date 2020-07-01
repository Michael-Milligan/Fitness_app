using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class ComplexesForm : Window
    {
        ExerciseComplex[] Complexes;
        public ComplexesForm(ExerciseComplex[] Complexes)
        {
            this.Complexes = Complexes;
            Title = "Комплексы упражнений:";
            Grid Panel = new Grid();
            Button[] Buttons = new Button[Complexes.Length];

            Panel.ColumnDefinitions.Add(new ColumnDefinition());
            Panel.ColumnDefinitions.Add(new ColumnDefinition());
            Panel.ColumnDefinitions[0].Width = new GridLength(90, GridUnitType.Star);
            Panel.ColumnDefinitions[1].Width = new GridLength(10, GridUnitType.Star);

            Button[] Edit = new Button[Buttons.Length];
            

            for (int i = 0; i < Buttons.Length; ++i)
            {
                Panel.RowDefinitions.Add(new RowDefinition());
                Buttons[i] = new Button();
                Buttons[i].Tag = i;
                Buttons[i].Content = Complexes[i].MuscleGroup;
                Buttons[i].Click += ReadinessControl;
                Panel.Children.Add(Buttons[i]);
                Grid.SetColumn(Buttons[i], 0);
                Grid.SetRow(Buttons[i], i);

                Edit[i] = new Button();
                Edit[i].Tag = i;
                Edit[i].Content = "Изменить";
                Edit[i].Click += EditOnClick;
                Panel.Children.Add(Edit[i]);
                Grid.SetColumn(Edit[i], 1);
                Grid.SetRow(Edit[i], i);
            }

            Button Return = new Button();
            Panel.RowDefinitions.Add(new RowDefinition());
            Return.Content = "Назад";
            Return.Click += ExitOnClick;
            Panel.Children.Add(Return);
            Grid.SetRow(Return, Buttons.Length);
            Grid.SetColumnSpan(Return, 2);

            Content = Panel;
        }

        public void EditOnClick(object Sender, RoutedEventArgs Args)
        {
            Button button = Sender as Button;
            ExerciseComplexForm Form = new ExerciseComplexForm(Complexes[(int)button.Tag].MuscleGroup, 
                Complexes[(int)button.Tag],
                (int)button.Tag);
            Application.Current.Windows[0].Content = Form.Content;
        }

        public void ReadinessControl(object Sender, RoutedEventArgs Args)
        {
            MessageBoxResult Result = MessageBox.Show("Вы готовы приступить к выполнению?", 
                "Вопрос:", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                Button button = Sender as Button;
                Application.Current.Windows[0].Content = new ExerciseExecutionForm(Complexes[(int)button.Tag]).Content;
            }
        }

        public void ExitOnClick(object Sender, RoutedEventArgs Args)
        {
            Application.Current.Windows[0].Content = new MainWindow().Content;
        }
    }
}
