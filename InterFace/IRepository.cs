namespace Hospital_Management.InterFace
{
    public interface IRepository<T> where T : class   //interface is having the specification of Type('T') parameter, where Type('T') should support the constraint of any class
    {
        List<T> GetAll();//returns all the record in the form of Object of List of generic type
        T GetById(object id); //By taking the value of id, GetById will return a single object of the of the Class Type ('T')
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}

