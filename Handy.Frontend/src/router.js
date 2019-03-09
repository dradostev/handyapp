import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home'
import NotesList from '@/views/NotesList'
import RemindersList from '@/views/RemindersList'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/notes',
      name: 'notes-list',
      component: NotesList
    },
    {
      path: '/reminders',
      name: 'reminders-list',
      component: RemindersList
    },
    {
      path: '/logout',
      name: 'logout',
      component: Home
    }
  ]
})
