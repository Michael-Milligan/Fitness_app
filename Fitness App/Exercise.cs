using System;

namespace Fitness_App
{
    public class Exercise
    {
        public string Name { get; private set; }
        public int NumberOfTimes { get; private set; }
        public Exercise(string Name, int NumberOfTimes)
        {
            this.Name = Name;
            this.NumberOfTimes = NumberOfTimes;
        }
        public void ChangeNumberOfTimes(int NewNumberOfTimes)
        {
            if (NewNumberOfTimes > 0) NumberOfTimes = NewNumberOfTimes;
            else throw new ArgumentException("Invalid number of times");
        }
        public Exercise(Exercise Example)
        {
            Name = Example.Name;
            NumberOfTimes = Example.NumberOfTimes;
        }
    }
}
