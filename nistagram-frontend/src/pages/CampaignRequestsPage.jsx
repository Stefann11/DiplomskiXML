import React, { Component } from "react";
import CampaignRequests from "../components/Campaign/CampaignRequests";
import Layout from "../layouts/Layout";

class CampaignRequestsPage extends Component {
  render() {
    return (
      <Layout>
        <CampaignRequests />
      </Layout>
    );
  }
}

export default CampaignRequestsPage;
