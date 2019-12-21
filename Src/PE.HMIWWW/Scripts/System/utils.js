var KpiGauge = function () {
    this.colors = {
        green: '#20D812',
        orange: '#EFA106',
        red: '#F71C1C',
        blue: '#006487'
    }
    this.createdGauges = [];
    this.kpisData = [];
    this.gauges = document.getElementsByTagName('kpi-gauge');
    console.log(this.gauges);
    for (el of this.gauges) {
        let id = 'kpi-id-' + el.getAttribute('kpi-id');
        this.kpisData['kpi-id-' + id] = this.createTargets(el);
        this.createdGauges[targetHtml.id] = this.createGauge(this.kpisData['kpi-id-' + id], id);
        this.createdGauges[targetHtml.id].value = this.kpisData['kpi-id-' + id].currValue;
        this.createdGauges[targetHtml.id].update();
    }
}
KpiGauge.prototype.createTargets = function (gaugeTarget) {
    targetHtml = document.createElement('canvas');
    targetHtml.id = ('kpi-id-' + gaugeTarget.getAttribute('kpi-id'));
    gaugeTarget.parentNode.insertBefore(targetHtml, gaugeTarget.nextSibling);
    //gaugeTarget.parentNode.removeChild(gaugeTarget);
    return {
        name: gaugeTarget.getAttribute('kpi-name'),
        id: gaugeTarget.getAttribute('kpi-id'),
        start: (gaugeTarget.getAttribute('start')) || 0,
        min: (gaugeTarget.getAttribute('min')),
        mid: (gaugeTarget.getAttribute('mid')),
        max: (gaugeTarget.getAttribute('max')),
        currValue: (gaugeTarget.getAttribute('curr-value')),
        unit: gaugeTarget.getAttribute('unit'),
    }

};
KpiGauge.prototype.createGauge = function (kpiData, id) {
    return new RadialGauge({
        renderTo: id,
        width: 300,
        height: 300,
        units: kpiData.unit,
        //title: kpiData.currValue,

        startAngle: 90,
        ticksAngle: 180,
        colorPlate: "#fff",
        colorPlateEnd: "#fff",
        colorUnits: "#000",
        colorNumbers: "#534638",
        colorMajorTicks: "rgba(0,0,0,0)",
        minValue: 0,
        maxValue: kpiData.max,
        strokeTicks: false,
        highlights: [
            {
                "from": kpiData.start,
                "to": kpiData.min,
                "color": this.colors.green
            },
            {
                "from": kpiData.min,
                "to": kpiData.mid,
                "color": this.colors.orange
            },
            {
                "from": kpiData.mid,
                "to": kpiData.max,
                "color": this.colors.red
            }
        ],
        exactTicks: true,
        majorTicks: [kpiData.min, kpiData.mid, kpiData.max],
        minorTicks: 0,
        highlightsWidth: 5,
        numbersMargin: 2,
        barWidth: 4,
        colorBar: "transparent",
        colorBarProgress: this.currColor(kpiData),
        animation: true,
        animationRule: "linear",
        animatedValue: true,
        borders: false,
        valueBox: true,
        colorValueBoxBackground: 'transparent',
        valueBoxStroke: 0,
        colorValueBox: 0,
        colorValueBox: 0,
        needleType: "line",
        needleWidth: 0,
        needleCircleInner: false,
        needleCircleOuter: false,
        needleShadow: false,
        borderShadowWidth: 0
    }).draw();
}
KpiGauge.prototype.currColor = function (kpiData) {
    if (kpiData.currValue > kpiData.min && kpiData.currValue < kpiData.mid) {
        return this.colors.orange;
    } else if (kpiData.currValue < kpiData.min) {
        return this.colors.green;
    } else if (kpiData.currValue > kpiData.mid) {
        return this.colors.red;
    }
}
var constract = new KpiGauge();