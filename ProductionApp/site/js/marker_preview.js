function MarkerPreview(destCanvasId, mainCanvas) {
    this.ptrs = {
        radius: 10,
        r: 255,
        g: 200,
        b: 200,
        percent: 70
    };
    this.markerSliderId = '#previewMarkSz';
    this.ptrs.radius = $(this.markerSliderId).val();
    $(this.markerSliderId).on('input change', function() { 
        window.model.markerPreview.onSlider();
    });

    this.prevW = 100;
    this.prevH = 100;
    this.mainCanvas = mainCanvas;
    this.previewCanvas = document.getElementById(destCanvasId);
    this.loadSettings();
    $(this.markerSliderId).val(this.ptrs.radius);
    this.wasInit = false;
}

MarkerPreview.prototype.loadSettings = function() {
    var json = localStorage["settings_markerPreview"];
    if (!json) return;
    try {
        this.ptrs = JSON.parse(json);
    }
    catch (ex) {}
}

MarkerPreview.prototype.saveSettings = function() {
    localStorage["settings_markerPreview"] = JSON.stringify(this.ptrs);
}

MarkerPreview.prototype.onSlider = function() {
    var rad = $(this.markerSliderId).val();
    if (rad == this.ptrs.radius) return;
    this.ptrs.radius = rad;
    this.saveSettings();

    if (this.wasInit) this.drawSpotPreview();
}

MarkerPreview.prototype.drawSpotPreview = function() {
    // cursor stays on this.canvasPos
    var destCtx = this.previewCanvas.getContext('2d');
    var sx = this.canvasPos.x - this.prevW / 2;
    if (sx < 0) sx = 0;
    if (sx + this.prevW >= this.w) sx = this.w - this.prevW - 1;

    var sy = this.canvasPos.y - this.prevH / 2;
    if (sy < 0) sy = 0;
    if (sy + this.prevH >= this.h) sy = this.h - this.prevH - 1;

    this.preMarkerX = this.canvasPos.x - sx;
    this.preMarkerY = this.canvasPos.y - sy;

    destCtx.drawImage(this.mainCanvas, sx, sy, this.prevW, this.prevH, 0, 0, this.prevW, this.prevH);
    this.drawPreviewMarker(destCtx);
    this.determineLaserSpot(destCtx);
    this.wasInit = true;
}

MarkerPreview.prototype.drawPreviewMarker = function(destCtx) {
    var rad = this.ptrs.radius;
    var cx = this.preMarkerX;
    var cy = this.preMarkerY;

    destCtx.beginPath();
    destCtx.strokeStyle = 'red';
    destCtx.arc(cx, cy, rad, 0, 2 * Math.PI / 3);
    destCtx.stroke();

    destCtx.beginPath();
    destCtx.strokeStyle = 'black';
    destCtx.arc(cx, cy, rad, 2 * Math.PI / 3, 4 * Math.PI / 3);
    destCtx.stroke();

    destCtx.beginPath();
    destCtx.strokeStyle = 'green';
    destCtx.arc(cx, cy, rad, 4 * Math.PI / 3, 2 * Math.PI);
    destCtx.stroke();

    // 'rays'
    drawLineInPolar(destCtx, 'blue', cx, cy, 0 * Math.PI / 3, rad, rad + 10);
    drawLineInPolar(destCtx, 'green', cx, cy, 2 * Math.PI / 3, rad, rad + 10);
    drawLineInPolar(destCtx, 'red', cx, cy, 4 * Math.PI / 3, rad, rad + 10);
}

MarkerPreview.prototype.determineLaserSpot = function(destCtx) {
    var imageData = destCtx.getImageData(0, 0, this.prevW, this.prevH);
    var rgbValues = imageData.data;
    var stride = 4;
    var w = this.prevW;
    var h = this.prevH;
    var rad = this.ptrs.radius;
    var rad2 = rad * rad;
    var cx = this.preMarkerX;
    var cy = this.preMarkerY;

    var truecl = this.getColorInPointAliased(rgbValues);

    var deltas = [];
    for (var y = 0; y < h; y++) {
        for (var x = 0; x < w; x++) {
            // in marker?
            var range = (x - cx)*(x - cx) + (y - cy)*(y - cy);
            if (range > rad2) continue;

            // color
            var ind = (x + y * w) * stride;            
            var red = rgbValues[ind];
            var green = rgbValues[ind + 1];
            var blue = rgbValues[ind + 2];

            // delta
            var delta = (red - truecl.r) * (red - truecl.r) + 
                (green - truecl.g) * (green - truecl.g) + (blue - truecl.b) * (blue - truecl.b);
            deltas.push(delta);
        }
    }
    deltas = deltas.sort();
    // cut the tail
    var index = Math.round(this.ptrs.percent * deltas.length / 100);
    var maxDev = deltas[index];
    console.log('RGB: ' + truecl.r + '-' + truecl.g + '-' + truecl.b + ', max dev: ' + maxDev);
}

MarkerPreview.prototype.getColorInPointAliased = function (rgbValues) {
    // returns object {red, green, blue}
    var cx = this.preMarkerX;
    var cy = this.preMarkerY;
    var stride = 4;
    var w = this.prevW;
    var h = this.prevH;

    var rgbAr = [ [], [], [] ];

    for (var i = 0; i < 3; i++) {
        rgbAr[i].push(rgbValues[(cx + cy * w) * stride + i]); // exact point
        if (cx > 0)
            rgbAr[i].push(rgbValues[(cx - 1 + cy * w) * stride + i]); // left
        if (cx < (w - 1))
            rgbAr[i].push(rgbValues[(cx + 1 + cy * w) * stride + i]); // right
        if (cy > 0)
            rgbAr[i].push(rgbValues[(cx + (cy - 1) * w) * stride + i]); // top
        if (cy < (h - 1))
            rgbAr[i].push(rgbValues[(cx + (cy + 1) * w) * stride + i]); // bottom
    }
    return {
        r: getAverage(rgbAr[0]),
        g: getAverage(rgbAr[1]),
        b: getAverage(rgbAr[2])
    };
}

function getAverage(arr) {
    if (!arr.length) return 0;
    var sum = 0;
    for (var i = 0; i < arr.length; i++) sum += arr[i];
    return sum / arr.length;
}

function drawLineInPolar(context, color, cx, cy, alpha, rStart, rEnd) {
    context.beginPath();
    context.moveTo(cx + Math.cos(alpha) * rStart, cy + Math.sin(alpha) * rStart);
    context.lineTo(cx + Math.cos(alpha) * rEnd, cy + Math.sin(alpha) * rEnd);
    context.strokeStyle = color;
    context.stroke();
}