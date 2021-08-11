import React, { Component } from "react";
import NavigationBar from "./NavigationBar";
import NotLoggedNavigationBar from "./NotLoggedNavigationBar";

class Header extends Component {
  state = {};
  render() {
    return (
      <header>
        {sessionStorage.getItem("tokenAgentApp") !== "" ? (
          <NavigationBar />
        ) : (
          <NotLoggedNavigationBar />
        )}

        <hr />
      </header>
    );
  }
}

export default Header;
