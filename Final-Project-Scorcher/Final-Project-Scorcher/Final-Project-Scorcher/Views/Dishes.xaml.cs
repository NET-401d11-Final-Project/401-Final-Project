using Final_Project_Scorcher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_Project_Scorcher.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dishes : ContentPage
    {
        public string YelpId { get; set; }
        public Dishes(string restaurantId)
        {
            InitializeComponent();
            YelpId = restaurantId;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var results = await App.database.GetAllDishesByYelpId(YelpId);
            if(results.Count <= 0)
            {
                SeedDishData.SeedRestaurantDataFromYelpId(YelpId);
                results = await App.database.GetAllDishesByYelpId(YelpId);
            }
            DishesList.ItemsSource = results;

        }

        public void NavigateToRatedDish(object sender, EventArgs e)
        {

        }
    }
}