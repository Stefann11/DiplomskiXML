import React, { Component } from "react";
import CollectionsMenu from "../components/Collection/CollectionsMenu";
import Layout from "../layouts/Layout";

class CollectionsMenuPage extends Component {
  render() {
    return (
      <Layout>
        <CollectionsMenu />
      </Layout>
    );
  }
}

export default CollectionsMenuPage;
