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
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<Dish> dishList = new List<Dish>();
            Dish displayDish = await App.database.GetDish(Id2);
            dishList.Add(displayDish);
            DishList.ItemsSource = dishList;
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            Dish displayDish = await App.database.GetDish(Id2);
            var num = Convert.ToDecimal(DishRating.Text);
            displayDish.TotalVotes += 1;
            displayDish.TotalLevel += num;
            displayDish.AvgLevel = Math.Round(displayDish.TotalLevel / displayDish.TotalVotes, 1);
            displayDish.RestaurantDishOffset = Math.Round(displayDish.Level - displayDish.AvgLevel, 1);
            await App.database.CreateDish(displayDish);
            string yelpId = await App.database.ReturnYelpIdByDish(displayDish.Id);
            await App.database.UpdateRestarauntOffSet(yelpId);
            await Navigation.PushAsync(new Dishes(yelpId));
        }
    }
}