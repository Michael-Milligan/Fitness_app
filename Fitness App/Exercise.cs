using System;
using System.Runtime.Serialization;

namespace Fitness_App
{
    [DataContract]
    public class Exercise
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public int Quantity { get; private set; }
        [DataMember]
        public bool MeasuredInTimes { get; private set; }
        [DataMember]
        public string Type { get; private set; }
        [DataMember]
        public string PathToPicture { get; private set; }

        public Exercise(string type ,string name, int NumberOfTimes, bool measuredInTimes, string path)
        {
            Name = name;
            Quantity = NumberOfTimes;
            MeasuredInTimes = measuredInTimes;
            Type = type;
            PathToPicture = path;
        }
        public void ChangeQuantity(int NewNumberOfTimes)
        {
            if (NewNumberOfTimes > 0) Quantity = NewNumberOfTimes;
            else throw new ArgumentException("Неверное кол-во раз");
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
