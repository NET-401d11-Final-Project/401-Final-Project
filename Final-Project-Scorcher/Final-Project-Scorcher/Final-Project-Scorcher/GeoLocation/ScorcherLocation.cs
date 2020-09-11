using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Final_Project_Scorcher.GeoLocation
{
    public class ScorcherLocation
    {
        public async static Task<Location> GetDeviceLocation()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null || LocationIsStale(location))
            {
                location = await Geolocation.GetLocationAsync();
            }
            return location;
        }

        private static bool LocationIsStale(Location location)
        {
            return DateTime.Now.Subtract(location.Timestamp.DateTime.ToLocalTime()).TotalMinutes > 2;
        }
    }
}
