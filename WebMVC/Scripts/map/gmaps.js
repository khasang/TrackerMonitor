$(document).ready(function () {
    GetMap();
});

function GetMap()
{
    alert(1);
    google.maps.visualRefresh = true;
    var mapProp = { //если нет сообщений то ловим NullReference
        center: new google.maps.LatLng(55.752622, 37.617567), //new google.maps.LatLng(@Model.Last().Latitude.ToString((new CultureInfo("en-US")).NumberFormat), @Model.Last().Longitude.ToString((new CultureInfo("en-US")).NumberFormat)),
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    alert(2);
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    var myLatlng = new google.maps.LatLng(55.752622, 37.617567);
    alert(3);
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
    });
    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png')
    alert(4);
    //Получение данных
    $(function () {
        //событие нажатия на кнопку
        $('#start-creation').on('click', function () {
            if ($(this).hasClass('off_receive')) {
                alert(5);
                $(this).removeClass('off_receive').addClass('on_receive').html('Отписаться');
                $.connection.hub.start();
                return
            }
            alert(6);
            $(this).removeClass('on_receive').addClass('off_receive').html('Подписаться');
            $.connection.hub.stop();
        });
        alert(7);
        var pushNotify = $.connection.pushNotify;
        pushNotify.client.ShowMessage = function (message) {
            alert(8);
            var marker = new google.maps.Marker({
                'position': new google.maps.LatLng(message.Longitude, message.Latitude),
                'map': map
            })
            alert(9);
        };
    });
}