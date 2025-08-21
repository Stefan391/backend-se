using backend_se.Common.Consts;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Search;

namespace backend_se.Data.Providers
{
    public class CategoryProvider : IDataProvider<CategoryModel, CategorySearch>
    {
        public CategoryModel? GetById(long id)
        {
            return StaticData.Categories.FirstOrDefault(x => x.Id == id);
        }

        public CategoryModel? Add(CategoryModel model)
        {
            if (StaticData.Categories.FirstOrDefault(x => x.Id == model.Id) != null)
                return null;

            StaticData.Categories.Add(model);

            return model;
        }

        public CategoryModel? Update(CategoryModel model)
        {
            if (StaticData.Categories.FirstOrDefault(x => x.Id == model.Id) == null)
                return null;

            var category = StaticData.Categories.FirstOrDefault(x => x.Id == model.Id);
            category = model;

            return category;
        }

        public CategoryModel? Save(CategoryModel model)
        {
            if (StaticData.Categories.Where(x => x.Id == model.Id) != null)
            {
                var category = StaticData.Categories.FirstOrDefault(x => x.Id == model.Id);
                category = model;
                return category;
            }

            StaticData.Categories.Add(model);
            return model;
        }

        public bool Delete(long id)
        {
            var category = StaticData.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
                return false;

            StaticData.Categories.Remove(category);

            return true;
        }

        public IQueryable<CategoryModel> GetList(CategorySearch search)
        {
            return StaticData.Categories.AsQueryable();
        }
    }
}
