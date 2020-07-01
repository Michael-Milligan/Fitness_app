using System;
using System.Runtime.Serialization;

namespace Fitness_App
{
    [DataContract]
    class Locale
    {
        [DataMember]
        string[] MainWindowText;
        [DataMember]
        string[] ComplexExerciseFormText;
    }
}
