using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class SetLocaleForm : Window
    {
        public SetLocaleForm()
        {
            Title = "Changing the interface language";

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Label Question = new Label();
            Question.Content = "In which language would you like to see app?";
            grid.Children.Add(Question);
            Grid.SetRow(Question, 0);
            Grid.SetColumnSpan(Question, 2);

            Button en = new Button();
            en.Content = "English";
            en.Click += enOnClick;
            grid.Children.Add(en);
            Grid.SetRow(en, 1);
            Grid.SetColumn(en, 0);

            Button ru = new Button();
            ru.Content = "Russian";
            ru.Click += ruOnClick;
            grid.Children.Add(ru);
            Grid.SetRow(ru, 1);
            Grid.SetColumn(ru, 1);

            Content = grid;
        }

        public void enOnClick(object Sender, RoutedEventArgs Args)
        {
            var Form = new MainWindow() {locale = new Locale() {Type = "en"}};
            Form.locale.GenerateText();
            Form.RefreshWindow();
            Application.Current.Windows[0].Content = Form.Content;
        }

        public void ruOnClick(object Sender, RoutedEventArgs Args)
        {
            var Form = new MainWindow() { locale = new Locale() {Type = "ru"} };
            Form.locale.GenerateText();
            Form.RefreshWindow();
            Application.Current.Windows[0].Content = Form.Content;
        }
    }
}
