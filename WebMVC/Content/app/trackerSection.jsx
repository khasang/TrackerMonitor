var TrackerSection = React.createClass({
	getInitialState: function() {
		return {
			isNewTracker: false
		}
	},
	handlePlusClick: function() {
		this.setState({
			isNewTracker: !this.state.isNewTracker
		});
	},
	handleSuccess: function(){
		this.setState({
			isNewTracker: !this.state.isNewTracker
		});
	},
	render: function() {
		return(
			<div style={{paddingLeft: 15 + 'px', paddingRight: 15 + 'px'}}>
				<h2>Трекеры</h2>
				<hr />
				
				{this.state.isNewTracker ?
					<div>
						<div className="form-group">
							<button className='btn btn-default' onClick={this.handlePlusClick}>
								<span className="glyphicon glyphicon-minus"></span>
							</button>
						</div>
						
						<TrackerEditForm cancelClick={this.handlePlusClick} trackerId={null} trackerName={''} url={'trackers/register'} success={this.handleSuccess}/>
					</div>
					:
					
					<div className="form-group">
						<button className='btn btn-default' onClick={this.handlePlusClick}>
							<span className="glyphicon glyphicon-plus"></span>
						</button>
					</div>
				}
				
				<TrackerList trackers={trackers}/>
			</div>
		)
	}
});