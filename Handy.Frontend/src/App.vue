<template>
  <div id="app">
    <b-container>
      <b-row>
        <SidebarNav />
        <b-col md="11">
          <router-view/>
        </b-col>
      </b-row>
    </b-container>
    
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

</style>