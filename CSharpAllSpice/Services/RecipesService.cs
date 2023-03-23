namespace CSharpAllSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repo.CreateRecipe(recipeData);
        return recipe;
    }

    internal List<Recipe> Get(string userId)
    {
        List<Recipe> recipes = _repo.GetAll();
        recipes = recipes.FindAll(a => a.CreatorId == userId);
        return recipes;
    }

    internal Recipe GetOne(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null) throw new Exception("Recipe is not there1");
        if (recipe.CreatorId != userId) throw new Exception("This is not your recipe!");
        return recipe;
    }
}
