import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import { NavItem, Nav } from "reactstrap";
import { NavLink } from "react-router-dom";

import {
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
} from "reactstrap";

class NavigationBar extends Component {
  state = {};

  render() {
    const NavBar = () => {
      return (
        <Nav
          className="navbar navbar-light pl-5 pr-5"
          style={{ textDecoration: "none" }}
        >
          <NavItem>
            <NavLink
              exact
              to="/"
              className="nav-link"
              activeClassName="nav-link-active"
            >
              Agent Application
            </NavLink>
          </NavItem>
          <NavItem>
            <NavLink
              exact
              to="/"
              className="nav-link"
              activeClassName="nav-link-active"
            >
              <img src="/images/home.png" />
            </NavLink>
          </NavItem>
          <NavItem style={{ float: "right" }}>
            <NavLink
              exact
              to="/token"
              className="nav-link"
              activeClassName="nav-link-active"
            >
              <img src="/images/campaign.png" />
            </NavLink>
          </NavItem>

          <UncontrolledDropdown style={{ float: "right" }}>
            <DropdownToggle nav caret>
              {sessionStorage.getItem("usernameAgentApp")}
            </DropdownToggle>
            <DropdownMenu right>
              <DropdownItem>
                <NavLink to="/login" onClick={this.logout.bind(this)}>
                  Logout
                </NavLink>
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </Nav>
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
    localStorage.setItem("tokenAgentApp", "");
    localStorage.setItem("userIdAgentApp", "");
    localStorage.setItem("roleAgentApp", "");
    localStorage.setItem("usernameAgentApp", "");
  }

  removeSessionStorage() {
    localStorage.setItem("storage", "");
    sessionStorage.setItem("tokenAgentApp", "");
    sessionStorage.setItem("userIdAgentApp", "");
    sessionStorage.setItem("roleAgentApp", "");
    sessionStorage.setItem("usernameAgentApp", "");
  }
}

export default withRouter(NavigationBar);
