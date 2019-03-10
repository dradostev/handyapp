import ReminderService from '@/services/ReminderService';

export default {
    namespaced: true,
    state: {
        reminders: [],
        reminder: {}
    },
    mutations: {
        SET_REMINDERS(state, reminders) {
            state.reminders = reminders;
        },
        SET_REMINDER(state, reminder) {
            state.reminder = reminder;
        }
    },
    actions: {
        fetchReminders({commit}, {limit, offset, filter}) {
            return ReminderService
                .getReminders(limit, offset, filter)
                .then(res => {
                    commit('SET_REMINDERS', res.data);
                })
                .catch(error => console.error(error.response));
        },
        fetchReminder({commit}, id) {
            return ReminderService
                .getReminder(id)
                .then(res => {
                    commit('SET_REMINDER', res.data);
                })
                .catch(error => console.error(error.response));
        }
    }
}