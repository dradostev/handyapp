import ReminderService from '@/services/ReminderService';
import Vue from 'vue'

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
        },
        UPDATE_REMINDER(state, reminder) {
            state.reminders = state.reminders.map(x => x.id === reminder.id ? x = reminder : x);
        },
        DELETE_REMINDER(state, id) {
            state.reminders = state.reminders.filter(x => x.id !== id);
        }
    },
    actions: {
        fetchReminders({commit}, {limit, offset, filter}) {
            return ReminderService
                .getReminders(limit, offset, filter)
                .then(res => {
                    commit('SET_REMINDERS', res.data);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        fetchReminder({commit}, id) {
            return ReminderService
                .getReminder(id)
                .then(res => {
                    commit('SET_REMINDER', res.data);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        createReminder({commit}, reminder) {
            return ReminderService
                .postReminder(reminder)
                .then(res => {
                    commit('SET_REMINDER', res.data);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        updateReminderContent({commit}, reminder) {
            return ReminderService
                .patchReminderContent(reminder)
                .then(res => {
                    commit('UPDATE_REMINDER', res.data);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        updateReminderTime({commit}, reminder) {
            return ReminderService
                .patchReminderTime(reminder)
                .then(res => {
                    commit('UPDATE_REMINDER', res.data);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        switchReminderEnabled({commit}, reminder) {
            return ReminderService
                .patchReminderEnabled(reminder)
                .then(res => {
                    commit('UPDATE_REMINDER', res.data);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        },
        deleteReminder({commit}, id) {
            return ReminderService
                .deleteReminder(id)
                .then(() => {
                    commit('DELETE_REMINDER', id);
                })
                .catch(error => Vue.notify({
                    group: 'messages',
                    type: 'error',
                    title: error.response
                }));
        }
    }
}