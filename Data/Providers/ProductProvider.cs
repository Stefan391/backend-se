using backend_se.Common.Consts;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Search;

namespace backend_se.Data.Providers
{
    public class ProductProvider : IDataProvider<ProductModel, ProductSearch>
    {
        public ProductModel? GetById(long id)
        {
            return StaticData.Products.FirstOrDefault(x => x.Id == id);
        }

        public ProductViewModelDTO? GetViewProduct(long id)
        {
            var product = StaticData.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return null;

            var user = StaticData.Users.FirstOrDefault(x => x.Id == product.UserId);

            var currency = StaticData.Currencies.FirstOrDefault(x => x.Id == product.CurrencyId);

            var city = StaticData.Cities.FirstOrDefault(x => x.Id == user!.CityId);

            var specifications = StaticData.ProductSpecifications.Where(x => x.ProductId == product.Id);
            var productSpecifications = new List<ProductSpecificationView>();

            foreach(var specification in specifications)
            {
                var spec = StaticData.Specifications.FirstOrDefault(x => x.Id == specification.SpecificationId);
                productSpecifications.Add(new ProductSpecificationView
                {
                    Name = spec!.Name,
                    Value = spec.IsBool ? specification.BoolValue.ToString() : specification.Value
                });
            }

            var images = StaticData.ProductImages.Where(x => x.ProductId == product.Id).OrderBy(x => x.DisplayIndex).Select(x => x.ImageUrl).ToList();

            var res = new ProductViewModelDTO
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                UserId = product.UserId,
                Username = user!.Username,
                Created = product.Created,
                Price = product.Price,
                CurrencyId = currency!.Id,
                CurrencyName = currency!.DisplayName,
                CityName = city!.Name,
                Condition = product.Condition,
                ConditionName = ((eProductCondition)product.Condition).ToString(),
                Specifications = productSpecifications,
                Images = images,
                Displayed = product.Displayed
            };

            return res;
        }

        public ProductModel? Add(ProductModel model)
        {
            if (StaticData.Products.FirstOrDefault(x => x.Id == model.Id) != null)
                return null;

            StaticData.Products.Add(model);

            return model;
        }

        public ProductModel? Update(ProductModel model)
        {
            if (StaticData.Products.FirstOrDefault(x => x.Id == model.Id) == null)
                return null;

            var product = StaticData.Products.FirstOrDefault(x => x.Id == model.Id);
            product = model;

            return product;
        }

        public ProductModel? Save(ProductModel model)
        {
            if (StaticData.Products.Where(x => x.Id == model.Id) != null)
            {
                var product = StaticData.Products.FirstOrDefault(x => x.Id == model.Id);
                product = model;
                return product;
            }

            StaticData.Products.Add(model);
            return model;
        }

        public bool Delete(long id)
        {
            var product = StaticData.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return false;

            StaticData.Products.Remove(product);

            return true;
        }

        public IQueryable<ProductModel> GetList(ProductSearch search)
        {
            var res = StaticData.Products.AsQueryable();

            if (search.Displayed.HasValue)
                res = res.Where(x => x.Displayed == search.Displayed);

            if (search.UserId.HasValue)
                res = res.Where(x => x.UserId == search.UserId);

            if (!string.IsNullOrWhiteSpace(search.Name))
                res = res.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));

            if (search.MinPrice.HasValue)
                res = res.Where(x => x.Price >= search.MinPrice);

            if (search.MaxPrice.HasValue)
                res = res.Where(x => x.Price <= search.MaxPrice);

            if (search.CurrencyId.HasValue)
                res = res.Where(x => x.CurrencyId == search.CurrencyId);

            if (search.Condition.HasValue)
                res = res.Where(x => x.Condition == search.Condition);

            if (search.CategoryId.HasValue)
                res = res.Where(x => StaticData.ProductCategories.FirstOrDefault(y => y.CategoryId == search.CategoryId && y.ProductId == x.Id) != null);

            if (search.Specifications.Count > 0)
            {
                foreach (var specification in search.Specifications.Where(x => x.SpecificationValue.Count > 0 || x.IsBool))
                {
                    var temp = StaticData.ProductSpecifications
                             .Where(y => y.SpecificationId == specification.SpecificationId &&
                                             (specification.IsBool ?
                                                (y.BoolValue == true)
                                                : specification.SpecificationValue.Contains(y.Value))
                                             ).AsQueryable();

                    res = res.Where(x => (specification.IsBool && specification.BoolValue == false) ?
                                            temp.FirstOrDefault(y => y.ProductId == x.Id) == null
                                            : temp.FirstOrDefault(y => y.ProductId == x.Id) != null);
                }
            }

            return res;
        }
    }
}
