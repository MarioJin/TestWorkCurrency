namespace SystemGlobalServicesTask.Models
{
    public class MainModel
    {
        public string Date { get; set; }

        public string PreviousDate { get; set; }

        public string? PreviousURL { get; set; }

        public string Timestamp { get; set; }

        public CurrencyModels Valute { get; set; }
    }
}