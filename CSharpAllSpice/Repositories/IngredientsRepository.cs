namespace CSharpAllSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO ingredients
        (name, quantity, recipeId, creatorId)
        VALUES
        (@name, @quantity, @recipeId, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }

    internal List<Ingredient> FindByID(int recipeId)
    {
        string sql = @"
        SELECT
       ingred.*
       FROM ingredients ingred
       WHERE ingred.recipeId = @recipeId;
        ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal List<Ingredient> GetAll()
    {
        string sql = @"
   SELECT
  *
  FROM ingredients;
   ";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql).ToList();
        return ingredients;
    }


}
