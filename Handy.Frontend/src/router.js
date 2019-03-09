import Vue from 'vue';
import Router from 'vue-router';
import DefaultLayout from '@/views/layouts/DefaultLayout';
import LoginLayout from '@/views/layouts/LoginLayout';
import Home from '@/views/Home';
import Login from '@/views/Login';
import NotesList from '@/views/NotesList';
import RemindersList from '@/views/RemindersList';

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      component: DefaultLayout,
      children: [
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
        }
      ]
    },
    {
      path: '/login',
      component: LoginLayout,
      children: [
        {
          path: '/login',
          name: 'login',
          component: Login
        }
      ]
    }
  ]
})
