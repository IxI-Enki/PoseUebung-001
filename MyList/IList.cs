namespace MyList;

public interface IList : IEnumerable<object>
{
  int Count { get; }
  object this[ int index ] { get; set; }

  public object GetLast();
  public object[ ] ToArray();
  public int IndexOfValue(object value);
  public bool ContainsValue(object value);

  public void Clear();
  public void RemoveLast();
  public void Add(object item);
  public void Remove(object item);
  public void RemoveAt(int index);
  public void Insert(int index , object item);
  public void CopyTo(Array array , int arrayIndex);

  public new IEnumerator<object> GetEnumerator();
}