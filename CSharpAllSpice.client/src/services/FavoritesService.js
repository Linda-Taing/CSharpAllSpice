import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService {


    async getMyFavorites() {
        // NOTE: originally had this function routed from AuthService using the PostItVue. but not sure again how to tie all back to the HOMEPAGE.
        const res = await api.get('account/favorites')
        logger.log('[GETTING MY FAVORITES]', res.data)
        AppState.recipes = res.data.map(r => new Recipe(r))
        logger.log('this fave is in the service', AppState.recipes)
    }
    async addFavorites(recipeId) {
        logger.log('[ADDING FAVORITES HERE]')
        const res = await api.post('api/favorites', { recipeId })
        logger.log('[did fave ADD here?]')

    }


}

export const favoritesService = new FavoritesService()