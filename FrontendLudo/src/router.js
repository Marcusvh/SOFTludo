import { createRouter, createWebHistory } from 'vue-router'
import Start from './views/Start.vue'
import Lobby from './views/Lobby.vue'
import Game from './views/Game.vue'

export default createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: Start },
    { path: '/lobby', component: Lobby },
    { path: '/game', component: Game }
  ]
})
