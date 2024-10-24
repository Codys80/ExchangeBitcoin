using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace waluty
{
    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void EurClicked(object sender, EventArgs e)
        {
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/eur/2024-10-22/?format=json";
            string json;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json);
            string s = $"Nazwa waluty: {c.currency} \n"; ;
            s += $"Kod waluty: {c.code}\n";
            s += $"Date: {c.rates[0].effectiveDate}\n";
            s += $"Cena Skupu: {c.rates[0].bid} zł\n";
            s += $"Cena sprzedaży: {c.rates[0].ask} zł\n";
            Head.Text = s;
        }
        private void UsClicked(object sender, EventArgs e)
        {
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/usd/2024-10-22/?format=json";
            string json;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json);   
            string s = $"Nazwa waluty: {c.currency} \n"; ;
            s += $"Kod waluty: {c.code}\n";
            s += $"Date: {c.rates[0].effectiveDate}\n";
            s += $"Cena Skupu: {c.rates[0].bid} zł\n";
            s += $"Cena sprzedaży: {c.rates[0].ask} zł\n";
            Head.Text = s;
        }

    }
}