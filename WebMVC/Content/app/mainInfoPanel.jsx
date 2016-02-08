var MainInfoPanel = React.createClass({
	componentDidMount: function() {
	
	},
    render: function () {
        return(
            <div className="col-lg-10 col-lg-offset-2 col-md-9 col-md-offset-3 col-sm-9 col-sm-offset-3 col-xs-8 col-xs-offset-4" id='infopanel' style={{padding: 0 + 'px'}}>
				{this.props.currentTab === 1 ?
					<UserProfile />
                :null}
				{this.props.currentTab === 2 ?
					<GoogleMap trackers={this.props.trackers}/>
                :null}
				{this.props.currentTab === 3 ?
					<TrackerSection trackers={this.props.trackers}/>
                :null}
			</div>
        )
    }
});