import React, { Component } from "react";
import PostsInCollection from "../components/Collection/PostsInCollection";
import Layout from "../layouts/Layout";

class PostsInCollectionPage extends Component {
  render() {
    return (
      <Layout>
        <PostsInCollection />
      </Layout>
    );
  }
}

export default PostsInCollectionPage;
