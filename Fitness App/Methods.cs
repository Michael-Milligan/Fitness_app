using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;
using System.Runtime.Serialization.Json;

namespace Fitness_App
{
    public class Methods
    {
        public static void RewriteExercises(ExerciseComplex[] ComplexesData)
        {
            ComplexesData = ComplexesData.OrderBy(item => item.MuscleGroup).ToArray();
            foreach (var Complex in ComplexesData)
            {
                Complex.Exercises = Complex.Exercises.
                    OrderBy(item => item.Type).
                    ToList();
            }

            var json = new DataContractJsonSerializer(typeof(ExerciseComplex[]));
            using (var file = new FileStream("Exercises.json", FileMode.OpenOrCreate))
            {
                json.WriteObject(file, ComplexesData);
            }
        }

        public static ExerciseComplex[] SynthesizeComplexes()
        {
            var json = new DataContractJsonSerializer(typeof(ExerciseComplex[]));
            ExerciseComplex[] Results;
            using (var file = new FileStream("Exercises.json", FileMode.OpenOrCreate))
            {
                Results = json.ReadObject(file) as ExerciseComplex[];
            }
            return Results;
        }
    }
}
