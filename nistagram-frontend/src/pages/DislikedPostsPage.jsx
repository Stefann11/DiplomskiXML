import React, { Component } from "react";
import DislikedPosts from "../components/Post/DislikedPosts";
import Layout from "../layouts/Layout";

class DislikedPostsPage extends Component {
  render() {
    return (
      <Layout>
        <DislikedPosts />
      </Layout>
    );
  }
}

export default DislikedPostsPage;
