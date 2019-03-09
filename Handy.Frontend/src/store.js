import Vue from 'vue'
import Vuex from 'vuex'
import account from '@/store/account';
import note from '@/store/note';

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    account,
    note
  },
  state: {

  },
  mutations: {

  },
  actions: {

  }
})
