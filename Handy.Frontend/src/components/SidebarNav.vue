<template>
    <b-col md="1">
        <b-nav vertical pills>
            <b-nav-item :to="{name: 'home'}" v-b-tooltip.hover.right title="Home Page"><v-icon name="home" scale="2" /></b-nav-item>
            <b-nav-item :to="{name: 'notes-list'}" v-b-tooltip.hover.right title="Notes"><v-icon name="sticky-note" scale="2" /></b-nav-item>
            <b-nav-item :to="{name: 'reminders-list'}" v-b-tooltip.hover.right title="Reminders"><v-icon name="clock" scale="2" /></b-nav-item>
            <b-nav-item :to="{name: 'login'}" v-b-tooltip.hover.right title="Log Out"><v-icon name="sign-out-alt" scale="2" /></b-nav-item>
        </b-nav>
    </b-col>
</template>

<script>
export default {
    
}
</script>


<style lang="scss" scoped>
@import '~bootstrap/scss/bootstrap.scss';
.nav {
    background-color: $purple;
    box-shadow: 2px 2px 8px rgba(0,0,0,.3);
    height: 100%;
}

.nav-item {
    padding: 5px 0;
}

a {
    color: $white;
    &:hover, &.router-link-exact-active {
        color: $yellow;
    }
}
</style>
