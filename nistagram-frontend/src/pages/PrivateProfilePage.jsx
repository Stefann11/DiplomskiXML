import React, { Component } from "react";
import PrivateProfile from "../components/Profile/PrivateProfile";
import Layout from "../layouts/Layout";

class ProfilePage extends Component {
  render() {
    return (
      <Layout>
        <PrivateProfile />
      </Layout>
    );
  }
}

export default ProfilePage;
