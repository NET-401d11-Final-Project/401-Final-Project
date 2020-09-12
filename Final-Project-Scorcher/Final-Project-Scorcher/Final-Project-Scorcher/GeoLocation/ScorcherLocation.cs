using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Final_Project_Scorcher.GeoLocation
{
    public class ScorcherLocation
    {
        /// <summary>
        /// Gets the device's location from Xamarin.Essentials.Geolocation.
        /// </summary>
        /// <returns>
        /// Task<Location>: the device's current location.
        /// </returns>
        public async static Task<Location> GetDeviceLocation()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null || LocationIsStale(location))
            {
                location = await Geolocation.GetLocationAsync();
            }
            return location;
        }

        /// <summary>
        /// Private method to check if the cached location from GetLastKnownLocationAsync() is more than 2 minutes old.
        /// </summary>
        /// <param name="location">
        /// Location: a Location object.
        /// </param>
        /// <returns>
        /// bool: true if the location is more than 2 minutes old, false otherwise.
        /// </returns>
        private static bool LocationIsStale(Location location)
        {
            return DateTime.Now.Subtract(location.Timestamp.DateTime.ToLocalTime()).TotalMinutes > 2;
        }
    }
}
