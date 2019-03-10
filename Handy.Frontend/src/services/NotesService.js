import api from '@/services/ApiService';

export default {
    getNotes(limit, offset) {
        return api.get('/notes', {params: {limit, offset}});
    },
    getNote(id) {
        return api.get(`/notes/${id}`);
    },
    postNote(note) {
        return api.post('/notes', note);
    },
    putNote(note) {
        return api.put(`/notes/${note.id}`, note);
    },
    deleteNote(id) {
        return api.delete(`/notes/${id}`);
    }
}