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
        /// <summary>
        /// the below method allows dish data to present for the selected restaurant
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //App.database.DeleteAllData();
            var restaraunt = await App.database.FindRestarauntYelpId(YelpId);
            RestaurantName.Text = restaraunt.Name;
            var results = await App.database.GetAllDishesByYelpId(YelpId);
            if(results.Count <= 0)
            {
                await SeedDishData.SeedRestaurantDataFromYelpId(YelpId);
                results = await App.database.GetAllDishesByYelpId(YelpId);
            }
            DishesList.ItemsSource = results;

        }
        /// <summary>
        /// the below method transitions to the dish page
        /// </summary>
        /// <param name="sender">the object taht sent the touch event</param>
        /// <param name="e">the sender's arguments</param>
        public async void NavigateToRatedDish(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            Label idLabel = (Label)grid.FindByName("Id");
            int id = Convert.ToInt32(idLabel.Text);
            await Navigation.PushAsync(new RatedDish(id));
        }
        /// <summary>
        /// the below method allows a user to return to home page after arriving at the dish page
        /// </summary>
        /// <param name="sender">the object that sent the touch event</param>
        /// <param name="e">the sender's arguments</param>
        async void NavigateHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}