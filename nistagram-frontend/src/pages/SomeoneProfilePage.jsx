import React, { Component } from "react";
import SomeoneProfile from "../components/Profile/SomeoneProfile";
import Layout from "../layouts/Layout";

class SomeoneProfilePage extends Component {
  render() {
    return (
      <Layout>
        <SomeoneProfile />
      </Layout>
    );
  }
}

export default SomeoneProfilePage;
