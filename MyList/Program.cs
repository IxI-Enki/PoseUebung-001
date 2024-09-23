namespace MyList;
internal class Program
{
  static void Main()
  {
    /* With <LangVersion>12</LangVersion>
                LinkedList list = [ "Item One" ];
            is the same as :
                LinkedList list = new LinkedList();
                list.Add("Item One");
        */
    LinkedList list = [ "Item One" ];

    // some ways to Add Elements:
    list.Add("Item Two");
    list.Add(item: "Item Three");
    list.Add(item: "Item Four");
    list.Add(item: "Item Five");
    list.Add(item: "Item Six");
    list.Insert(index: 3 , item: "Item ThreePointFive");

    // some ways to Remove Elements:
    list.Remove(item: "Item Five");
    list.RemoveLast();
    Console.WriteLine("At Index 1: " + list[ 1 ]
      + "\n ..remove..");
    list.RemoveAt(index: 1);

    // print all Elements in a List:
    foreach (var listObject in list)
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