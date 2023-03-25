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
    async createRecipe(recipeData) {
        const res = await api.post('api/recipes', recipeData)
        logger.log('[CREATING RECIPE]', res.data)
        AppState.recipes.unshift(res.data)
    }

}
export const recipesService = new RecipesService()