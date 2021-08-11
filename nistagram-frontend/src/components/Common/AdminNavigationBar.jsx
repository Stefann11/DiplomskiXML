import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import { withRouter } from "react-router-dom";
import {
  NavbarBrand,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
} from "reactstrap";

class AdminNavigationBar extends Component {
  state = {};

  render() {
    const NavBar = () => {
      return (
        <React.Fragment>
          <NavLink
            exact
            to="/reports"
            onClick={() => {
              window.location = "/reports";
            }}
          >
            <img src="/images/ban.png" />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/viewVerificationRequests"
            onClick={() => {
              window.location = "/viewVerificationRequests";
            }}
          >
            <img src="/images/verified.png" />
          </NavLink>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <NavLink
            exact
            to="/approval"
            onClick={() => {
              window.location = "/approval";
            }}
          >
            <img src="/images/agent-verification.png" />
          </NavLink>
          <UncontrolledDropdown style={{ float: "right" }}>
            <DropdownToggle nav caret></DropdownToggle>
            <DropdownMenu right>
              <DropdownItem divider />
              <DropdownItem>
                <NavLink to="/login" onClick={this.logout.bind(this)}>
                  Logout
                </NavLink>
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </React.Fragment>
      );
    };
    return <NavBar />;
  }

  logout() {
    this.removeLocalStorage();
    this.removeSessionStorage();
    this.props.history.push("/login");
  }
  removeLocalStorage() {
    localStorage.setItem("storage", "");
    localStorage.setItem("token", "");
    localStorage.setItem("id", "");
    localStorage.setItem("role", "");
    localStorage.setItem("username", "");
  }

  removeSessionStorage() {
    localStorage.setItem("storage", "");
    sessionStorage.setItem("token", "");
    sessionStorage.setItem("id", "");
    sessionStorage.setItem("role", "");
    sessionStorage.setItem("username", "");
  }
}

export default withRouter(AdminNavigationBar);
