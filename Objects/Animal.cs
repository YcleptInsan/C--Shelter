using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Shelter.Objects
{
  public class Animal
  {
    private int _id;
    private string _name;
    private string _gender;
    private string _date;
    private string _breed;
    private int _categoryId;

    public Animal(string Name, string Gender, string Date, string Breed, int CategoryId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _gender = Gender;
      _date = Date;
      _breed = Breed;
      _categoryId = CategoryId;
    }

    public override bool Equals(System.Object otherAnimal)
    {
      if (!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = this.GetId() == newAnimal.GetId();
        bool nameEquality = this.GetName() == newAnimal.GetName();
        bool genderEquality = this.GetGender() == newAnimal.GetGender();
        bool dateEquality = this.GetDate() == newAnimal.GetDate();
        bool breedEquality = this.GetBreed() == newAnimal.GetBreed();
        bool categoryEquality = this.GetCategoryId() == newAnimal.GetCategoryId();
        return (idEquality && nameEquality && genderEquality && dateEquality && breedEquality && categoryEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public string GetGender()
    {
      return _gender;
    }
    public void SetGender(string newGender)
    {
      _gender = newGender;
    }
    public string GetDate()
    {
      return _date;
    }
    public void SetDate(string newDate)
    {
      _date = newDate;
    }
    public string GetBreed()
    {
      return _breed;
    }
    public void SetBreed(string newBreed)
    {
      _breed = newBreed;
    }
    public int GetCategoryId()
    {
      return _categoryId;
    }
    public void SetCategoryId(int newCategoryId)
    {
      _categoryId = newCategoryId;
    }

    public static List<Animal> GetAll()
    {
      List<Animal> AllAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal ORDER BY breed;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        string animalDate = rdr.GetString(3);
        string animalBreed = rdr.GetString(4);
        int animalCategoryId = rdr.GetInt32(5);
        Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalCategoryId, animalId);
        AllAnimals.Add(newAnimal);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllAnimals;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO animal (name, gender, date, breed, category_id) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalGender, @AnimalDate, @AnimalBreed, @AnimalCategoryId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AnimalName";
      nameParameter.Value = this.GetName();

      SqlParameter genderParameter = new SqlParameter();
      genderParameter.ParameterName = "@AnimalGender";
      genderParameter.Value = this.GetGender();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@AnimalDate";
      dateParameter.Value = this.GetDate();

      SqlParameter breedParameter = new SqlParameter();
      breedParameter.ParameterName = "@AnimalBreed";
      breedParameter.Value = this.GetBreed();

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@AnimalCategoryId";
      categoryIdParameter.Value = this.GetCategoryId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(genderParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(breedParameter);
      cmd.Parameters.Add(categoryIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal WHERE id = @AnimalId;", conn);
      SqlParameter animalIdParameter = new SqlParameter();
      animalIdParameter.ParameterName = "@AnimalId";
      animalIdParameter.Value = id.ToString();
      cmd.Parameters.Add(animalIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundAnimalId = 0;
      string foundAnimalName = null;
      string foundAnimalGender = null;
      string foundAnimalDate = null;
      string foundAnimalBreed = null;
      int foundAnimalCategoryId = 0;

      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalName = rdr.GetString(1);
        foundAnimalGender = rdr.GetString(2);
        foundAnimalDate = rdr.GetString(3);
        foundAnimalBreed = rdr.GetString(4);
        foundAnimalCategoryId = rdr.GetInt32(5);
      }
      Animal foundAnimal = new Animal(foundAnimalName, foundAnimalGender, foundAnimalDate, foundAnimalBreed, foundAnimalCategoryId, foundAnimalId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAnimal;
    }

    // public static Animal Sort()
    // {
    //
    // }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animal;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
