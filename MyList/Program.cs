namespace MyList;
internal class Program
{
  static void Main()
  {
    /* With <LangVersion>12</LangVersion>
                LinkedList linkedList = [ "Item One" ];
            is the same as :
                LinkedList linkedList = new LinkedList();
                linkedList.Add("Item One");
        */
    LinkedList linkedList = [ "Item One" ];

    // some ways to Add Elements:
    linkedList.Add("Item Two");
    linkedList.Add(item: "Item Three");
    linkedList.Add(item: "Item Four");
    linkedList.Add(item: "Item Five");
    linkedList.Add(item: "Item Six");
    linkedList.Insert(index: 3 , item: "Item ThreePointFive");

    // some ways to Remove Elements:
    linkedList.Remove(item: "Item Five");
    linkedList.RemoveLast();
    Console.WriteLine("At Index 1: " + linkedList[ 1 ]
      + "\n ..remove..");
    linkedList.RemoveAt(index: 1);

    // print all Elements in a List:
    foreach (var listObject in linkedList)
      Console.WriteLine(listObject);

    /// USE OF FACTORY PATTERN:
    IList factoryList = ListFactory.Create();
    factoryList.Add("First Item");
    factoryList.Add(item: "Second Item");

    Console.WriteLine("\n\nFactory Pattern:");
    foreach (var item in factoryList)
      Console.WriteLine(item);
  }
}