using System.Collections.Generic;
using System.Dynamic;

namespace Fitness_App
{
    public class ExerciseComplex
    {
        public List<Exercise> Exercises { get; private set; }

        public string MuscleGroup { get;}

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
