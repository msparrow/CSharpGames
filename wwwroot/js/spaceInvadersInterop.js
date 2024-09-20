// wwwroot/js/spaceInvadersInterop.js

window.setCanvasFocus = function (canvas) {
    canvas.focus();
};

window.initializeSpaceInvadersCanvas = function (canvas, dotNetHelper) {
    canvas.tabIndex = 0; // Ensure the canvas is focusable
    canvas.focus();

    canvas.addEventListener('keydown', function (event) {
        const keyCode = event.keyCode;
        if (keyCode === 37 || keyCode === 39 || keyCode === 32) {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('HandleKeyDown', keyCode);
        }
    });

    canvas.addEventListener('keyup', function (event) {
        const keyCode = event.keyCode;
        if (keyCode === 37 || keyCode === 39) {
            event.preventDefault();
            dotNetHelper.invokeMethodAsync('HandleKeyUp', keyCode);
        }
    });
};

window.drawSpaceInvadersGame = function (canvas, gameState) {
    const context = canvas.getContext('2d');

    // Fill the background with black
    context.fillStyle = 'black';
    context.fillRect(0, 0, gameState.width, gameState.height);

    // Draw text
    context.fillStyle = 'white';
    context.font = '20px Arial';
    context.fillText(`Aliens remaining: ${gameState.aliensRemaining}`, 10, 20);

    // Draw ship
    context.fillStyle = 'blue';
    context.fillRect(gameState.ship.x, gameState.ship.y, gameState.ship.width, gameState.ship.height);

    // Draw bullets
    context.fillStyle = 'red';
    gameState.bullets.forEach(bullet => {
        context.fillRect(bullet.x, bullet.y, bullet.width, bullet.height);
    });

    // Draw aliens
    context.fillStyle = 'green';
    gameState.aliens.forEach(alien => {
        if (alien.alive) {
            context.fillRect(alien.x, alien.y, alien.width, alien.height);
        }
    });
};