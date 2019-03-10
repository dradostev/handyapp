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
        },
        UPDATE_NOTE(state, note) {
            state.notes = state.notes.map(x => x.id === note.id ? x = note : x);
        },
        DELETE_NOTE(state, id) {
            state.notes = state.notes.filter(x => x.id !== id);
        }
    },
    actions: {
        fetchNotes({commit}, {limit, offset}) {
            return NotesService
                .getNotes(limit, offset)
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
        },
        createNote({commit}, note) {
            return NotesService
                .postNote(note)
                .then(res => {
                    commit('SET_NOTE', res.data);
                })
                .catch(error => console.error(error.response));
        },
        updateNote({commit}, note) {
            return NotesService
                .putNote(note)
                .then(res => {
                    commit('UPDATE_NOTE', res.data);
                })
                .catch(error => console.error(error.response));
        },
        deleteNote({commit}, id) {
            return NotesService
                .deleteNote(id)
                .then(() => {
                    commit('DELETE_NOTE', id);
                })
                .catch(error => console.error(error.response));
        }
    }
}