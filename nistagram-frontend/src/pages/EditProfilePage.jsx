import React, { Component } from "react";
import EditProfile from "../components/Profile/EditProfile";
import Layout from "../layouts/Layout";

class EditProfilePage extends Component {
  render() {
    return (
      <Layout>
        <EditProfile />
      </Layout>
    );
  }
}

export default EditProfilePage;
