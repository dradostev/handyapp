<template>
    <b-card img-top>
        <h4 slot="header" :class="reminder.enabled ? 'text-success' : 'text-muted'">@ {{ reminder.fireOn | formatDate }}</h4>
        <b-card-text>
        {{ reminder.content }}
        </b-card-text>
        <b-button-group>
            <b-button :to="{name: 'reminder-details', params: {id: reminder.id}}" variant="dark" v-b-tooltip.hover.bottom title="Details"><v-icon name="arrow-right" /></b-button>
            <b-button href="#" variant="info" v-b-tooltip.hover.bottom title="Reassign to another time"><v-icon name="clock" /></b-button>
            <b-button href="#" variant="secondary" v-b-tooltip.hover.bottom title="Turn Off"><v-icon name="power-off" /></b-button>
        </b-button-group>
    </b-card>
</template>

<script>
export default {
    props: {
        reminder: Object,
        required: true
    }
}
</script>
