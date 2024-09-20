// wwwroot/js/snakeInterop.js

window.initializeCanvas = function (canvas, dotNetHelper) {
    document.addEventListener('keydown', function (event) {
        event.preventDefault();
        dotNetHelper.invokeMethodAsync('ChangeDirection', event.keyCode);
    });
};

window.drawGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');

    // Clear the canvas
    context.fillStyle = 'black';
    context.fillRect(0, 0, canvas.width, canvas.height);

    // Draw the snake
    context.fillStyle = 'green';
    gameState.snake.forEach(segment => {
        context.fillRect(segment.x, segment.y, 20, 20);
    });

    // Draw the food
    context.fillStyle = 'red';
    context.fillRect(gameState.food.x, gameState.food.y, 20, 20);

    // Draw the score
    context.fillStyle = 'white';
    context.font = '20px Arial';
    context.fillText(`Food eaten: ${gameState.foodEaten}`, 10, 20);
};