import React, { Component } from "react";
import VerificationRequest from "../components/Profile/VerificationRequest";
import Layout from "../layouts/Layout";

class VerificationRequestPage extends Component {
  render() {
    return (
      <Layout>
        <VerificationRequest />
      </Layout>
    );
  }
}

export default VerificationRequestPage;
