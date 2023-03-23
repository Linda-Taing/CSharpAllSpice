namespace CSharpAllSpice.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string RecipeId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    public Recipe Recipe { get; set; }
}
