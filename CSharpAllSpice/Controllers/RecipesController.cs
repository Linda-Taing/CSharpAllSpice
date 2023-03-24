namespace CSharpAllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth;
    private readonly IngredientsService _ingredientsService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth, IngredientsService ingredientsService)
    {
        _recipesService = recipesService;
        _auth = auth;
        _ingredientsService = ingredientsService;
    }
    [HttpGet]
    async public Task<ActionResult<List<Recipe>>> Find()
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipes = _recipesService.Find(userInfo?.Id);
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> Find(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _recipesService.Find(id, userInfo.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            recipe.Creator = userInfo;
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("{id}")]
    [Authorize]
    async public Task<ActionResult<Recipe>> Update(int id, [FromBody] Recipe updateData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _recipesService.UpdateRecipe(id, updateData, userInfo);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpGet("{id}/ingredients")]
    public ActionResult<List<Ingredient>> FindIngredientById(int id)
    {
        try
        {
            List<Ingredient> ingredients = _ingredientsService.FindById(id);
            return ingredients;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
