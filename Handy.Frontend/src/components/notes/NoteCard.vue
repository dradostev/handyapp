<template>
    <b-card img-top>
        <h4 slot="header">{{ note.title || 'Note' }}</h4>
        <b-card-text>
        {{ note.content }}
        </b-card-text>
        <b-button-group>
            <b-button href="#" variant="warning" v-b-tooltip.hover.bottom title="Edit Note"><v-icon name="edit" /></b-button>
            <b-button href="#" variant="dark" v-b-tooltip.hover.bottom title="Details"><v-icon name="arrow-right" /></b-button>
            <b-button href="#" variant="danger" v-b-tooltip.hover.bottom title="Delete Note"><v-icon name="trash-alt" /></b-button>
        </b-button-group>
        <em slot="footer" class="small text-muted">Added on {{ note.created }}</em>
    </b-card>
</template>

<script>
export default {
    props: {
        note: Object
    }
}
</script>
