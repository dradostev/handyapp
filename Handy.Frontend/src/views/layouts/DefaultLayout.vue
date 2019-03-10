<template>
    <b-container>
      <b-row>
        <SidebarNav />
        <b-col md="11">
          <router-view />
        </b-col>
      </b-row>
    </b-container>
</template>

<script>
import SidebarNav from '@/components/SidebarNav';
import Topbar from '@/components/Topbar';

export default {
    components: {
        SidebarNav,
        Topbar
    }
}
</script>

<style lang="scss" scoped>
.col-md-11 {
  background-color: #fff;
  box-shadow: 2px 2px 8px rgba(0,0,0,.3);
}
</style>
