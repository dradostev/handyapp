<template>
    <b-card img-top md="4">
        <h4 slot="header">{{ note.title || 'Note' }}</h4>
        <b-card-text>
        {{ note.content }}
        </b-card-text>
        <b-button :to="{'name': 'note-details', params:{id: note.id}}" variant="dark" class="pull-right">Details <v-icon name="arrow-right" /></b-button>
        <em slot="footer" class="small text-muted">Added on {{ note.created | formatDate }}</em>
    </b-card>
</template>

<script>
export default {
    props: {
        note: Object,
        required: true
    }
}
</script>
