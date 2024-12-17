using CookieCookbook.Recipes.Ingredients;
using System.ComponentModel.DataAnnotations;

namespace CookieCookbook.Recipes;

public class Recipe
{
    public IEnumerable<Ingredient> Ingredients { get; }

    public Recipe(IEnumerable<Ingredient> ingredients)
    {
        Ingredients = ingredients;
    }

    public override string ToString()
    {
        //LINQ USED IN HERE
        var steps = Ingredients.Select(ingredient => $"{ingredient.Name}. {ingredient.PreparationInstructions}");
        //var steps = new List<string>();
        //foreach(var ingredient in Ingredients)
        //{
        //    steps.Add($"{ingredient.Name}. {ingredient.PreparationInstructions}");
        //}

        return string.Join(Environment.NewLine, steps);
    }
}
