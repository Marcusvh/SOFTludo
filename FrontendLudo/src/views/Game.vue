<template>
  <div id="wrapper">
    <div id="wrapper">
    <!-- Left Section -->
    <div class="section">
      <!-- Left content (can be empty) -->
    </div>
    </div>
    <div class="flex flex-col items-center justify-center min-h-screen bg-gray-100">
      <h2 class="text-2xl font-bold mb-4">Ludo Game Board</h2>
      <div class="grid grid-cols-13 grid-rows-13 w-[600px] h-[600px] border-4 border-black">
        <template v-for="(cell, index) in board" :key="index">
          <div
            class="relative border border-black flex items-center justify-center text-xs"
            :class="cell.color"
          >
            <div
              v-if="cell.piece"
              class="w-6 h-6 rounded-full absolute"
              :class="getPieceClass(cell.piece.color)"
            ></div>
          </div>
        </template>
      </div>
    </div>

    <div class="diceSection">
      <h1>tESTo</h1>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const board = ref(Array(169).fill().map(() => ({ color: '', piece: null })))

const setupBoard = () => {
  const set = (indexes, colorClass) => {
    indexes.forEach(i => board.value[i].color = colorClass)
  }

  // Home zones (4x4 squares)
  set([
    0,1,2,3,4,13,14,15,16,17,26,27,28,29,30,39,40,41,42,43,52,53,54,55,56
  ], 'bg-red-500')
  set([
    8,9,10,11,12,21,22,23,24,25,34,35,36,37,38,47,48,49,50,51,60,61,62,63,64
  ], 'bg-blue-500')
  set([
    104,105,106,107,108,117,118,119,120,121,130,131,132,133,134,143,144,145,146,147,156,157,158,159,160
  ], 'bg-green-500')
  set([
    112,113,114,115,116,125,126,127,128,129,138,139,140,141,142,151,152,153,154,155,164,165,166,167,168
  ], 'bg-yellow-500')

  // Red path
  set([79,80,81,82,83], 'bg-red-400')
  set([78], 'arrow red') // path to goal arrow
  set([66], 'bg-red-400') // spawn point

  // // Blue path
  set([19,32,45,58,71], 'bg-blue-400')
  set([6], 'arrow blue') // path to goal arrow
  set([20], 'bg-blue-400') // spawn point

  // // Yellow path
  set([85,86,87,88,89], 'bg-yellow-400')
  set([90], 'arrow yellow') // path to goal arrow
  set([102], 'bg-yellow-400') // spawn point

  // // Green path
  set([97,110,123,136,149], 'bg-green-400')
  set([162], 'arrow green') // path to goal arrow
  set([148], 'bg-green-400') // spawn point

  // Center square
  board.value[84].color = 'bg-pink-700'

  placeInitialPieces('red', [14, 16, 40, 42])   // Red piece spawn points
  placeInitialPieces('blue', [22, 24, 48, 50])  // Blue piece spawn points
  placeInitialPieces('yellow', [126, 128, 152, 154])  // Yellow piece spawn points
  placeInitialPieces('green', [118, 120, 146, 144])  // Green piece spawn points

}

const placeInitialPieces = (color, positions) => {
  positions.forEach(pos => {
    board.value[pos].piece = { color }
  })
}

const fetchGameState = async () => {
  try {
    const res = await fetch('http://localhost:5000/api/game/state')
    const data = await res.json()
    updatePieces(data.pieces)
  } catch (err) {
    console.error('Failed to fetch game state', err)
  }
}

const updatePieces = (pieces) => {
  board.value.forEach(cell => cell.piece = null)
  pieces.forEach(p => {
    board.value[p.position].piece = { color: p.color }
  })
}

const getPieceClass = (color) => {
  return {
    red: 'bg-red-700 border-2 border-black',
    green: 'bg-green-700 border-2 border-black',
    blue: 'bg-blue-700 border-2 border-black',
    yellow: 'bg-yellow-700 border-2 border-black'
  }[color] || 'bg-black'
}

onMounted(() => {
  setupBoard()
  fetchGameState()
  setInterval(fetchGameState, 2000)
})
</script>

<style scoped>
.grid-cols-13 {
  grid-template-columns: repeat(13, minmax(0, 1fr));
}
.grid-rows-13 {
  grid-template-rows: repeat(13, minmax(0, 1fr));
}
.arrow {
  position: relative;
  background-color: white;
}

.arrow::before {
  content: '';
  position: absolute;
  top: 50%;
  left: 50%;
  width: 0;
  height: 0;
  transform: translate(-50%, -50%) rotate(0deg);
  border-left: 10px solid transparent;
  border-right: 10px solid transparent;
  border-bottom: 16px solid;
}

.arrow.red::before {
  border-bottom-color: #f87171;
  transform: translate(-50%, -50%) rotate(90deg); /* Pointing down */
}

.arrow.blue::before {
  border-bottom-color: #60a5fa;
  transform: translate(-50%, -50%) rotate(180deg); /* Pointing left */
}

.arrow.green::before {
  border-bottom-color: #34d399;
  transform: translate(-50%, -50%) rotate(0deg); /* Pointing up */
}

.arrow.yellow::before {
  border-bottom-color: #facc15;
  transform: translate(-50%, -50%) rotate(270deg); /* Pointing right */
}

#wrapper {
  display:grid;
  grid-template-columns: 1fr 1fr 1fr;
}
</style>