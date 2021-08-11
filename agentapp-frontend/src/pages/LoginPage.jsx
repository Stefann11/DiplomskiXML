import React, { Component } from "react";
import "../css/app.css";
import { userLoggedIn } from "../actions/actionsUser";
import { connect } from "react-redux";
import background from "../images/nistagrambg.jpg";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import Header from "../components/Common/Header";

class LoginPage extends Component {
  state = {
    username: "",
    password: "",
  };
  render() {
    return (
      <div
        style={{
          width: 1580,
          height: 1080,
          backgroundImage: `url(${background})`,
        }}
      >
        <div className="container">
          <Header />
        </div>
        <div id="wrapper">
          <div class="main-content">
            <div class="l-part">
              <input
                type="text"
                placeholder="Username"
                name="username"
                class="input-1"
                onChange={this.handleChange}
              />
              <div class="overlap-text">
                <input
                  type="password"
                  placeholder="Password"
                  name="password"
                  class="input-2"
                  onChange={this.handleChange}
                />
                <a href="#">Forgot?</a>
              </div>
              <input
                type="button"
                value="Log in"
                class="btn btn-primary"
                onClick={this.login.bind(this)}
              />
            </div>
          </div>
          <div class="sub-content">
            <div class="s-part">
              Don't have an account?
              <a onClick={this.register.bind(this)} href="javascript:;">
                Sign up
              </a>
            </div>
          </div>
        </div>
      </div>
    );
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

  register() {
    this.props.history.replace({
      pathname: "/registration",
    });
  }

  async login() {
    debugger;
    var successful = false;
    successful = await this.props.userLoggedIn({
      Username: this.state.username,
      Password: this.state.password,
    });

    if (successful === true) {
      window.location = "/";
    } else {
      toast.configure();
      toast.error("Unsuccessful login!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }
}

const mapStateToProps = (state) => ({ userLoggedIn: state.userLoggedIn });

export default compose(
  withRouter,
  connect(mapStateToProps, {
    userLoggedIn,
  })
)(LoginPage);
