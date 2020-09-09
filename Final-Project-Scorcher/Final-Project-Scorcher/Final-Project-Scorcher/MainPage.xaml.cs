using Final_Project_Scorcher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Final_Project_Scorcher
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AvgLevel.Text))
            {
                var num = Convert.ToDecimal(AvgLevel.Text);
                var dish = await App.database.CreateDish(new Dish
                {
                     AvgLevel = num

                });

                AvgLevel.Text = string.Empty;
                ContactListView.ItemsSource = await App.database.GetAllDishes();
            }
        }
    }
}
