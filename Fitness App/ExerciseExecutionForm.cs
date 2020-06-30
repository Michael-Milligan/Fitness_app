using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Media;
using System.Windows.Media.Imaging;

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
        SoundPlayer Player = new SoundPlayer(@"src\Whistle.wav");
        bool IsPause = false;
        Image image;

        public ExerciseExecutionForm(ExerciseComplex Complex)
        {
            this.Complex = new ExerciseComplex(Complex.MuscleGroup);
            this.Complex.Exercises = Complex.Exercises.Where(item => item == item).ToList();
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

            #region Picture
            grid.RowDefinitions.Add(new RowDefinition());
            try
            {
            Uri uri = new Uri(exercise.PathToPicture, UriKind.RelativeOrAbsolute);
            BitmapImage bit = new BitmapImage(uri);
            image = new Image();
            image.Source = bit;
            grid.Children.Add(image);
            Grid.SetColumnSpan(image, 2);
            Grid.SetRow(image, 1);
            }
            catch (Exception) { }
            #endregion

            Quantity = new Label();
            grid.RowDefinitions.Add(new RowDefinition());
            Quantity.Content = exercise.Quantity;
            Quantity.HorizontalContentAlignment = HorizontalAlignment.Center;
            Quantity.VerticalAlignment = VerticalAlignment.Center;
            grid.Children.Add(Quantity);
            Grid.SetColumnSpan(Quantity, 2);
            Grid.SetRow(Quantity, 2);

            grid.RowDefinitions.Add(new RowDefinition());
            Button Previous = new Button();
            Previous.Content = "Previous";
            Previous.HorizontalContentAlignment = HorizontalAlignment.Center;
            grid.Children.Add(Previous);
            Grid.SetColumn(Previous, 0);
            Grid.SetRow(Previous, 3);
            Previous.Click += PreviousOnClick;

            Button Next = new Button();
            Next.Content = "Next";
            Next.HorizontalContentAlignment = HorizontalAlignment.Center;
            grid.Children.Add(Next);
            Grid.SetColumn(Next, 1);
            Grid.SetRow(Next, 3);
            Next.Click += NextOnClick;

            return grid;
        }

        public void QuantityOnTick(object Sender, EventArgs Args)
        {
            if (!IsPause)
            {
                if (!CurrentExercise.MeasuredInTimes)
                {
                    Quantity.Content = (int)Quantity.Content - 1;
                }
                if ((int)Quantity.Content == 0)
                {
                    Player.Play();
                    MakeAPause();
                }
            }
            else
            {
                Quantity.Content = (int)Quantity.Content - 1;
                if ((int)Quantity.Content == 0)
                {
                    Player.Play();
                    NextExercise();
                    IsPause = false;
                }
            }
        }

        public void MakeAPause()
        {
            NameLabel.Content = "Rest! The remaining time:";
            Quantity.Content = 15;
            var grid = Application.Current.Windows[0].Content as Grid;
            grid.Children.Remove(image);
            IsPause = true;
        }

        public void NextExercise()
        {
            try
            {
                CurrentExercise = new Exercise(Complex.Exercises[++ExerciseIndex]);
                Application.Current.Windows[0].Content = BuildExercise(CurrentExercise);
            }
            catch (ArgumentOutOfRangeException)
            {
                Timer.Stop();
                MessageBox.Show("The complex was done! Congrats!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBoxResult Result = MessageBox.Show("Question", "Would you like to return to the Complexes page?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    (new ComplexesForm(Methods.SynthesizeComplexes())).Show();
                    Application.Current.Windows[0].Close();
                }
            }
        }

        public void NextOnClick(object Sender, RoutedEventArgs Args)
        {
            if (IsPause) NextExercise();
            else MakeAPause();
        }

        public void PreviousOnClick(object Sender, RoutedEventArgs Args)
        {
            try
            {
                CurrentExercise = new Exercise(Complex.Exercises[--ExerciseIndex]);
                Application.Current.Windows[0].Content = BuildExercise(CurrentExercise);
            }
            catch (ArgumentOutOfRangeException) { }
            if (IsPause) IsPause = false;
        }
    }
}
