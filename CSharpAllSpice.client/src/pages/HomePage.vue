<template>
  <div v-if="account" class="container">
    <div class="row">
      <!-- <button @click="changeFilterCategory('favorites')" class="btn btn-secondary">My Favorites</button>
      <button @click="changeFilterCategory('recipes')" class="btn btn-secondary">My Recipes</button> -->
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
    const filterCategory = ref('');
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
      recipes: computed(() => {
        if (filterCategory.value == "") {
          let filter = AppState.recipes;
          return filter;
        }
        else {
          return AppState.recipes.filter(a => a.category == filterCategory.value);
        }
      }),
      changeFilterCategory(category) {
        filterCategory.value = category;
      },
      // <------END OF RETURN----->
    };
  },
  components: { CreateRecipeForm }
}
</script>

<style scoped lang="scss"></style>
