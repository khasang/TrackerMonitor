var UserDetails = React.createClass({
	handleEditClick: function(){
		this.props.changeMode();
	},
	render: function(){
		return(
			<div className="form-horizontal">
				<div className="form-group">
					<label className="control-label col-lg-2">Имя:</label>
					<div className="col-lg-10" style={{paddingTop: '7px'}}>
						<span>{this.props.userName}</span>
					</div>
				</div>

				<div  className="form-group">
					<div className="col-lg-offset-2 col-lg-10">
						<div className="btn-toolbar">
							<button className='btn btn-default' onClick={this.handleEditClick}>
								<span className="glyphicon glyphicon-pencil"></span>
							</button>
						</div>
					</div>
				</div>
			</div>
		)
	}
});

var UserEdit = React.createClass({
	handleCancelClick: function(){
		this.props.changeMode();
	},
	render: function(){
		return(
			<form>
				<div className="form-group">
					<label style={{display:'inline-block'}}>Имя:</label>
					<input className='form-control' style={{display:'inline-block'}}/>
				</div>

				<div className="btn-toolbar">
					<input type="submit" className="btn btn-primary" value="Сохранить" />
					
					<button className='btn btn-default' type='button' onClick={this.handleCancelClick}>Отмена</button>
				</div>
			</form>
		)
	}
});

var UserProfile = React.createClass({
	getInitialState: function(){
		return {
			isEdit: false,
			user: {
				id: user.id,
				name: user.name
			}
		};
	},
	changeMode: function(){
		this.setState({ isEdit: !this.state.isEdit, user: {id: user.id, name: user.name} });
	},
	render: function(){
		return(
			<div style={{paddingLeft: 15 + 'px', paddingRight: 15 + 'px'}}>
				<h2>Профайл</h2>
				<hr />
				{this.state.isEdit == true ?
					<UserProfileForm changeMode={this.changeMode} userId={this.state.user.id} userName={this.state.user.name} url={'personalarea/edit/'}/>
                :
					<UserDetails changeMode={this.changeMode} userId={this.state.user.id} userName={this.state.user.name}/>}
			</div>
		)
	}
});