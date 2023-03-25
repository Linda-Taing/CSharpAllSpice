<template>
    <div v-if="recipe">
        <img class="instruction-img" :src="recipe.img" alt="">
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
export default {


    setup() {
        const editable = ref({})
        return {
            editable,
            recipe: computed(() => AppState.recipe)
        }
    }
}
</script>


<style lang="scss" scoped>
.instruction-img {
    height: 20em;
    width: 20em;

}
</style>