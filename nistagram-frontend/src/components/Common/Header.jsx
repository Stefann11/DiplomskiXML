import React, { Component } from "react";
import AdminNavigationBar from "./AdminNavigationBar";
import NavigationBar from "./NavigationBar";
import NotLoggedNavigationBar from "./NotLoggedNavigationBar";
import SearchBar from "./SearchBar";

class Header extends Component {
  state = {};
  render() {
    return (
      <header className="pt-4">
        <img height="50" src="/images/nistagram-logo.PNG" />
        <span style={{ width: 200, display: "inline-block" }}></span>
        <span style={{ width: 300, display: "inline-block" }}>
          <SearchBar></SearchBar>
        </span>
        {sessionStorage.getItem("token") !== "" ? (
          sessionStorage.getItem("role") !== "Admin" ? (
            <span
              style={{ display: "inline-block", float: "right", zIndex: "4" }}
            >
              <NavigationBar />
            </span>
          ) : (
            <span
              style={{ display: "inline-block", float: "right", zIndex: "4" }}
            >
              <AdminNavigationBar />
            </span>
          )
        ) : (
          <span
            style={{ display: "inline-block", float: "right", zIndex: "4" }}
          >
            <NotLoggedNavigationBar />
          </span>
        )}

        <hr />
      </header>
    );
  }
}

export default Header;
