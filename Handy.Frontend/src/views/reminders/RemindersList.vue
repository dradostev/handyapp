<template>
    <div>
        <h1 class="display-2 mb-4 text-center">Reminders</h1>
        <b-card class="mb-4">
            <b-form-group label="Filter Reminders">
                <b-form-checkbox-group
                    v-model="filter.selected"
                    switches
                    button-variant="primary"
                    :options="filter.options"
                    @input="fetch()"
                />
            </b-form-group>
        </b-card>
        <b-card-group columns v-if="reminder.reminders.length > 0">
            <ReminderCard v-for="reminder in reminder.reminders" :key="reminder.id" :reminder="reminder" />
        </b-card-group>
        <EmptyList text="No reminders created yet" v-else />
    </div>
</template>

<script>
import EmptyList from '@/components/EmptyList';
import ReminderCard from '@/components/reminders/ReminderCard';
import { mapState, mapActions } from 'vuex';

export default {
    components: {
        EmptyList,
        ReminderCard
    },
    data() {
        return {
            filter: {
                selected: ['active'],
                options: [
                    {text: 'Active', value: 'active'},
                    {text: 'Disabled', value: 'disabled'}
                ]
            }
        }
    },
    computed: mapState(['reminder']),
    mounted() {
        this.fetch();
    },
    methods: {
        fetch() {
            this.fetchReminders({
                limit: 10,
                offset: 0,
                filter: this.filter.selected
            });
        },
        ...mapActions('reminder', ['fetchReminders']),
    } 
}
</script>
