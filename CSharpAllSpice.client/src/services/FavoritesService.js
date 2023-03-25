import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService {
    async getMyFavorites() {
        const res = await api.get('account/favorites')
        logger.log('[GETTING MY FAVORITES]', res.data)
        AppState.favorites = res.data.map(f => new Favorite(f))
        logger.log('this fave is in the service', AppState.favorites)
    }
    async addFavorites(recipeId) {
        debugger
        logger.log('[ADDING FAVORITES HERE]')
        const res = await api.post('api/favorites', recipeId)
        logger.log('[did fave ADD here?]')
        let i = AppState.favorites.findIndex(r => r.id == recipeId)
        if (i != 1) {
            AppState.favorites.splice(i, 1)
        }


    }
}
export const favoritesService = new FavoritesService()