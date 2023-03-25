<template>
    <div class="card">
        <img class="img-h selectable" :src="recipe.img" :alt="recipe.title">
        <div class="card-body p-1">
            <p class="fs-6 my-0 py-0">{{ recipe.title }}</p>
            <p class="fs-6 my-0 py-0">{{ recipe.category }}</p>

        </div>

    </div>
</template>


<script>
import { Recipe } from '../models/Recipe.js';

export default {
    props: {
        recipe: {
            type: Recipe,
            required: true
        }
    },
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped>
.img-h {
    height: 15em;
    object-fit: cover;
}

.card-body {
    height: 4em;
    overflow: hidden;
}
</style>