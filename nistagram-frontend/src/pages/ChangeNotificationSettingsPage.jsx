import React, { Component } from "react";
import ChangeNotificationSettings from "../components/Notification/ChangeNotificationSettings";
import Layout from "../layouts/Layout";

class ChangeNotificationSettingsPage extends Component {
  render() {
    return (
      <Layout>
        <ChangeNotificationSettings />
      </Layout>
    );
  }
}

export default ChangeNotificationSettingsPage;
