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

    internal List<Ingredient> FindById(int recipeId)
    {
        List<Ingredient> ingredients = _repo.FindByID(recipeId);
        return ingredients;
    }

}
