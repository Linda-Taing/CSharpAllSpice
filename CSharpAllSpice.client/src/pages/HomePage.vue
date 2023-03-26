<template>
  <div v-if="account" class="container">
    <div class="row">
      <div class="col-md-12 d-flex justify-content-evenly my-5 p-2">
        <button class="btn btn-secondary sm-button">Home</button>
        <!-- FIXME: FILTER BUTTONS ARE NOT FILTERING, THEY FILTER TO A BLANK. -->
        <button @click="getMyRecipes()" class="sm-button btn btn-secondary">My Recipes</button>
        <button @click="getMyFavorites()" class="sm-button btn btn-secondary">My Favorites</button>
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
  <div v-if="account.id" class="d-flex justify-content-end p-3 sticky-bottom" title="Add Recipe" data-bs-toggle="modal"
    data-bs-target="#test-modal">
    <i @click="createRecipe()" class="selectable mdi fs-1 text-primary mdi-plus-circle"></i>
  </div>
  <Modal id="test-modal" modal-title="Create New Recipe">
    <CreateRecipeForm />
  </Modal>
</template>

<script>
import { onMounted, computed, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js'
import { AppState } from '../AppState.js';
import CreateRecipeForm from '../components/CreateRecipeForm.vue';

export default {
  setup() {
    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error, "[GETTING ALL RECIPES]");
      }
    }
    onMounted(() => {
      getAllRecipes();

    });
    return {
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      recipes: computed(() => AppState.recipes);

      // <------END OF RETURN----->
    };
  },
  components: { CreateRecipeForm }
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
