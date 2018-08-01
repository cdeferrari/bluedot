$(function () {
    var _events = [];

    var $events = $(".event-hidden");
    for (var i = 0; i < $events.length; i++) {
        var _event = {
            title: $events.eq(i).data("title"),
            start: new Date($events.eq(i).data("year"), $events.eq(i).data("month"), $events.eq(i).data("day")),
            color: $events.eq(i).data("color"),
            url: $events.eq(i).data("url"),
            allDay: true,
            //description: 'Some description'
        };
        _events.push(_event);
    }

    var CompCalendar = function () {
        return {
            init: function () {
                
                var date = new Date();
                var d = date.getDate();
                var m = date.getMonth();
                var y = date.getFullYear();

                $('#calendar').fullCalendar({
                    header: {
                        left: '',
                        center: 'title',
                        right: 'prev,next'
                    },
                    firstDay: 1,
                    eventRender: function (eventObj, $el) {
                        $el.popover({
                            title: eventObj.title,
                            //content: eventObj.description,
                            trigger: 'hover',
                            placement: 'top',
                            container: 'body'
                        });
                    },
                    events: _events,
                    locale: 'es',

                });
            }
        };
    }();

    CompCalendar.init();
});