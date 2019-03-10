import api from '@/services/ApiService';

export default {
    getReminders(limit, offset, filter) {
        return api.get('/reminders', {params: {limit, offset, filter}});
    },
    getReminder(id) {
        return api.get(`/reminders/${id}`);
    },
    postReminder(reminder) {
        return api.post('/reminders', reminder);
    }
}