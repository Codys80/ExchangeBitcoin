using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        string waluta, compare;
       
        public MainPage()
        {
            InitializeComponent();
            DateTime dzis =DateTime.Now;
            Date.MaximumDate = dzis;
        }
        private void EurClicked(object sender, EventArgs e)
        {
            waluta = EntryValue.Text;
            compare = EntryValueToCompare.Text;
            double value = double.Parse(Value.Text);
            string dateToday = Date.Date.ToString("yyyy-MM-dd");
            string url = $"https://api.nbp.pl/api/exchangerates/rates/c/{waluta}/{dateToday}/?format=json";
            string json1, json2;
            
            using (var webClient = new WebClient())
            {
                json1 = webClient.DownloadString(url);
            }
            url = $"https://api.nbp.pl/api/exchangerates/rates/c/{compare}/{dateToday}/?format=json";
            using (var webClient = new WebClient())
            {
                json2 = webClient.DownloadString(url);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json1);
            Currency cc = JsonSerializer.Deserialize<Currency>(json2);
            double exchangeRate;
            exchangeRate = Convert.ToDouble(c.rates[0].bid)/ Convert.ToDouble(cc.rates[0].bid);
            value *= exchangeRate;
            string s = $"Nazwa wymiany: {c.currency} / {cc.currency} \n"; 
            s += $"Wartość: ${value}";
            //s += $"Kod waluty: {c.code}\n";
            //s += $"Date: {c.rates[0].effectiveDate}\n";
            //s += $"Cena Skupu: {c.rates[0].bid - cc.rates[0].bid} zł\n";
            //s += $"Cena sprzedaży: {c.rates[0].ask - cc.rates[0].ask} zł\n";


            Head.Text = s;
        }
    }
}