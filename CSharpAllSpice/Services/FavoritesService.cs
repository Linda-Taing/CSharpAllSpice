namespace CSharpAllSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
        _repo = repo;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        Favorite favorite = _repo.CreateFavorite(favoriteData);
        return favorite;
    }

    internal List<FavoriteRecipe> GetMyFavorites(string accountId)
    {
        List<FavoriteRecipe> favoritedRecipes = _repo.GetMyFavorites(accountId);
        return favoritedRecipes;
    }

    internal string removeFavorite(int id, string userInfo)
    {
        List<Favorite> favorite = _repo.Find(id);
        if (favorite == null) throw new Exception($"You didn't favorite this {id}!");
        _repo.RemoveFavorite(id);
        return $"This is no longer your favorite recipe.";
    }
}
