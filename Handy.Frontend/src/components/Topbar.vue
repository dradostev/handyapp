<template>
<div>
    <b-navbar toggleable="lg" type="dark" variant="info">
        <b-navbar-brand href="#">Handy.App</b-navbar-brand>

        <b-navbar-toggle target="nav_collapse" />

        <b-collapse is-nav id="nav_collapse">
        <b-navbar-nav>
            <b-nav-item href="#">Link</b-nav-item>
            <b-nav-item href="#" disabled>Disabled</b-nav-item>
        </b-navbar-nav>

        <b-navbar-nav class="ml-auto">
            <b-nav-item-dropdown right>
                <template slot="button-content"><v-icon name="user" /> {{ account.currentUser.screenName || currentUser.login }}</template>
                <b-dropdown-item><v-icon name="envelope" /> {{ account.currentUser.login }}</b-dropdown-item>
                <b-dropdown-item @click="logOut()"><v-icon name="sign-out-alt" /> Sign Out</b-dropdown-item>
            </b-nav-item-dropdown>
        </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</div>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
    computed: mapState(['account']),
    created() {
        this.getProfile();
    },
    methods: {
        logOut() {
            this.signOut();
            this.$router.go({name: 'login'})
        },
        ...mapActions('account', ['signOut', 'getProfile'])
    }
}
</script>
