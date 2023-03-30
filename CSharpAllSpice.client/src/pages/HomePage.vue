<template>
  <div v-if="account" class="container">
    <div class="row">
      <div class="col-md-4"></div>
      <div class="col-md-4 d-flex bg-secondary elevation-2 justify-content-evenly my-5 p-2">
        <!-- NOTE: Andrew helped me with this part to make sure that I could retrieve the favorites in the all my recipes. This section I can get AllRecipes and Favorites but not drawing the MyRecipes data to console.  -->
        <!-- <router-link :to="{ name: 'Home' }">
          <button class="btn btn-secondary sm-button">Home</button>
        </router-link> -->
        <!-- <button @click="getAllRecipes()" class="btn btn-secondary sm-button">Home</button>
          <button @click="getMyRecipes()" class="sm-button btn btn-secondary">My Recipes</button>
          <button @click="getMyFavorites()" class="sm-button btn btn-secondary">My Favorites</button> -->
        <div @click="changeFilterCategory(0)" class=" selectable fs-4">Home</div>
        <div @click="changeFilterCategory(1)" class=" selectable fs-4">My Recipes</div>
        <div @click="changeFilterCategory(2)" class="selectable fs-4">My Favorites</div>
      </div>
      <div class="d-flex justify-content-end">
        <div class="col-md-2">
          <div v-if="recipes">
            <form @submit.prevent="searchRecipes()">
              <div class="input-group mb-5 me-5">
                <input v-model="search" type="text" class="form-control" placeholder="Search Recipes"
                  aria-label="Search Recipes" aria-describedby="button-addon2">
                <button class="btn btn-outline-secondary" type="submit" id="button-addon2">
                  <i class="mdi mdi-magnify"></i>
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div class="row">


        <div v-for="r in recipes" class="col-md-3 pb-0">
          <RecipeCard :recipe="r" />
        </div>
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
    <button class="btn btn-circle">
      <i data-bs-toggle="modal" data-bs-target="#test-modal" class="mdi fs-1 text-primary mdi-plus-circle"></i></button>
  </div>
  <Modal id="test-modal">
    <CreateRecipeForm />
  </Modal>
</template>

<script>
import { onMounted, computed, ref, watchEffect } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { AppState } from '../AppState.js';
import { favoritesService } from '../services/FavoritesService.js';

export default {
  setup() {
    const filterCategory = ref(0);
    const search = ref('')
    // NOTE: Sample of Jacobs live search bar...
    // const editable = ref({
    //   search: ""
    // })
    // function searchCategory() {
    //   logger.log('[YOU HERE SEARCHING?]')
    //   setTimeout(() => {
    //     AppState.recipe = AppState.allRecipes.filter(r => r.category.toLowerCase().includes(editable.value.search.toLowerCase()))
    //   }, 300)
    //   if (editable.value.search == "") { AppState.recipe = AppState.allRecipes, logger.log('[ARE YOU SEARCHING RECIPES?]') }

    // }

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
      search,
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      recipes: computed(() => AppState.recipes),
      creator: computed(() => AppState.account),


      // editable,


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

      async searchRecipes() {
        try {
          // let searchData = editable.value;
          await recipesService.searchRecipes(search.value);
          // editable.value = {};
        } catch (error) {
          logger.error(error, '[Did you search the homepage?]');
          Pop.error(error.message);
        }
      }





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
