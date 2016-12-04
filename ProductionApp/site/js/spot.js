//////////////////////////////////////////////////////////////////////////////////////////////
// описание пятна лазерной указки для поиска его на картинке
//////////////////////////////////////////////////////////////////////////////////////////////
function LazerSpot() {
    this.patRed = 255;
    this.patGreen = 200;
    this.patBlue = 200;
    this.squareDev = 5000;
    this.minPointsCount = 10;
    this.maxPointsToConsiderDot = 100;
}

//////////////////////////////////////////////////////////////////////////////////////////////
// скан-линия - составляющая скан-кластера, образующего пятно лазерной указки
//////////////////////////////////////////////////////////////////////////////////////////////
function ScanLine() {
    this.y = 0;
    this.scans = new Array(0); // Point[]
}

ScanLine.prototype.Consume = function (line) { // scanLine
    
    var newScans = new Array(this.scans.length + line.scans.length);
    for (var i = 0; i < this.scans.length; i++)
        newScans[i] = this.scans[i];
    for (var i = 0; i < line.scans.length; i++)
        newScans[this.scans.length + i] = line.scans[i];
    
    newScans.sort(function(a, b) { return a.x - b.x; });
    this.scans = newScans;
}

//////////////////////////////////////////////////////////////////////////////////////////////
// скан-кластер, образующий пятно лазерной указки
//////////////////////////////////////////////////////////////////////////////////////////////
function ScanCluster() {
    this.lines = new Array(0); // array of ScanLine
}

ScanCluster.prototype.AreStick = function (cluster) { // in - ScanCluster, returns - bool    
    // cluster is beneath this
    var lastLine = cluster.lines[cluster.lines.length - 1];
    var ownScan = this.lines[this.lines.length - 1].y == lastLine.y - 1
                      ? this.lines[this.lines.length - 1].scans
                      : this.lines.length > 1 && this.lines[this.lines.length - 2].y == lastLine.y - 1
                            ? this.lines[this.lines.length - 2].scans : 0;
    if (ownScan)
    {
        for (var i = 0; i < ownScan.length; i++) {
            var span = ownScan[i];        
            var spanX = span.x;
            var spanY = span.y;
            
            if (lastLine.scans.some(function (s) {
                    return (s.x >= spanX && s.x <= spanY) || (s.y >= spanX && s.y <= spanY);
                })) return true;
                
            if (lastLine.scans.some(function (s) {
                    return (spanX >= s.x && spanY <= s.x) || (spanX >= s.y && spanY <= s.y);
                })) return true;                            
        }
    }
    return false;
}

ScanCluster.prototype.ConsumeCluster = function (cluster) {// in - ScanCluster
    var thisStart = this.lines[0].y;
    var thisEnd = this.lines[this.lines.length - 1].y;
    var clusterStart = cluster.lines[0].y;
    var clusterEnd = cluster.lines[cluster.lines.length - 1].y;

    var scans = new Array(0); // List of ScanLine
    var min = Math.min(thisStart, clusterStart);
    var max = Math.max(thisEnd, clusterEnd);
    for (var i = min; i <= max; i++)
    {
        if (i >= thisStart && i <= thisEnd && (i < clusterStart || i > clusterEnd))
        {
            scans.push(this.lines[i - thisStart]);
            continue;
        }
        if (i >= clusterStart && i <= clusterEnd && (i < thisStart || i > thisEnd))
        {
            scans.push(cluster.lines[i - clusterStart]);
            continue;
        }
        // склеить
        this.lines[i - thisStart].Consume(cluster.lines[i - clusterStart]);
        scans.push(this.lines[i - thisStart]);
    }
    this.lines = scans;
}

ScanCluster.prototype.ToPointCluster = function () {// out - PointCluster
    var cluster = new PointCluster();
    for (var i = 0; i < this.lines.length; i++) {
        var line = this.lines[i];
        for (var j = 0; j < line.scans.length; j++) {
            var span = line.scans[j];
            for (var x = span.x; x <= span.y; x++)
                cluster.points.push(new Point(x, line.y));            
        }
    }
    return cluster;
}

//////////////////////////////////////////////////////////////////////////////////////////////
// кластер из точек (x, y)
//////////////////////////////////////////////////////////////////////////////////////////////
function PointCluster() {
    this.points = new Array(0); // массив Point
}

PointCluster.prototype.GetMassCenter = function () {
    if (this.points.length == 0) return 0;
    var sumX = 0, sumY = 0;
    for (var i = 0; i < this.points.length; i++) {
        sumX += this.points[i].x;
        sumY += this.points[i].y;
    }
    return new Point(sumX / this.points.length, sumY / this.points.length);
}

function MergeClusters (
    curLines, // массив ScanLine
    clusters) // массив ScanCluster
{
    var curClusters = new Array(0); // массив скан-кластеров
    for (var k = 0; k < curLines.length; k++) {
        var line = curLines[k];
        var newCluster = new ScanCluster();
        newCluster.lines = [line];
        curClusters.push(newCluster);
        
        // объединить линии в кластеры                
        for (var j = 0; j < curClusters.length; j++) {
            var curCluster = curClusters[j];
            for (var i = 0; i < clusters.length; i++) {
                var cluster = clusters[i];
                if (cluster.AreStick(curCluster)) {
                    curCluster.ConsumeCluster(cluster);
                    clusters.splice(i);
                    i--;
                    continue;
                }
            }
        }
    }
    
    for (var j = 0; j < curClusters.length; j++)
        clusters.push(curClusters[j]);
}

// out - a list of PointCluster
// in: LazerSpot spotParams, rgbValues, w, h
function FindClusters(spotParams, imgData) {
    var clusters = new Array(0); // list of ScanCluster
    var curLines = null; // list of ScanLine
    
    var rgbValues = imgData.data;
    var stride = 4;
    var w = imgData.width;
    var h = imgData.height;

    for (var y = 0; y < h; y++) {
        var curLine = null; // ScanLine
        curLines = new Array(0);

        for (var x = 0; x < w; x++) {
            var ind = (x + y * w) * stride;
            
            var red = rgbValues[ind];
            var green = rgbValues[ind + 1];
            var blue = rgbValues[ind + 2];
            var delta = (red - spotParams.patRed) * (red - spotParams.patRed) + (green - spotParams.patGreen) * (green - spotParams.patGreen) +
                        (blue - spotParams.patBlue) * (blue - spotParams.patBlue);

            var isIn = delta < spotParams.squareDev;
            if (isIn)
            {
                if (!curLine) 
                {
                    curLine = new ScanLine();
                    curLine.y = y;
                    curLine.scans = [new Point(x, x)];
                }
                else curLine.scans[0] = new Point(curLine.scans[0].x, x);
            }
            else
                if (curLine)  {
                    curLines.push(curLine);
                    curLine = null;
                }
        } // end of for (var x = 0; ...
        if (curLine)
            curLines.push(curLine);
        if (curLines.length == 0) continue;
        MergeClusters(curLines, clusters);
    }

    if (curLines && curLines.length > 0) 
        MergeClusters(curLines, clusters);

    var pointClusters = new Array(0);
    for (var i = 0; i < clusters.length; i++) {
        var pointClusterB = clusters[i].ToPointCluster();
        if (pointClusterB.points.length == 0) continue;
        pointClusters.push(pointClusterB);
    }
        
    return pointClusters;
}

function FindLaserSpotCenter(spotParams, imgData) {
    var clusters = FindClusters(spotParams, imgData);
    if (!clusters || clusters.length == 0) return;

    // из всех кластеров выбрать максимальный - 
    // при том его размер (в точках) должен лежать в заданном диапазоне
    var indexOfLargest = -1, pointsCount = 0;
    for (var i = 0; i < clusters.length; i++) {
        if (clusters[i].points.length < pointsCount ||
            clusters[i].points.length < spotParams.minPointsCount ||
            clusters[i].points.length > spotParams.maxPointsToConsiderDot) continue;
        indexOfLargest = i;
        pointsCount = clusters[i].points.length;
    }

    if (indexOfLargest < 0) return null;
    return clusters[indexOfLargest].GetMassCenter();
}

// нарисовать крестик на переданном контексте в указанной точке
function drawColorCross(context, x, y, sz) {
    if (sz == undefined) sz = 3;

    context.beginPath();
    context.moveTo(x - sz, y - sz);
    context.lineTo(x - 1, y - 1);
    context.strokeStyle = 'red';
    context.stroke();

    context.beginPath();
    context.moveTo(x - sz, y + sz);
    context.lineTo(x - 1, y + 1);
    context.strokeStyle = 'green';
    context.stroke();

    context.beginPath();
    context.moveTo(x + sz, y - sz);
    context.lineTo(x + 1, y - 1);
    context.strokeStyle = 'blue';
    context.stroke();

    context.beginPath();
    context.moveTo(x + sz, y + sz);
    context.lineTo(x + 1, y + 1);
    context.strokeStyle = 'black';
    context.stroke();
}