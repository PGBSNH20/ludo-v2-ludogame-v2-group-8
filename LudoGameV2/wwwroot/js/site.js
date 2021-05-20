// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const canvas = document.getElementById('ludoBoard');
const ctx = canvas.getContext('2d');
// ctx is the drawing object of the canvas.

ctx.fillStyle = 'green';
ctx.fillRect(0, 0, 150, 75);
// 0,0 means that it starts at the top left corner
// 150 is the width of the rectangle 
// 75 is the height of the rectangle.