import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from '../models/Recipe.js'

class RecipesService {
    setActiveRecipe(recipe) {
        AppState.recipe = recipe
    }
    async getAllRecipes() {
        const res = await api.get('api/recipes')
        logger.log('[GETTING RECIPES]', res.data)
        AppState.recipes = res.data.map(r => new Recipe(r))
        logger.log('this is the service', AppState.recipes)

    }
    async getMyRecipes() {
        const res = await api.get('api/recipes')
        // filtering out the recipes with the account.id from the List of AllRecipes.
        const recipes = res.data.filter(d => d.creatorId = AppState.account.id)
        logger.log(res.data)
        // Mapping the List back into an object.
        AppState.recipes = recipes.map(r => new Recipe(r))
        logger.log('[did you complete full function?]')

    }
    async createRecipe(recipeData) {
        const res = await api.post('api/recipes', recipeData)
        logger.log('[CREATING RECIPE]', res.data)
        AppState.recipes.unshift(res.data)
    }

}
export const recipesService = new RecipesService()