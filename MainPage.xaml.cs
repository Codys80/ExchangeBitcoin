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
        string waluta;
        /*************************************************************************************************************
        // nazwa funkcji:       MainPage
        // parametry wejściowe: brak
        // wartość zwracana:    brak
        // informacje:          funkcja inicjuje elementy graficznego interfejsu użytkownika oraz ustala maksymalną datę
        //                      uniemożliwiając błędne pobranie api z nieistniejącego dnia
        // autor:               Bartosz Semczuk
        ***************************************************************************************************************/
        public MainPage()
        {
            InitializeComponent();
            DateTime dzis =DateTime.Now;
            Date.MaximumDate = dzis;
        }
        /*************************************************************************************************************
        // nazwa funkcji:       EurClicked
        // parametry wejściowe: obiekt wywołujący funkcje
        // wartość zwracana:    brak, funkcja typu void
        // informacje:          funkcja pobiera kod waluty wprowadzony przez użytkownika końcowego w polu entry po 
        //                      przez naciśnięcie przycisku, pobiera informacje o walucie z oficjalnej strony NBP 
        //                      i wyświetla informacje o niej
        // autor:               Bartosz Semczuk
        ***************************************************************************************************************/
        private void EurClicked(object sender, EventArgs e)
        {
            waluta = EntryValue.Text;
            string dateToday = Date.Date.ToString("yyyy-MM-dd");
            string url = $"https://api.nbp.pl/api/exchangerates/rates/c/{waluta}/{dateToday}/?format=json";
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