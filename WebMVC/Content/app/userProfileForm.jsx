var UserProfileForm = React.createClass({
	getInitialState: function(){
		return {
			user:{
				id:this.props.userId,
				name:this.props.userName
			},
			isValid:true
		}
	},
	componentWillMount: function(){
		this.inputs = [];
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
			data: { name: this.inputs[0].state.value, __RequestVerificationToken: antiForgeryToken },
			success: function(data) {
				user.name = data.name;
				this.props.changeMode();
			}.bind(this),
			error: function(xhr, status, err) {
				console.error(this.props.url, status, err.toString());
			}.bind(this)
		});
	},
	
	registerInput: function(input){
		this.inputs.push(input);
	},
	
	handleCancelClick: function() {
		this.props.changeMode();
	},
	
	render: function(){
		return(
			<form className="form-horizontal" onSubmit={this.handleSubmit}>
				<div className="form-group">
					<label className="control-label col-lg-2">Имя:</label>
					<div className="col-lg-10">
						<TextInput 
							text={this.state.user.name}
							validate={this.nameValidate}
							required={true}
							onChange={this.setUserName} 
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
						
							<button className='btn btn-default' type='button' onClick={this.handleCancelClick}>Отмена</button>
						</div>
					</div>
				</div>
			</form>
		)
	}
});