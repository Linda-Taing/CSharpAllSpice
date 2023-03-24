namespace CSharpAllSpice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repo;

    public IngredientsService(IngredientsRepository repo)
    {
        _repo = repo;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        Ingredient ingredient = _repo.CreateIngredient(ingredientData);
        return ingredient;
    }

    internal List<Ingredient> GetAll()
    {
        List<Ingredient> ingredients = _repo.GetAll();
        return ingredients;
    }

    internal List<Ingredient> FindByID(int recipeId)
    {
        List<Ingredient> ingredient = _repo.FindByID(recipeId);
        if (ingredient == null) throw new Exception("Ingredient is not there!");
        return ingredient;
    }

    internal string removeIngredient(int recipeId, string userInfo)
    {
        Ingredient ingredient = _repo.FindIngredient(recipeId);
        // if (ingredient == null) throw new Exception($"No ingredient with that {recipeId}.");
        _repo.RemoveIngredient(recipeId);
        return "Your ingredient has been deleted.";

    }
}
