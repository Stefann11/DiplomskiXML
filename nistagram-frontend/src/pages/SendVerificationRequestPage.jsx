import React, { Component } from "react";
import SomeoneProfile from "../components/Profile/SomeoneProfile";
import SendVerificationRequest from "../components/VerificationRequest/SendVerificationRequest";
import Layout from "../layouts/Layout";

class SendVerificationRequestPage extends Component {
  render() {
    return (
      <Layout>
        <SendVerificationRequest />
      </Layout>
    );
  }
}

export default SendVerificationRequestPage;
