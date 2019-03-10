<template>
    <div>
        <h1 class="display-2">Create New Reminder</h1>
        <hr>
        <b-form @submit.prevent="onSubmit" @reset.prevent="onReset">

            <b-form-group
                label="Reminder Content:">
                <b-form-textarea
                    type="text"
                    v-model="reminder.content"
                    placeholder="Enter your fucking data"
                    @blur="$v.reminder.content.$touch()"
                    :state="$v.reminder.content.$dirty ? !$v.reminder.content.$error: null"
                    rows="7"
                    max-rows="20" />
                    <b-form-invalid-feedback v-if="!$v.reminder.content.required">Content is required field!</b-form-invalid-feedback>
            </b-form-group>

            <b-form-group label="Trigger On:">
                <datetime type="datetime" v-model="reminder.fireOn" input-class="form-control" />
            </b-form-group>

            <b-form-group>
                <b-button-group>
                    <b-button type="submit" variant="primary" :disabled="$v.reminder.$invalid"><v-icon name="plus-square" /> Submit</b-button>
                    <b-button type="reset" variant="danger"><v-icon name="recycle" /> Reset</b-button>
                </b-button-group>
            </b-form-group>

        </b-form>
    </div>
</template>

<script>
import { mapActions, mapState } from 'vuex';
import { validationMixin } from 'vuelidate';
import { required } from 'vuelidate/lib/validators';

export default {
    data() {
        return {
            reminder: this.emptyModel()
        }
    },
    mixins: [validationMixin],
    validations: {
        reminder: {
            content: {required},
            fireOn: {required}
        }
    },
    computed: mapState({newReminder: state => state.reminder.reminder}),
    methods: {
        onSubmit() {
            this.createReminder(this.reminder).then(() => {
                this.$router.push({name: 'reminder-details', params:{id: this.newReminder.id}});
            });
        },
        onReset() {
            this.reminder = this.emptyModel();
        },
        emptyModel() {
            return {
                content: '',
                fireOn: ''
            }
        },
        ...mapActions('reminder', ['createReminder'])
    }
}
</script>

