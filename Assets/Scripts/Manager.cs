using System.Collections;
public interface Manager<T> where T : class{
    IEnumerable GetAll();
    T Get(int id);
    void Add(T t);
    void Remove(int id);
    void RemoveAll();
}
