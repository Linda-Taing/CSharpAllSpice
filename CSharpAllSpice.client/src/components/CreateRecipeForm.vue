<template>
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title text-dark fs-5" id="exampleModalLabel">Create Recipe</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createRecipe()">
            <div class="modal-body text-dark">
                <div class="mb-3">
                    <label for="title" class="form-label">Recipe Title</label>
                    <input v-model="editable.title" required type="text" class="form-control" id="title"
                        aria-describedby="emailHelp">
                </div>
                <div class="mb-3">
                    <label for="img" class="form-label">Recipe Image</label>
                    <input v-model="editable.img" required type="url" class="form-control" id="img"
                        aria-describedby="emailHelp">
                </div>
                <div class="mb-3">
                    <label for="category" class="form-label me-2">Category</label>
                    <select class="rounded fs-4" required name="category" id="category" v-model="editable.category">
                        <option value="Italian">Italian</option>
                        <option value="Soup">Soup</option>
                        <option value="Cheese">Cheese</option>
                        <option value="Specialty Coffee">Specialty Coffee</option>
                        <option value="Breakfast" selected>Breakfast</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label for="instructions" class="form-label">Instructions</label>
                    <input v-model="editable.instructions" required type="text" textarea name="instructions"
                        class="form-control" id="instructions" aria-describedby="emailHelp">
                </div>
                <div class="modal-footer">
                    <button type="reset" class="btn btn-secondary" data-bs-dismiss="modal">CLOSE</button>
                    <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">CREATE RECIPE</button>
                </div>
            </div>
        </form>
    </div>
</template>


<script>
import { ref, computed } from 'vue';
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { recipesService } from '../services/RecipesService.js';
import { AppState } from '../AppState.js';
import { Recipe } from '../models/Recipe.js';

export default {
    // props: {
    //     recipe: {
    //         type: Recipe, required: true
    //     }
    // },
    setup() {
        const editable = ref({})
        return {
            editable,
            recipes: computed(() => AppState.recipes),
            async createRecipe() {
                try {
                    const recipeData = editable.value
                    await recipesService.createRecipe(recipeData)
                    editable.value = {}
                    Pop.success('[YOU HAVE CREATED A RECIPE!]')
                } catch (error) {
                    logger.log('[CREATE RECIPE]')
                    Pop.error(error.message);
                }
            }

            // <<<<-------END OF RETURN------>>>>>>
        }
    }
}
</script>


<style lang="scss" scoped></style>