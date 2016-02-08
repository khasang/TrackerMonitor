var Tab = React.createClass({
	handleClick: function(e){
        e.preventDefault();
        this.props.handleClick();
    },
	render: function(){
		return(
			<li className={this.props.active} key={this.props.key}><a onClick={this.handleClick} href="#">{this.props.name}</a></li>
		)
	}
});

var Tabs = React.createClass({
	getInitialState: function() {
		return{
			currentTabId: 1
		}
	},
	handleClick: function(tab){
		this.setState({ currentTabId: tab.id });
        this.props.changeTab(tab);
    },
	
	render: function(){
		return(
			<ul className="nav nav-sidebar">
				{this.props.tabList.map(function(tab) {
					return (
						<Tab
							active={this.state.currentTabId == tab.id ? 'active' : 'nonactive'}
							handleClick={this.handleClick.bind(this, tab)}
							name={tab.name}
							key={tab.id}/>
					)
				}.bind(this))}
			</ul>
		)
	}
});

var MainControlPanel = React.createClass({
	handleClick: function(tab){
        this.props.changeTab(tab);
    },
	render: function(){
		return(
			<div className="col-lg-2 col-md-3 col-sm-3 col-xs-4 sidebar">
                <Tabs
					tabList={this.props.tabList}
					changeTab={this.handleClick}/>
            </div>
		)
	}
});