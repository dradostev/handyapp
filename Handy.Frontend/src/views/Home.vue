<template>
    <div>
        <h1 class="display-1">Handy.App</h1>
        <b-card variant="secondary" bg-variant="secondary" text-variant="white">
            <p>Handy.App is a simple task manager combined with Telegram bot.</p>
            <p>You can add, remove and edit different types of drafts: <strong>Notes</strong> which are simple pieces of text, and <strong>Reminders</strong> which are basically alarms set on definite date and time to trigger.</p>\
            <p>This application was made for demo purposes only.</p>
        </b-card>
        <section class="homepage-section">
            <h2 class="display-4">Latest Notes</h2>
            <b-card-group deck v-if="note.notes.length > 0">
                <NoteCard v-for="note in note.notes" :key="note.id" :note="note" />
            </b-card-group>
            <EmptyList text="No notes created yet" v-else />
        </section>
        <section class="homepage-section">
            <h2 class="display-4">Latest Reminders</h2>
            <b-card-group deck v-if="reminder.reminders.length > 0">
                <ReminderCard v-for="rem in reminder.reminders" :key="rem.id" :reminder="rem" />
            </b-card-group>
            <EmptyList text="No active reminders for now" v-else />
        </section>
    </div>
</template>

<script>
import EmptyList from '@/components/EmptyList';
import NoteCard from '@/components/notes/NoteCard';
import ReminderCard from '@/components/reminders/ReminderCard';
import { mapState, mapActions } from 'vuex';

export default {
    components: {
        EmptyList,
        NoteCard,
        ReminderCard
    },
    computed: mapState(['note', 'reminder']),
    created() {
        this.fetchNotes({limit: 3, offset: 0});
        this.fetchReminders({limit: 3, offset: 0, filter: ['active']});
    },
    methods: {
        ...mapActions('note', ['fetchNotes']),
        ...mapActions('reminder', ['fetchReminders'])
    } 
}
</script>

<style lang="scss" scoped>
.homepage-section {
    margin: 2rem 0;
}

h1, h2 {
    text-align: center;
}
</style>
