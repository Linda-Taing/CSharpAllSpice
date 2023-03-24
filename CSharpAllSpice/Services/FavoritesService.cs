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
}
