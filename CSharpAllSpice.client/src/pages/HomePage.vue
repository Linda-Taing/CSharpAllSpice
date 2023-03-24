<template>
  <h1>ALL SPICE</h1>
</template>

<script>
import { onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js'

export default {
  setup() {
    async function getAllRecipes() {
      try {
        await recipesService.getAllRecipes();
      } catch (error) {
        logger.log(error);
        Pop.error(error, '[GETTING ALL SPICES]');
      }
    }
    onMounted(() => {
      getAllRecipes();
    })
    return {



      // <------END OF RETURN----->
    }
  }
}
</script>

<style scoped lang="scss"></style>
