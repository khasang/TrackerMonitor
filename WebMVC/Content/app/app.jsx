var App = React.createClass({
	getInitialState: function () {        
        return {
            tabList: tabList,
            currentTab: 1
        };
    },

    changeTab: function(tab) {
        this.setState({ currentTab: tab.id });
    },
    render: function () {
        return(
            <div className="row">
                <MainControlPanel
					tabList={this.state.tabList}
					changeTab={this.changeTab} />
				<MainInfoPanel currentTab={this.state.currentTab} trackers={this.props.trackers}/>
            </div>
        )
    }
});