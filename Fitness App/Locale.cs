using System;
using System.Runtime.Serialization;

namespace Fitness_App
{
    [DataContract]
    public class Locale
    {
        [DataMember]
        public string[] MainWindowText;
        [DataMember]
        public string[] ComplexExerciseFormText;
        public string Type;

        public void GenerateText()
        {

        }
    }
}
