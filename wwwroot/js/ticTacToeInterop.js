// wwwroot/js/ticTacToeInterop.js

window.initializeTicTacToeCanvas = function (canvas) {
    const context = canvas.getContext('2d');
    context.canvas.width = 300;
    context.canvas.height = 300;
};

window.drawTicTacToeGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');
    const cellSize = gameState.cellSize;
    const width = gameState.width;
    const height = gameState.height;

    // Clear the canvas
    context.clearRect(0, 0, width, height);

    // Fill background
    context.fillStyle = 'black';
    context.fillRect(0, 0, width, height);

    // Draw grid lines
    context.strokeStyle = 'white';
    context.lineWidth = 5;

    for (let i = 1; i < 3; i++) {
        // Vertical lines
        context.beginPath();
        context.moveTo(i * cellSize, 0);
        context.lineTo(i * cellSize, height);
        context.stroke();

        // Horizontal lines
        context.beginPath();
        context.moveTo(0, i * cellSize);
        context.lineTo(width, i * cellSize);
        context.stroke();
    }

    // Draw marks
    for (let row = 0; row < 3; row++) {
        for (let col = 0; col < 3; col++) {
            const player = gameState.board[row][col];
            if (player !== '') {
                drawMark(context, row, col, player, cellSize);
            }
        }
    }
};

function drawMark(context, row, col, player, cellSize) {
    const x = col * cellSize + cellSize / 2;
    const y = row * cellSize + cellSize / 2;

    if (player === 'X') {
        context.strokeStyle = 'red';
        context.lineWidth = 5;
        context.beginPath();
        context.moveTo(x - 25, y - 25);
        context.lineTo(x + 25, y + 25);
        context.stroke();

        context.beginPath();
        context.moveTo(x + 25, y - 25);
        context.lineTo(x - 25, y + 25);
        context.stroke();
    } else if (player === 'O') {
        context.strokeStyle = 'blue';
        context.lineWidth = 5;
        context.beginPath();
        context.arc(x, y, 25, 0, 2 * Math.PI);
        context.stroke();
    }
}