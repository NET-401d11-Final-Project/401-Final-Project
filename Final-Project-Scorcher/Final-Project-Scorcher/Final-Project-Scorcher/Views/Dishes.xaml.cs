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
            //App.database.DeleteAllRestaurants();

            var results = await App.database.GetAllDishesByYelpId(YelpId);
            if(results.Count <= 0)
            {
               await SeedDishData.SeedRestaurantDataFromYelpId(YelpId);
                results = await App.database.GetAllDishesByYelpId(YelpId);
            }
            DishesList.ItemsSource = results;

        }

        public async void NavigateToRatedDish(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            Label idLabel = (Label)grid.FindByName("Id");
            int id = Convert.ToInt32(idLabel.Text);
            await Navigation.PushAsync(new RatedDish(id));
        }
    }
}