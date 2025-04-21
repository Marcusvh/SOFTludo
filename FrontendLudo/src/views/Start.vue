<template>
  <div class="flex justify-center items-center h-screen">
    <div class="bg-white p-8 rounded shadow-md w-full max-w-md space-y-6">
      <h1 class="text-2xl font-bold text-center">Ludo Game</h1>
      <input v-model="playerName" type="text" placeholder="Your name" class="input" />
      <button @click="createGame" class="btn-primary w-full">Create Game</button>
      <div class="text-center text-gray-500">OR</div>
      <input v-model="gameId" type="text" placeholder="Game ID to join" class="input" />
      <button @click="joinGame" class="btn-secondary w-full">Join Game</button>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import { ref } from 'vue'

const router = useRouter()
const playerName = ref('')
const gameId = ref('')

function createGame() {
  router.push('/lobby')
}

function joinGame() {
  router.push('/lobby')
}
</script>
