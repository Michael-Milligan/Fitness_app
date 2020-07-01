using System;
using System.Linq;
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
        string Type;
        int ComplexIndex;
        string Path;

        public Exercise Result { get; private set; }

        public AddExerciseForm(int ComplexIndex)
        {
            Title = "Добавление упражнения:";
            Width = 500;
            Height = 300;
            this.ComplexIndex = ComplexIndex;

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            #region Labels


            Label TypeLabel = new Label();
            TypeLabel.Content = "Часть: ";
            grid.Children.Add(TypeLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(TypeLabel, 0);
            Grid.SetRow(TypeLabel, 0);

            Label NameLabel = new Label();
            NameLabel.Content = "Имя: ";
            grid.Children.Add(NameLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(NameLabel, 0);
            Grid.SetRow(NameLabel, 1);

            Label QuantityLabel = new Label();
            QuantityLabel.Content = "Кол-во упражнений: ";
            grid.Children.Add(QuantityLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(QuantityLabel, 0);
            Grid.SetRow(QuantityLabel, 2);

            Label MeasureLabel = new Label();
            MeasureLabel.Content = "Оно измеряется в количестве раз?(1/0): ";
            grid.Children.Add(MeasureLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(MeasureLabel, 0);
            Grid.SetRow(MeasureLabel, 3);

            Label PathLabel = new Label();
            PathLabel.Content = "Имя файла с расширением: ";
            grid.Children.Add(PathLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(PathLabel, 0);
            Grid.SetRow(PathLabel, 4);

            #endregion

            #region TextBoxes

            TextBox TypeBox = new TextBox();
            TypeBox.TextChanged += TypeOnChange;
            grid.Children.Add(TypeBox);
            Grid.SetColumn(TypeBox, 1);
            Grid.SetRow(TypeBox, 0);

            TextBox NameBox = new TextBox();
            NameBox.TextChanged += NameOnChange;
            grid.Children.Add(NameBox);
            Grid.SetColumn(NameBox, 1);
            Grid.SetRow(NameBox, 1);

            TextBox QuantityBox = new TextBox();
            QuantityBox.TextChanged += QuantityOnChange;
            grid.Children.Add(QuantityBox);
            Grid.SetColumn(QuantityBox, 1);
            Grid.SetRow(QuantityBox, 2);

            TextBox MeasureBox = new TextBox();
            MeasureBox.TextChanged += MeasureOnChange;
            grid.Children.Add(MeasureBox);
            Grid.SetColumn(MeasureBox, 1);
            Grid.SetRow(MeasureBox, 3);

            TextBox PathBox = new TextBox();
            PathBox.TextChanged += PathOnChange;
            grid.Children.Add(PathBox);
            Grid.SetColumn(PathBox, 1);
            Grid.SetRow(PathBox, 4);
            #endregion

            #region Buttons
            Button Send = new Button();
            Send.Content = "Отправить";
            grid.RowDefinitions.Add(new RowDefinition());
            Send.Click += SendOnClick;
            grid.Children.Add(Send);
            Grid.SetRow(Send, 5);
            Grid.SetColumn(Send, 0);

            Button Return = new Button();
            Return.Content = "Назад";
            Return.Click += ReturnOnClick;
            grid.Children.Add(Return);
            Grid.SetRow(Return, 5);
            Grid.SetColumn(Return, 1);
            #endregion
            
            Show();
            Content = grid;
        }

        #region Done
        
        public void TypeOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = this.Content as Grid;
            Type = (grid.Children[5] as TextBox).Text;
        }

        public void NameOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = this.Content as Grid;
            ExerciseName = (grid.Children[6] as TextBox).Text;
        }

        public void QuantityOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            Quantity = Convert.ToInt32((grid.Children[7] as TextBox).Text);
        }

        public void MeasureOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            MeasuredInTimes = Convert.ToBoolean(Convert.ToInt32((grid.Children[8] as TextBox).Text));
        }

        public void PathOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            Path = (grid.Children[9] as TextBox).Text;
        }

        public void SendOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Result[ComplexIndex].AddExercise(new Exercise(Type, ExerciseName, Quantity, MeasuredInTimes, 
                $@"src\img\{Path}"));
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
