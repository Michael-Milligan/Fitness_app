using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Fitness_App
{
    class ExerciseComplexForm : Window
    {
        int ComplexIndex { get; }
        public ExerciseComplexForm(string Title, ExerciseComplex ComplexArgument, int ComplexIndex)
        {
            this.ComplexIndex = ComplexIndex;
            this.Title = Title;
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            for (int i = 0; i < 7; ++i)
            {
                ColumnDefinition Column = new ColumnDefinition();
                if (i != 1) Column.Width = new GridLength(150, GridUnitType.Pixel);
                else Column.Width = new GridLength(500, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(Column);
            }

            for (int i = 0; i < ComplexArgument.Exercises.Count; ++i)
            {
                RowDefinition Row = new RowDefinition();
                Row.Height = new GridLength(50, GridUnitType.Pixel);
                grid.RowDefinitions.Add(Row);

                #region Up
                Button Up = new Button();
                Up.Content = "^";
                Up.Tag = i;
                Up.Click += UpOnClick;

                grid.Children.Add(Up);
                Grid.SetRow(Up, i);
                Grid.SetColumn(Up, 0);
                #endregion

                #region Name
                Button Name = new Button();
                Name.Content = ComplexArgument.Exercises[i].Name;

                grid.Children.Add(Name);
                Grid.SetRow(Name, i);
                Grid.SetColumn(Name, 1);
                #endregion

                #region Down
                Button Down = new Button();
                Down.Content = "";
                Down.Tag = i;
                Down.Click += DownOnClick;

                grid.Children.Add(Down);
                Grid.SetRow(Down, i);
                Grid.SetColumn(Down, 2);
                #endregion

                #region Plus
                Button Plus = new Button();
                Plus.Content = "+";
                Plus.Tag = i;
                Plus.Click += PlusOnClick;

                grid.Children.Add(Plus);
                Grid.SetRow(Plus, i);
                Grid.SetColumn(Plus, 3);
                #endregion

                #region Number
                Button Number = new Button();
                Number.Content = ComplexArgument.Exercises[i].Quantity;

                grid.Children.Add(Number);
                Grid.SetRow(Number, i);
                Grid.SetColumn(Number, 4);
                #endregion

                #region Minus
                Button Minus = new Button();
                Minus.Content = "-";
                Minus.Tag = i;
                Minus.Click += MinusOnClick;

                grid.Children.Add(Minus);
                Grid.SetRow(Minus, i);
                Grid.SetColumn(Minus, 5);
                #endregion

                #region Remove
                Button Remove = new Button();
                Remove.Content = "Remove";
                Remove.Tag = i;
                Remove.Click += RemoveOnClick;

                grid.Children.Add(Remove);
                Grid.SetRow(Remove, i);
                Grid.SetColumn(Remove, 6);
                #endregion
            }

            #region Add
            RowDefinition RowAdd = new RowDefinition();
            RowAdd.Height = new GridLength(50, GridUnitType.Pixel);
            grid.RowDefinitions.Add(RowAdd);

            Button Add = new Button();
            Add.Content = "Add";
            Add.Click += AddOnClick;

            grid.Children.Add(Add);
            
            Grid.SetRow(Add, ComplexArgument.Exercises.Count);
            Grid.SetColumnSpan(Add, 7);
            #endregion

            #region Return
            RowDefinition RowReturn = new RowDefinition();
            RowReturn.Height = new GridLength(50, GridUnitType.Pixel);
            grid.RowDefinitions.Add(RowReturn);

            Button Return = new Button();
            Return.Content = "Return";
            Return.Click += ReturnOnClick;

            grid.Children.Add(Return);

            Grid.SetRow(Return, ComplexArgument.Exercises.Count + 1);
            Grid.SetColumnSpan(Return, 7);
            #endregion

            Content = grid;
            Focus();
        }

        public void PlusOnClick(object Sender, RoutedEventArgs Args)
        {
            //Changing quantity in object
            Button Plus = Sender as Button;
            //Rewriting the resource file
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Result[ComplexIndex].Exercises[(int)Plus.Tag].ChangeQuantity(Result[ComplexIndex].Exercises[(int)Plus.Tag].Quantity + 1);
            //Changing showed number
            Grid grid = Application.Current.Windows[0].Content as Grid;
            Button Number = grid.Children[grid.Children.IndexOf(Plus) + 1] as Button;
            Number.Content = Result[ComplexIndex].Exercises[(int)Plus.Tag].Quantity;
            
            Methods.RewriteExercises(Result);
        }

        public void MinusOnClick(object Sender, RoutedEventArgs Args)
        {
            Button Minus = Sender as Button;
            try
            {
                ExerciseComplex[] Result = Methods.SynthesizeComplexes();
                Result[ComplexIndex].Exercises[(int)Minus.Tag].ChangeQuantity(Result[ComplexIndex].Exercises[(int)Minus.Tag].Quantity - 1);
                Grid grid = Application.Current.Windows[0].Content as Grid;
                Button Number = grid.Children[grid.Children.IndexOf(Minus) - 1] as Button;
                Number.Content = Result[ComplexIndex].Exercises[(int)Minus.Tag].Quantity;
                Methods.RewriteExercises(Result);
            }
            catch (System.ArgumentException) { }
        }

        public void UpOnClick(object Sender, RoutedEventArgs Args)
        {
            //Getting the complexes from the file
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            //GEtting the Button and the index
            Button Up = Sender as Button;
            int Index = (int)Up.Tag;
            //Swapping the items
            try
            {
                Exercise temp = Result[ComplexIndex].Exercises[Index];
                Result[ComplexIndex].Exercises[Index] = 
                    new Exercise(Result[ComplexIndex].Exercises[Index - 1]);
                Result[ComplexIndex].Exercises[Index - 1] = new Exercise(temp);

                //Rewriting the file
                Methods.RewriteExercises(Result);
                //Changing Form
                Application.Current.Windows[0].Content =
                    new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup, 
                    Result[ComplexIndex], 
                    ComplexIndex).Content as Grid;
            }
            catch (System.OverflowException) { }
            
        }

        public void DownOnClick(object Sender, RoutedEventArgs Args)
        {
            //Getting the complexes from the file
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            //GEtting the Button and the index
            Button Down = Sender as Button;
            int Index = (int)Down.Tag;
            //Swapping the items
            try
            {
                Exercise temp = Result[ComplexIndex].Exercises[Index];
                Result[ComplexIndex].Exercises[Index] =
                    new Exercise(Result[ComplexIndex].Exercises[Index + 1]);
                Result[ComplexIndex].Exercises[Index + 1] = new Exercise(temp);

                //Rewriting the file
                Methods.RewriteExercises(Result);
                //Changing Form
                Application.Current.Windows[0].Content =
                    new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup,
                    Result[ComplexIndex],
                    ComplexIndex).Content as Grid;
            }
            catch (System.Exception) { }
        }

        public void AddOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();

            AddExerciseForm Form = new AddExerciseForm(ComplexIndex);
            Application.Current.Windows[0].Close();
        }

        public void RemoveOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            Button Remove = Sender as Button;
            Result[ComplexIndex].
                RemoveExercise(
                Result[ComplexIndex].
                Exercises[(int)Remove.Tag]);
            Application.Current.Windows[0].Content =
                (new ExerciseComplexForm(Result[ComplexIndex].MuscleGroup, Result[ComplexIndex], ComplexIndex).Content as Grid);
            Methods.RewriteExercises(Result);
        }

        public void ReturnOnClick(object Sender, RoutedEventArgs Args)
        {
            ExerciseComplex[] Result = Methods.SynthesizeComplexes();
            new ComplexesForm(Result).Show();
            Application.Current.Windows[0].Close();
        }
    }
}
