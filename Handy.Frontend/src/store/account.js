import AccountService from '@/services/AccountService';

export default {
    namespaced: true,
    state: {
        token: localStorage.getItem('token') || ''
    },
    mutations: {
        SET_TOKEN(state, token) {
            localStorage.setItem('token', token);
        },
        UNSET_TOKEN(state) {
            localStorage.removeItem('token');
        }
    },
    actions: {
        signIn({commit}, loginForm) {
            return AccountService
                .login(loginForm.email, loginForm.password)
                .then(res => {
                    commit('SET_TOKEN', res.data.token);
                })
                .catch(error => console.error(error.response));
        },
        signOut({commit}) {
            commit('UNSET_TOKEN');
        }
    },
    getters: {
        isAuthenticated: state => !!state.token
    }
}