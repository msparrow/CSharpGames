// wwwroot/js/pongInterop.js

window.setCanvasFocus = function (canvas) {
    canvas.focus();
};

window.initializePongCanvas = function (canvas, dotNetHelper) {
    canvas.tabIndex = 0; // Ensure the canvas is focusable
    canvas.focus();

    canvas.addEventListener('keydown', function (event) {
        const keyCode = event.keyCode;
        if (keyCode === 38 || keyCode === 40) {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('KeyEvent', keyCode, true);
        }
    });

    canvas.addEventListener('keyup', function (event) {
        const keyCode = event.keyCode;
        if (keyCode === 38 || keyCode === 40) {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('KeyEvent', keyCode, false);
        }
    });
};

window.drawPongGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');

    // Clear the canvas
    context.clearRect(0, 0, gameState.width, gameState.height);
    context.fillStyle = 'black';
    context.fillRect(0, 0, gameState.width, gameState.height);

    // Draw paddles
    context.fillStyle = 'white';
    context.fillRect(0, gameState.leftPaddleY, gameState.paddleWidth, gameState.paddleHeight);
    context.fillRect(gameState.width - gameState.paddleWidth, gameState.rightPaddleY, gameState.paddleWidth, gameState.paddleHeight);

    // Draw ball
    context.beginPath();
    context.arc(gameState.ballX, gameState.ballY, gameState.ballSize, 0, Math.PI * 2);
    context.closePath();
    context.fill();
};