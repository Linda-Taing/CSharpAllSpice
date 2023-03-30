namespace CSharpAllSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (creatorId, title, instructions, img, category)
        VALUES
        (@creatorId, @title, @instructions, @img, @category);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, recipeData);
        recipeData.Id = id;
        return recipeData;

    }

    internal List<Recipe> GetAll()
    {
        string sql = @"
        SELECT
        rec.*,
        acct.*
        FROM recipes rec
        JOIN accounts acct ON rec.creatorId = acct.id;
        ";
        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, prof) =>
        {
            recipe.Creator = prof;
            return recipe;
        }).ToList();
        return recipes;
    }
    internal List<Recipe> SearchRecipes(string search)
    {
        search = $"%{search}%";
        string sql = @"
        SELECT
        rec.*,
        acct.*
        FROM recipes rec
        JOIN accounts acct ON rec.creatorId = acct.id
        Where rec.title LIKE @search OR rec.category LIKE @search OR rec.instructions LIKE @search;
        ";
        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, prof) =>
        {
            recipe.Creator = prof;
            return recipe;
        }).ToList();
        return recipes;
    }
    internal Recipe GetOne(int id)
    {
        string sql = @"
        SELECT
        rec.*,
        acct.*
        FROM recipes rec
        JOIN accounts acct ON rec.creatorId = acct.id
        WHERE rec.id = @id;";
        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, prof) =>
        {
            recipe.Creator = prof;
            return recipe;
        }, new { id }).FirstOrDefault();
        return recipe;
    }

    internal void RemoveRecipe(int id)
    {
        string sql = @"
        
        DELETE FROM recipes WHERE id = @id;
        ";
        int rows = _db.Execute(sql, new { id });
    }


    internal int UpdateRecipe(Recipe update)
    {
        string sql = @"
       UPDATE recipes 
       SET
       title = @title,
       instructions = @instructions,
       img = @img,
       category = @category
       WHERE id = @id;
    ";
        int rows = _db.Execute(sql, update);
        return rows;
    }
}
