<template>
  <div id="app">
    <notifications group="messages" />
    <router-view />
  </div>
</template>

<script>
  import SidebarNav from '@/components/SidebarNav';
  
  export default {
    components: {
      SidebarNav
    }
  }
</script>

<style lang="scss">
body {
  background-image: url('./assets/irongrip.png');
}
</style>