// src/router/index.js

import {
  createRouter,
  createWebHistory
} from 'vue-router'

// PAGES
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import Profile from '../views/Profile.vue'
import TransactionList from '../views/TransactionList.vue'
const routes = [

  // HOME PAGE
  {
    path: '/',
    name: 'Home',
    component: Home
  },

  // LOGIN PAGE
  {
    path: '/login',
    name: 'Login',
    component: Login
  },

  // DASHBOARD
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard
  },

  // PROFILE
  {
    path: '/profile',
    name: 'Profile',
    component: Profile
  },

  // TRANSACTIONS
  {
    path: '/transactions',
    name: 'TransactionList',
    component: TransactionList
  }

]

const router = createRouter({

  history: createWebHistory(),

  routes

})

export default router