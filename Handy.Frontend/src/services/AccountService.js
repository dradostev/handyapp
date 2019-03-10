import api from '@/services/ApiService';

export default {
    login(email, password) {
        return api.post('account/login', {login: email, password: password});
    },
    getProfile() {
        return api.get('account');
    }
}