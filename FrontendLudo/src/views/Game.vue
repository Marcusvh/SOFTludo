<template>
  <div id="wrapper">
    <!-- LEFT SIDE: Completed Pieces -->
    <div class="flex flex-col items-start justify-center p-4 bg-white rounded shadow min-h-screen">
      <h2 class="text-xl font-bold mb-4">Goals</h2>
      <div v-for="(pieces, color) in finishedPieces" :key="color" class="mb-4">
        <h3 class="font-semibold capitalize">{{ color }} ({{ pieces.length }})</h3>
        <div class="flex flex-wrap gap-1 mt-1">
          <div
            v-for="(p, idx) in pieces"
            :key="idx"
            class="w-5 h-5 rounded-full"
            :class="getPieceClass(p.color)"
          ></div>
        </div>
      </div>
    </div>

    <!-- MIDDLE: Game Board -->
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
              class="w-6 h-6 rounded-full absolute piece"
              :class="[getPieceClass(cell.piece.color), cell.piece.selected ? 'selected' : '']"
              @click="handlePieceClick(cell.piece, index)"
            ></div>
          </div>
        </template>
      </div>
    </div>

    <!-- RIGHT SIDE: Dice -->
    <div class="diceSection items-center justify-center min-h-screen flex flex-col">
      <h2 class="text-2xl font-bold mb-4">Dice</h2>
      <div class="dice flex flex-col items-center justify-center">
        <div class="dice-face w-16 h-16 bg-white border-4 border-black rounded-lg flex items-center justify-center text-3xl font-bold">
          {{ diceValue }}
        </div>
        <button 
          @click="rollDice"
          :disabled="isDiceDisabled"
          class="mt-4 px-4 py-2 bg-blue-500 text-white rounded">
          Roll Dice
        </button>
      </div>
    </div>
  </div>
</template>


<script setup>
import { ref, onMounted, reactive } from 'vue'

const board = ref(Array(169).fill().map(() => ({ color: '', piece: null })))

const validPositions = [5, 6, 7, 20, 33, 46, 59, 72, 73, 74, 75, 76, 77, 90, 103, 102, 101, 100, 99, 98, 111, 124, 137, 150, 163, 162, 161, 148, 135, 122, 109, 96, 95, 94, 93, 92, 91, 78, 65, 66, 67, 68, 69, 70, 57, 44, 31, 18];
const arrowPositions = {
  red: 78,
  blue: 6,
  yellow: 90,
  green: 162
};

const goalPaths = {
  red: [79, 80, 81, 82, 83],
  blue: [19, 32, 45, 58, 71],
  yellow: [89, 88, 87, 86, 85],
  green: [149, 136, 123, 110, 97]
};

const spawnPoints = {
  red: 66,
  blue: 20,
  yellow: 102,
  green: 148
};

const setupBoard = () => {
  const set = (indexes, colorClass) => {
    indexes.forEach(i => {
      if (board.value[i].color) {
        console.warn(`Position ${i} is being overwritten. Previous color: ${board.value[i].color}, New color: ${colorClass}`);
      }
      board.value[i].color = colorClass;
  });
};
  

  // Home zones (5x5 squares)
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
  set(goalPaths.red, 'bg-red-400')
  set([arrowPositions.red], 'arrow red') // path to goal arrow
  set([spawnPoints.red], 'bg-red-400') // spawn point

  // // Blue path
  set(goalPaths.blue, 'bg-blue-400')
  set([arrowPositions.blue], 'arrow blue') // path to goal arrow
  set([spawnPoints.blue], 'bg-blue-400') // spawn point

  // // Yellow path
  set(goalPaths.yellow, 'bg-yellow-400')
  set([arrowPositions.yellow], 'arrow yellow') // path to goal arrow
  set([spawnPoints.yellow], 'bg-yellow-400') // spawn point

  // // Green path
  set(goalPaths.green, 'bg-green-400')
  set([arrowPositions.green], 'arrow green') // path to goal arrow
  set([spawnPoints.green], 'bg-green-400') // spawn point

  // Center square
  board.value[84].color = 'bg-pink-700'

//   set(validPositions, 'bg-gray-700') // valid white path positions

  placeInitialPieces('red', [14, 16, 40, 42, 94])   // Red piece home spawn
  placeInitialPieces('blue', [22, 24, 48, 50])  // Blue piece home spawn
  placeInitialPieces('yellow', [126, 128, 152, 154])  // Yellow piece home spawn
  placeInitialPieces('green', [118, 120, 146, 144])  // Green piece home spawn

}

const placeInitialPieces = (color, positions) => {
  positions.forEach(pos => {
    board.value[pos].piece = { color }
  })
}

let diceValue = ref(0);
let isDiceDisabled = ref(false);
const hasRolled = ref(false);

const finishedPieces = reactive({
  red: [],
  blue: [],
  green: [],
  yellow: [],
});


const rollDice = () => {
  if (isDiceDisabled.value || hasRolled.value) return;
  console.log('Rolling the dice...');

  isDiceDisabled.value = true;
  diceValue.value = Math.floor(Math.random() * 6) + 1;
  console.log('Dice rolled:', diceValue.value);
  hasRolled.value = true;
  isDiceDisabled.value = false;
};

const movePieceFromHomeToStart = (piece, index) => {
  const color = piece.color;
  const startIndex = colorStartIndices[color];
  const newIndex = validPositions[startIndex];

  if (!board.value[newIndex].piece) {
    board.value[newIndex].piece = piece;
    board.value[index].piece = null;
    piece.selected = false;
    piece.inHome = false;

    console.log(`Moved ${color} piece from home to start.`);
  } else {
    console.log('Start square occupied.');
  }

  resetTurn();
};

let selectedPiece = null;
let selectedPieceIndex = null;
const handlePieceClick = (piece, index) => {
  if (!hasRolled.value) {
    console.log('Roll first.');
    return;
  }

  if (selectedPiece && selectedPieceIndex === index) {
    board.value[index].piece.selected = false;
    selectedPiece = null;
    selectedPieceIndex = null;
  } else {
    if (selectedPieceIndex !== null) {
      board.value[selectedPieceIndex].piece.selected = false;
    }

    selectedPiece = piece;
    selectedPieceIndex = index;
    board.value[index].piece.selected = true;

    const isInHomePath = selectedPiece.homeSteps !== undefined;
    const isOnBoard = validPositions.includes(index);

    if (isInHomePath) {
      moveWithinHomePath();
    } else if (diceValue.value === 6 && !isOnBoard) {
      movePieceFromHomeToStart(piece, index);
    } else if (isOnBoard) {
      movePiece();
    } else {
      console.log('Invalid move.');
    }

  }
};

function moveWithinHomePath() {
  const path = goalPaths[selectedPiece.color];
  let currentStep = selectedPiece.homeSteps || 0;
  let nextStep = currentStep + diceValue.value;

  if (nextStep > goalIndexNumber) {
    const overshoot = nextStep - goalIndexNumber;
    nextStep = goalIndexNumber - overshoot;
    console.log(`Overshot! Bouncing back to step ${nextStep}`);
  }

  if (nextStep === goalIndexNumber) {
    pieceGotToGoal();
    return;
  }

  if (nextStep < 0 || nextStep >= path.length) {
    console.log("Invalid move: out of home path bounds.");
    resetTurn();
    return;
  }

  const newIndex = path[nextStep];
  updateBoard(newIndex);
  selectedPiece.homeSteps = nextStep;
  resetTurn();
}

const colorStartIndices = {
  red: validPositions.indexOf(spawnPoints.red),
  blue: validPositions.indexOf(spawnPoints.blue),
  yellow: validPositions.indexOf(spawnPoints.yellow),
  green: validPositions.indexOf(spawnPoints.green),
};
const goalIndexNumber = 5;

const movePiece = () => {
  if (!selectedPiece || selectedPieceIndex === null || diceValue.value === 0) {
    console.log('Invalid move: missing selection or dice value.');
    return;
  }

  const color = selectedPiece.color;
  const startIndex = colorStartIndices[color];
  const currentIndex = validPositions.indexOf(selectedPieceIndex);

  if (currentIndex === -1) {
    console.log('Invalid move: not on valid path.');
    return;
  }

  const relativeIndex = (currentIndex - startIndex + validPositions.length) % validPositions.length;
  const newRelativeIndex = (relativeIndex + diceValue.value) % validPositions.length;
  const arrowRelIdx = (validPositions.indexOf(arrowPositions[color]) - startIndex + validPositions.length) % validPositions.length;

  if (crossesArrow(relativeIndex, newRelativeIndex, color)) {
    const stepsToArrow = (arrowRelIdx - relativeIndex + validPositions.length) % validPositions.length;
    const arrowAbsoluteIndex = validPositions[(startIndex + arrowRelIdx) % validPositions.length];
    
    board.value[selectedPieceIndex].piece = null;
    board.value[arrowAbsoluteIndex].piece = selectedPiece;
    selectedPieceIndex = arrowAbsoluteIndex;

    const remainingSteps = diceValue.value - stepsToArrow;

    if (remainingSteps > 0) {
      diceValue.value = remainingSteps;
      enterHomePath();
    } else {
      resetTurn();
    }

    return;
  }

  // Normal move
  const newAbsoluteIndex = (startIndex + newRelativeIndex) % validPositions.length;
  const newIndex = validPositions[newAbsoluteIndex];

  if (!board.value[newIndex].piece) {
    board.value[newIndex].piece = selectedPiece;
    board.value[selectedPieceIndex].piece = null;
    selectedPiece.selected = false;

    console.log(`Moved ${color} piece from ${selectedPieceIndex} to ${newIndex}`);
  } else {
    console.log('Target position is occupied.');
  }

  resetTurn();
};

function pieceGotToGoal() {
  console.log('Piece reached the goal!');

  board.value[selectedPieceIndex].piece = null;
  finishedPieces[selectedPiece.color].push(selectedPiece);
  selectedPiece = null;
  selectedPieceIndex = null;
}

function crossesArrow(currentRelIdx, newRelIdx, color) {
  const arrowAbsIdx = validPositions.indexOf(arrowPositions[color]);
  const arrowRelIdx = (arrowAbsIdx - colorStartIndices[color] + validPositions.length) % validPositions.length;

  return (
    (currentRelIdx < arrowRelIdx && newRelIdx >= arrowRelIdx) ||
    (currentRelIdx > newRelIdx && (arrowRelIdx > currentRelIdx || arrowRelIdx <= newRelIdx))
  );
}

function enterHomePath() {
  const path = goalPaths[selectedPiece.color];
  const startIndex = colorStartIndices[selectedPiece.color];
  const currentIndex = validPositions.indexOf(selectedPieceIndex);

  const arrowAbsIdx = validPositions.indexOf(arrowPositions[selectedPiece.color]);
  const arrowRelIdx = (arrowAbsIdx - startIndex + validPositions.length) % validPositions.length;
  const currentRelIdx = (currentIndex - startIndex + validPositions.length) % validPositions.length;

  const stepsToArrow = (arrowRelIdx - currentRelIdx + validPositions.length) % validPositions.length;
  const remainingSteps = diceValue.value - stepsToArrow;

  if (remainingSteps <= 0) {
    console.log("Not enough steps to enter home path.");
    resetTurn();
    return;
  }

  const nextStep = remainingSteps - 1; // Adjust for 0-based index

  if (nextStep === goalIndexNumber) {
    pieceGotToGoal();
    return;
  }

  if (nextStep >= path.length) {
    console.log("Can't move: exceeds home path.");
    resetTurn();
    return;
  }

  const newIndex = path[nextStep];
  updateBoard(newIndex);
  selectedPiece.homeSteps = nextStep;
  resetTurn();
}

function updateBoard(newIndex) {
  board.value[selectedPieceIndex].piece = null;
  board.value[newIndex].piece = selectedPiece;
  board.value[newIndex].piece.selected = false;
  console.log(`Moved piece from ${selectedPieceIndex} to ${newIndex}`);

  isDiceDisabled.value = false;
}

const resetTurn = () => {
  selectedPiece = null;
  selectedPieceIndex = null;
  diceValue.value = 0;
  hasRolled.value = false;
};


// const fetchGameState = async () => {
//   try {
//     const res = await fetch('http://localhost:5000/api/game/state')
//     const data = await res.json()
//     updatePieces(data.pieces)
//   } catch (err) {
//     console.error('Failed to fetch game state', err)
//   }
// }

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
  // fetchGameState()
  // setInterval(fetchGameState, 2000)
})
</script>

<style scoped>
.selected {
  outline: 3px solid #00FFFF; /* neon cyan */
  outline-offset: 2px;
  box-shadow: 0 0 10px #00FFFF;
}



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
  display: grid;
  grid-template-columns: 1fr 2fr 1fr;
  gap: 1rem;
}

</style>