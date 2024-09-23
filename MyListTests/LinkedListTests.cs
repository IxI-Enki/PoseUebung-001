using MyList;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyListTests;

[TestClass]
public class LinkedListTests
{
  private LinkedList? _list;

  [TestInitialize]
  public void TestInitialize() => _list = [ ];

  #region Count Tests
  [TestMethod]
  public void Count_EmptyList_ReturnsZero()
  {
    Assert.AreEqual(0 , _list.Count);
  }

  [TestMethod]
  public void Count_SingleItem_ReturnsOne()
  {
    _list.Add("item");
    Assert.AreEqual(1 , _list.Count);
  }

  [TestMethod]
  public void Count_MultipleItems_ReturnsCorrectCount()
  {
    _list.Add("item1");
    _list.Add("item2");
    _list.Add("item3");
    Assert.AreEqual(3 , _list.Count);
  }

  [TestMethod]
  public void Count_AfterRemoval_DecreasesCorrectly()
  {
    _list.Add("item1");
    _list.Add("item2");
    _list.Remove("item1");
    Assert.AreEqual(1 , _list.Count);
  }

  [TestMethod]
  public void Count_AfterClear_ReturnsZero()
  {
    _list.Add("item1");
    _list.Add("item2");
    _list.Clear();
    Assert.AreEqual(0 , _list.Count);
  }
  #endregion

  #region Add Tests
  [TestMethod]
  public void Add_NullItem_DoesNotThrowException()
  {
    Assert.ThrowsException<ArgumentNullException>(() => _list.Add(null));
  }

  [TestMethod]
  public void Add_MultipleItems_AddsAllItems()
  {
    _list.Add("item1");
    _list.Add("item2");
    _list.Add("item3");
    Assert.AreEqual(3 , _list.Count);
  }

  [TestMethod]
  public void Add_DuplicateItems_AddsAllItems()
  {
    _list.Add("item");
    _list.Add("item");
    Assert.AreEqual(2 , _list.Count);
  }

  [TestMethod]
  public void Add_LargeNumberOfItems_AddsAllItems()
  {
    for (int i = 0 ; i < 1000 ; i++)
    {
      _list.Add(i);
    }
    Assert.AreEqual(1000 , _list.Count);
  }
  #endregion

  #region Clear Tests
  [TestMethod]
  public void Clear_EmptyList_DoesNotThrowException()
  {
    _list.Clear();
    Assert.AreEqual(0 , _list.Count);
  }

  [TestMethod]
  public void Clear_NonEmptyList_RemovesAllItems()
  {
    _list.Add("item1");
    _list.Add("item2");
    _list.Clear();
    Assert.AreEqual(0 , _list.Count);
  }

  [TestMethod]
  public void Clear_CalledMultipleTimes_DoesNotThrowException()
  {
    _list.Add("item");
    _list.Clear();
    _list.Clear();
    Assert.AreEqual(0 , _list.Count);
  }
  #endregion

  #region Remove Tests
  [TestMethod]
  public void Remove_ExistingItem_RemovesItem()
  {
    _list.Add("item");
    _list.Remove("item");
    Assert.AreEqual(0 , _list.Count);
  }

  [TestMethod]
  public void Remove_NonExistingItem_DoesNotChangeList()
  {
    _list.Add("item");
    _list.Remove("non-existing");
    Assert.AreEqual(1 , _list.Count);
  }

  [TestMethod]
  public void Remove_FromEmptyList_DoesNotThrowException()
  {
    _list.Remove("item");
    Assert.AreEqual(0 , _list.Count);
  }

  [TestMethod]
  public void Remove_NullItem_ThrowsArgumentNullException()
  {
    Assert.ThrowsException<ArgumentNullException>(() => _list.Remove(null));
  }

  [TestMethod]
  public void Remove_DuplicateItems_RemovesFirstOccurrence()
  {
    _list.Add("item");
    _list.Add("item");
    _list.Remove("item");
    Assert.AreEqual(1 , _list.Count);
  }

  [TestMethod]
  public void Remove_AllItems_ResultsInEmptyList()
  {
    _list.Add("item1");
    _list.Add("item2");
    _list.Add("item3");
    _list.Remove("item1");
    _list.Remove("item2");
    _list.Remove("item3");
    Assert.AreEqual(0 , _list.Count);
  }

  #endregion

  #region GetLast Tests
  [TestMethod]
  public void GetLast_NonEmptyList_ReturnsLastElement()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add(1);
    linkedList.Add(2);

    // Act
    object lastItem = linkedList.GetLast();

    // Assert
    Assert.AreEqual(2 , lastItem);
  }

  [TestMethod]
  [ExpectedException(typeof(InvalidOperationException))]
  public void GetLast_EmptyList_ThrowsInvalidOperationException()
  {
    // Arrange
    var linkedList = new LinkedList();

    // Act
    linkedList.GetLast();

    // Assert (reached by ExpectedException)
  }

  [TestMethod]
  public void GetLast_SingleElementList_ReturnsElement()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add("Item");

    // Act
    object lastItem = linkedList.GetLast();

    // Assert
    Assert.AreEqual("Item" , lastItem);
  }
  #endregion

  #region RemoveLast Tests
  [TestMethod]
  public void RemoveLast_NonEmptyList_RemovesLastElement()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add(1);
    linkedList.Add(2);

    // Act
    linkedList.RemoveLast();

    // Assert
    Assert.AreEqual(1 , linkedList.Count);
    Assert.AreEqual(1 , linkedList[ 0 ]);
  }

  [TestMethod]
  [ExpectedException(typeof(InvalidOperationException))]
  public void RemoveLast_EmptyList_ThrowsInvalidOperationException()
  {
    // Arrange
    var linkedList = new LinkedList();

    // Act
    linkedList.RemoveLast();

    // Assert (reached by ExpectedException)
  }

  [TestMethod]
  public void RemoveLast_SingleElementList_RemovesElementAndSetsHeadToNull()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add("Item");

    // Act
    linkedList.RemoveLast();

    // Assert
    Assert.AreEqual(0 , linkedList.Count);
  }
  #endregion

  #region ContainsValue Tests
  [TestMethod]
  public void ContainsValue_ExistingValue_ReturnsTrue()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add(1);
    linkedList.Add(2);

    // Act
    bool containsValue = linkedList.ContainsValue(2);

    // Assert
    Assert.IsTrue(containsValue);
  }

  [TestMethod]
  public void ContainsValue_NonExistingValue_ReturnsFalse()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add(1);
    linkedList.Add(2);

    // Act
    bool containsValue = linkedList.ContainsValue(3);

    // Assert
    Assert.IsFalse(containsValue);
  }

  [TestMethod]
  [ExpectedException(typeof(ArgumentNullException))]
  public void ContainsValue_NullValue_ThrowsArgumentNullException()
  {
    // Arrange
    var linkedList = new LinkedList();

    // Act
    linkedList.ContainsValue(null);

    // Assert (reached by ExpectedException)
  }
  #endregion

  #region IndexOfValue Tests
  [TestMethod]
  public void IndexOfValue_ValueAtHead_ReturnsZero()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add("Item");
    linkedList.Add("Another Item");

    // Act
    int index = linkedList.IndexOfValue("Item");

    // Assert
    Assert.AreEqual(0 , index);
  }

  [TestMethod]
  public void IndexOfValue_MultipleOccurrences_ReturnsIndexOfFirstOccurrence()
  {
    // Arrange
    var linkedList = new LinkedList();
    linkedList.Add(1);
    linkedList.Add(2);
    linkedList.Add(1);

    // Act
    int index = linkedList.IndexOfValue(1);

    // Assert
    Assert.AreEqual(0 , index);
  }

  [TestMethod]
  public void IndexOfValue_EmptyList_ReturnsNegativeOne()
  {
    // Arrange
    var linkedList = new LinkedList();

    // Act
    int index = linkedList.IndexOfValue(5);

    // Assert
    Assert.AreEqual(-1 , index);
  }
  #endregion

  #region ToArray Tests
  [TestMethod]
  public void ToArray_NonEmptyList_ReturnsArrayWithElements()
  {
    /// ARANGE
    var linkedList = new LinkedList
    {
      1 ,
      2 ,
      3
    };

    /// ACT
    object[ ] array = [ .. linkedList ];
    // is the same as: 
    //     object[ ] array = linkedList.ToArray();

    /// ASSERT
    Assert.AreEqual(3 , array.Length);
    Assert.AreEqual(1 , array[ 0 ]);
    Assert.AreEqual(2 , array[ 1 ]);
    Assert.AreEqual(3 , array[ 2 ]);
  }

  [TestMethod]
  public void ToArray_EmptyList_ReturnsEmptyArray()
  {
    // Arrange
    var linkedList = new LinkedList();

    // Act
    object[ ] array = linkedList.ToArray();

    // Assert
    Assert.AreEqual(0 , array.Length);
  }
  #endregion
}