﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    class MainWindow : Window
    {
        public Locale locale;

        [STAThread]
        static void Main()
        {
            new Application().Run(new MainWindow());
        }

        public MainWindow()
        {
            if (locale == null)
                locale = new Locale() { Type = "en" };
            locale.GenerateText();
            //locale = new Locale();
            //locale.SetText();

            Title = locale.MainWindowText[0];

            StackPanel Panel = new StackPanel();

            #region Exercises Complexes
            Button ExercisesComplexes = new Button();
            ExercisesComplexes.Height = 50;
            ExercisesComplexes.Width = 500;
            ExercisesComplexes.Content = locale.MainWindowText[1];
            ExercisesComplexes.Click += ProceedToCopmlexes;
            Panel.Children.Add(ExercisesComplexes);

            Button LanguageSwitch = new Button();
            LanguageSwitch.Height = 50;
            LanguageSwitch.Width = 500;
            LanguageSwitch.Content = locale.MainWindowText[2];
            LanguageSwitch.Click += LanguageSwitchOnClick;
            Panel.Children.Add(LanguageSwitch);

            Button Exit = new Button();
            Exit.Height = 50;
            Exit.Width = 500;
            Exit.Content = locale.MainWindowText[3];
            Exit.Click += ExitOnClick;
            Panel.Children.Add(Exit);

            #endregion


            Content = Panel;
        }

        public void ProceedToCopmlexes(object Sender, RoutedEventArgs Args)
        {
            ComplexesForm Form = new ComplexesForm(Methods.SynthesizeComplexes());
            Application.Current.Windows[0].Content = Form.Content;
        }

        public void ExitOnClick(object Sender, RoutedEventArgs Args)
        {
            MessageBoxResult Result = 
                MessageBox.Show(locale.MainWindowText[4],
                locale.MainWindowText[5],
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) Application.Current.Shutdown();
        }

        public void LanguageSwitchOnClick(object Sender, RoutedEventArgs Args)
        {
            Application.Current.Windows[0].Content = new SetLocaleForm().Content;
        }

        public void RefreshWindow()
        {
            var Panel = (Content as StackPanel);
            Title = locale.MainWindowText[0];
            (Panel.Children[0] as Button).Content = locale.MainWindowText[1];
            (Panel.Children[1] as Button).Content = locale.MainWindowText[2];
            (Panel.Children[2] as Button).Content = locale.MainWindowText[3];
        }
    }
}
