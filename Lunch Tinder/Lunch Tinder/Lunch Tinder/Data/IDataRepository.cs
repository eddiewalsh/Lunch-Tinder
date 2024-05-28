namespace Lunch_Tinder.Data
{
    public interface IDataRepository<T>
    {
        public void Add(T item);
        public void Update(T item);
        public T GetById(int id);
        public List<T> GetAll();
        public void Delete(T item);
    }
}
