import NotesService from '@/services/NotesService';

export default {
    namespaced: true,
    state: {
        notes: [],
        note: {}
    },
    mutations: {
        SET_NOTES(state, notes) {
            state.notes = notes;
        },
        SET_NOTE(state, note) {
            state.note = note;
        }
    },
    actions: {
        fetchNotes({commit}) {
            return NotesService
                .getNotes()
                .then(res => {
                    commit('SET_NOTES', res.data);
                })
                .catch(error => console.error(error.response));
        },
        fetchNote({commit}, id) {
            return NotesService
                .getNote(id)
                .then(res => {
                    commit('SET_NOTE', res.data);
                })
                .catch(error => console.error(error.response));
        }
    }
}