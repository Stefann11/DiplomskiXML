import React, { Component } from "react";
import NotLoggedPublicProfile from "../components/Profile/NotLoggedPublicProfile";
import PublicProfile from "../components/Profile/PublicProfile";
import Layout from "../layouts/Layout";

class ProfilePage extends Component {
  render() {
    return (
      <Layout>
        {sessionStorage.getItem("token") === "" ? (
          <NotLoggedPublicProfile />
        ) : (
          <PublicProfile />
        )}
      </Layout>
    );
  }
}

export default ProfilePage;
