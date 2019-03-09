import api from '@/services/ApiService';

export default {
    getNotes() {
        return api.get('/notes');
    },
    getNote(id) {
        return api.get(`/notes/${id}`);
    },
    postNote(note) {
        return api.post('/notes', note);
    }
}