import React, { Component } from "react";
import FollowRequestsTable from "../components/FollowRequest/FollowRequestsTable";
import Layout from "../layouts/Layout";

class FollowRequestPage extends Component {
  render() {
    return (
      <Layout>
        <FollowRequestsTable />
      </Layout>
    );
  }
}

export default FollowRequestPage;
