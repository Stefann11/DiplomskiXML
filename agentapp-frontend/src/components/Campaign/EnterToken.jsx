import React, { Component } from "react";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { compose } from "redux";

class EnterToken extends Component {
  state = {
    showCreateCollectionModal: false,
    token: "",
  };

  componentDidMount() {}

  render() {
    return (
      <div>
        <div className="wrap bg-white pt-3 pb-3" style={{ height: "100vh" }}>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <div className="text-center">
              <label className="label">Token:</label>
              <textarea
                name="token"
                value={this.state.token}
                onChange={this.handleChange}
                cols="40"
                rows="4"
                class="form-control"
                placeholder="Enter token"
              ></textarea>
            </div>
            <div className="text-center">
              <button
                disabled={this.state.token.length === 0}
                className="btn btn-lg btn-primary btn-block"
                onClick={this.register.bind(this)}
              >
                Save
              </button>
            </div>
          </div>
        </div>
      </div>
    );
  }

  register() {
    debugger;
    var parts = this.state.token.split("."); // header, payload, signature
    var userInfo = JSON.parse(atob(parts[1]));
    sessionStorage.setItem("savedToken", this.state.token);
    sessionStorage.setItem("savedUserId", userInfo.user_id);
    sessionStorage.setItem("savedRole", userInfo.role);
    sessionStorage.setItem("savedUsername", userInfo.username);
    window.location = "/campaign";
  }

  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };
}

const mapStateToProps = (state) => ({});

export default compose(withRouter, connect(mapStateToProps, {}))(EnterToken);
