using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Shelter.Objects;

namespace Shelter
{
  public class ShelterTest : IDisposable
  {
    public ShelterTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shelter_test;Integrated Security=SSPI;";
    }


    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Animal.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameName()
    {
      //Arrange, Act
      Animal firstName = new Animal("James", "Male", "2017-01-01", 1, 0);
      Animal secondName = new Animal("James", "Male", "2017-01-01", 1, 0);

      //Assert
      Assert.Equal(firstName, secondName);
    }

    // [Fact]
    // public void Test_Save()
    // {
    //   //Arrange
    //   Task testTask = new Task("Mow the lawn", 1);
    //   testTask.Save();
    //
    //   //Act
    //   List<Task> result = Task.GetAll();
    //   List<Task> testList = new List<Task>{testTask};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }
    //
    // [Fact]
    // public void Test_SaveAssignsIdToObject()
    // {
    //   //Arrange
    //   Task testTask = new Task("Mow the lawn", 1);
    //   testTask.Save();
    //
    //   //Act
    //   Task savedTask = Task.GetAll()[0];
    //
    //   int result = savedTask.GetId();
    //   int testId = testTask.GetId();
    //
    //   //Assert
    //   Assert.Equal(testId, result);
    // }
    //
    // [Fact]
    // public void Test_FindFindsTaskInDatabase()
    // {
    //   //Arrange
    //   Task testTask = new Task("Mow the lawn", 1);
    //   testTask.Save();
    //
    //   //Act
    //   Task foundTask = Task.Find(testTask.GetId());
    //
    //   //Assert
    //   Assert.Equal(testTask, foundTask);
    // }
    public void Dispose()
    {
      Animal.DeleteAll();
      Category.DeleteAll();
    }
  }
}
