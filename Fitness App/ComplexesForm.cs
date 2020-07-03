using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Fitness_App
{
    class ComplexesForm : Window
    {
        ExerciseComplex[] Complexes;
        public ComplexesForm(ExerciseComplex[] Complexes)
        {
            ScrollViewer Scroll = new ScrollViewer();

            this.Complexes = Complexes;
            Title = Info.locale.ComplexesFormText[0];
            Grid Panel = new Grid();
            Button[] Buttons = new Button[Complexes.Length];

            Panel.ColumnDefinitions.Add(new ColumnDefinition());
            Panel.ColumnDefinitions.Add(new ColumnDefinition());
            Panel.ColumnDefinitions.Add(new ColumnDefinition());
            Panel.ColumnDefinitions[0].Width = new GridLength(70, GridUnitType.Star);
            Panel.ColumnDefinitions[1].Width = new GridLength(20, GridUnitType.Star);
            Panel.ColumnDefinitions[2].Width = new GridLength(10, GridUnitType.Star);

            Button[] Edit = new Button[Buttons.Length];
            Button[] Remove = new Button[Buttons.Length];

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
                Edit[i].Content = Info.locale.ComplexesFormText[1];
                Edit[i].Click += EditOnClick;
                Panel.Children.Add(Edit[i]);
                Grid.SetColumn(Edit[i], 1);
                Grid.SetRow(Edit[i], i);

                Remove[i] = new Button();
                Remove[i].Tag = i;
                Remove[i].Content = Info.locale.ComplexesFormText[2];
                Remove[i].Click += RemoveOnClick;
                Panel.Children.Add(Remove[i]);
                Grid.SetColumn(Remove[i], 2);
                Grid.SetRow(Remove[i], i);
            }

            Button AddComplex = new Button();
            Panel.RowDefinitions.Add(new RowDefinition());
            AddComplex.Content = Info.locale.ComplexesFormText[3];
            AddComplex.Click += AddOnClick;
            Panel.Children.Add(AddComplex);
            Grid.SetRow(AddComplex, Buttons.Length);
            Grid.SetColumn(AddComplex, 0);

            Button Return = new Button();
            Return.Content = Info.locale.ComplexesFormText[4];
            Return.Click += ExitOnClick;
            Panel.Children.Add(Return);
            Grid.SetRow(Return, Buttons.Length);
            Grid.SetColumnSpan(Return, 2);
            Grid.SetColumn(Return, 1);

            Scroll.Content = Panel;
            Content = Scroll;
        }

        private void RemoveOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Button Remove = Sender as Button;
            int ComplexIndex = (int)Remove.Tag;

            Result = Result.Where(item => item != Result[ComplexIndex]).ToArray();

            Methods.RewriteExercises(Result);
            Application.Current.Windows[0].Content = new ComplexesForm(Result).Content;
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
            MessageBoxResult Result = MessageBox.Show(Info.locale.ComplexesFormText[5],
                Info.locale.ComplexesFormText[6], 
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

        public void AddOnClick(object Sender, RoutedEventArgs Args)
        {
            Application.Current.Windows[0].Content = new AddComplexForm().Content;
        }
    }
}
