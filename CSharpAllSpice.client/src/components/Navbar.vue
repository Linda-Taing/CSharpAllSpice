<template>
  <nav class="navbar navbar-expand-lg spice-bg  navbar-dark px-3 ">
    <!-- <img class="spice-bg p-0" src="../assets/img/ALLSPICE.png" alt=""> -->
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column text-dark align-items-center">
        <!-- NOTE: CHANGE FONT -->
        <p class="text-center text-light">All Spice</p>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <!-- FIXME: trying to add a search bar!! -->
      <!-- <div v-if="recipes">
        <form @submit="searchCategory()">
          <div class="input-group mb-3 me-3">
            <input v-model="editable.search" required type="text" class="form-control" placeholder="Search Recipes"
              aria-label="Search Recipes" aria-describedby="button-addon2">
            <button class="btn btn-outline-secondary" type="submit" id="button-addon2">
              <i class="mdi mdi-magnify"></i>
            </button>
          </div>
        </form>
      </div> -->
      <ul class="navbar-nav me-auto">
        <li>
          <!-- <router-link :to="{ name: 'About' }" class="btn text-light lighten-30 selectable text-uppercase">
            About
          </router-link> -->
        </li>
      </ul>

      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import Login from './Login.vue'
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
export default {
  setup() {
    const editable = ref({
      search: ""
    })

    function searchCategory() {
      logger.log('[YOU HERE SEARCHING?]')
      setTimeout(() => {
        AppState.recipe = AppState.allRecipes.filter(r => r.category.toLowerCase().includes(editable.value.search.toLowerCase()))
      }, 300)
      if (editable.value.search == "") { AppState.recipe = AppState.allRecipes, logger.log('[ARE YOU SEARCHING RECIPES?]') }

    }
    return {
      recipes: computed(() => AppState.allRecipes),
      searchCategory
    }
  },
  components: { Login }
}
</script>

<style scoped>
.spice-bg {
  background-image: url("../assets/img/ALLSPICE.png");
  height: 200px;

  background-position: center;
  background-size: cover;
}

.bg-grade {

  background-image: linear-gradient(to bottom, #7fb29d, #9ac0a8, #b4ceb5, #cddcc4, #e4ebd6);
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 200px;
  }
}
</style>
