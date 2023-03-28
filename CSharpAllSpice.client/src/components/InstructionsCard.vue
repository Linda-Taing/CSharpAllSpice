<template>
    <!--NOTE: NEED TO FILL OUT INFORMATION FOR RECIPE MODAL. -->
    <div v-if="recipe" class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title text-dark fs-5" id="exampleModalLabel"> {{ recipe.title }}</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-md-3">
                    <img class="instruction-img" :src="recipe.img" alt="">
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <p><b>Instructions:</b> {{ recipe.instructions }}</p>
                    <div v-for="i in ingredients"><b>Ingredients List:</b>
                        <li>{{ i.name }}</li>
                        <li>{{ i.quantity }}</li>
                    </div>
                </div>
                <div>

                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
export default {



    setup() {
        const editable = ref({})
        return {
            editable,
            ingredients: computed(() => AppState.ingredients),
            recipe: computed(() => AppState.recipe),
            async getInstructionsById() {
                try {
                    await instructionsService.getInstructionsById()
                } catch (error) {
                    logger.error(error)
                    Pop.error(e, '[Instructions you here?]')

                }
            },



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