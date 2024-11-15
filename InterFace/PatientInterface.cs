namespace Hospital_Management.InterFace
{
    public interface PatientInterface<T> where T : class
    {
        List<T> GetAll(int uid);//returns all the record in the form of Object of List of generic type
        T GetById(object id, int uid); //By taking the value of id, GetById will return a single object of the of the Class Type ('T')
        void Add(T obj, int uid);
        void Update(T obj, int uid);
        void Delete(object id, int uid);
    }
}
