import api from '@/services/ApiService';

export default {
    getReminders() {
        return api.get('/reminders');
    },
    getReminder(id) {
        return api.get(`/reminders/${id}`);
    },
    postReminder(reminder) {
        return api.post('/reminders', reminder);
    }
}