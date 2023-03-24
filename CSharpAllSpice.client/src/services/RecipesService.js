import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from '../models/Recipe.js'

class RecipesService {
    async getAllRecipes() {
        const res = await api.get('api/recipes')
        logger.log('[GETTING RECIPES]', res.data)
        AppState.recipes = res.data.map(r => new Recipe(r))
        logger.log(AppState.recipes)

    }
}
export const recipesService = new RecipesService()