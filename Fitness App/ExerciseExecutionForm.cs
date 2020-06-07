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
        SoundPlayer Player = new SoundPlayer("Whistle.mp3");

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
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            NameLabel = new Label();
            grid.RowDefinitions.Add(new RowDefinition());
            NameLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            NameLabel.Content = "Name";
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
            Time.Content = 15;
            Time.HorizontalContentAlignment = HorizontalAlignment.Center;
            Panel.Children.Add(Time);

            Timer timer = new Timer(new TimerCallback(TimerOnTick), Time.Content, 0, 1000);
            if ((int)Time.Content == 0)
            {
                Player.Play();
                timer.Dispose();
                NextExercise();
            }
            Application.Current.Windows[0].Content = Panel;
        }

        public void TimerOnTick(object Time)
        {
            Timer.IsEnabled = false;
            Time = (int)Time - 1;
            ((Application.Current.Windows[0].Content as StackPanel).Children[1] as Label).Content = Time;
            Timer.Start();
        }

        public void NextExercise()
        {
            CurrentExercise = new Exercise(Complex.Exercises[++ExerciseIndex]);
            Application.Current.Windows[0].Content = BuildExercise(CurrentExercise);
        }
                
    }
}
