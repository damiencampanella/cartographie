window.jsInterops = {
    init_tooltips: function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        });
        $('[data-toggle="tooltip"]').on('click', function () {
            $(this).tooltip('hide')
        });
    },
    init_popovers: function () {
        $('[data-toggle="popover"]').popover();
    },
    init_datepickers: function (language) {
        $('#datepicker').datepicker({
            locale: language,
            format: 'dd/mm/yyyy',
            weekStartDay: 1,
            iconsLibrary : 'fontawesome',
            uiLibrary: 'bootstrap4'
        });
        //locale: 'fr-fr', format: 'dd/mm/yyyy', weekStartDay: 1, iconsLibrary: 'fontawesome', size: 'small', uiLibrary: 'bootstrap4'
    },
    set_active_tab: function (tabId, tabRef) {
        //var instr = '#' + tabId + 'a[href = \"#' + tabRef + '\"]';
        $('#' + tabId + ' a[href="#' + tabRef + '"]').tab('show');
        //$('#tabs_carto a[href="#lines"]').tab('show');
    },
    set_server_time: function (hour, minute) {
        $('#ServerTime').text(hour + ':' + minute);
    }
};



//function addLayer(mapId, layer, layerId) {
//    layer.id = layerId;
//    layers[mapId].push(layer);
//    layer.addTo(maps[mapId]);
//}


