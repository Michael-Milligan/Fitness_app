using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Media;

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
        SoundPlayer Player = new SoundPlayer("Whistle.wav");

        public ExerciseExecutionForm(ExerciseComplex Complex)
        {
            this.Complex = new ExerciseComplex(Complex.MuscleGroup);
            this.Complex.Exercises = Complex.Exercises.Where(item => item == item).ToList();
            this.Complex.Exercises = this.Complex.Exercises.OrderBy(exercise => exercise.Type).
                ThenBy(exercise => exercise.Name).ToList();
            Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, QuantityOnTick, Dispatcher.CurrentDispatcher);
            Title = this.Complex.MuscleGroup + " Complex";

            CurrentExercise = new Exercise(Complex.Exercises[0]);
            ExerciseIndex = this.Complex.Exercises.FindIndex(new Predicate<Exercise>(item => item == Complex.Exercises[0]));
            Content = BuildExercise(Complex.Exercises[0]);
        }

        public Grid BuildExercise(Exercise exercise)
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            NameLabel = new Label();
            grid.RowDefinitions.Add(new RowDefinition());
            NameLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            NameLabel.Content = exercise.Name;
            grid.Children.Add(NameLabel);
            Grid.SetColumnSpan(NameLabel, 2);
            Grid.SetRow(NameLabel, 0);

            Quantity = new Label();
            grid.RowDefinitions.Add(new RowDefinition());
            Quantity.Content = exercise.Quantity;
            Quantity.HorizontalContentAlignment = HorizontalAlignment.Center;
            grid.Children.Add(Quantity);
            Grid.SetColumnSpan(Quantity, 2);
            Grid.SetRow(Quantity, 1);

            Button Previous = new Button();
            grid.RowDefinitions.Add(new RowDefinition());
            Previous.Content = "Previous";
            grid.Children.Add(Previous);
            Grid.SetColumn(Previous, 0);
            Grid.SetRow(Previous, 2);

            Button Next = new Button();
            grid.RowDefinitions.Add(new RowDefinition());
            Next.Content = "Next";
            grid.Children.Add(Next);
            Grid.SetColumn( Next, 1);
            Grid.SetRow(Next, 2);

            return grid;
        }

        public void QuantityOnTick(object Sender, EventArgs Args)
        {
            if (!CurrentExercise.MeasuredInTimes)
            {
                Quantity.Content = (int)Quantity.Content - 1;
            }
            if ((int)Quantity.Content == 0)
            {
                Player.Play();
                Timer.Stop();
                MakeAPause();
            }
        }
        public void MakeAPause()
        {
            StackPanel Panel = new StackPanel();

            Label Message = new Label();
            Message.HorizontalContentAlignment = HorizontalAlignment.Center;
            Message.Content = "Rest! The remaining time:";
            Panel.Children.Add(Message);

            Label Time = new Label();
            Time.Content = 2;
            Time.HorizontalContentAlignment = HorizontalAlignment.Center;
            Panel.Children.Add(Time);

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1),
                DispatcherPriority.Normal, 
                TimerOnTick, 
                Dispatcher.CurrentDispatcher);
            Application.Current.Windows[0].Content = Panel;
        }

        public void TimerOnTick(object Time, EventArgs Args)
        {
            Label LocalTime = new Label();

            LocalTime.Content = 
                (int)(((Application.Current.Windows[0].Content as StackPanel).Children[1] as Label).Content) - 1;
            ((Application.Current.Windows[0].Content as StackPanel).Children[1] as Label).Content = LocalTime.Content;
            if ((int)LocalTime.Content == 0)
            { 
                Player.Play();
                NextExercise();
                Timer.Start();
                (Time as DispatcherTimer).Stop();
            }
        }

        public void NextExercise()
        {
            try
            {
                CurrentExercise = new Exercise(Complex.Exercises[++ExerciseIndex]);
                Application.Current.Windows[0].Content = BuildExercise(CurrentExercise);
                Timer.Start();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("The complex was done! Congrats!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBoxResult Result = MessageBox.Show("Would you like to return to the Complexes page?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.No)
                {
                    new ComplexesForm(Methods.SynthesizeComplexes());
                    Application.Current.Windows[0].Close();
                }
            }
        }
                
    }
}
