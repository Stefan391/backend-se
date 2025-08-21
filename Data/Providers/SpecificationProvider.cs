using backend_se.Common.Consts;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Search;

namespace backend_se.Data.Providers
{
    public class SpecificationProvider : IDataProvider<SpecificationModel, SpecificationSearch>
    {
        public SpecificationModel? GetById(long id)
        {
            return StaticData.Specifications.FirstOrDefault(x => x.Id == id);
        }

        public SpecificationModel? Add(SpecificationModel model)
        {
            if (StaticData.Specifications.FirstOrDefault(x => x.Id == model.Id) != null)
                return null;

            StaticData.Specifications.Add(model);

            return model;
        }

        public SpecificationModel? Update(SpecificationModel model)
        {
            if (StaticData.Specifications.FirstOrDefault(x => x.Id == model.Id) == null)
                return null;

            var specification = StaticData.Specifications.FirstOrDefault(x => x.Id == model.Id);
            specification = model;

            return specification;
        }

        public SpecificationModel? Save(SpecificationModel model)
        {
            if (StaticData.Specifications.Where(x => x.Id == model.Id) != null)
            {
                var specification = StaticData.Specifications.FirstOrDefault(x => x.Id == model.Id);
                specification = model;
                return specification;
            }

            StaticData.Specifications.Add(model);
            return model;
        }

        public bool Delete(long id)
        {
            var specification = StaticData.Specifications.FirstOrDefault(x => x.Id == id);
            if (specification == null)
                return false;

            StaticData.Specifications.Remove(specification);

            return true;
        }

        public IQueryable<SpecificationModel> GetList(SpecificationSearch search)
        {
            var response = StaticData.Specifications.AsQueryable();

            if (search.CategoryId.HasValue)
            {
                response = response.Where(x => StaticData.SpecificationCategories.FirstOrDefault(y => y.SpecificationId == x.Id && y.CategoryId == search.CategoryId) != null);
            }

            return response;
        }

        public IQueryable<SpecificationSearchDTO> GetSearchList(SpecificationSearch search)
        {
            if (!search.CategoryId.HasValue)
                search.CategoryId = 0;

            var data = StaticData.Specifications.AsQueryable();

            data = data.Where(x => StaticData.SpecificationCategories.FirstOrDefault(y => y.SpecificationId == x.Id && y.CategoryId == search.CategoryId) != null);

            var response = data.Select(x => new SpecificationSearchDTO
            {
                SpecificationId = x.Id,
                Name = x.Name,
                IsBool = x.IsBool,
                Values = x.IsBool ? new List<string>() : StaticData.ProductSpecifications.Where(y => y.SpecificationId == x.Id).Select(y => y.Value).Distinct().ToList()
            });

            return response;
        }
    }
}
