import Vue from 'vue';
import Router from 'vue-router';
import store from './store';
import NProgress from 'nprogress';
import DefaultLayout from '@/views/layouts/DefaultLayout';
import LoginLayout from '@/views/layouts/LoginLayout';
import LandingLayout from '@/views/layouts/LandingLayout';
import Home from '@/views/Home';
import Login from '@/views/Login';
import Landing from '@/views/Landing';
import NotesList from '@/views/notes/NotesList';
import NoteDetails from '@/views/notes/NoteDetails';
import NoteCreate from '@/views/notes/NoteCreate';
import NoteEdit from '@/views/notes/NoteEdit';
import RemindersList from '@/views/reminders/RemindersList';
import ReminderDetails from '@/views/reminders/ReminderDetails';
import ReminderCreate from '@/views/reminders/ReminderCreate';

Vue.use(Router)

const authenticated = (to, from, next) => {
  if (store.getters['account/isAuthenticated']) {
    next();
    return;
  }
  next({name: 'landing'});
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
        },
        {
          path: '/reminders/create',
          name: 'reminder-create',
          component: ReminderCreate
        },
        {
          path: '/reminders/:id',
          name: 'reminder-details',
          component: ReminderDetails,
          props: true
        }
      ]
    },
    // {
    //   path: '/login',
    //   component: LoginLayout,
    //   beforeEnter: notAuthenticated,
    //   children: [
    //     {
    //       path: '/login',
    //       name: 'login',
    //       component: Login
    //     }
    //   ]
    // },
    {
      path: '/landing',
      component: LandingLayout,
      beforeEnter: notAuthenticated,
      children: [
        {
          path: '/landing',
          name: 'landing',
          component: Landing
        }
      ]
    }
  ]
});

router.beforeEach((routeTo, routeFrom, next) => {
  NProgress.start();
  next();
});

router.afterEach(() => NProgress.done());

export default router;