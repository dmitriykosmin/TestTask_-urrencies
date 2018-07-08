using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestTask_Сurrencies.Models;

namespace TestTask_Сurrencies.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ICurrencyCodeRepository currencyCodeRepository;
        private ICurrencyRateRepository currencyRateRepository;

        private CurrencyRate selectedRateFirst;
        private CurrencyRate selectedRateSecond;

        private string amountForSelectedRateFirst;
        private string amountForSelectedRateSecond;
        private string search;

        public ObservableCollection<CurrencyCode> CurrencyCodes { get; set; }
        public ObservableCollection<CurrencyRate> CurrencyRates { get; set; }
        public ObservableCollection<SearchResult> SearchResults { get; set; }

        public CurrencyRate SelectedRateFirst
        {
            get
            {
                return selectedRateFirst;
            }
            set
            {
                selectedRateFirst = value;
                OnPropertyChanged("SelectedRateFirst");
            }
        }

        public CurrencyRate SelectedRateSecond
        {
            get
            {
                return selectedRateSecond;
            }
            set
            {
                selectedRateSecond = value;
                OnPropertyChanged("SelectedRateSecond");
            }
        }

        public string AmountForSelectedRateFirst
        {
            get
            {
                return amountForSelectedRateFirst;
            }
            set
            {
                amountForSelectedRateFirst = value;
                OnPropertyChanged("AmountForSelectedRateFirst");
                if (SelectedRateFirst != null && SelectedRateSecond != null)
                {
                    var style = NumberStyles.AllowDecimalPoint;
                    var culture = CultureInfo.CreateSpecificCulture("ru-RU");
                    if (Decimal.TryParse(amountForSelectedRateFirst, style,
                        culture, out decimal firstAmount))
                    {
                        if (Decimal.TryParse(SelectedRateFirst.Value, style,
                            culture, out decimal firstCurs) &&
                            Decimal.TryParse(SelectedRateSecond.Value, style,
                            culture, out decimal secondCurs))
                        {
                            firstCurs /= SelectedRateFirst.Nominal;
                            secondCurs /= SelectedRateSecond.Nominal;
                            amountForSelectedRateSecond = Math.Round(
                                (firstAmount * firstCurs / secondCurs), 4
                                ).ToString();
                            OnPropertyChanged("AmountForSelectedRateSecond");
                        }
                    }
                }
            }
        }

        public string AmountForSelectedRateSecond
        {
            get
            {
                return amountForSelectedRateSecond;
            }
            set
            {
                amountForSelectedRateSecond = value;
                OnPropertyChanged("AmountForSelectedRateSecond");
                if (SelectedRateFirst != null && SelectedRateSecond != null)
                {
                    var style = NumberStyles.AllowDecimalPoint;
                    var culture = CultureInfo.CreateSpecificCulture("ru-RU");
                    if (Decimal.TryParse(amountForSelectedRateSecond, style,
                        culture, out decimal secondAmount))
                    {
                        if (Decimal.TryParse(SelectedRateFirst.Value, style,
                            culture, out decimal firstCurs) &&
                            Decimal.TryParse(SelectedRateSecond.Value, style,
                            culture, out decimal secondCurs))
                        {
                            firstCurs /= SelectedRateFirst.Nominal;
                            secondCurs /= SelectedRateSecond.Nominal;
                            amountForSelectedRateFirst = Math.Round(
                                (secondAmount * secondCurs / firstCurs), 4
                                ).ToString();
                            OnPropertyChanged("AmountForSelectedRateFirst");
                        }
                    }
                }
            }
        }

        public string Search
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
                OnPropertyChanged("Search");
            }
        }

        private RelayCommand loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                    (loadCommand = new RelayCommand(obj =>
                    {
                        LoadCurrencyCodesAsync();
                    }));
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                    (updateCommand = new RelayCommand(obj =>
                    {
                        LoadCurrencyRatesAsync();
                    }));
            }
        }

        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                    (searchCommand = new RelayCommand(obj =>
                    {
                        SearchRates();
                    }));
            }
        }

        public MainWindowViewModel(
            ICurrencyCodeRepository currencyCodeRepository,
            ICurrencyRateRepository currencyRateRepository
            )
        {
            this.currencyCodeRepository = currencyCodeRepository;
            this.currencyRateRepository = currencyRateRepository;

            CurrencyCodes = new ObservableCollection<CurrencyCode>();
            CurrencyRates = new ObservableCollection<CurrencyRate>();
            SearchResults = new ObservableCollection<SearchResult>();

            LoadCurrencyCodesAsync();
            LoadCurrencyRatesAsync();
        }

        private async void LoadCurrencyCodesAsync()
        {
            CurrencyCodes.Clear();
            var newCurrencyCodes = await currencyCodeRepository.GetCurrencyCodes();

            if (newCurrencyCodes != null)
            {
                foreach (var code in newCurrencyCodes)
                {
                    CurrencyCodes.Add(code);
                }
            }
        }

        private async void LoadCurrencyRatesAsync()
        {
            CurrencyRates.Clear();
            var newCurrencyRates = await currencyRateRepository.GetCurrencyRates();

            if (newCurrencyRates != null)
            {
                foreach (var code in newCurrencyRates)
                {
                    CurrencyRates.Add(code);
                }

                SelectedRateFirst = CurrencyRates[0];
                SelectedRateSecond = CurrencyRates[1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SearchRates()
        {
            if (!String.IsNullOrWhiteSpace(search))
            {
                var searchResults = CurrencyRates.Where(
                    r => r.Id == search ||
                    r.NumCode == search ||
                    r.CharCode == search ||
                    r.Name.ToLower()
                    .Contains(search.ToLower())
                    );

                SearchResults.Clear();
                foreach (var item in searchResults)
                {
                    SearchResult result = new SearchResult(item);
                    var style = NumberStyles.AllowDecimalPoint;
                    var culture = CultureInfo.CreateSpecificCulture("ru-RU");
                    if (Decimal.TryParse(result.Value, style,
                        culture, out decimal resultCurs))
                    {
                        var dollar = CurrencyRates.FirstOrDefault(
                            r => r.Id == "R01235"
                            );
                        if (dollar != null && Decimal.TryParse(dollar.Value, style,
                        culture, out decimal dollarCurs))
                        {
                            resultCurs /= result.Nominal;
                            dollarCurs /= dollar.Nominal;

                            result.RateToDollar = Math.Round(
                                (resultCurs / dollarCurs), 4
                                ).ToString();
                        }
                    }
                    SearchResults.Add(result);
                }
            }
        }
    }
}
