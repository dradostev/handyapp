<template>
    <div class="no-items">
        <v-icon name="times-circle" scale="7" />
        <p>{{ text }}</p>
    </div>
</template>

<script>
export default {
    props: {
        text: {
            type: String,
            default: ''
        }
    }
}
</script>


<style lang="scss" scoped>
.no-items {
    text-align: center;
    margin: 2rem 0;
    font-size: 3rem;
    color: #999;
    border: 3px dashed #ddd;
    padding: 30px 0 10px 0;
}
</style>