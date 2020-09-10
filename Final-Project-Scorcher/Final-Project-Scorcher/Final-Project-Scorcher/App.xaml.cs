using Final_Project_Scorcher.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Final_Project_Scorcher.Views;

namespace Final_Project_Scorcher
{
    public partial class App : Application
    {
        public static ScorcherDatabase database;


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
