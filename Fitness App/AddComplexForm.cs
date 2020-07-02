using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class AddComplexForm : Window
    {
        string NameRU;
        string NameEN;
        public AddComplexForm()
        {
            Title = Info.locale.AddComplexFormText[0];

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Label Question = new Label();
            Question.Content = Info.locale.AddComplexFormText[1]; //"What is the name of the complex?";
            grid.Children.Add(Question);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumnSpan(Question, 2);
            Grid.SetRow(Question, 0);

            TextBox InputRU = new TextBox();
            InputRU.Text = "Русский";
            InputRU.TextChanged += NameRUOnChange;
            grid.Children.Add(InputRU);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(InputRU, 0);
            Grid.SetRow(InputRU, 1);

            TextBox InputEN = new TextBox();
            InputEN.Text = "English";
            InputEN.TextChanged += NameENOnChange;
            grid.Children.Add(InputEN);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(InputEN, 1);
            Grid.SetRow(InputEN, 1);

            Button Send = new Button();
            Send.Content = Info.locale.AddComplexFormText[2];
            Send.Click += SendOnClick;
            grid.Children.Add(Send);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(Send, 0);
            Grid.SetRow(Send, 2);

            Button Return = new Button();
            Return.Content = Info.locale.AddComplexFormText[3];
            Return.Click += ReturnOnClick;
            grid.Children.Add(Return);
            Grid.SetColumn(Return, 1);
            Grid.SetRow(Return, 2);

            Content = grid;
        }

        public void NameRUOnChange(object Sender, RoutedEventArgs Args)
        {
            NameRU = (Sender as TextBox).Text;
        }

        public void NameENOnChange(object Sender, RoutedEventArgs Args)
        {
            NameEN = (Sender as TextBox).Text;
        }

        public void SendOnClick(object Sender, RoutedEventArgs Args)
        {
            var Result = AddComplexRU();

            Result = AddComplexEN();

            Info.locale.Type = File.ReadAllText(@"src\locales\initiation.txt");
            Methods.RefreshPath();
            Application.Current.Windows[0].Content = new ComplexesForm(Methods.SynthesizeComplexes()).Content;
        }

        public ExerciseComplex[] AddComplexRU()
        {
            Info.locale.Type = "ru";
            Info.locale.GenerateText();
            Methods.RefreshPath();
            var temp = Methods.SynthesizeComplexes();
            ExerciseComplex[] Result = new ExerciseComplex[temp.Length + 1];
            for (int i = 0; i < temp.Length; ++i)
            {
                Result[i] = temp[i];
            }
            Result[Result.Length - 1] = new ExerciseComplex(NameRU)
            {
                Exercises = new System.Collections.Generic.List<Exercise>()
            };
            Result[Result.Length - 1].Exercises.Add(new Exercise(Info.locale.AddComplexFormText[4],
                Info.locale.AddComplexFormText[5], 1, false, ""));
            Methods.RewriteExercises(Result);
            return Result;
        }

        public ExerciseComplex[] AddComplexEN()
        {
            Info.locale.Type = "en";
            Info.locale.GenerateText();
            Methods.RefreshPath();
            var temp = Methods.SynthesizeComplexes();
            ExerciseComplex[] Result = new ExerciseComplex[temp.Length + 1];
            for (int i = 0; i < temp.Length; ++i)
            {
                Result[i] = temp[i];
            }
            Result[Result.Length - 1] = new ExerciseComplex(NameEN)
            {
                Exercises = new System.Collections.Generic.List<Exercise>()
            };
            Result[Result.Length - 1].Exercises.Add(new Exercise(Info.locale.AddComplexFormText[4],
                Info.locale.AddComplexFormText[5], 1, false, ""));
            Methods.RewriteExercises(Result);
            return Result;
        }

        public void ReturnOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Application.Current.Windows[0].Content = new ComplexesForm(Result).Content;
        }
    }
}
