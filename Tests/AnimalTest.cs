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
      Animal firstName = new Animal("James", "Male", "2017-01-01","Lab", 1);
      Animal secondName = new Animal("James", "Male", "2017-01-01", "Lab", 1);

      //Assert
      Assert.Equal(firstName, secondName);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Animal testAnimal = new Animal("James", "Male", "2017-01-01","Lab", 1);
      testAnimal.Save();

      //Act
      List<Animal> result = Animal.GetAll();
      List<Animal> testList = new List<Animal>{testAnimal};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Animal testAnimal = new Animal("James", "Male", "2017-01-01","Lab", 1);
      testAnimal.Save();

      //Act
      Animal savedAnimal = Animal.GetAll()[0];

      int result = savedAnimal.GetId();
      int testId = testAnimal.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsAnimalInDatabase()
    {
      //Arrange
      Animal testAnimal = new Animal("James", "Male", "2017-01-01","Lab", 1);
      testAnimal.Save();

      //Act
      Animal foundAnimal = Animal.Find(testAnimal.GetId());

      //Assert
      Assert.Equal(testAnimal, foundAnimal);
    }
    public void Dispose()
    {
      Animal.DeleteAll();
      Category.DeleteAll();
    }
  }
}
