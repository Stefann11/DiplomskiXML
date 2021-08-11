import React, { Component } from "react";
import ChangeProfilePicture from "../components/Profile/ChangeProfilePicture";
import Layout from "../layouts/Layout";

class ChangeProfilePicturePage extends Component {
  render() {
    return (
      <Layout>
        <ChangeProfilePicture />
      </Layout>
    );
  }
}

export default ChangeProfilePicturePage;
