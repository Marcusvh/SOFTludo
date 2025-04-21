<template>
  <div class="flex flex-col items-center justify-center h-screen text-center">
    <h1 class="text-3xl font-semibold mb-6">Lobby</h1>
    <p>Waiting for players...</p>
    <button @click="startGame" class="btn-primary mt-4">Start Game</button>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
const router = useRouter()

function startGame() {
  router.push('/game')
}
</script>
