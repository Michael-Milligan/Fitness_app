using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Input;

namespace Fitness_App
{
    public class Methods
    {
        public static void RewriteExercises(ExerciseComplex[] ComplexesData)
        {
            File.Delete(MainWindow.Path);
            FileStream file =  File.Create(MainWindow.Path);
            file.Close();

            ComplexesData = ComplexesData.
                OrderBy(complex => complex.MuscleGroup).
                ToArray();
            
            using (StreamWriter Writer = new StreamWriter(MainWindow.Path))
            {
                foreach (var Complex in ComplexesData)
                {
                    foreach (var exercise in Complex.Exercises)
                    {
                        Writer.WriteLine($"{Complex.MuscleGroup} {exercise.Name} {exercise.NumberOfTimes}");
                    }
                }
            }
            
        }

        public static ExerciseComplex[] SynthesizeComplexes()
        {
            string[][] Data = File.
                ReadAllLines(MainWindow.Path).
                Select(String => String.Split()).
                ToArray();

            Data = Data.OrderBy(data => data[0]).ToArray();

            List<string> Groups = new List<string>();

            foreach (var data in Data)
            {
                if (!Groups.Contains(data[0])) Groups.Add(data[0]);
            }

            ExerciseComplex[] Results = new ExerciseComplex[Groups.Count];

            int j = 0;

            for (int i = 0; i < Data.GetLength(0);++j)
            {
                Results[j] = new ExerciseComplex(Data[i][0]);
                while(i < Data.GetLength(0) && Data[i][0] == Results[j].MuscleGroup)
                {
                    Results[j].Exercises.Add(new Exercise(Data[i][1], Convert.ToInt32(Data[i][2])));
                    ++i;
                }
            }

            return Results;
        }
    }
}
