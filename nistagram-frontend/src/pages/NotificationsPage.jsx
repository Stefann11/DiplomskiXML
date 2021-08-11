import React, { Component } from "react";
import Notifications from "../components/Notification/Notifications";
import Layout from "../layouts/Layout";

class NotificationsPage extends Component {
  render() {
    return (
      <Layout>
        <Notifications />
      </Layout>
    );
  }
}

export default NotificationsPage;
