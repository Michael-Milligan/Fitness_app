using System;

namespace Fitness_App
{
    public class Exercise
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public bool MeasuredInTimes { get; private set; }
        public string Type { get; private set; }

        public Exercise(string Type ,string Name, int NumberOfTimes, bool MeasuredInTimes)
        {
            this.Name = Name;
            this.Quantity = NumberOfTimes;
            this.MeasuredInTimes = MeasuredInTimes;
            this.Type = Type;
        }
        public void ChangeQuantity(int NewNumberOfTimes)
        {
            if (NewNumberOfTimes > 0) Quantity = NewNumberOfTimes;
            else throw new ArgumentException("Invalid number of times");
        }
        public Exercise(Exercise Example)
        {
            Name = Example.Name;
            Quantity = Example.Quantity;
            MeasuredInTimes = Example.MeasuredInTimes;
            Type = Example.Type;
        }
    }
}
