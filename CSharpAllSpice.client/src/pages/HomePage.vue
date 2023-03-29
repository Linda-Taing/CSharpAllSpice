<template>
  <div v-if="account" class="container">
    <div class="row">
      <div class="col-md-12 d-flex justify-content-evenly my-5 p-2">

        <!-- NOTE: Andrew helped me with this part to make sure that I could retrieve the favorites in the all my recipes. This section I can get AllRecipes and Favorites but not drawing the MyRecipes data to console.  -->
        <!-- <router-link :to="{ name: 'Home' }">
          <button class="btn btn-secondary sm-button">Home</button>
        </router-link> -->
        <!-- <button @click="getAllRecipes()" class="btn btn-secondary sm-button">Home</button>
        <button @click="getMyRecipes()" class="sm-button btn btn-secondary">My Recipes</button>
        <button @click="getMyFavorites()" class="sm-button btn btn-secondary">My Favorites</button> -->
        <button @click="changeFilterCategory(0)" class="btn btn-secondary sm-button">Home</button>
        <button @click="changeFilterCategory(1)" class="sm-button btn btn-secondary">My Recipes</button>
        <button @click="changeFilterCategory(2)" class="sm-button btn btn-secondary">My Favorites</button>

      </div>
      <div v-for="r in recipes" class="col-md-3 pb-0">
        <RecipeCard :recipe="r" />
      </div>
    </div>
  </div>
  <div v-else class="text-success text-center p-5">
    <h1 class="display-1">
      <i class="mdi mdi-pinwheel mdi-spin"></i><i class="mdi mdi-pinwheel mdi-spin"></i><i
        class="mdi mdi-pinwheel mdi-spin"></i><i class="mdi mdi-pinwheel mdi-spin"></i>
    </h1>
  </div>

  <div v-if="account.id" class="d-flex justify-content-end p-3 sticky-bottom" title="Add Recipe">
    <i data-bs-toggle="modal" data-bs-target="#test-modal" class="selectable mdi fs-1 text-primary mdi-plus-circle"></i>
  </div>
</template>

<script>
import { onMounted, computed, ref, watchEffect } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js'
import { AppState } from '../AppState.js';
import CreateRecipeForm from '../components/CreateRecipeForm.vue';
import { favoritesService } from '../services/FavoritesService.js';

export default {
  setup() {
    const filterCategory = ref(0);

    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error, "[GETTING ALL RECIPES]");
      }
    }
    async function getMyFavorites() {
      try {
        await favoritesService.getMyFavorites()
      } catch (error) {
        logger.log('[GETTING MY FAVES]')
        Pop.error(error)
      }
    }

    onMounted(() => {
      getAllRecipes()

    });
    watchEffect(() => {
      if (AppState.account.id) {
        getMyFavorites()
      }
    })

    return {
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      recipes: computed(() => AppState.recipes),
      creator: computed(() => AppState.account),


      getAllRecipes,
      // NOTE: This is set for the filter, when filtering it would go to a blank page so I am looking for different routes.
      recipes: computed(() => {
        if (!filterCategory.value) {
          let filter = AppState.recipes
          return filter;
        }
        else if (filterCategory.value == 1) {
          let filter = AppState.recipes.filter(r => r.creatorId == AppState.account.id)
          return filter;
        }
        else {
          return AppState.recipes.filter(r => r.favoriteId != 0)
        }
      }),
      changeFilterCategory(category) {
        filterCategory.value = category;
      },
      // async createRecipe() {
      //   try {
      //     const recipeData = editable.value
      //     await recipesService.createRecipe(recipeData)
      //     editable.value = {}
      //     Pop.success('[YOU HAVE CREATED A RECIPE!]')
      //   } catch (error) {
      //     logger.log('[CREATE RECIPE]')
      //     Pop.error(error.message);
      //   }
      // },




      // <------END OF RETURN----->
    };
  },
}
</script>

<style scoped lang="scss">
.card-filter {
  background-color: dark;
  width: 100vw;
}

.sm-button {
  width: 10em;
  margin-left: 3em;
}
</style>
