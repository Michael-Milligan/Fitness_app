using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;

namespace Fitness_App
{
    [DataContract]
    public class ExerciseComplex
    {
        [DataMember]
        public List<Exercise> Exercises { get; set; }

        [DataMember]
        public string MuscleGroup { get; set; }

        public ExerciseComplex(string MuscleGroup)
        {
            this.MuscleGroup = MuscleGroup;
            Exercises = new List<Exercise>();
        }

        public void AddExercise(Exercise NewExercise)
        {
            Exercises.Add(NewExercise);
        }

        public void RemoveExercise(Exercise ExerciseToRemove)
        {
            Exercises.Remove(ExerciseToRemove);
        }
    }
}
