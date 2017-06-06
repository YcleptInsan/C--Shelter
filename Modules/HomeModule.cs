using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using Shelter.Objects;

namespace Shelter
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Category> AllCategories = Category.GetAll();
        return View["index.cshtml", AllCategories];
      };
      Get["/animals"] = _ => {
        List<Animal> AllAnimals = Animal.GetAll();
        return View["animals.cshtml", AllAnimals];
      };
      Get["/animals/{id}"] = parameters => {
        var selectedAnimal = Animal.Find(parameters.id);
        return View["animal.cshtml", selectedAnimal];
      };
      Get["/categories"] = _ => {
        List<Category> AllCategories = Category.GetAll();
        return View["categories.cshtml", AllCategories];
      };
      Get["/categories/new"] = _ => {
        return View["categories_form.cshtml"];
      };
      Post["/categories/new"] = _ => {
        Category newCategory = new Category(Request.Form["category-name"]);
        newCategory.Save();
        return View["success.cshtml"];
      };
      Get["/animals/new"] = _ => {
        List<Category> AllCategories = Category.GetAll();
        return View["animal_form.cshtml", AllCategories];
      };
      Post["/animals/new"] = _ => {
        Animal newAnimal = new Animal(Request.Form["animal-name"], Request.Form["animal-gender"], Request.Form["animal-date"], Request.Form["animal-breed"], Request.Form["category-id"]);
        newAnimal.Save();
        return View["success.cshtml"];
      };
      Post["/animals/delete"] = _ => {
        Animal.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["/categories/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCategory = Category.Find(parameters.id);
        var CategoryAnimals = SelectedCategory.GetAnimals();
        model.Add("category", SelectedCategory);
        model.Add("animals", CategoryAnimals);
        return View["category.cshtml", model];
      };
    }
  }
}
