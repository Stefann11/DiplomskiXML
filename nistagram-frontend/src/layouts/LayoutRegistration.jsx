import React, { Component } from "react";

class LayoutRegistration extends Component {
  render() {
    return (
      <div className="container">
        <div className="wrapper">{this.props.children}</div>
      </div>
    );
  }
}

export default LayoutRegistration;
