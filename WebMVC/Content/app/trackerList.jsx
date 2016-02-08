var ConfirmDelete = React.createClass({
	handleConfirm: function(e) {
		e.preventDefault();
		$.ajax({
			url: this.props.url,
			dataType: 'json',
			type: 'POST',
			data: { __RequestVerificationToken: antiForgeryToken },
			success: function(data) {
				for(var i =0; i < trackers.length; i++)
					{
						if(trackers[i].id == data)
						{
							trackers.splice(i, 1);
							this.props.success();
							return;
						}
					}
			}.bind(this),
			error: function(xhr, status, err) {
				console.error(this.props.url, status, err.toString());
			}.bind(this)
		});
		this.props.success();
	},
	handleCancelClick: function() {
		this.props.cancel();
	},
	render: function(){
		return(
			<div className="form-group">
				<h2>{this.props.title}</h2>
				<hr />

				<form onSubmit={this.handleConfirm}>
					<h4>{this.props.message}</h4>
					<div className="btn-toolbar">
						<button type="submit" className="btn btn-danger" onClick={this.handleConfirmClick}>Да</button>
						<button type="button" className="btn btn-default" onClick={this.handleCancelClick}>Нет</button>
					</div>
				</form>
			</div>
		)
	}
});

var TrackerListRow = React.createClass({
	getInitialState: function(){
		return{
			isEditing: false,
			isConfirmDeleting: false
		}
	},
	btnEditClickHandle: function(){
		this.setState({
			isEditing: !this.state.isEditing,
			isConfirmDeleting: this.state.isConfirmDeleting
		});
	},
	btnDeleteClickHandle: function(){
		this.setState({
			isEditing: this.state.isEditing,
			isConfirmDeleting: !this.state.isConfirmDeleting
		});
	},
	handleCancelClick: function(){
		this.setState({
			isEditing: !this.state.isEditing,
			isConfirmDeleting: this.state.isConfirmDeleting
		});
	},
	handleSuccess: function(){
		this.setState({
			isEditing: !this.state.isEditing,
			isConfirmDeleting: this.state.isConfirmDeleting
		});
		this.props.changedTracker();
	},
	handleDeleteSuccess: function(){
		this.props.changedTracker();
	},
	handleDeleteCancel: function(){
		this.setState({
			isEditing: this.state.isEditing,
			isConfirmDeleting: !this.state.isConfirmDeleting
		});
	},
	render: function(){
		return(
			this.state.isEditing ?
				<tr key={this.props.key} >
					<td colSpan={3}><TrackerEditForm trackerId={this.props.tracker.id} trackerName={this.props.tracker.name} cancelClick={this.handleCancelClick} success={this.handleSuccess} url={'trackers/edit'}/></td>
				</tr> :
				this.state.isConfirmDeleting ? 
					<tr key={this.props.key} >
						<td colSpan={3}>
							<ConfirmDelete 
								url={'trackers/delete/' + this.props.tracker.id}
								title={"Подтверждение удаления трекера"}
								message={"Вы действительно хотите удалить трекер " + this.props.tracker.name}
								success={this.handleDeleteSuccess}
								cancel={this.handleDeleteCancel}/>
						</td>
					</tr> :
					<tr key={this.props.key}>
						<td>{this.props.tracker.id}</td>
						<td>{this.props.tracker.name}</td>
						<td>
							<div className="btn-toolbar">
								<button className='btn btn-default' onClick={this.btnEditClickHandle}>
									<span className="glyphicon glyphicon-pencil"></span>
								</button>
								<button className='btn btn-default' onClick={this.btnDeleteClickHandle}>
									<span className="glyphicon glyphicon-remove"></span>
								</button>
							</div>
						</td>
					</tr>
		)
	}
});
var TrackerList = React.createClass({
	getInitialState: function(){
		return{
			trackerList: trackers
		}
	},
	handleChangedTracker: function(){
		this.setState({ trackerList: trackers });
	},
	render: function(){
		return(
			<table className='table table-bordered'>
				<thead>
					<tr>
						<th>ID</th>
						<th>Название</th>
						<th>Действия</th>
					</tr>
				</thead>
				<tbody>
					{this.state.trackerList.map(function(tracker, index) {
						return (
							<TrackerListRow key={tracker.id} tracker={tracker} changedTracker={this.handleChangedTracker}/>
						);
					}.bind(this))}
				</tbody>
			</table>
		)
	}
});