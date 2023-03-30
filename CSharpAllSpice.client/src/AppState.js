import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Recipe.js').Recipe | null} */
  recipe: null,
  activeRecipe: null,
  /** @type {import('./models/Recipe.js').Recipe[]} */
  recipes: [],
  /** @type {import('./models/Recipe.js').Recipe[]} */
  myFavorites: [],
  /** @type {import('./models/Favorite.js').Favorite[]} */
  favorites: [],
  /** @type {import('./models/Favorite.js').Favorite | null} */
  favorite: null,

  /** @type {import('./models/Ingredient.js').Ingredient[]} */
  ingredients: [],
  /** @type {import('./models/Ingredient.js').Ingredient| Null} */
  ingredient: null,

  editInstruction: false,
  allRecipes: null,
  recipe: null,
})
