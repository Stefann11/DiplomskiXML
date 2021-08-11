import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import { withRouter } from "react-router-dom";
import {
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
} from "reactstrap";

class NotLoggedNavigationBar extends Component {
  state = {};

  render() {
    const NavBar = () => {
      return (
        <React.Fragment>
          <UncontrolledDropdown style={{ float: "right" }}>
            <DropdownToggle nav caret>
              {" "}
            </DropdownToggle>
            <DropdownMenu right>
              <DropdownItem>
                <NavLink to="/registration">Register</NavLink>
              </DropdownItem>
              <DropdownItem divider />
              <DropdownItem>
                <NavLink to="/login">Login</NavLink>
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </React.Fragment>
      );
    };
    return <NavBar />;
  }
}

export default withRouter(NotLoggedNavigationBar);
