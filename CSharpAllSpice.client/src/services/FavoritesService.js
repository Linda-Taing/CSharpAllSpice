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
}
export const favoritesService = new FavoritesService()