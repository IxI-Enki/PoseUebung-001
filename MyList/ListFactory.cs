namespace MyList;

internal class ListFactory
{
  public static IList Create() => new LinkedList();
}