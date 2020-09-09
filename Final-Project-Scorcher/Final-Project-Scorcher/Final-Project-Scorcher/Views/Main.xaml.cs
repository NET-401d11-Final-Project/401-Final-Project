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
using Final_Project_Scorcher.ViewModels;
using Final_Project_Scorcher.Models;
namespace Final_Project_Scorcher.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        public Main()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            restaurantList.ItemsSource = await SearchYelpNoTerm();
        }

        void NewSearch(object sender, EventArgs e)
        {
            NewSearchAsync(sender, e);
        }

        private async void NewSearchAsync(object sender, EventArgs e)
        {
            SearchBar bar = (SearchBar)sender;
            var yelp = await SearchYelp(bar.Text);
            var location = await ScorcherLocation.GetDeviceLocation();
            var restaraunts = await App.database.GetAllRestarauntsByLocation(location.Latitude, location.Longitude);
            restaurantList.ItemsSource = restaraunts;
                foreach(var item in yelp)
                {
                    Restaraunt restaraunt = new Restaraunt()
                    {
                        Date = DateTime.Now,
                        Lat = item.Coordinates.Latitude,
                        Lon = item.Coordinates.Longitude,
                        Name = item.Name,
                        Address = item.Location.Address1,
                        City = item.Location.City,
                        State = item.Location.State,
                        Zip = item.Location.ZipCode,
                        YelpId = item.Id,
                        ImageUrl = item.ImageUrl,
                        LevelMax = 5,
                        LevelMin = 1,
                        Description = "I am such a spicey restaraunt, please bring your own milk!!!!"

                    };
                    var id = await App.database.FindRestarauntYelpId(item.Id);
                    if(id == null)
                    {
                    await App.database.CreateRestaraunt(restaraunt);

                    }
                    if(DateTime.Now.Subtract(id.Date).TotalDays > 3)
                {
                    await App.database.UpdateRestaraunt(restaraunt);
                }
                    
                }
                restaurantList.ItemsSource = await App.database.GetAllRestarauntsByLocation(location.Latitude, location.Longitude);
            
        }

        private async Task<IList<Yelp.Api.Models.BusinessResponse>> SearchYelpNoTerm()
        {
            return await SearchYelp("");
        }

        private async Task<IList<Yelp.Api.Models.BusinessResponse>> SearchYelp(string search)
        {
            Location location = await ScorcherLocation.GetDeviceLocation();
            return await YelpAPIAccess.GetYelpDataAsync(location, search);
        }
    }
}