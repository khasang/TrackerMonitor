var GoogleMap = React.createClass({
	getDefaultProps: function () {
        return {
            initialZoom: 8,
            mapCenterLat: initialLocLat,
            mapCenterLng: initialLocLng,
        };
    },
	getInitialState: function (){
		return {
			map: null,
			visTrackers: []
		};
	},
	componentDidMount: function (rootNode) {
        var mapOptions = {
            center: this.mapCenterLatLng(),
            zoom: this.props.initialZoom,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        },
        map = new google.maps.Map(document.getElementById('react-valuation-map'), mapOptions);
        this.setState({map: map, visTrackers: this.state.visTrackers });
		
		var pushNotify = $.connection.pushNotify;
		
		pushNotify.client.ShowMessage = this.showMessage;
		
		$.connection.hub.start();
    },
	componentWillUnmount: function () {
		var pushNotify = $.connection.pushNotify;
		
		pushNotify.client.ShowMessage = function (message) {};
	},
	showMessage: function (message){
		var visTrackers = this.state.visTrackers;
		var tracker = null;
		
		for(var i = 0; i < visTrackers.length; i++){
			if(visTrackers[i].id == message.GPSTrackerId){
				tracker = visTrackers[i];
				break;
			}
		}
		
		if(tracker != null) {
			this.updateMarker(message, tracker);
		}
	},
	updateMarker: function(message, tracker){
		if(tracker.marker != null)
			tracker.marker.setPosition(new google.maps.LatLng(message.Latitude, message.Longitude));
		else
			tracker.marker = new google.maps.Marker({
														position: new google.maps.LatLng(message.Latitude, message.Longitude),
														title: tracker.name,
														map: this.state.map});
		
		//var bounds = new google.maps.LatLngBounds();
		
		//var visTrackers = this.state.visTrackers;
		
		//for(var i=0; i<visTrackers.length; i++) {
		//	if(visTrackers[i].marker != null){
		//		bounds.extend(visTrackers[i].marker.getPosition());
		//	}
		//}
		
		//var map = this.state.map;
		
		//map.fitBounds(bounds);
	},
	mapCenterLatLng: function () {
        var props = this.props;
        return new google.maps.LatLng(props.mapCenterLat, props.mapCenterLng);
    },
	changeSelection: function(tracker){
		var visTrackers = this.state.visTrackers;
		
		if(visTrackers.length == 0)
			visTrackers.push(tracker);
		else
			for(var i = 0; i < visTrackers.length; i++){
				if(visTrackers[i].id == tracker.id){
					visTrackers.splice(i, 1);
					break;
				}
				
				if(i == visTrackers.length - 1){
					visTrackers.push(tracker);
					break;
				}
			}
			
		this.setState({map: this.state.map, visTrackers: visTrackers });
	},
	searchTracker: function (tracker) {
		var marker = null;
		var visTrackers = this.state.visTrackers;
		
		for(var i = 0; i < visTrackers.length; i++){
				if(visTrackers[i].id == tracker.id){
					marker = tracker.marker;
					break;
				}
		}
		
		if(marker == null) {
			$.get("trackers/getlastlocation", { id: tracker.id }, function (message) {
				var mapOptions = {
					center: new google.maps.LatLng(message.latitude, message.longitude),
					zoom: 16,
					mapTypeId: google.maps.MapTypeId.ROADMAP
				};
				this.state.map.setOptions(mapOptions);
				tracker.marker = new google.maps.Marker({
														position: new google.maps.LatLng(message.latitude, message.longitude),
														title: tracker.name,
														map: this.state.map});
			}.bind(this));
		} else {
			var mapOptions = {
					center: marker.getPosition(),
					zoom: 16,
					mapTypeId: google.maps.MapTypeId.ROADMAP
				};
				
			this.state.map.setOptions(mapOptions);
		}		
	},
	render: function () {
        return (
			<div style={{position: 'relative'}}>
				<TrackerListPanel trackers={this.props.trackers} changeSelection={this.changeSelection} searchTracker={this.searchTracker}/>
				<div id='react-valuation-map' style={{height:921 + 'px'}}>
					
				</div>
			</div>
        );
    }
});