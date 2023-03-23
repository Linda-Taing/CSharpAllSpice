namespace CSharpAllSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
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
        recipes = recipes.FindAll(a => a.CreatorId == userId);
        return recipes;
    }

    internal Recipe Find(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null) throw new Exception("Recipe is not there1");
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

}
