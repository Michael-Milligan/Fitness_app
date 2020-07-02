using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class AddComplexForm : Window
    {
        string Name;
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

            TextBox Input = new TextBox();
            Input.TextChanged += NameOnChange;
            grid.Children.Add(Input);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumnSpan(Input, 2);
            Grid.SetRow(Input, 1);

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

        public void NameOnChange(object Sender, RoutedEventArgs Args)
        {
            Name = (Sender as TextBox).Text;
        }

        public void SendOnClick(object Sender, RoutedEventArgs Args)
        {
            var temp = Methods.SynthesizeComplexes();
            ExerciseComplex[] Result = new ExerciseComplex[temp.Length + 1];
            for (int i = 0; i < temp.Length; ++i)
            {
                Result[i] = temp[i];
            }
            Result[Result.Length - 1] = new ExerciseComplex(Name)
            {
                Exercises = new System.Collections.Generic.List<Exercise>()
            };
            Result[Result.Length - 1].Exercises.Add(new Exercise(Info.locale.AddComplexFormText[4],
                Info.locale.AddComplexFormText[5], 1, false, ""));
            Methods.RewriteExercises(Result);

            Application.Current.Windows[0].Content = new ComplexesForm(Result).Content;
        }

        public void ReturnOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Application.Current.Windows[0].Content = new ComplexesForm(Result).Content;
        }
    }
}
