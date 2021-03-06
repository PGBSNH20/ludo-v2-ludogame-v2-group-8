var currPos = 0;
var step = 49.5;
var currcolor = "";
var NumOfPaw = "";
var num = 0;
var clicked = false;
var currpawn = "";
var allcolor = ["red", "blue", "green", "yellow"];
var inGoal = { red: 0, blue: 0, green: 0, yellow: 0 };
var pawnData = [];
var gameLoaded = false;
var playerAmount = 2; // Glöm inte att lägga till för tre spelare

var positions = {
    redpawn1: 0,
    redpawn2: 0,
    redpawn3: 0,
    redpawn4: 0,
    bluepawn1: 0,
    bluepawn2: 0,
    bluepawn3: 0,
    bluepawn4: 0,
    greenpawn1: 0,
    greenpawn2: 0,
    greenpawn3: 0,
    greenpawn4: 0,
    yellowpawn1: 0,
    yellowpawn2: 0,
    yellowpawn3: 0,
    yellowpawn4: 0,
};
var onboard = {
    redpawn1: 0,
    redpawn2: 0,
    redpawn3: 0,
    redpawn4: 0,
    bluepawn1: 0,
    bluepawn2: 0,
    bluepawn3: 0,
    bluepawn4: 0,
    greenpawn1: 0,
    greenpawn2: 0,
    greenpawn3: 0,
    greenpawn4: 0,
    yellowpawn1: 0,
    yellowpawn2: 0,
    yellowpawn3: 0,
    yellowpawn4: 0,
};

function HaveHover() {
    var count = 0;
    var toKnock = "";
    for (var i = 0; i < allcolor.length; i++) {
        for (var n = 1; n <= 4; n++) {
            var firstPawn = document.getElementById(allcolor[i] + "pawn" + n);
            var secondPawn = document.getElementById(currpawn);
            const uncertainty = 5;
            if (
                parseInt(firstPawn.style.top) + uncertainty >=
                parseInt(secondPawn.style.top) &&
                parseInt(firstPawn.style.top) - uncertainty <=
                parseInt(secondPawn.style.top) &&
                parseInt(firstPawn.style.left) + uncertainty >=
                parseInt(secondPawn.style.left) &&
                parseInt(firstPawn.style.left) - uncertainty <=
                parseInt(secondPawn.style.left) &&
                currcolor != allcolor[i] &&
                currPos + num < 44
            ) {
                count++;
                toKnock = allcolor[i] + "pawn" + n;
                return toKnock;
            }
        }
    }
    return false;
}

function Stuck() {
    var text = document.getElementById("player");
    if (onboard[currpawn] == 0 || currPos + num > 44) {
        if (DontHaveOtherFree() || currPos + num > 44) {
            var badtext = document.getElementById("badtext");
            badtext.innerText = "Unfortunatlly you stuck";
            clicked = false;
            var dice = document.getElementById("dice");
            dice.style.backgroundImage = "url(../js/Images/dice1.gif)";
            window.setTimeout(changePlayer, 1000);
        }
    }
}
function changePlayer() {
    if (num != 6 && playerAmount == 4) {
        var text = document.getElementById("player");
        switch (text.innerText) {
            case "red":
                text.innerText = text.style.color = "blue";
                break;
            case "blue":
                text.innerText = text.style.color = "yellow";
                break;
            case "yellow":
                text.innerText = text.style.color = "green";
                break;
            case "green":
                text.innerText = text.style.color = "red";
                break;
        }
    }

    if (
        num != 6 &&
        playerAmount == 2 &&
        (player.style.color == "red" || player.style.color == "yellow")
    ) {
        var text = document.getElementById("player");
        switch (text.innerText) {
            case "red":
                text.innerText = text.style.color = "yellow";
                break;
            case "yellow":
                text.innerText = text.style.color = "red";
                break;
        }
    }

    if (
        num != 6 &&
        playerAmount == 2 &&
        (player.style.color == "blue" || player.style.color == "green")
    ) {
        var text = document.getElementById("player");
        switch (text.innerText) {
            case "blue":
                text.innerText = text.style.color = "green";
                break;
            case "green":
                text.innerText = text.style.color = "blue";
                break;
        }
    }

    var badtext = document.getElementById("badtext");
    badtext.innerText = "";

    //   document.getElementById("takeoutpiece-log").innerText = "";
    //   document.getElementById("knockout-log").innerText = "";
    //   document.getElementById("action-log").innerText = "";

    var dice = document.getElementById("dice");
    dice.style.backgroundImage = "url(../js/Images/dice1.gif)";
}

function DontHaveOtherFree() {
    var text = document.getElementById("player");
    for (var i = 1; i <= 4; i++) {
        if (
            onboard[text.innerText + "pawn" + i] == 1 ||
            positions[text.innerText + "pawn" + i] + num >= 44
        )
            return false;
    }
    return true;
}
function CheckForWinner() {
    if (inGoal[currcolor] == 4) {
        var dice = document.getElementById("dice");
        var player = document.getElementById("badtext");
        dice.innerText = "";
        dice.style.visibility = "hidden";
        player.innerText = "The Winner is the " + currcolor + " player";
    }
}
function stepDown() {
    var doc = document.getElementById(currcolor + "pawn" + NumOfPaw);
    var curr = Number(doc.style.top.replace(/[a-z]/g, ""));
    doc.style.top = curr + step + "px";
    currPos++;
}
function stepUp() {
    var doc = document.getElementById(currpawn);
    var curr = Number(doc.style.top.replace(/[a-z]/g, ""));
    doc.style.top = curr - step + "px";
    currPos++;
}
function stepLeft() {
    var doc = document.getElementById(currpawn);
    var curr = Number(doc.style.left.replace(/[a-z]/g, ""));
    doc.style.left = curr - step + "px";
    currPos++;
}
function stepRight() {
    var doc = document.getElementById(currpawn);
    var curr = Number(doc.style.left.replace(/[a-z]/g, ""));
    doc.style.left = curr + step + "px";
    currPos++;
}
var stepsRed = [];
var stepsYellow = [];
var stepsBlue = [];
var stepsGreen = [];
function pushSteps(value, steps, count) {
    for (i = 0; i < count; i++) steps.push(value);
}
//Red pawns path
pushSteps(stepDown, stepsRed, 4);
pushSteps(stepRight, stepsRed, 4);
pushSteps(stepDown, stepsRed, 2);
pushSteps(stepLeft, stepsRed, 4);
pushSteps(stepDown, stepsRed, 4);
pushSteps(stepLeft, stepsRed, 2);
pushSteps(stepUp, stepsRed, 4);
pushSteps(stepLeft, stepsRed, 4);
pushSteps(stepUp, stepsRed, 2);
pushSteps(stepRight, stepsRed, 4);
pushSteps(stepUp, stepsRed, 4);
pushSteps(stepRight, stepsRed, 1);
pushSteps(stepDown, stepsRed, 5);

//Yellow pawns path
pushSteps(stepUp, stepsYellow, 4);
pushSteps(stepLeft, stepsYellow, 4);
pushSteps(stepUp, stepsYellow, 2);
pushSteps(stepRight, stepsYellow, 4);
pushSteps(stepUp, stepsYellow, 4);
pushSteps(stepRight, stepsYellow, 2);
pushSteps(stepDown, stepsYellow, 4);
pushSteps(stepRight, stepsYellow, 4);
pushSteps(stepDown, stepsYellow, 2);
pushSteps(stepLeft, stepsYellow, 4);
pushSteps(stepDown, stepsYellow, 4);
pushSteps(stepLeft, stepsYellow, 1);
pushSteps(stepUp, stepsYellow, 5);

//Blue pawns path
pushSteps(stepLeft, stepsBlue, 4);
pushSteps(stepDown, stepsBlue, 4);
pushSteps(stepLeft, stepsBlue, 2);
pushSteps(stepUp, stepsBlue, 4);
pushSteps(stepLeft, stepsBlue, 4);
pushSteps(stepUp, stepsBlue, 2);
pushSteps(stepRight, stepsBlue, 4);
pushSteps(stepUp, stepsBlue, 4);
pushSteps(stepRight, stepsBlue, 2);
pushSteps(stepDown, stepsBlue, 4);
pushSteps(stepRight, stepsBlue, 4);
pushSteps(stepDown, stepsBlue, 1);
pushSteps(stepLeft, stepsBlue, 5);

//Green pawns path
pushSteps(stepRight, stepsGreen, 4);
pushSteps(stepUp, stepsGreen, 4);
pushSteps(stepRight, stepsGreen, 2);
pushSteps(stepDown, stepsGreen, 4);
pushSteps(stepRight, stepsGreen, 4);
pushSteps(stepDown, stepsGreen, 2);
pushSteps(stepLeft, stepsGreen, 4);
pushSteps(stepDown, stepsGreen, 4);
pushSteps(stepLeft, stepsGreen, 2);
pushSteps(stepUp, stepsGreen, 4);
pushSteps(stepLeft, stepsGreen, 4);
pushSteps(stepUp, stepsGreen, 1);
pushSteps(stepRight, stepsGreen, 5);
function ResetPawn(victim) {
    onboard[victim] = 0;
    positions[victim] = 0;
    var pawnToMove = document.getElementById(victim);
    switch (victim) {
        case "redpawn1":
            pawnToMove.style.top = 153 + "px";
            pawnToMove.style.left = 440 + "px";
            break;
        case "redpawn2":
            pawnToMove.style.top = 105 + "px";
            pawnToMove.style.left = 393 + "px";
            break;
        case "redpawn3":
            pawnToMove.style.top = 58 + "px";
            pawnToMove.style.left = 440 + "px";
            break;
        case "redpawn4":
            pawnToMove.style.top = 106 + "px";
            pawnToMove.style.left = 488 + "px";
            break;
        case "bluepawn1":
            pawnToMove.style.top = 455 + "px";
            pawnToMove.style.left = 487 + "px";
            break;
        case "bluepawn2":
            pawnToMove.style.top = 455 + "px";
            pawnToMove.style.left = 393 + "px";
            break;
        case "bluepawn3":
            pawnToMove.style.top = 408 + "px";
            pawnToMove.style.left = 440 + "px";
            break;
        case "bluepawn4":
            pawnToMove.style.top = 502 + "px";
            pawnToMove.style.left = 441 + "px";
            break;
        case "greenpawn1":
            pawnToMove.style.top = 152 + "px";
            pawnToMove.style.left = 90 + "px";
            break;
        case "greenpawn2":
            pawnToMove.style.top = 105 + "px";
            pawnToMove.style.left = 138 + "px";
            break;
        case "greenpawn3":
            pawnToMove.style.top = 59 + "px";
            pawnToMove.style.left = 90 + "px";
            break;
        case "greenpawn4":
            pawnToMove.style.top = 106 + "px";
            pawnToMove.style.left = 43 + "px";
            break;
        case "yellowpawn1":
            pawnToMove.style.top = 455 + "px";
            pawnToMove.style.left = 43 + "px";
            break;
        case "yellowpawn2":
            pawnToMove.style.top = 455 + "px";
            pawnToMove.style.left = 138 + "px";
            break;
        case "yellowpawn3":
            pawnToMove.style.top = 408 + "px";
            pawnToMove.style.left = 90 + "px";
            break;
        case "yellowpawn4":
            pawnToMove.style.top = 502 + "px";
            pawnToMove.style.left = 91 + "px";
            break;
    }
}
function randomNum() {
    if (!clicked) {
        num = Math.floor(Math.random() * 6 + 1);
        // num = 6;
        var dice = document.getElementById("dice");
        dice.style.backgroundImage = "url(../js/Images/" + num + ".jpg)";
        clicked = true;
    }
    if (num !== 6 && DontHaveOtherFree()) {
        var bad = document.getElementById("badtext");
        bad.innerText = "Unfortunatlly you stuck";
        window.setTimeout(changePlayer, 1000);
        clicked = false;
    }
}

function randomMove(Color, paw) {
    var text = document.getElementById("player");
    NumOfPaw = paw;
    currcolor = Color;
    currpawn = currcolor + "pawn" + NumOfPaw;
    currPos = positions[currpawn];
    if (num + currPos > 44) {
        Stuck();
    } else {
        if (clicked) {
            document.getElementById("takeoutpiece-log").innerText = "";
            document.getElementById("knockout-log").innerText = "";
            document.getElementById("action-log").innerText = "";
            var position = currPos;
            if (text.innerText == currcolor) {
                if (onboard[currpawn] === 1 || num === 6) {
                    if (onboard[currpawn] === 0) {
                        var doc = document.getElementById(currpawn);
                        switch (
                        Color //PLACEMENT ON BOARD
                        ) {
                            case "red":
                                doc.style.left = 315 + "px";
                                doc.style.top = 32 + "px";
                                break;

                            case "yellow":
                                doc.style.left = 216 + "px";
                                doc.style.top = 528 + "px";
                                break;

                            case "blue":
                                doc.style.left = 513 + "px";
                                doc.style.top = 330 + "px";
                                break;

                            case "green":
                                doc.style.left = 18 + "px";
                                doc.style.top = 231 + "px";
                                break;
                        }
                        onboard[currpawn] = 1;
                        takeoutpieceLog();

                        var victim = HaveHover();
                        if (victim != false) {
                            ResetPawn(victim);
                            knockLog(victim);
                        }
                    } else {
                        switch (Color) {
                            case "red":
                                for (i = currPos; i < position + num; i++) {
                                    stepsRed[i]();
                                }
                                break;

                            case "yellow":
                                for (i = currPos; i < position + num; i++) {
                                    stepsYellow[i]();
                                }
                                break;

                            case "blue":
                                for (i = currPos; i < position + num; i++) {
                                    stepsBlue[i]();
                                }
                                break;

                            case "green":
                                for (i = currPos; i < position + num; i++) {
                                    stepsGreen[i]();
                                }
                                break;
                        }
                        positions[currpawn] = currPos;
                        actionLog(num);

                        var victim = HaveHover();
                        if (victim != false) {
                            ResetPawn(victim);
                            knockLog(victim);
                        }
                        if (currPos == 44) {
                            inGoal[currcolor]++;
                            onboard[currpawn] = 0;
                            positions[currpawn] = 0;
                            document.getElementById(currpawn).style.visibility = "hidden";
                        }
                        CheckForWinner();
                        changePlayer();
                    }
                    num = 0;
                    clicked = false;
                    var dice = document.getElementById("dice");
                    dice.style.backgroundImage = "url(../js/Images/dice1.gif)";
                } else Stuck();
            }
        }
    }
    getPawnData();
}

function getPawnData() {
    if (pawnData.length != 0) {
        pawnData = [];
    }

    for (let i = 0; i < allcolor.length; i++) {
        for (let j = 1; j <= 4; j++) {
            var pawnName = allcolor[i] + "pawn" + j;

            var json = `{
                "${pawnName}": {
                    "top": "${document.getElementById(pawnName).style.top}",
                    "left": "${document.getElementById(pawnName).style.left}",
                    position: "${positions.pawnName}"
                }
            }`;

            pawnData.push(json);
        }
    }
}

function actionLog(num) {
    var printAction = document.getElementById("action-log");
    printAction.style.color = currcolor;
    printAction.innerHTML = currpawn + " moved " + num + " steps.";
}

function knockLog(victim) {
    var printAction = document.getElementById("knockout-log");
    printAction.style.color = currcolor;
    printAction.innerHTML = currpawn + " knocked out " + victim;
}

function takeoutpieceLog() {
    var printAction = document.getElementById("takeoutpiece-log");
    printAction.style.color = currcolor;
    printAction.innerHTML = currcolor + ": moved " + currpawn + " into play!";
}

// DETTA FUNKAR!!!
// if (!gameLoaded) {
//   loadGame("bluepawn3", "330px", "364.5px", 3, 1, 0);
//   gameLoaded = true;
// }

function loadGame(pawnName, top, left, position, onBoard, ingoal) {
    // var loadPawn = document.getElementById(pawnName);
    document.getElementById(pawnName).style.top = top;
    document.getElementById(pawnName).style.left = left;

    positions[pawnName] = position;

    onboard[pawnName] = onBoard;

    var color = document.getElementById(pawnName).style.backgroundColor;

    switch (color) {
        case "red":
            inGoal.red = ingoal;
            break;

        case "blue":
            inGoal.blue = ingoal;
            break;

        case "green":
            inGoal.green = ingoal;
            break;

        case "yellow":
            inGoal.green = ingoal;
            break;

        default:
            break;
    }
}
