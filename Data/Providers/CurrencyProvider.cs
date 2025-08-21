using backend_se.Common.Consts;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Search;

namespace backend_se.Data.Providers
{
    public class CurrencyProvider : IDataProvider<CurrencyModel, CurrencySearch>
    {
        public CurrencyModel? GetById(long id)
        {
            return StaticData.Currencies.FirstOrDefault(x => x.Id == id);
        }

        public CurrencyModel? Add(CurrencyModel model)
        {
            if (StaticData.Currencies.FirstOrDefault(x => x.Id == model.Id) != null)
                return null;

            StaticData.Currencies.Add(model);

            return model;
        }

        public CurrencyModel? Update(CurrencyModel model)
        {
            if (StaticData.Currencies.FirstOrDefault(x => x.Id == model.Id) == null)
                return null;

            var currency = StaticData.Currencies.FirstOrDefault(x => x.Id == model.Id);
            currency = model;

            return currency;
        }

        public CurrencyModel? Save(CurrencyModel model)
        {
            if (StaticData.Currencies.Where(x => x.Id == model.Id) != null)
            {
                var currency = StaticData.Currencies.FirstOrDefault(x => x.Id == model.Id);
                currency = model;
                return currency;
            }

            StaticData.Currencies.Add(model);
            return model;
        }

        public bool Delete(long id)
        {
            var currency = StaticData.Currencies.FirstOrDefault(x => x.Id == id);
            if (currency == null)
                return false;

            StaticData.Currencies.Remove(currency);

            return true;
        }

        public IQueryable<CurrencyModel> GetList(CurrencySearch search)
        {
            return StaticData.Currencies.AsQueryable();
        }
    }
}
