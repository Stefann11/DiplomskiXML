import React, { Component } from "react";
import VerificationRequest from "../components/Profile/VerificationRequest";
import ViewVerificationRequests from "../components/VerificationRequest/ViewVerificationRequests";
import Layout from "../layouts/Layout";

class ViewVerificationRequestPage extends Component {
  render() {
    return (
      <Layout>
        <ViewVerificationRequests />
      </Layout>
    );
  }
}

export default ViewVerificationRequestPage;
