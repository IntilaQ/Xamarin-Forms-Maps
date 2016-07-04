using System;
using System.Linq;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinForms.ViewModels;

namespace MapsApp.Views
{
    public partial class MainPage : ContentPage
    {

        private readonly FoursquareViewModel _foursquareViewModel;
        private Pin _selectedPin;

        public MainPage(FoursquareViewModel foursquareViewModel)
        {
            InitializeComponent();

            _foursquareViewModel = foursquareViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MainMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(new Position(36.89, 10.18),
                Distance.FromKilometers(5)));

            var items = _foursquareViewModel.FoursquareVenues.Response.Groups[0].Items;

            foreach (var item in items)
            {
                var pin = new Pin
                {
                    Position = new Position(
                        item.Venue.Location.Lat,
                        item.Venue.Location.Lng),
                    Label = item.Venue.Name,
                    Address = item.Venue.Location.FormattedAddress[0]
                };

                pin.Clicked += Pin_Clicked;

                MainMap.Pins.Add(pin);
            }
        }

        private void Pin_Clicked(object sender, EventArgs eventArgs)
        {

            _selectedPin = sender as Pin;

            PlaceStackLayout.BindingContext = _foursquareViewModel.FoursquareVenues.Response
                .Groups[0].Items.First(item => item.Venue.Name == _selectedPin?.Label);
            //DisplayAlert(selectedPin?.Label, selectedPin?.Address, "Ok");
        }

        private void GetDirections_Clicked(object sender, EventArgs e)
        {
            CrossExternalMaps.Current.NavigateTo(
                _selectedPin.Label,
                _selectedPin.Position.Latitude,
                _selectedPin.Position.Longitude,
                NavigationType.Driving);
        }
    }
}
