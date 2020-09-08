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
            restaurantList.ItemsSource = await SearchYelp(bar.Text);
        }

        private async Task<IList<Yelp.Api.Models.BusinessResponse>> SearchYelpNoTerm()
        {
            Location location = await ScorcherLocation.GetDeviceLocation();
            return await YelpAPIAccess.GetYelpDataAsync(location, "");
        }

        private async Task<IList<Yelp.Api.Models.BusinessResponse>> SearchYelp(string search)
        {
            Location location = await ScorcherLocation.GetDeviceLocation();
            return await YelpAPIAccess.GetYelpDataAsync(location, search);
        }
    }
}