using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using Final_Project_Scorcher.APIs;
using Final_Project_Scorcher.GeoLocation;
using Final_Project_Scorcher.Models;
using Yelp.Api.Models;

namespace Final_Project_Scorcher.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        public int MyProperty { get; set; }
        
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overriden ContentPage method. Called just before the device renders the XAML page. 
        /// Assigns the XAML page's ItemSource to a list of Restaurant objects with a blank search term.
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            RestaurantList.ItemsSource = await GetRestaurantsByLocation("");
        }

        /// <summary>
        /// Conducts a search of cached or Yelp data using the search term from the XAML search bar, and assigns it to the XAML ItemSource property.
        /// </summary>
        /// <param name="sender">
        /// object: the object that sent the touch event.
        /// </param>
        /// <param name="e">
        /// EventArgs e: the sender's arguments.
        /// </param>
        void NewSearch(object sender, EventArgs e)
        {
            NewSearchAsync(sender, e);
        }

        /// <summary>
        /// Private asynchronous method to get cached or Yelp data with the sender's search term.
        /// </summary>
        /// <param name="sender">
        /// object: the object that sent the touch event.
        /// </param>
        /// <param name="e">
        /// EventArgs e: the sender's arguments.
        /// </param>
        private async void NewSearchAsync(object sender, EventArgs e)
        {
            SearchBar bar = (SearchBar)sender;
            RestaurantList.ItemsSource = await GetRestaurantsByLocation(bar.Text);
        }
        
        /// <summary>
        /// Gets restaurants from the cache or Yelp, updates cached data with Yelp data, and returns List of that data normalized into Restaurant objects.
        /// </summary>
        /// <param name="searchTerm">
        /// string: a search term
        /// </param>
        /// <returns>
        /// List<Restaurant>: the collection of normalized restaurants for the given search term.
        /// </returns>
        private async Task<List<Restaraunt>> GetRestaurantsByLocation(string searchTerm)
        {
            Xamarin.Essentials.Location location = await ScorcherLocation.GetDeviceLocation();
            List<Restaraunt> restarauntsWithinRadius = await App.database.GetAllRestarauntsByLocation(location.Latitude, location.Longitude, searchTerm);

            IList<BusinessResponse> yelpRestaurants = await SearchYelp(location, searchTerm);
            foreach (BusinessResponse oneBiz in yelpRestaurants)
            {
                Restaraunt restaraunt = new Restaraunt()
                {
                    Date = DateTime.Now,
                    Lat = oneBiz.Coordinates.Latitude,
                    Lon = oneBiz.Coordinates.Longitude,
                    Name = oneBiz.Name,
                    Address = oneBiz.Location.Address1,
                    City = oneBiz.Location.City,
                    State = oneBiz.Location.State,
                    Zip = oneBiz.Location.ZipCode,
                    YelpId = oneBiz.Id,
                    YelpCategory = searchTerm,
                    ImageUrl = oneBiz.ImageUrl,
                    LevelMax = 5,
                    LevelMin = 1,
                    RestarauntOffset = 0.0m
                };
                Restaraunt dbRestaurant = await App.database.FindRestarauntYelpId(restaraunt.YelpId);
                if (dbRestaurant == null)
                {
                    await App.database.CreateRestaraunt(restaraunt);
                    restarauntsWithinRadius.Add(restaraunt);
                }
                else
                {
                    if (DateTime.Now.Subtract(dbRestaurant.Date).TotalDays > 3)
                    {
                        restaraunt.RestarauntOffset = dbRestaurant.RestarauntOffset;
                        await App.database.UpdateRestaraunt(restaraunt);
                    }
                }
            }
            return restarauntsWithinRadius;
        }

        /// <summary>
        /// Private method. Performs the search of Yelp using the parameter location and search term.
        /// </summary>
        /// <param name="location">
        /// Location: the device's location.
        /// </param>
        /// <param name="search">
        /// string: the search term.
        /// </param>
        /// <returns>
        /// IList<BusinessResponse>: a collection of BusinessResponse objects from Yelp.Api.
        /// </returns>
        private async Task<IList<Yelp.Api.Models.BusinessResponse>> SearchYelp(Xamarin.Essentials.Location location, string search)
        {
            return await YelpAPIAccess.GetYelpDataAsync(location, search);
        }
        
        public async void NavigateToDishesAsync(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            Label idLabel = (Label)grid.FindByName("Id");
            string id = idLabel.Text;
            await Navigation.PushAsync(new Dishes(id));
        }
    }
}