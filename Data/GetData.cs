using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;
using SystemGlobalServicesTask.Models;

namespace SystemGlobalServicesTask.Data
{
    public class GetData
    {
        public MainModel mainModel = new MainModel();
        public List<String> propList = new List<String>();
        public List<CurrencyModel> CurrencyModelList = new List<CurrencyModel>();

        private readonly string uri = "https://www.cbr-xml-daily.ru/daily_json.js";

        public async Task GetJsonCBR()
        {
            using var client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.GetAsync(uri);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            mainModel = JsonConvert.DeserializeObject<MainModel>(result);
        }

        public CurrencyViewModel GetViewModel()
        {
            GetJsonCBR().Wait();

            PropertyInfo[] props = mainModel.Valute.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                propList.Add(prop.Name);
                CurrencyModelList.Add((CurrencyModel)prop.GetValue(mainModel.Valute));
            }

            CurrencyViewModel viewModel = new CurrencyViewModel();
            viewModel.CurrencySelectList = new List<SelectListItem>();
            foreach (var item in CurrencyModelList)
            {
                viewModel.CurrencySelectList.Add(new SelectListItem { Text = item.Name, Value = item.CharCode });
            }
            return viewModel;
        }

        public CurrencyModel GetCurrencyModel(string charCode)
        {
            GetJsonCBR().Wait();
            PropertyInfo[] props = mainModel.Valute.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == charCode)
                {
                    return (CurrencyModel)prop.GetValue(mainModel.Valute);
                }
            }
            return null;
        }
    }
}