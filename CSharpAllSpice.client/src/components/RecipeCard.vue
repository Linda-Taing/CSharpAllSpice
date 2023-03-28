<template>
    <div class="container-fluid ">
        <div class="row">
            <div class="col-md-12">
                <img @click="setActiveRecipe(), getIngredientsById(recipe.id)" data-bs-toggle="modal"
                    data-bs-target="#instructions" class="selectable rounded img-h elevation-1 border border-1 border-dark"
                    title="Recipe Information" :src="recipe.img" :alt="recipe.title">
                <div>
                </div>
                <div class="col-md-12">
                    <div class="">
                        <Modal class="modal-instructions" id="instructions" :modal-title="recipe.title">
                            <InstructionsCard />
                        </Modal>
                    </div>
                </div>
            </div>
            <div class=" col-md-12 d-flex justify-content-end p-3">
                <!-- FIXME: @CLICK WILL GIVE ERROR OF 415 -->
                <i @click="addFavorites(recipe.id)"
                    class="fave-heart text-light fs-3 p-2 opacity-75 selectable mdi mdi-heart-outline"></i>
            </div>
            <div class="col-md-12">
                <div class="d-flex justify-content-center align-content-end">
                    <div class="rounded frosted-card-title elevation-1 p-1">
                        <p class="fs-6 my-0 py-0 text-light fw-bold">{{ recipe.title }}</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 d-flex justify-content-start">
            <div class="rounded elevation-1 frosted-card-cat ms-2 mt-0">
                <p class="fs-6 my-0 py-0 text-center text-light">{{ recipe.category }}</p>
            </div>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState.js';
import { Recipe } from '../models/Recipe.js';
import { favoritesService } from '../services/FavoritesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { computed, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import { useRoute } from 'vue-router';
import { ingredientsService } from '../services/IngredientsService.js'
export default {
    props: {
        recipe: {
            type: Recipe,
            required: true
        }
    },
    setup(props) {
        const route = useRoute()

        onMounted(() => {
        })
        return {
            activeRecipe: computed(() => AppState.recipe),
            account: computed(() => AppState.account),
            recipes: computed(() => AppState.recipes),

            setActiveRecipe() {
                recipesService.setActiveRecipe(props.recipe);
            },
            async addFavorites(recipeId) {
                try {
                    await favoritesService.addFavorites(recipeId);
                }
                catch (error) {
                    logger.log("ADD FAVORITE", error);
                    Pop.error(error.message);
                }
            },
            async getIngredientsById(recipeId) {
                try {
                    await ingredientsService.getIngredientsById(recipeId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error, '[Ingredients here?]')

                }
            },

            // <-------END OF RETURN------>
        };
    },
}
</script>


<style lang="scss" scoped>
.modal-instructions {
    height: 100em;
    width: 100em;
}

.img-h {
    height: 20em;
    width: 18em;
    object-fit: cover;
}

.frosted-card-title {
    width: 15em;
    transform: translateY(-300%);
    box-shadow: 0 0 5px 0;
    background: inherit;
    backdrop-filter: blur(50px);
    text-shadow: 1px 1px 2px black;
}

.frosted-card-cat {
    width: 8em;
    content: object-fit;
    transform: translateY(-22em);
    box-shadow: 0 0 5px 0;
    background: inherit;
    backdrop-filter: blur(50px);
    text-shadow: 1px 1px 2px black;
}

.fave-heart {
    transform: translateY(-12em);
    position: absolute;
}
</style>