// wwwroot/js/brickBreakerInterop.js

window.setCanvasFocus = function (canvas) {
    canvas.focus();
};

window.initializeBrickBreakerCanvas = function (canvas, dotNetHelper) {
    canvas.tabIndex = 0; // Ensure the canvas is focusable
    canvas.focus();

    canvas.addEventListener('keydown', function (event) {
        const key = event.key;
        if (key === 'ArrowLeft' || key === 'ArrowRight') {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('HandleKeyDown', key);
        }
    });

    canvas.addEventListener('keyup', function (event) {
        const key = event.key;
        if (key === 'ArrowLeft' || key === 'ArrowRight') {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('HandleKeyUp', key);
        }
    });
};

window.drawBrickBreakerGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');

    // Fill the background with black
    context.fillStyle = 'black';
    context.fillRect(0, 0, gameState.width, gameState.height);

    // Draw text
    context.fillStyle = 'white';
    context.font = '20px Arial';
    context.fillText(`Bricks destroyed: ${gameState.bricksDestroyed}`, 10, 20);

    // Draw paddle
    context.fillStyle = 'blue';
    context.fillRect(gameState.paddle.x, gameState.paddle.y, gameState.paddle.width, gameState.paddle.height);

    // Draw ball
    context.fillStyle = 'red';
    context.beginPath();
    context.arc(gameState.ball.x, gameState.ball.y, gameState.ball.radius, 0, Math.PI * 2);
    context.closePath();
    context.fill();

    // Draw bricks
    context.fillStyle = 'green';
    gameState.bricks.forEach(brick => {
        if (!brick.hit) {
            context.fillRect(brick.x, brick.y, brick.width, brick.height);
        }
    });
};
