import { createStore } from "vuex";
import searchString from "./modules/search-string";
export default createStore({
  modules: {
    searchString,
  },
});
