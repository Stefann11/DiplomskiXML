import React, { Component } from "react";
import NotLoggedAgentRegistration from "../components/Agent/NotLoggedAgentRegistration";
import Layout from "../layouts/Layout";

class NotLoggedAgentRegistrationPage extends Component {
  render() {
    return (
      <Layout>
        <NotLoggedAgentRegistration />
      </Layout>
    );
  }
}

export default NotLoggedAgentRegistrationPage;
