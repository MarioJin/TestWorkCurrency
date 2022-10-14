using Microsoft.AspNetCore.Mvc;
using SystemGlobalServicesTask.Data;
using SystemGlobalServicesTask.Models;

namespace SystemGlobalServicesTask.Controllers
{
    public class GETController : Controller
    {
        public MainModel mainModel = new MainModel();
        public CurrencyModel currencyModel;
        public GetData data = new GetData();

        public async Task<IActionResult> Currencies(int page = 1)
        {
            int pageSize = 5;

            data.GetViewModel();
            List<CurrencyModel> currencies = data.CurrencyModelList;
            var count = currencies.Count();
            var items = currencies.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Valutes = items
            };
            return View(viewModel);
        }

        public IActionResult Currency()
        {
            var model = data.GetViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Currency(CurrencyViewModel mod)
        {
            CurrencyModel currency = data.GetCurrencyModel(mod.SelectedCurrency);
            var model = data.GetViewModel();
            model.SelectedCurrency = mod.SelectedCurrency;
            ViewBag.SelectedValute = currency ?? new CurrencyModel();
            return View(model);
        }
    }
}