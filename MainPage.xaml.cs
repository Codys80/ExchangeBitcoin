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

    public class BitCoin
    {
        public string? chartname { get; set; }
        public BPI bpi { get; set; }
    }
    public class BPI
    {
        public BitRate USD { get; set; }
        public BitRate GBP { get; set; }
        public BitRate EUR { get; set; }

    }
    public class BitRate
    {
        public string code { get; set; }
        public string symbol { get; set; }        
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        public Currency createCurrency(string input)
        {
            string dateToday = Date.Date.ToString("yyyy-MM-dd");
            string url = $"https://api.nbp.pl/api/exchangerates/rates/c/{input}/{dateToday}/?format=json";
            string json;

            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json);
            return c;
        }
        public BitCoin createCurrency(int a) //przysłonięta wersja dla bitcoina
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            string json;

            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            BitCoin c = JsonSerializer.Deserialize<BitCoin>(json);

            return c;
        }
            
        string waluta, compare;
        public MainPage()
        {
            InitializeComponent();
            DateTime dzis =DateTime.Now;
            Date.MaximumDate = dzis;
        }
        private void EurClicked(object sender, EventArgs e)
        {
            //waluta = EntryValue.Text;
            //compare = EntryValueToCompare.Text;
            double value = double.Parse(Value.Text);

            //Currency PLN = createCurrency("PLN");
            double PLN = value;
            Currency USD = createCurrency("USD");
            Currency EUR = createCurrency("EUR");
            BitCoin BIT = createCurrency(0);


            //Currency cc = createCurrency(compare);
            //double exchangeRate;
            //exchangeRate = Convert.ToDouble(BIT.rates[0].bid)/ Convert.ToDouble(EUR.rates[0].bid);
            //value *= exchangeRate;
            string s = $"Nazwa waluty: PLN - Kurs: 1\n";
            s += $"Nazwa waluty: {USD.currency} - Kurs: {USD.rates[0].bid}\n";
            s += $"Nazwa waluty: {EUR.currency} - Kurs: {EUR.rates[0].bid}\n";
            value *= BIT.bpi.EUR.rate_float;
            string r = $"Przeliczenie Bitcoina w EUR: {value}\n";
            value = double.Parse(Value.Text);

            value *= BIT.bpi.USD.rate_float;
            r += $"Przeliczenie Bitcoina w USD: {value}\n";
            value = double.Parse(Value.Text);

            double bitcoinRate = 273532.7349;
            value *= bitcoinRate;
            r += $"Przeliczenie Bitcoina w PLN: {value}\n";
            Head.Text = s;
            Raport.Text = r;
        }
    }
}