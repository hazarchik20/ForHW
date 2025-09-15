using BLL;
using BLL.Responses;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IWeatherApiSerice _weatherApiService;
        private readonly ICityApiService _cityApiService;
        private int _counter = 0;
        private double _lat ;
        private double _lon ;
        City[] _listCity;
       

        public MainWindow()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("jsconfig1.json")
                .Build();
            _weatherApiService = new WeatherApiService(config);
            _cityApiService = new CityApiService(config);
            InitializeComponent();
        }

        private async void ShowCurrentWeatherBT_Click(object sender, RoutedEventArgs e)
        {
            this.weatherInfoSP.Visibility = Visibility.Visible;
            this.imgSP.Visibility = Visibility.Visible;
            var weather = await _weatherApiService.GetCurrentWeatherAsync(_lat, _lon);
            if ((sender as Button).Name == "NextPageBT")
            {
                _counter++;
                if (_counter == 40)
                    _counter = 0;
            }
            if ((sender as Button).Name == "PreviousPageBT")
            {
                _counter--;
                if (_counter == -1)
                    _counter = 39;
            }
            weatherNameTb.Text = weather.list[_counter].Weather[0].Main;
            tempTb.Text = $"temper: {Convert.ToInt32(weather.list[_counter].Main.Temp - 273)} C\n pressure:{weather.list[_counter].Main.pressure}\n humidity:{weather.list[_counter].Main.humidity}";
            WindTb.Text = $"Wind:  {weather.list[_counter].Wind.speed} m/s\n {weather.list[_counter].Wind.deg} (deg)\"";
            weatherImg.Source = new BitmapImage(new Uri($"https://openweathermap.org/img/wn/{weather.list[_counter].Weather[0].Icon}@4x.png"));
            DateTime dt = DateTime.Parse(weather.list[_counter].dt_txt);
            DateTb.Text = dt.ToString("yyyy-MM-dd");
            TimeTb.Text = dt.ToString("HH:mm:ss");

        }
        private async void LoadCityNameSP(object sender, RoutedEventArgs e)
        {
            var cityEU = await _cityApiService.GetCurrentCityAsync("europe");
            var cityAM = await _cityApiService.GetCurrentCityAsync("america");
            for (int i = 0; i < cityEU.Length; i++)
            {
                Button btn = new Button();
                btn.Click += UserChoiseCurrentCitynameBT_Click;
                btn.Content = $"{cityEU[i].name.common}|{cityEU[i].latlng[0]}||{cityEU[i].latlng[1]}|"; 
                ForCityNameSP.Children.Add(btn);
            }

            for (int i = 0; i < cityAM.Length; i++)
            {
                Button btn = new Button();
                btn.Click += UserChoiseCurrentCitynameBT_Click;
                btn.Content = $"{cityAM[i].name.common}|{cityAM[i].latlng[0]}||{cityAM[i].latlng[1]}|";
                ForCityNameSP.Children.Add(btn);
            }
        }
        private async void UserChoiseCurrentCitynameBT_Click(object sender, RoutedEventArgs e)
        {
            _lat = Convert.ToDouble((sender as Button).Content.ToString().Split('|')[1]);
            _lon = Convert.ToDouble((sender as Button).Content.ToString().Split('|')[3]);

        }
    }
}