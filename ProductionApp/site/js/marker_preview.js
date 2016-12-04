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
}