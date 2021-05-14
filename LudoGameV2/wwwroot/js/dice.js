

let btn = document.getElementById('dice_btn');
btn.addEventListener('click', function() {
    const number = Math.trunc(Math.random() * 6 + 1);
    console.log(number);

    dice_image = new Image();
    dice_image.src = `images/dice_images/dice-${number}.png`;
    dice_image.onload = function () {
    return ctx.drawImage(dice_image,170,600,100,100);
}
});