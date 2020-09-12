using Final_Project_Scorcher.Models;
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
    public partial class RatedDish : ContentPage
    {
        public int Id2 { get; set; }

        public RatedDish(int id)
        {
            InitializeComponent();
            Id2 = id;
        }

        /// <summary>
        /// Overriden ContentPage method. Called just before the device renders the XAML page. 
        /// Gets the necessary data to render the RatedDish page and sets it to the DishList.ItemSource property from the XAML page.
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<Dish> dishList = new List<Dish>();
            Dish displayDish = await App.database.GetDish(Id2);
            dishList.Add(displayDish);
            DishList.ItemsSource = dishList;
        }

        /// <summary>
        /// TapGestureRecognizer method when a user taps on the Add Dish Rating button, adds the user's rating to the cache and updates the dishe's data.
        /// </summary>
        /// <param name="sender">
        /// object: the object that sent the tap event. 
        /// </param>
        /// <param name="e">
        /// EventArgs: the sender's arguments.
        /// </param>
        async void OnButtonClicked(object sender, EventArgs e)
        {
            Dish displayDish = await App.database.GetDish(Id2);
            var num = Convert.ToDecimal(DishRating.Text);
            displayDish.TotalVotes += 1;
            displayDish.TotalLevel += num;
            displayDish.AvgLevel = Math.Round(displayDish.TotalLevel / displayDish.TotalVotes, 1);
            displayDish.RestaurantDishOffset = Math.Round(displayDish.AvgLevel - displayDish.Level, 1);
            await App.database.CreateDish(displayDish);
            string yelpId = await App.database.ReturnYelpIdByDish(displayDish.Id);
            await App.database.UpdateRestarauntOffSet(yelpId);
            await Navigation.PopAsync();
        }

        /// <summary>
        /// TapGestureRecognizer method when a user taps on the Scorcher label that brings the user back to the Main page.
        /// </summary>
        /// <param name="sender">
        /// object: the object that sent the tap event. 
        /// </param>
        /// <param name="e">
        /// EventArgs: the sender's arguments.
        /// </param>
        async void NavigateHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}