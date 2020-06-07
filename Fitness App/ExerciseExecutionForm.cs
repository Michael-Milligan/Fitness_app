using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Fitness_App
{
    class ExerciseExecutionForm : Window 
    {
        DispatcherTimer Timer;
        Label Quantity;
        ExerciseComplex Complex;
        Label NameLabel;
        Exercise CurrentExercise;
        int ExerciseIndex;

        public ExerciseExecutionForm(ExerciseComplex Complex)
        {
            this.Complex = new ExerciseComplex(Complex.MuscleGroup);
            this.Complex.Exercises = Complex.Exercises.Where(item => item == item).ToList();
            Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, QuantityOnTick, Dispatcher.CurrentDispatcher);
            Title = Complex.MuscleGroup + " Complex";

            foreach (var exercise in Complex.Exercises)
            {
                CurrentExercise = new Exercise(exercise);
                ExerciseIndex = Complex.Exercises.FindIndex(new Predicate<Exercise>(item => item == exercise));
                Content = BuildExercise(exercise);
            }
        }

        public Grid BuildExercise(Exercise exercise)
        {
            Grid grid = new Grid();
            grid.ShowGridLines = true;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            NameLabel = new Label();
            grid.RowDefinitions.Add(new RowDefinition());
            NameLabel.Content = "Name";
            NameLabel.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(NameLabel);
            Grid.SetColumnSpan(NameLabel, 2);
            Grid.SetRow(NameLabel, 0);

            Quantity = new Label();
            grid.RowDefinitions.Add(new RowDefinition());
            Quantity.Content = exercise.Quantity;
            Quantity.VerticalAlignment = VerticalAlignment.Bottom;
            grid.Children.Add(Quantity);
            Grid.SetColumnSpan(Quantity, 2);
            Grid.SetRow(Quantity, 0);

            Button Next = new Button();

            return grid;
        }

        public void QuantityOnTick(object Sender, EventArgs Args)
        {
            if (!CurrentExercise.MeasuredInTimes)
            {
                Quantity.Content = (int)Quantity.Content - 1;
            }
            if ((int)Quantity.Content == 0) 
                ++ExerciseIndex;
        }
    }
}
