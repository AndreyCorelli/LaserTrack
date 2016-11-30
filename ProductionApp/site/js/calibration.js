var imageData;
var calibrationContext;
var calibrationCanvas;
var position;
var clickCounter = 1;


function calibrationCanvasOnClickLegacy(e) {
    clickCounter++;
    if (clickCounter == 2) clickCounter = 0;
    
    calibrationContext.putImageData(imageData, 0, 0);

    var oldPosition = { x: 0, y: 0 };
    var radius = 30;
    if (clickCounter == 1) {
        oldPosition = position;
        position = getCursorPosition(e);
        radius = 30 + Math.sqrt((oldPosition.x - position.x) * (oldPosition.x - position.x) + (oldPosition.y - position.y) * (oldPosition.y - position.y));
    } else {
        position = getCursorPosition(e);
    }

    var x = clickCounter == 1 ? oldPosition.x : position.x;
    var y = clickCounter == 1 ? oldPosition.y : position.y;

    calibrationContext.beginPath();
    calibrationContext.arc(x, y, radius, 0, 2 * Math.PI, false);

    calibrationContext.fillStyle = "rgba(0, 200, 0, 0.1)";
    calibrationContext.fill();
    calibrationContext.stroke();

    calibrationContext.lineWidth = 2;
    calibrationContext.strokeStyle = '#003300';
    calibrationContext.stroke();
}

function calibrationCanvasOnClick(e) {
    var pos = getCursorPosition(e);
    var imgBits = imageData;
    var stride = 4;
    var w = imgBits.width;
    var ind = (pos.x + pos.y * w) * stride;

    var red = imgBits[ind];
    var green = imgBits[ind + 1];
    var blue = imgBits[ind + 2];
    $('#spotRed').val(red);
    $('#spotGreen').val(green);
    $('#spotBlue').val(blue);
}

function getCursorPosition(e) {
    var rect = calibrationCanvas.getBoundingClientRect();
    return {
        x: e.clientX - rect.left,
        y: e.clientY - rect.top
    };
}