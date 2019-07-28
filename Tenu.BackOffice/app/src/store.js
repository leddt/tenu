import Vue from "vue";
import Vuex from "vuex";

import contentTree from "@/modules/contentTree";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    contentTree
  }
});
