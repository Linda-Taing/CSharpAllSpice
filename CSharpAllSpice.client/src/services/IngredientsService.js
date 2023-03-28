import { api } from "./AxiosService.js"
import { logger } from "../utils/Logger.js"
import { AppState } from "../AppState.js"

class IngredientsService {
    async getIngredientsById(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log('[Ingredients in service]', res.data)
        AppState.ingredients = res.data


    }

}
export const ingredientsService = new IngredientsService