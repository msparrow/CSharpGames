// wwwroot/js/flappyBirdInterop.js

window.setCanvasFocus = function (canvas) {
    canvas.focus();
};

window.initializeFlappyBirdCanvas = function (canvas, dotNetHelper) {
    canvas.tabIndex = 0; // Ensure the canvas is focusable
    canvas.focus();

    canvas.addEventListener('keydown', function (event) {
        const keyCode = event.keyCode;
        if (keyCode === 32) { // Space bar
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('HandleKeyDown', keyCode);
        }
    });
};

window.drawFlappyBirdGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');
    const width = gameState.width;
    const height = gameState.height;

    // Fill the background with sky blue
    context.fillStyle = 'skyblue';
    context.fillRect(0, 0, width, height);

    // Draw bird
    const bird = gameState.bird;
    context.fillStyle = 'yellow';
    context.beginPath();
    context.arc(bird.x, bird.y, bird.radius, 0, Math.PI * 2);
    context.closePath();
    context.fill();

    // Draw pipes
    context.fillStyle = 'green';
    const pipes = gameState.pipes;
    const pipeWidth = gameState.pipeWidth;
    pipes.forEach(pipe => {
        context.fillRect(pipe.x, 0, pipeWidth, pipe.top);
        context.fillRect(pipe.x, height - pipe.bottom, pipeWidth, pipe.bottom);
    });

    // Draw score
    context.fillStyle = 'white';
    context.font = '20px Arial';
    context.fillText(`Score: ${gameState.score}`, 10, 20);
};