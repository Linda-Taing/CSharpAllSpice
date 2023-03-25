<template>
    <div class="card">
        {{ recipe }}

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


<style lang="scss" scoped></style>