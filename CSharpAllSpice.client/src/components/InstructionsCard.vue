<template>
    <div v-if="recipe" class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title text-dark fs-5" id="exampleModalLabel"> {{ recipe.title }}</h1>
            <i v-if="account.id == recipe.creatorId" @click="deleteRecipe(recipe.id)"
                class=" fs-3 ms-5 selectable text-danger mdi mdi-delete-forever" title="Delete Recipe"></i>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-md-3">
                    <img class="instruction-img" :src="recipe.img" alt="">
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <p class="mb-0"><b>Instructions:</b> {{ recipe.instructions }}</p>
                    <div v-if="account.id == recipe.creatorId" class="d-flex justify-content-end">
                        <button class="btn btn-circle"> <i @click="openEdit()" title="Edit Instructions"
                                class="mdi selectable mdi-pencil-circle text-warning fs-3"></i></button>
                    </div>
                </div>
                <div v-if="account.id == recipe.creatorId" class="d-flex justify-content-end">
                    <form @submit.prevent="editInstruction(recipe.id)" v-if="edited">
                        <textarea v-model="editable.instructions" class="" cols="60" rows="2" type="text"
                            id="instructions"></textarea>
                        <div class="d-flex justify-content-end">
                            <button title="Save your changes!" class="btn btn-circle " type="submit"><i
                                    class="mdi fs-3 selectable mdi-check-circle text-success"></i></button>
                        </div>
                    </form>
                </div>
                <div class="row">
                    <div v-if="account.id == recipe.creatorId" class="col-md-12">
                        <div><b>Ingredients List:</b></div>
                        <div v-for="i in ingredients">
                            <li>{{ i.name }}</li>
                            <li>{{ i.quantity }}
                                <i @click="removeIngredient(i.id)" title="Delete Ingredient"
                                    class="text-danger mdi selectable fw-bold mdi-minus-circle fs-5"></i>
                            </li>
                        </div>
                    </div>
                </div>
                <form>
                    <div v-if="account.id == recipe.creatorId" class="d-flex mt-3">
                        <label for="ingredients" class="form-label text-dark"> </label>
                        <input placeholder="add ingredient" v-model="editable.name" required type="text"
                            class="form-control" id="ingredient" aria-describedby="emailHelp">
                        <label for="quantity" class="form-label text-dark"> </label>
                        <input placeholder="quantity" v-model="editable.quantity" required type="text" class="form-control"
                            id="quantity" aria-describedby="emailHelp">
                        <i @click="addIngredient(recipe.id)" title="Add Ingredient"
                            class="mdi fw-bold fs-3 text-success mdi-plus-circle"></i>

                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { ingredientsService } from '../services/IngredientsService.js';
export default {



    setup() {
        const editable = ref({})
        return {
            editable,
            ingredient: computed(() => AppState.ingredient),
            account: computed(() => AppState.account),
            ingredients: computed(() => AppState.ingredients),
            recipe: computed(() => AppState.recipe),
            edited: computed(() => AppState.editInstruction),
            async getInstructionsById() {
                try {
                    await instructionsService.getInstructionsById()
                } catch (error) {
                    logger.error(error)
                    Pop.error(error, '[Instructions you here?]')
                }
            },
            async deleteRecipe(recipeId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this recipe?'))
                        await recipesService.removeRecipe(recipeId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error, '[Delete Recipes are you here?]')
                }
            },
            async addIngredient(recipeId) {
                try {
                    const ingredientData = editable.value
                    ingredientData.recipeId = recipeId
                    await ingredientsService.addIngredient(ingredientData)
                    editable.value = {}
                    Pop.success('[You have added ingredients!]')
                } catch (error) {
                    logger.error(error)
                    Pop.error(error, '[add ingredients here?]')
                }
            },
            async removeIngredient(ingredientId) {
                try {
                    logger.log('[ingredient id]', ingredientId)
                    if (await Pop.confirm('Are you sure you want to delete the ingredients?'))
                        await ingredientsService.removeIngredient(ingredientId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error, '[delete ingredients here?]')
                }
            },
            async editInstruction(recipeId) {
                try {
                    const editedData = editable.value
                    await recipesService.editInstruction(recipeId, editedData)
                    Pop.success('You have updated your ingredients!')
                    editable.value = {}
                    AppState.editInstruction = false
                } catch (error) {
                    logger.error(error)
                    Pop.error(error, '[delete ingredients here?]')
                }
            },
            openEdit() {
                AppState.editInstruction = true
                logger.log('[Are you opening edit?]')
            }








            // <<<< END of RETURN --->>>>>
        }
    }
}
</script>


<style lang="scss" scoped>
.instruction-img {
    height: 10em;
    width: 10em;

}
</style>