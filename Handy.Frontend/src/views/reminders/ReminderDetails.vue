<template>
    <div>
        <h1 class="display-4">Remind <em>at</em><br> <span :class="reminder.enabled ? 'text-success' : 'text-muted'"><v-icon name="clock" scale="3" /> {{ reminder.fireOn | formatDate }}</span></h1>
        <hr>
        <b-button-group>
            <b-button :to="{name: 'reminders-list'}" variant="dark"><v-icon name="arrow-left" /> Back to list</b-button>
            <b-button v-b-modal.editContent variant="warning"><v-icon name="edit" /> Edit</b-button>
            <b-button v-b-modal.reassignTime variant="info"><v-icon name="clock" /> Reassign time</b-button>
            <b-button variant="secondary" @click="switchEnabled()"><v-icon name="power-off" /> Switch</b-button>
            <b-button v-b-modal.confirmRemove variant="danger"><v-icon name="trash-alt" /> Delete</b-button>
        </b-button-group>
        <hr>
        <p>{{ reminder.content }}</p>
        <!-- modals -->
        <b-modal id="confirmRemove" @ok="remove()">Are you sure you want to delete reminder for {{ reminder.fireOn | formatDate }}?</b-modal>
        <b-modal id="editContent" title="Edit content" @ok="updateContent">
            <form @submit.stop.prevent="updateContent">
                <b-form-input 
                    type="text" 
                    placeholder="Reminder Text"
                    @blur="$v.reminder.content.$touch()"
                    :state="$v.reminder.content.$dirty ? !$v.reminder.content.$error: null"
                    v-model="reminder.content" />
                <b-form-invalid-feedback v-if="!$v.reminder.content.required">Content is required field!</b-form-invalid-feedback>
            </form>
        </b-modal>
        <b-modal id="reassignTime" title="Assign new trigger time" @ok="updateTime()">
            <form>
                <datetime type="datetime" v-model="reminder.fireOn" input-class="form-control" />
            </form>
        </b-modal>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex';
import { validationMixin } from 'vuelidate';
import { required } from 'vuelidate/lib/validators';

export default {
    props: {
        id: String,
        required: true
    },
    mixins: [validationMixin],
    validations: {
        reminder: {
            content: {required}
        }
    },
    computed: mapState({reminder: state => state.reminder.reminder}),
    created() {
        this.fetchReminder(this.id)
    },
    methods: {
        remove() {
            this.deleteReminder(this.id).then(() => this.$router.push({name: 'reminders-list'}));
        },
        updateContent(e) {
            if (this.$v.$invalid) {
                e.preventDefault();
                return;
            }
            this.updateReminderContent(this.reminder);
        },
        updateTime() {
            this.updateReminderTime(this.reminder);
        },
        switchEnabled() {
            this.switchReminderEnabled(this.reminder)
                .then(() => this.reminder.enabled = !this.reminder.enabled);
        },
        ...mapActions('reminder', [
            'fetchReminder',
            'deleteReminder',
            'updateReminderContent',
            'updateReminderTime',
            'switchReminderEnabled'
        ])
    }
}
</script>
