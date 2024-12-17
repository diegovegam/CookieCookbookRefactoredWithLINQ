using CookieCookbook.DataAccess;
using CookieCookbook.Recipes.Ingredients;

namespace CookieCookbook.Recipes;

public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientsRegister _ingredientsRegister;
    private const string Separator = ",";

    public RecipesRepository(
        IStringsRepository stringsRepository,
        IIngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    public List<Recipe> Read(string filePath)
    {
        //LINQ USED IN HERE 

        return _stringsRepository.Read(filePath).Select(recipe => RecipeFromString(recipe)).ToList();
        //var recipes = new List<Recipe>();

        //foreach (var recipeFromFile in recipesFromFile)
        //{
        //    var recipe = RecipeFromString(recipeFromFile);
        //    recipes.Add(recipe);
        //}

        
    }

    private Recipe RecipeFromString(string recipeFromFile)
    {
        //LINQ USED IN HERE
        var textualIds = recipeFromFile.Split(Separator);
        var ingredients = textualIds.Select(textualId => _ingredientsRegister.GetById(int.Parse(textualId)));
        //var ingredients = new List<Ingredient>();

        //foreach (var textualId in textualIds)
        //{
        //    var id = int.Parse(textualId);
        //    var ingredient = _ingredientsRegister.GetById(id);
        //    ingredients.Add(ingredient);
        //}

        return new Recipe(ingredients);
    }

    public void Write(string filePath, List<Recipe> allRecipes)
    {
        //LINQ USED HERE.
        var recipesAStrings = allRecipes.Select(recipe =>
        {
            var allIds = recipe.Ingredients.Select(ingredient => ingredient.Id);
            return string.Join(Separator, allIds);
        }
        );


        _stringsRepository.Write(filePath, recipesAStrings.ToList());



        //var recipesAsStrings = new List<string>();
        //foreach (var recipe in allRecipes)
        //{
        //    var allIds = new List<int>();
        //    foreach (var ingredient in recipe.Ingredients)
        //    {
        //        allIds.Add(ingredient.Id);
        //    }
        //    recipesAsStrings.Add(string.Join(Separator, allIds));
        //}
        //_stringsRepository.Write(filePath, recipesAStrings);

    }
}
