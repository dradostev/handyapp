import Vue from 'vue';
import Router from 'vue-router';
import store from './store';
import DefaultLayout from '@/views/layouts/DefaultLayout';
import LoginLayout from '@/views/layouts/LoginLayout';
import Home from '@/views/Home';
import Login from '@/views/Login';
import NotesList from '@/views/notes/NotesList';
import NoteDetails from '@/views/notes/NoteDetails';
import NoteCreate from '@/views/notes/NoteCreate';
import NoteEdit from '@/views/notes/NoteEdit';
import RemindersList from '@/views/reminders/RemindersList';

Vue.use(Router)

const authenticated = (to, from, next) => {
  if (store.getters['account/isAuthenticated']) {
    next();
    return;
  }
  next({name: 'login'});
};
const notAuthenticated = (to, from, next) => {
  if (!store.getters['account/isAuthenticated']) {
    next();
    return;
  }
  next({name: 'home'});
};

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: DefaultLayout,
      beforeEnter: authenticated,
      children: [
        {
          path: '/',
          name: 'home',
          component: Home
        },
        {
          path: '/notes',
          name: 'notes-list',
          component: NotesList,
        },
        {
          path: '/notes/create',
          name: 'note-create',
          component: NoteCreate
        },
        {
          path: '/notes/:id',
          name: 'note-details',
          component: NoteDetails,
          props: true
        },
        {
          path: '/notes/:id/edit',
          name: 'note-edit',
          component: NoteEdit,
          props: true
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
      beforeEnter: notAuthenticated,
      children: [
        {
          path: '/login',
          name: 'login',
          component: Login
        }
      ]
    }
  ]
});

export default router;