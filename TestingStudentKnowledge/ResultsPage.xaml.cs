﻿using System;
using System.IO;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestingStudentKnowledge
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();
            Load();
            ReadFile();
            localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        const string resultsFileName = "ResultsFile.txt";
        string localPath;

        private async void GoToMainMenu(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ReadFile()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(resultsFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    TestLabel.Text = await reader.ReadToEndAsync();
                }
            }
        }

        private void Load()
        {
            TestLabel.Text = File.ReadAllText(Path.Combine(localPath, resultsFileName));
        }

        private void Save()
        {
            File.WriteAllText(localPath, TestLabel.Text);
        }

        // полезный мусор, не понятно как записывать в файл
        // System.IO.File.Delete("/storage/emulated/0/Android/data/com.companyname.app/files/count.txt");

        // adb shell pm grant com.companyname.app android.permission.WRITE_EXTERNAL_STORAGE
        // https://docs.microsoft.com/en-us/xamarin/android/platform/files/external-storage?tabs=windows
        // await Application.Current.SavePropertiesAsync();
    }
}