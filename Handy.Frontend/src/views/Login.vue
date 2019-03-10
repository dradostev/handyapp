<template>
<div class="form-wrapper">
    <b-card>
        <h1 class="text-center" slot="header"><v-icon name="key" scale="2" /> Sign in to application</h1>
        <b-form @submit.prevent="onSubmit">
        <b-form-group label="Email address:">
            <b-form-input required placeholder="Enter your Telegram login" v-model="loginForm.login" />
        </b-form-group>

        <b-form-group label="Password:">
            <b-form-input type="password" required placeholder="Enter password" v-model="loginForm.password" />
        </b-form-group>

        <b-button type="submit" variant="primary"><v-icon name="sign-in-alt" /> Sign In</b-button>
        </b-form>
    </b-card>
</div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
    data() {
        return {
            loginForm: {
                login: null,
                password: null
            }
        }
    },
    methods: {
        onSubmit() {
            this.signIn(this.loginForm)
                .then(() => {
                    this.$router.go({name: 'home'})
                });
        },
        ...mapActions('account', ['signIn'])
    }
}
</script>


<style lang="scss" scoped>
.form-wrapper {
    margin: 8rem 0 0 0;
}
</style>