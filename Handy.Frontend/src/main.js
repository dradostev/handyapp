import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import BootstrapVue from "bootstrap-vue";
import 'bootstrap/scss/bootstrap.scss'
import 'bootstrap-vue/src/index.scss'
import 'vue-awesome/icons'
import Icon from 'vue-awesome/components/Icon'
import { Datetime } from 'vue-datetime';
import 'vue-datetime/dist/vue-datetime.css'
import moment from 'moment';
import Notifications from 'vue-notification'

Vue.filter('formatDate', value => {
  if (value) return moment(String(value)).add(moment().utcOffset() / 60, 'hours').format('LLLL');
})

Vue.config.productionTip = false
Vue.use(Notifications)
Vue.use(BootstrapVue);
Vue.component('datetime', Datetime);
Vue.component('v-icon', Icon);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
