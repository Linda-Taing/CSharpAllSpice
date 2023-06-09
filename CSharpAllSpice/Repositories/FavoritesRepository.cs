namespace CSharpAllSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
       INSERT INTO favorites
       (recipeId, accountId)
       VALUES
       (@recipeId, @accountId);
       SELECT LAST_INSERT_ID();      
       ";
        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        return favoriteData;
    }

    internal Favorite FindFavorite(int id)
    {
        string sql = @"
        SELECT
        fave.*
        FROM favorites fave
        WHERE fave.id = @id;
        ";
        Favorite favorite = _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
        return favorite;
    }
    internal List<FavoriteRecipe> GetMyFavorites(string accountId)
    {
        string sql = @"
       SELECT
       rec.*,
       fave.*,
       creator.*
       FROM favorites fave
       JOIN recipes rec ON fave.recipeId = rec.id
       JOIN accounts creator ON rec.creatorId = creator.id
       WHERE fave.accountId = @accountId;
       ";
        List<FavoriteRecipe> favoritedRecipes = _db.Query<FavoriteRecipe, Favorite, Profile, FavoriteRecipe>(sql, (favoriteRecipe, favorite, profile) =>
        {
            favoriteRecipe.FavoriteId = favorite.Id;
            favoriteRecipe.Creator = profile;
            return favoriteRecipe;

        }, new { accountId }).ToList();
        return favoritedRecipes;
    }

    internal void RemoveFavorite(int id)
    {
        string sql = @"
       DELETE FROM favorites
       WHERE id = @id;
       ";
        _db.Execute(sql, new { id });
    }
}
