<template>
    <div class="modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <slot></slot>
        </div>
    </div>
</template>


<script>
export default {
    props: {
        modalTitle: {
            type: String, default: "Oops I forgot to name my Modal!!"
        }
    },

    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped></style>