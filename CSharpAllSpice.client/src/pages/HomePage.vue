<template>
  <div class="container">
    <div class="row">
      <div v-for="r in recipes" class="col-md-4 p-3">
        <RecipeCard :recipe="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js'
import { AppState } from '../AppState.js';

export default {
  setup() {
    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      }
      catch (error) {
        logger.log(error);
        Pop.error(error, "[GETTING ALL SPICES]");
      }
    }
    onMounted(() => {
      getAllRecipes();
    });
    return {
      recipes: computed(() => AppState.recipes),
      // <------END OF RETURN----->
    };
  },
}
</script>

<style scoped lang="scss"></style>
