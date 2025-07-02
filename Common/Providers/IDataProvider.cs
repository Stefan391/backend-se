namespace backend_se.Common.Providers
{
    public interface IDataProvider<Model>
    {
        public Model? GetById(long id);
        public Model? Add(Model model);
        public Model? Update(Model model);
        public bool Delete(long id);
        public List<Model> GetAll();
    }
}
