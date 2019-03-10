<template>
    <div>
        <h1 class="display-2 mb-4 text-center">Notes</h1>
        <b-card variant="secondary" bg-variant="secondary" text-variant="white" class="mb-4">
            <p><strong>Notes</strong> contains short texts and pictures you want to store.</p>
            <b-button :to="{name: 'note-create'}" variant="warning"><v-icon name="folder-plus" /> Create Note</b-button>
        </b-card>
        <b-card-group columns v-if="note.notes.length > 0">
            <NoteCard v-for="note in note.notes" :key="note.id" :note="note" />
        </b-card-group>
        <EmptyList text="No notes created yet" v-else />
    </div>
</template>

<script>
import EmptyList from '@/components/EmptyList';
import NoteCard from '@/components/notes/NoteCard';
import { mapState, mapActions } from 'vuex';

export default {
    components: {
        EmptyList,
        NoteCard
    },
    computed: mapState(['note']),
    created() {
        this.fetchNotes({limit: 10, offset: 0});
    },
    methods: {
        ...mapActions('note', ['fetchNotes']),
    } 
}
</script>
