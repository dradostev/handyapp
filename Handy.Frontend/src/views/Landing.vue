<template>
    <b-col md="12">
        <b-jumbotron header="Handy.App" lead="Simple Demo Application with Telegram Bot">
            <p>For more information visit website</p>
            <b-button variant="primary" href="#">More Info</b-button>
            <vue-telegram-login 
                mode="callback"
                telegram-login="TestBydloBot"
                @callback="logInOrRegister" />
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
    margin: 5rem 0;
}
</style>