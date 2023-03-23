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

    internal string removeIngredient(int id, string userInfo)
    {
        Ingredient ingredient = _repo.GetOne(id);
        if (ingredient == null) throw new Exception($"No ingredient with this {id}.");
        if (ingredient.RecipeId != userInfo) throw new Exception("This ingredient is not your list to delete.");
        _repo.RemoveIngredient(id);
        return "Your ingredient list has been removed.";
    }
}
