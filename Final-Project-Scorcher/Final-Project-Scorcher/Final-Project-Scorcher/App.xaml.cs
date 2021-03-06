﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Final_Project_Scorcher.Views;
using Final_Project_Scorcher.Data;

namespace Final_Project_Scorcher
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public static ScorcherDatabase database;

        /// <summary>
        /// Sets the app's main page and its SQLite database.
        /// </summary>
        public App()
        {
            InitializeComponent();

            if (database == null)
            {
                database = new ScorcherDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
            }


            MainPage = new NavigationPage(new Main());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}