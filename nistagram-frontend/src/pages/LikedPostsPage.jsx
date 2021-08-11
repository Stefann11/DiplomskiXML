import React, { Component } from "react";
import LikedPosts from "../components/Post/LikedPosts";
import Layout from "../layouts/Layout";

class LikedPostsPage extends Component {
  render() {
    return (
      <Layout>
        <LikedPosts />
      </Layout>
    );
  }
}

export default LikedPostsPage;
