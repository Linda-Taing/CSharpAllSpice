namespace CSharpAllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
    {
        _ingredientsService = ingredientsService;
        _auth = auth;
    }
    [HttpGet]
    public ActionResult<List<Ingredient>> GetAll()
    {
        try
        {
            List<Ingredient> ingredients = _ingredientsService.GetAll();
            return ingredients;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            ingredientData.CreatorId = userInfo.Id;
            Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
            ingredient.Creator = userInfo;
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> RemoveIngredient(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string message = _ingredientsService.removeIngredient(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
