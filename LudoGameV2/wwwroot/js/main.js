const canvas = document.getElementById('ludoBoard');
canvas.height = 800;
canvas.width = canvas.height;
const ctx = canvas.getContext('2d');

const diceBtn = document.getElementById('dice_btn');
function randomNumber() {
    return Math.trunc(Math.random() * 6 + 1);
}

// Canvas automatic resizing
function resize() {
    // Our canvas must cover full height of screen regardless of the resolution
    const height = window.innerHeight - 20;
  
    // So we need to calculate the proper scaled width that should work well with every resolution
    const ratio = canvas.width / canvas.height;
    const width = height * ratio;
  
    canvas.style.width = width + 'px';
    canvas.style.height = height + 'px';

    // Just to see how big the active playing field is...
    // console.log(document.getElementById('ludoBoard').style.height);
}

// resizes the canvas every 0ms
setInterval(() => {
    document.getElementById('body').onresize = resize();
}, 0);

function GameBasics(canvas) {

    this.canvas = canvas;
    this.width = canvas.width;
    this.height = canvas.height;

    this.setting = {
        // game setting
    };

    // We collect here the different positions, states of the game
    this.positionContainer = [];
}

// let ludoBoard_image = new Image();
// ludoBoard_image.src = 'images/Ludo_board.svg';

// ludoBoard_image.onload = () => {
//     return ctx.drawImage(ludoBoard_image,0,0);
// }