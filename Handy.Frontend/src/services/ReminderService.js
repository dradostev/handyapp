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
    },
    deleteReminder(id) {
        return api.delete(`/reminders/${id}`);
    },
    patchReminderContent(reminder) {
        return api.patch(`/reminders/${reminder.id}/content`, {content: reminder.content});
    },
    patchReminderTime(reminder) {
        return api.patch(`/reminders/${reminder.id}/time`, {fireOn: reminder.fireOn});
    },
    patchReminderEnabled(reminder) {
        return api.patch(`/reminders/${reminder.id}/enabled`, {});
    }
}