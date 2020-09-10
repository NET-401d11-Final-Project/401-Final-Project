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
        public Dishes(string restaurantId)
        {
            InitializeComponent();

            Id2.Text = restaurantId;
        }
    }
}