using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Shelter.Objects;

namespace Shelter
{
  public class CategoryTest : IDisposable
  {
    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shelter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CategoriesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Category.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Category firstCategory = new Category("Household chores");
      Category secondCategory = new Category("Household chores");

      //Assert
      Assert.Equal(firstCategory, secondCategory);
    }

    [Fact]
    public void Test_Save_SavesCategoryToDatabase()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToCategoryObject()
    {
      //Arrange
      Category testCategory = new Category("Dogs");
      testCategory.Save();

      //Act
      Category savedCategory = Category.GetAll()[0];


      int result = savedCategory.GetId();
      int testId = testCategory.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCategoryInDatabase()
    {
      //Arrange
      Category testCategory = new Category("Dogs");
      testCategory.Save();

      //Act
      Category foundCategory = Category.Find(testCategory.GetId());

      //Assert
      Assert.Equal(testCategory, foundCategory);
    }
    [Fact]
    public void Test_GetAnimals_RetrievesAllAnimalsWithCategory()
    {
      Category testCategory = new Category("Dogs");
      testCategory.Save();

      Animal firstAnimal = new Animal("James", "Male", "2017-01-01","Lab", testCategory.GetId());
      firstAnimal.Save();
      Animal secondAnimal = new Animal("Jake", "Male", "2017-01-01","Lab", testCategory.GetId());
      secondAnimal.Save();


      List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal};
      List<Animal> resultAnimalList = testCategory.GetAnimals();

      Assert.Equal(testAnimalList, resultAnimalList);
    }

    public void Dispose()
    {
      Animal.DeleteAll();
      Category.DeleteAll();
    }
  }
}
