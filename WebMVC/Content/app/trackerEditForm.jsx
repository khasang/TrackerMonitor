var TrackerEditForm = React.createClass({
	getInitialState: function(){
		return {
			tracker:{
				id:this.props.trackerId,
				name:this.props.trackerName
			},
			isValid:true
		}
	},
	componentWillMount: function(){
		this.inputs = {};
	},
	
	nameValidate: function () {
		//you could do something here that does general validation for any form field
		return true;
	},
	
	handleSubmit: function (e) {
		e.preventDefault();
		
		for(var i = 0; i < this.inputs.length; i++){
			if(!this.inputs[i].state.valid){
				return;
			}
		}
		
		$.ajax({
			url: this.props.url,
			dataType: 'json',
			type: 'POST',
			data: { id: this.inputs["trackerId"] == undefined ? this.state.tracker.id : this.inputs["trackerId"].state.value,
					name: this.inputs["trackerName"].state.value,
					phoneNumber: '',
					__RequestVerificationToken: antiForgeryToken },
			success: function(data) {
				if(this.state.tracker.id != null){
					for(var i =0; i < trackers.length; i++)
					{
						if(trackers[i].id == this.state.tracker.id)
						{
							trackers[i].name = data.name;
							this.props.success();
							return;
						}
					}
				} else {
					trackers.push({ id: data.id, name: data.name, isSelected: false, marker: null });
					this.props.success();
				}
			}.bind(this),
			error: function(xhr, status, err) {
				console.error(this.props.url, status, err.toString());
			}.bind(this)
		});
	},
	
	registerInput: function(input){
		this.inputs[input.props.name] = input;
	},
	
	handleCancelClick: function() {
		this.props.changeMode();
	},
	
	render: function(){
		return(
			<form className="form-horizontal" onSubmit={this.handleSubmit}>
				<div className="form-group">
					{ this.state.tracker.id == null ?
						<div>
							<label className="control-label col-lg-2">Идентификатор:</label>
							<div className="col-lg-10">
								<TextInput
									name="trackerId"
									text={this.state.tracker.id}
									validate={this.nameValidate}
									required={true}
									onChange={this.setTrackerId} 
									errorMessage="Name is invalid"
									emptyMessage="Name is required"
									register={this.registerInput}
								/>
							</div>
						</div> : 
						<div>
							<label className="control-label col-lg-2">Идентификатор:</label>
							<div className="col-lg-10">
								<div className="col-lg-2" style={{paddingTop: '7px'}}>
									<span>{this.state.tracker.id}</span>
								</div>
							</div>
						</div>
					}
				</div>
				
				<div className="form-group">
					<label className="control-label col-lg-2">Название:</label>
					<div className="col-lg-10">
						<TextInput
							name="trackerName"
							text={this.state.tracker.name}
							validate={this.nameValidate}
							required={true}
							onChange={this.setTrackerName} 
							errorMessage="Name is invalid"
							emptyMessage="Name is required"
							register={this.registerInput}
						/>
					</div>
				</div>

				<div  className="form-group">
					<div className="col-lg-offset-2 col-lg-10">
						<div className="btn-toolbar">
							<input type="submit" className="btn btn-primary" value="Сохранить" />
						
							<button className='btn btn-default' type='button' onClick={this.props.cancelClick}>Отмена</button>
						</div>
					</div>
				</div>
			</form>
		)
	}
});