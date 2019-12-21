$('.less').hide(100);

function showHideCategories() {
    var button = $('.show-hide-categories');
    var grid = $("#SearchGrid").data("kendoGrid");
    var button_array = $('.arrow-categories');
    if (button.hasClass('off')) {
        $('.more').hide(100);
        $('.less').show(100);
        $('.element-details').hide(100);
        columns.forEach(function (element) {
            grid.showColumn(element);
        });
        $('.grid-filter').toggleClass('col-11 col-3', 750, function () {

        });
    }
    if (button.hasClass('on')) {
        $('.more').show(100);
        $('.less').hide(100);
        columns.forEach(function (element) {
            grid.hideColumn(element);
        });
        $('.grid-filter').toggleClass('col-11 col-3', 650, function () {

            $('.element-details').show(650);
        });
    }
    button_array.toggleClass('k-i-arrow-right k-i-arrow-left');
    button.toggleClass('off on');
}

function hideCategories() {
    if (!$(".grid-filter").is(":animated")) {
        var button = $('.show-hide-categories');
        if (button.hasClass('on')) {
            var grid = $("#SearchGrid").data("kendoGrid");
            var button_array = $('.arrow-categories');
            button_array.toggleClass('right left');
            button.toggleClass('on off');
            columns.forEach(function (element) {
                grid.hideColumn(element);
            });
            $('.grid-filter').toggleClass('col-11 col-3', 650, function () {
                $('.element-details').show(650);
            });
        }
    }
}

function dataSourceChange(e) {
    var arrow = $('.arrow-categories');
    var currentFilter = this.filter();
    if (currentFilter == undefined || currentFilter.length < 1) {
        if (arrow.hasClass('arrow-yellow')) {
            arrow.toggleClass('arrow-yellow arrow-white');
        }
    }
    else {
        if (arrow.hasClass('arrow-white')) {
            arrow.toggleClass('arrow-yellow arrow-white');
        }
    }
}