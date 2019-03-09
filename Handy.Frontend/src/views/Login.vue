<template>
<div class="form-wrapper">
    <b-card>
        <h1 class="text-center" slot="header"><v-icon name="key" scale="2" /> Sign in to application</h1>
        <b-form>
        <b-form-group label="Email address:">
            <b-form-input type="email" required placeholder="Enter email" />
        </b-form-group>

        <b-form-group label="Password:">
            <b-form-input type="password" required placeholder="Enter password" />
        </b-form-group>

        <b-button type="submit" variant="primary"><v-icon name="sign-in-alt" /> Sign In</b-button>
        </b-form>
    </b-card>
</div>
</template>

<style lang="scss" scoped>
.form-wrapper {
    margin: 8rem 0 0 0;
}
</style>
