namespace MyList;

public class LinkedList : IList
{
  #region Embedded Class

  /// <summary>
  /// Represents a single element in the linked list.
  /// </summary>
  private class Element
  {
    /// <summary>
    /// The data stored in the element.
    /// </summary>
    public object Data { get; set; }

    /// <summary>
    /// A reference to the next element in the linked list.
    /// </summary>
    public Element Next { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Element"/> class.
    /// </summary>
    /// <param name="data">The data to store in the element.</param>
    /// <param name="next">A reference to the next element in the linked list (can be null).</param>
    public Element(object data , Element next)
    {
      Data = data;
      Next = next;
    }
  }

  #endregion

  #region Fields

  private Element? _head;  //  Head of the linked list (can be null for an empty list)
  private int _count;      //  Number of elements in the list

  #endregion

  #region Properties

  /// <summary>
  /// Gets the number of elements in the linked list.
  /// </summary>
  public int Count => _count;

  /// <summary>
  /// Gets or sets the element at the specified index in the linked list.
  /// This is an indexer property that allows accessing and modifying elements using a bracket notation (e.g., list[2]).
  /// </summary>
  /// <param name="index">The zero-based index of the element to get or set.</param>
  /// <returns>The data stored at the specified index (on get) or sets the data at the specified index (on set).</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is less than 0 or greater than or equal to the count of elements.</exception>
  public object this[ int index ]
  {
    get
    {
      CheckIndexOutOfRange(index);                 //  Ensure index is valid before accessing elements
      Element element = GetElementByIndex(index);  //  Retrieve the element at the given index
      return element.Data;                         //  Return the data stored in the element
    }
    set
    {
      CheckIndexOutOfRange(index);            //  Ensure index is valid before modifying elements
      GetElementByIndex(index).Data = value;  //  Set the data of the element at the given index
    }
  }

  /// <summary>
  /// Retrieves the element at the specified index in the linked list.
  /// This is a helper method used by the indexer to navigate the linked list and find the element at the desired index.
  /// </summary>
  /// <param name="index">The zero-based index of the element to retrieve.</param>
  /// <returns>The element at the specified index.</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is less than 0 or greater than or equal to the count of elements.</exception>
  private Element GetElementByIndex(int index)
  {
    Element run = _head;
    int counter = 0;
    while (counter < index)
    {
      run = run.Next;     //  raverse the linked list until reaching the desired index
      counter++;
    }
    return run;
  }

  /// <summary>
  /// Checks if the provided index is within the valid range for the linked list.
  /// This is a helper method used by both the indexer and `GetElementByIndex` to avoid potential out-of-bounds errors.
  /// </summary>
  /// <param name="index">The index to validate.</param>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is less than 0 or greater than or equal to the count of elements.</exception>
  private void CheckIndexOutOfRange(int index)
  {
    if (index < 0 || index > Count)
      throw new ArgumentOutOfRangeException(nameof(index));
  }

  #endregion

  #region Constructor

  /// <summary>
  /// Initializes a new instance of the <see cref="LinkedList"/> class.
  /// Creates an empty linked list.
  /// </summary>
  public LinkedList()
  {
    _head = null;
    _count = 0;
  }

  #endregion

  #region Methods
  /// <summary>
  /// Adds a new element to the end of the linked list.
  /// </summary>
  /// <param name="item">The data to store in the new element.</param>
  /// <exception cref="ArgumentNullException">Thrown if the item is null.</exception>
  public void Add(object item)
  {
    /* is the same as:  
          if (item == null)
            throw new ArgumentNullException(nameof(item));   
        */
    ArgumentNullException.ThrowIfNull(item);

    Element newElement = new(item , null);

    if (_head == null)
      _head = newElement;
    else
    {
      Element current = _head;

      while (current.Next != null)
        current = current.Next;

      current.Next = newElement;
    }
    _count++;
  }

  /// <summary>
  /// Removes all elements from the linked list.
  /// </summary>
  public void Clear()
  {
    _head = null;
    _count = 0;
  }

  /// <summary>
  /// Removes the first occurrence of a specified item from the linked list.
  /// </summary>
  /// <param name="item">The item to remove.</param>
  /// <exception cref="ArgumentNullException">Thrown if the item is null.</exception>
  public void Remove(object item)
  {
    //  Ensure item is not null
    if (item == null)
      throw new ArgumentNullException(nameof(item));

    //  If the list is empty, nothing to remove
    if (_head == null)
      return;

    //  Handle removing the head element (special case)
    if (_head.Data.Equals(item))
    {
      _head = _head.Next;    //  Update head reference to skip the removed element
      _count--;
      return;
    }

    //  Iterate through the list to find the item to remove
    Element run = _head;
    while (run.Next != null)    //  Loop until the next element is null (end of list)
    {
      if (run.Next.Data.Equals(item))     //  Check if the next element matches the item to remove
      {
        run.Next = run.Next.Next;   //  Skip the element to remove by setting current.Next to its Next.Next
        _count--;
        return;
      }
      run = run.Next;    //  Move to the next element
    }
  }

  /// <summary>
  /// Removes the element at the specified index from the linked list.
  /// </summary>
  /// <param name="index">The zero-based index of the element to remove.</param>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
  public void RemoveAt(int index)
  {
    // Validate the index to ensure it's within the valid range
    if (index < 0 || index >= Count)
      throw new ArgumentOutOfRangeException(nameof(index) , "Index must be within the range of 0 to Count-1.");

    // Handle the special case of removing the head element
    if (index == 0)
      _head = _head.Next; // Directly update the head reference
    else
    {
      // Find the predecessor of the element to be removed
      Element current = _head;
      for (int i = 0 ; i < index - 1 ; i++)
        current = current.Next;

      // Update the pointers to bypass the element to be removed
      current.Next = current.Next.Next;
    }
    // Decrement the count to reflect the removal
    _count--;
  }
  /// <summary>
  /// Inserts a new element at a specific index in the linked list.
  /// </summary>
  /// <param name="index">The zero-based index at which to insert the element.</param>
  /// <param name="item">The data to store in the new element.</param>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is less than 0 or greater than the count of elements.</exception>
  /// <exception cref="ArgumentNullException">Thrown if the item is null.</exception>
  public void Insert(int index , object item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof(item));

    if (index < 0 || index > Count)
      throw new ArgumentOutOfRangeException(nameof(index));

    Element newElement = new Element(item , null);
    if (_head == null || index == 0)
    {
      newElement.Next = _head;
      _head = newElement;
    }
    else
    {
      Element current = _head;
      int counter = 0;
      while (counter < index - 1)
      {
        current = current.Next;
        counter++;
      }
      newElement.Next = current.Next;
      current.Next = newElement;
    }

    _count++;
  }

  /// <summary>
  /// Copies the elements of the linked list to a new array starting at a specific array index.
  /// </summary>
  /// <param name="array">The destination array.</param>
  /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>   
  /// <exception cref="ArgumentNullException">Thrown if the array is null.</exception>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the arrayIndex is less than 0 or greater than the length of the destination array.</exception>
  /// <exception cref>ArgumentException">Thrown if the number of elements in the linked list is greater than the remaining space in the destination array.</exception>
  public void CopyTo(Array array , int arrayIndex)
  {
    if (array == null)
      throw new ArgumentNullException(nameof(array));

    if (arrayIndex < 0 || arrayIndex > array.Length)
      throw new ArgumentOutOfRangeException(nameof(arrayIndex));

    if (Count > array.Length - arrayIndex)
      throw new ArgumentException("The destination array has insufficient space.");

    Element current = _head;
    int i = arrayIndex;
    while (current != null)
    {
      array.SetValue(current.Data , i);
      current = current.Next;
      i++;
    }
  }

  /// <summary>
  /// Retrieves the data of the last element in the linked list.
  /// </summary>
  /// <returns>The data stored in the last element of the list.</returns>
  /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
  public object GetLast()
  {
    if (_head == null)
      throw new InvalidOperationException("The list is empty.");

    Element current = _head;
    while (current.Next != null)
      current = current.Next;

    return current.Data;
  }

  /// <summary>
  /// Removes the last element from the linked list.
  /// </summary>
  /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
  public void RemoveLast()
  {
    if (_head == null)
      throw new InvalidOperationException("The list is empty.");

    if (_head.Next == null)
    {
      _head = null;
      _count--;
      return;
    }

    Element current = _head;
    while (current.Next.Next != null)
      current = current.Next;

    current.Next = null;
    _count--;
  }

  /// <summary>
  /// Checks if the linked list contains an element with the specified value.
  /// </summary>
  /// <param name="value">The value to search for.</param>
  /// <returns>True if the list contains the value, False otherwise.</returns>
  /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
  public bool ContainsValue(object value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof(value));

    Element current = _head;
    while (current != null)
    {
      if (current.Data.Equals(value))
        return true;

      current = current.Next;
    }
    return false;
  }

  /// <summary>
  /// Gets the index of the first occurrence of a specific value in the linked list.
  /// </summary>
  /// <param name="value">The value to search for.</param>
  /// <returns>The zero-based index of the first occurrence of the value, or -1 if not found.</returns>
  /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
  public int IndexOfValue(object value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof(value));

    int index = 0;
    Element current = _head;
    while (current != null)
    {
      if (current.Data.Equals(value))
        return index;

      current = current.Next;
      index++;
    }
    return -1;
  }

  /// <summary>
  /// Converts all elements of the linked list to an array.
  /// </summary>
  /// <returns>A new array containing the elements of the linked list in the same order.</returns>
  public object[ ] ToArray()
  {
    object[ ] array = new object[ Count ];
    int i = 0;
    Element current = _head;
    while (current != null)
    {
      array[ i ] = current.Data;
      current = current.Next;
      i++;
    }
    return array;
  }
  #endregion

  #region Enumerator
  private class Enumerator : IEnumerator<object>
  {
    private Element _run;
    private Element _head;
    public Enumerator(Element head)
    { _head = head; }
    public object Current => _run.Data;

    public void Dispose() { }

    public bool MoveNext()
    {
      if (_run == null)
        _run = _head;
      else
        _run = _run.Next;
      return _run != null;
    }

    public void Reset() { _run = null; }
  }

  /// <summary>
  /// Implements the IEnumerator interface for iterating over the elements in the linked list.
  /// </summary>
  /// <returns>An enumerator that can be used to iterate over the elements.</returns>
  public IEnumerator GetEnumerator() => new Enumerator(_head);
  IEnumerator<object> IList.GetEnumerator() => new Enumerator(_head);
  IEnumerator<object> IEnumerable<object>.GetEnumerator() => new Enumerator(_head);
  #endregion
}