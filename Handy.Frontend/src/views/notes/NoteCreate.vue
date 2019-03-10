<template>
    <div>
        <h1 class="display-2">Create New Note</h1>
        <hr>
        <b-form @submit.prevent="onSubmit" @reset.prevent="onReset">

            <b-form-group
                label="Note Title:">
                <b-form-input
                    type="text"
                    v-model="note.title"
                    placeholder="Enter title"
                    @blur="$v.note.title.$touch()"
                    :state="$v.note.title.$dirty ? !$v.note.title.$error : null"/>
                <b-form-invalid-feedback v-if="!$v.note.title.required">Title is required field!</b-form-invalid-feedback>
            </b-form-group>

            <b-form-group
                label="Note Content:">
                <b-form-textarea
                    type="text"
                    v-model="note.content"
                    placeholder="Enter your fucking data"
                    @blur="$v.note.content.$touch()"
                    :state="$v.note.content.$dirty ? !$v.note.content.$error: null"
                    rows="7"
                    max-rows="20" />
                <b-form-invalid-feedback v-if="!$v.note.content.required">Content is required field!</b-form-invalid-feedback>
            </b-form-group>

            <b-form-group>
                <b-button-group>
                    <b-button type="submit" variant="primary" :disabled="$v.note.$invalid"><v-icon name="plus-square" /> Submit</b-button>
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
            note: this.emptyModel()
        }
    },
    mixins: [validationMixin],
    validations: {
        note: {
            title: {required},
            content: {required}
        }
    },
    computed: mapState({newNote: state => state.note.note}),
    methods: {
        onSubmit() {
            this.createNote(this.note).then(() => {
                this.$router.push({name: 'note-details', params:{id: this.newNote.id}});
                this.$notify({
                    group: 'messages',
                    type: 'success',
                    title: 'Note created'
                })
            });
        },
        onReset() {
            this.note = this.emptyModel();
        },
        emptyModel() {
            return {
                title: '',
                content: ''
            }
        },
        ...mapActions('note', ['createNote'])
    }
}
</script>

