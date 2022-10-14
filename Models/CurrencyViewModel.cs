using Microsoft.AspNetCore.Mvc.Rendering;

namespace SystemGlobalServicesTask.Models
{
    public class CurrencyViewModel
    {
        public string SelectedCurrency { get; set; }
        public List<SelectListItem> CurrencySelectList { get; set; }
    }
}