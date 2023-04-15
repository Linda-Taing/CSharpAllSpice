namespace CSharpAllSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;
    private readonly FavoritesService _favoritesService;

    public RecipesService(RecipesRepository repo, FavoritesService favoritesService)
    {
        _repo = repo;
        _favoritesService = favoritesService;
    }

    internal Recipe UpdateRecipe(int id, Recipe updateData, Account userInfo)
    {
        Recipe original = this.Find(id, userInfo.Id);
        if (original.CreatorId != userInfo.Id) throw new Exception("You can not update this Recipe!");
        original.Title = updateData.Title != null ? updateData.Title : original.Title;
        original.Instructions = updateData.Instructions != null ? updateData.Instructions : original.Instructions;
        original.Img = updateData.Img != null ? updateData.Img : original.Img;
        original.Category = updateData.Category != null ? updateData.Category : original.Category;
        int rowsAffected = _repo.UpdateRecipe(original);
        if (rowsAffected == 0) throw new Exception($"Could not update {updateData.Title}");
        if (rowsAffected > 1) throw new Exception($"Something terrible happened! Check your DB for {updateData.Title}");
        return original;
    }


    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repo.CreateRecipe(recipeData);
        return recipe;
    }

    internal List<Recipe> Find(string userId)
    {
        List<Recipe> recipes = _repo.GetAll();
        return recipes;
    }

    internal Recipe Find(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null) throw new Exception("Recipe is not there!");
        if (recipe.CreatorId != userId) throw new Exception("This is not your recipe!");
        return recipe;
    }


    internal string removeRecipe(int id, string userInfo)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null) throw new Exception($"No recipe with this {id}.");
        if (recipe.CreatorId != userInfo) throw new Exception("This is not your recipe to delete.");
        _repo.RemoveRecipe(id);
        return "Your recipe has been removed.";


    }

    // internal List<Recipe> searchRecipes(string userId, string search)
    // {
    //     List<Recipe> recipes = _repo.searchRecipes(search);
    //     List<Recipe> filteredRecipes = recipes.FindAll(r => r.CreatorId == userId);
    //     return filteredRecipes;

    // }
}
