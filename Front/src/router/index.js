import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import CalendarContent from '../views/CalendarContent.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/calendar',
    name: 'CalendarContent',
    component: CalendarContent
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
