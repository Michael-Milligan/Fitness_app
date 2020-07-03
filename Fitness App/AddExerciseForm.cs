using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class AddExerciseForm : Window
    {
        string ExerciseNameRU;
        string ExerciseNameEN;
        int Quantity;
        bool MeasuredInTimes;
        string TypeRU;
        string TypeEN;
        int ComplexIndex;
        string Path;

        public Exercise Result { get; private set; }

        public AddExerciseForm(int ComplexIndex)
        {
            Title = Info.locale.AddExerciseFormText[0];
            Width = 500;
            Height = 300;
            this.ComplexIndex = ComplexIndex;

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            #region Labels


            Label TypeRULabel = new Label();
            TypeRULabel.Content = Info.locale.AddExerciseFormText[1];
            grid.Children.Add(TypeRULabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(TypeRULabel, 0);
            Grid.SetRow(TypeRULabel, 0);

            Label TypeENLabel = new Label();
            TypeENLabel.Content = Info.locale.AddExerciseFormText[2];
            grid.Children.Add(TypeENLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(TypeENLabel, 0);
            Grid.SetRow(TypeENLabel, 1);

            Label NameRULabel = new Label();
            NameRULabel.Content = Info.locale.AddExerciseFormText[3];
            grid.Children.Add(NameRULabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(NameRULabel, 0);
            Grid.SetRow(NameRULabel, 2);

            Label NameENLabel = new Label();
            NameENLabel.Content = Info.locale.AddExerciseFormText[4];
            grid.Children.Add(NameENLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(NameENLabel, 0);
            Grid.SetRow(NameENLabel, 3);

            Label QuantityLabel = new Label();
            QuantityLabel.Content = Info.locale.AddExerciseFormText[5];
            grid.Children.Add(QuantityLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(QuantityLabel, 0);
            Grid.SetRow(QuantityLabel, 4);

            Label MeasureLabel = new Label();
            MeasureLabel.Content = Info.locale.AddExerciseFormText[6];
            grid.Children.Add(MeasureLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(MeasureLabel, 0);
            Grid.SetRow(MeasureLabel, 5);

            Label PathLabel = new Label();
            PathLabel.Content = Info.locale.AddExerciseFormText[7];
            grid.Children.Add(PathLabel);
            grid.RowDefinitions.Add(new RowDefinition());
            Grid.SetColumn(PathLabel, 0);
            Grid.SetRow(PathLabel, 6);

            #endregion

            #region TextBoxes

            TextBox TypeRUBox = new TextBox();
            TypeRUBox.TextChanged += TypeRUOnChange;
            grid.Children.Add(TypeRUBox);
            Grid.SetColumn(TypeRUBox, 1);
            Grid.SetRow(TypeRUBox, 0);

            TextBox TypeENBox = new TextBox();
            TypeENBox.TextChanged += TypeENOnChange;
            grid.Children.Add(TypeENBox);
            Grid.SetColumn(TypeENBox, 1);
            Grid.SetRow(TypeENBox, 1);

            TextBox NameRUBox = new TextBox();
            NameRUBox.TextChanged += NameRUOnChange;
            grid.Children.Add(NameRUBox);
            Grid.SetColumn(NameRUBox, 1);
            Grid.SetRow(NameRUBox, 2);

            TextBox NameENBox = new TextBox();
            NameENBox.TextChanged += NameENOnChange;
            grid.Children.Add(NameENBox);
            Grid.SetColumn(NameENBox, 1);
            Grid.SetRow(NameENBox, 3);

            TextBox QuantityBox = new TextBox();
            QuantityBox.TextChanged += QuantityOnChange;
            grid.Children.Add(QuantityBox);
            Grid.SetColumn(QuantityBox, 1);
            Grid.SetRow(QuantityBox, 4);

            TextBox MeasureBox = new TextBox();
            MeasureBox.TextChanged += MeasureOnChange;
            grid.Children.Add(MeasureBox);
            Grid.SetColumn(MeasureBox, 1);
            Grid.SetRow(MeasureBox, 5);

            TextBox PathBox = new TextBox();
            PathBox.TextChanged += PathOnChange;
            grid.Children.Add(PathBox);
            Grid.SetColumn(PathBox, 1);
            Grid.SetRow(PathBox, 6);
            #endregion

            #region Buttons
            Button Send = new Button();
            Send.Content = Info.locale.AddExerciseFormText[8];
            grid.RowDefinitions.Add(new RowDefinition());
            Send.Click += SendOnClick;
            grid.Children.Add(Send);
            Grid.SetRow(Send, 7);
            Grid.SetColumn(Send, 0);

            Button Return = new Button();
            Return.Content = Info.locale.AddExerciseFormText[9];
            Return.Click += ReturnOnClick;
            grid.Children.Add(Return);
            Grid.SetRow(Return, 7);
            Grid.SetColumn(Return, 1);
            #endregion
            
            Content = grid;
        }

        

        #region Done

        public void TypeRUOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            TypeRU = (grid.Children[7] as TextBox).Text;
        }

        public void TypeENOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            TypeEN = (grid.Children[8] as TextBox).Text;
        }

        public void NameRUOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = this.Content as Grid;
            ExerciseNameRU = (grid.Children[9] as TextBox).Text;
        }

        private void NameENOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            ExerciseNameEN = (grid.Children[10] as TextBox).Text;
        }

        public void QuantityOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            Quantity = Convert.ToInt32((grid.Children[11] as TextBox).Text);
        }

        public void MeasureOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            MeasuredInTimes = Convert.ToBoolean(Convert.ToInt32((grid.Children[12] as TextBox).Text));
        }

        public void PathOnChange(object Sender, TextChangedEventArgs Args)
        {
            Grid grid = Content as Grid;
            Path = (grid.Children[13] as TextBox).Text;
        }

        public void SendOnClick(object Sender, RoutedEventArgs Args)
        {
            Info.locale.Type = "en";
            Methods.RefreshPath();
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Result[ComplexIndex].AddExercise(new Exercise(TypeEN, ExerciseNameEN, Quantity, MeasuredInTimes, 
                $@"src\img\{Path}"));
            Methods.RewriteExercises(Result);

            Info.locale.Type = "ru";
            Methods.RefreshPath();
            Result = Methods.SynthesizeComplexes();
            Result[ComplexIndex].AddExercise(new Exercise(TypeRU, ExerciseNameRU, Quantity, MeasuredInTimes,
                $@"src\img\{Path}"));
            Methods.RewriteExercises(Result);

            Info.locale.Type = File.ReadAllText(@"src\locales\initiation.txt");
            Methods.RefreshPath();
            Info.locale.GenerateText();
            Result = Methods.SynthesizeComplexes();
            Application.Current.Windows[0].Content = new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup,
                Result[ComplexIndex],
                ComplexIndex).Content;
        }

        public void ReturnOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            
            Application.Current.Windows[0].Content = new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup,
                Result[ComplexIndex],
                ComplexIndex).Content;
        }
        #endregion
    }
}
