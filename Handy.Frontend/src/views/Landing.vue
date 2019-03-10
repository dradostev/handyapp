<template>
    <b-col md="12">
        <b-jumbotron header="Handy.App" lead="Very Simple Demo Application with Telegram Bot" bg-variant="dark" text-variant="white">
            <p>You can log in using your Telegram account</p>

            <vue-telegram-login 
                mode="callback"
                telegram-login="TestBydloBot"
                @callback="logInOrRegister" />
                
            <hr class="my-4" />
            <b-button variant="warning" href="https://github.com/dradostev/handyapp" size="lg"><v-icon name="code" scale="1.5" /> Code on GitHub</b-button>
        </b-jumbotron>
    </b-col>
</template>

<script>
import { vueTelegramLogin } from 'vue-telegram-login';
import { mapActions } from 'vuex';

export default {
    components: {
        vueTelegramLogin
    },
    methods: {
        logInOrRegister(user) {
            this.signInViaTelegram({
                login: user.username,
                chatId: user.id,
                screenName: `${user.first_name} ${user.last_name}`
            })
            .then(() => this.$router.go({name: 'home'}));
        },
        ...mapActions('account', ['signInViaTelegram'])
    }
}
</script>

<style lang="scss" scoped>
.col-md-12 {
  background-color: #fff;
  box-shadow: 2px 2px 8px rgba(0,0,0,.3);
}

.jumbotron {
    margin: 10rem 0 20rem 0;
}
</style>