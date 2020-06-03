using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class AddExerciseForm : Window
    {
        string ExerciseName;
        int Quantity;
        bool MeasuredInTimes;
        int ComplexIndex;

        public Exercise Result { get; private set; }

        public AddExerciseForm(int ComplexIndex)
        {
            Title = "Adding exercise";
            Width = 500;
            Height = 300;
            this.ComplexIndex = ComplexIndex;

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            #region Labels

            Label NameLabel = new Label();
            NameLabel.Content = "Name: ";
            grid.Children.Add(NameLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(NameLabel, 0);
            Grid.SetRow(NameLabel, 0);

            Label QuantityLabel = new Label();
            QuantityLabel.Content = "Quantity of exercises: ";
            grid.Children.Add(QuantityLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(QuantityLabel, 0);
            Grid.SetRow(QuantityLabel, 1);

            Label MeasureLabel = new Label();
            MeasureLabel.Content = "Is it measured in times(1/0): ";
            grid.Children.Add(MeasureLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(MeasureLabel, 0);
            Grid.SetRow(MeasureLabel, 2);

            #endregion

            #region TextBoxes

            TextBox NameBox = new TextBox();
            NameBox.TextChanged += NameOnChange;
            grid.Children.Add(NameBox);
            Grid.SetColumn(NameBox, 1);
            Grid.SetRow(NameBox, 0);

            TextBox QuantityBox = new TextBox();
            QuantityBox.TextChanged += QuantityOnChange;
            grid.Children.Add(QuantityBox);
            Grid.SetColumn(QuantityBox, 1);
            Grid.SetRow(QuantityBox, 1);

            TextBox MeasureBox = new TextBox();
            MeasureBox.TextChanged += MeasureOnChange;
            grid.Children.Add(MeasureBox);
            Grid.SetColumn(MeasureBox, 1);
            Grid.SetRow(MeasureBox, 2);
            #endregion

            Button Send = new Button();
            Send.Content = "Send";
            grid.RowDefinitions.Add(new RowDefinition());
            Send.Click += SendOnClick;
            grid.Children.Add(Send);
            Grid.SetRow(Send, 3);
            Grid.SetColumn(Send, 0);

            Button Return = new Button();
            Return.Content = "Return";
            Return.Click += ReturnOnClick;
            grid.Children.Add(Return);
            Grid.SetRow(Return, 3);
            Grid.SetColumn(Return, 1);

            Show();
            Content = grid;
        }

        
        public void MeasureOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = this.Content as Grid;
            MeasuredInTimes = Convert.ToBoolean(Convert.ToInt32((grid.Children[5] as TextBox).Text));
        }

        #region Done
        public void NameOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = this.Content as Grid;
            ExerciseName = (grid.Children[3] as TextBox).Text;
        }

        public void QuantityOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = this.Content as Grid;
            Quantity = Convert.ToInt32((grid.Children[4] as TextBox).Text);
        }
            
        public void SendOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Result[ComplexIndex].AddExercise(new Exercise(ExerciseName, Quantity, MeasuredInTimes));
            Methods.RewriteExercises(Result);

            new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup, 
                Result[ComplexIndex], 
                ComplexIndex).
                    Show();
            Application.Current.Windows[0].Close();
        }

        public void ReturnOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup,
                Result[ComplexIndex],
                ComplexIndex).
                    Show();
            Application.Current.Windows[0].Close();
        }
        #endregion
    }
}
