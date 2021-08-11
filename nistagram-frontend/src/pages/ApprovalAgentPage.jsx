import React, { Component } from "react";
import ApprovalAgentRequest from "../components/Agent/ApprovalAgentRequest";
import Layout from "../layouts/Layout";

class ApprovalAgentPage extends Component {
  render() {
    return (
      <Layout>
        <ApprovalAgentRequest />
      </Layout>
    );
  }
}

export default ApprovalAgentPage;
