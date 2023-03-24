import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
    async getAllRecipes() {
        const res = await api.get('api/recipes')
        logger.log('[GETTING RECIPES]', res.data)
        const recipes = res.data.map(r => new Recipe(r))
        AppState.recipes = recipes

    }
}
export const recipesService = new RecipesService()