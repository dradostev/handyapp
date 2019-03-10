import AccountService from '@/services/AccountService';
import Vue from 'vue'

export default {
    namespaced: true,
    state: {
        token: localStorage.getItem('token') || '',
        currentUser: {}
    },
    mutations: {
        SET_TOKEN(state, token) {
            localStorage.setItem('token', token);
        },
        UNSET_TOKEN(state) {
            localStorage.removeItem('token');
        },
        SET_CURRENT_USER(state, profile) {
            state.currentUser = profile;
        },
        UNSET_CURRENT_USER(state) {
            state.currentUser = {};
        }
    },
    actions: {
        signInViaTelegram({commit}, account) {
            return AccountService
                .telegramLogin(account)
                .then(res => {
                    commit('SET_TOKEN', res.data.token);
                })
                .catch(error =>  Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        signIn({commit}, loginForm) {
            return AccountService
                .login(loginForm.login, loginForm.password)
                .then(res => {
                    commit('SET_TOKEN', res.data.token);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        signOut({commit}) {
            commit('UNSET_TOKEN');
            commit('UNSET_CURRENT_USER');
        },
        getProfile({commit}) {
            return AccountService
                .getProfile()
                .then(res => {
                    commit('SET_CURRENT_USER', res.data)
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        }
    },
    getters: {
        isAuthenticated: state => !!state.token
    }
}