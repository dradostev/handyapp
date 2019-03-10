import api from '@/services/ApiService';

export default {
    login(login, password) {
        return api.post('/account/login', {login, password});
    },
    register(account) {
        return api.post('/account', account);
    },
    telegramLogin(account) {
        return api.post('/account/telegram-login', account);
    },
    getProfile() {
        return api.get('/account');
    }
}