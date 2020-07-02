using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Fitness_App
{
    [DataContract]
    public class Locale
    {
        [DataMember]
        public string[] MainWindowText;
        [DataMember]
        public string[] ExerciseComplexFormText;
        [DataMember]
        public string[] AddExerciseFormText;
        [DataMember]
        public string[] ComplexesFormText;
        [DataMember]
        public string[] ExerciseExecutionFormText;
        [DataMember]
        public string[] AddComplexFormText;
        public string Type;

        public void GenerateText()
        {
            var json = new DataContractJsonSerializer(typeof(Locale));
            Locale locale;

            using (var file = new FileStream($@"src\locales\{Type}.json", FileMode.Open))
            {
                locale = json.ReadObject(file) as Locale;
            }

            MainWindowText = locale.MainWindowText;
            ExerciseComplexFormText = locale.ExerciseComplexFormText;
            AddExerciseFormText = locale.AddExerciseFormText;
            ComplexesFormText = locale.ComplexesFormText;
            ExerciseExecutionFormText = locale.ExerciseExecutionFormText;
            AddComplexFormText = locale.AddComplexFormText;
        } 
        //For adding languages
        /*
        public void SetText()
        {
            var json = new DataContractJsonSerializer(typeof(Locale));

            using (var file = new FileStream($@"src\locales\{Type}.json", FileMode.OpenOrCreate))
            {
                json.WriteObject(file, this);
            }
        }
       public Locale()
        {
            Type = "ru";
            MainWindowText = new string[] { "Фитнес", "Комплексы упражнений","Смена языка", "Выход", "Вы действительно хотите выйти?", "Вопрос:" };
            ExerciseComplexFormText = new string[] { "Убрать", "Добавить", "Назад" };
            AddExerciseFormText = new string[] { "Добавление упражнения:", "Часть: ", "Название: ", "Кол-во упражнений: ", "Оно измеряется в количестве раз?(1/0): ",
                "Имя файла с расширением: ", "Отправить", "Назад"};
            ComplexesFormText = new string[] { "Комплексы упражнений:", "Изменить", "Назад", "Вы готовы приступить к выполнению?", "Вопрос:"};
            ExerciseExecutionFormText = new string[] {"Комплекс:", "Предыдущее", "Следующее", "Отдыхайте! Оставшееся время:",
                "Комплекс выполнен! Поздравляю!", "Поздравления", "Хотите вернуться к экрану комплексов упражнений?", "Вопрос: "};
        }*/
    }
}
