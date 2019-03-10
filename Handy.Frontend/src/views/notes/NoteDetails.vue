<template>
    <div>
        <h1 class="display-2">{{ note.title || note.id }}</h1>
        <b-button-group>
            <b-button :to="{name: 'notes-list'}" variant="dark"><v-icon name="arrow-left" /> Back to list</b-button>
            <b-button :to="{name: 'note-edit', params: {id: note.id, note: note}}" variant="warning"><v-icon name="edit" /> Edit</b-button>
            <b-button v-b-modal.confirmRemove variant="danger"><v-icon name="trash-alt" /> Delete</b-button>
        </b-button-group>
        <hr>
        <p>{{ note.content }}</p>
        <b-modal id="confirmRemove" @ok="remove()">Are you sure you want to delete "{{ note.title || note.id }}"?</b-modal>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
    props: {
        id: String,
        required: true
    },
    computed: mapState({note: state => state.note.note}),
    created() {
        this.fetchNote(this.id)
    },
    methods: {
        remove() {
            this.deleteNote(this.id).then(() => this.$router.push({name: 'notes-list'}));
        },
        ...mapActions('note', ['fetchNote', 'deleteNote'])
    }
}
</script>
