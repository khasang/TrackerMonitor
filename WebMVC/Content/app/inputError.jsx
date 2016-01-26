var InputError = React.createClass({
  getInitialState: function() {
    return {
      message: 'Input is invalid'
    };
  },
  render: function(){ 
    var errorClass = classNames(this.props.className, {
      'error_container':   true,
      'visible':           this.props.visible,
      'invisible':         !this.props.visible
    });

    return (
      <div>
        <span>{this.props.errorMessage}</span>
      </div>
    )
  }

});