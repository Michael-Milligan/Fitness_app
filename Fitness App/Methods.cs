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
        static string Path = $@"src\Exercises{Info.locale.Type.ToUpper()}.json";
        public static void RewriteExercises(ExerciseComplex[] ComplexesData)
        {

            var json = new DataContractJsonSerializer(typeof(ExerciseComplex[]));
            using (var file = new FileStream(Path, FileMode.Create))
            {
                json.WriteObject(file, ComplexesData);
            }
        }

        public static ExerciseComplex[] SynthesizeComplexes()
        {
            var json = new DataContractJsonSerializer(typeof(ExerciseComplex[]));
            ExerciseComplex[] Results;
            using (var file = new FileStream(Path, FileMode.OpenOrCreate))
            {
                Results = json.ReadObject(file) as ExerciseComplex[];
            }
            return Results;
        }

        public static void RefreshPath()
        {
            Path = $@"src\Exercises{Info.locale.Type.ToUpper()}.json";
        }
    }
}
