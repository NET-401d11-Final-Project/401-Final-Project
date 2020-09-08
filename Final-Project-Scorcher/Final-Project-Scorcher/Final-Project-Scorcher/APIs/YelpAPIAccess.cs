using System;
using System.Collections.Generic;
using System.Text;
using Yelp;
using Yelp.Api.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Final_Project_Scorcher.APIs
{
    public class YelpAPIAccess
    {
        public static async Task<IList<BusinessResponse>> GetYelpDataAsync(Xamarin.Essentials.Location location, string searchTerm)
        {
            var client = new Yelp.Api.Client("jJiKkWpvA9kYhAZlhAcSOLg4ahhk7VAxeraPSe5uhVYSIalCkap_3fiJ0jv51RYqDHDupu_f3aKFkzcQYeK0f3YZk7wbpuCumPYJSw2XAsNRLTOYxtiBsdulQkJVX3Yx");
            SearchRequest request = new SearchRequest();
            request.Latitude = location.Latitude;
            request.Longitude = location.Longitude;
            //10 miles as meters
            request.Radius = 16093;
            request.MaxResults = 10;
            request.Categories = "restaurants";
            request.Term = searchTerm;

            SearchResponse results = await client.SearchBusinessesAllAsync(request);
            return results.Businesses;
        }
    }
}