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
       (favoriteId, accountId)
       VALUES
       (@favoriteId, @accountId);
       SELECT LAST_INSERT_ID();      
       ";
        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        return favoriteData;
    }
}