import { api } from "./AxiosService.js"
import { logger } from "../utils/Logger.js"
import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"

class IngredientsService {
    async getIngredientsById(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log('[Ingredients in service]', res.data)
        AppState.ingredients = res.data
    }
    async addIngredient(ingredientData) {
        const res = await api.post('api/ingredients', ingredientData)
        AppState.ingredients.push = (res.data)

    }
    async removeIngredient(ingredientId) {
        const res = await api.delete(`api/ingredients/${ingredientId}`)
        let i = AppState.ingredients.findIndex(i => i.id == ingredientId)
        if (i != -1) {
            AppState.ingredients.splice(i, 1)
        }

    }
}


export const ingredientsService = new IngredientsService