<template>
    <div>
        <h1 class="display-2">Edit "{{ note.title || note.id }}"</h1>
        <hr>
        <b-form @submit.prevent="onSubmit" @reset.prevent="onReset">

            <b-form-group
                label="Note Title:">
                <b-form-input
                    type="text"
                    v-model="note.title"
                    @blur="$v.note.title.$touch()"
                    :state="$v.note.title.$dirty ? !$v.note.title.$error : null"
                    placeholder="Enter title"
                    required />
                <b-form-invalid-feedback v-if="!$v.note.title.required">Title is required field!</b-form-invalid-feedback>
            </b-form-group>

            <b-form-group
                label="Note Content:">
                <b-form-textarea
                    type="text"
                    v-model="note.content"
                    @blur="$v.note.content.$touch()"
                    :state="$v.note.content.$dirty ? !$v.note.content.$error: null"
                    placeholder="Enter your fucking data"
                    rows="7"
                    max-rows="20"
                    required />
                <b-form-invalid-feedback v-if="!$v.note.content.required">Content is required field!</b-form-invalid-feedback>
            </b-form-group>

            <b-form-group>
                <b-button-group>
                    <b-button type="submit" variant="primary"><v-icon name="plus-square" /> Submit</b-button>
                    <b-button type="reset" variant="danger"><v-icon name="recycle" /> Reset</b-button>
                </b-button-group>
            </b-form-group>

        </b-form>
    </div>
</template>

<script>
import { mapActions } from 'vuex';
import { validationMixin } from 'vuelidate';
import { required } from 'vuelidate/lib/validators';

export default {
    props: {
        id: String,
        note: Object
    },
    mixins: [validationMixin],
    validations: {
        note: {
            title: {required},
            content: {required}
        }
    },
    mounted() {
        this.fetchNote(this.id)
    },
    methods: {
        onSubmit() {
            this.updateNote(this.note).then(() => {
                this.$router.push({name: 'note-details', params:{id: this.note.id}});
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
        ...mapActions('note', ['fetchNote', 'updateNote'])
    }
}
</script>

