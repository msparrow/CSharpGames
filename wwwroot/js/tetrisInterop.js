console.log("tetrisInterop.js loaded");

window.setCanvasFocus = function (canvas) {
    canvas.focus();
};

window.initializeTetrisCanvas = function (canvas, dotNetHelper, width, height) {
    console.log("initializeTetrisCanvas called");
    canvas.tabIndex = 0; // Ensure the canvas is focusable
    canvas.width = width;
    canvas.height = height;
    canvas.focus();

    canvas.addEventListener('keydown', function (event) {
        const key = event.key;
        if (["ArrowLeft", "ArrowRight", "ArrowDown", "ArrowUp", " "].includes(key)) {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('HandleKeyDown', key);
        }
    });
};

window.drawTetrisGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');
    const cellSize = gameState.cellSize;
    const cols = gameState.cols;
    const rows = gameState.rows;

    // Clear the canvas
    context.clearRect(0, 0, canvas.width, canvas.height);

    // Draw the grid
    for (let y = 0; y < rows; y++) {
        for (let x = 0; x < cols; x++) {
            if (gameState.grid[y][x] !== 0) {
                context.fillStyle = "gray"; // You can map numbers to colors
                context.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
            }
        }
    }

    // Draw the current piece
    const shape = gameState.currentPiece.shape;
    const color = gameState.currentPiece.color;
    context.fillStyle = color;
    for (let y = 0; y < shape.length; y++) {
        for (let x = 0; x < shape[y].length; x++) {
            if (shape[y][x] !== 0) {
                context.fillRect(
                    (gameState.currentX + x) * cellSize,
                    (gameState.currentY + y) * cellSize,
                    cellSize,
                    cellSize
                );
            }
        }
    }

    // If game over, display message
    if (gameState.gameOver) {
        context.fillStyle = "red";
        context.font = "30px Arial";
        context.fillText("Game Over", canvas.width / 4, canvas.height / 2);
    }
};