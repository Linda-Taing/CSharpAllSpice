<template>
  <header>
    <Navbar />
  </header>
  <main class="bg-grade">
    <router-view />
  </main>
  <footer>
    <Modal id="test-modal">
      <CreateRecipeForm />
    </Modal>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.bg-grade {
  background-image: linear-gradient(to right bottom, #7fb29d, #9ac0a8, #b4ceb5, #cddcc4, #e4ebd6);
}

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
