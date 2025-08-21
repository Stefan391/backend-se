namespace backend_se.Common.Providers
{
    public interface IDataProvider<Model, SC>
    {
        public Model? GetById(long id);
        public Model? Add(Model model);
        public Model? Update(Model model);
        public Model? Save(Model model);
        public bool Delete(long id);
        public IQueryable<Model> GetList(SC search);
    }
}
