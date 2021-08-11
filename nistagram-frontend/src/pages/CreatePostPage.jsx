import React, { Component } from "react";
import CreatePost from "../components/Post/CreatePost";
import Layout from "../layouts/Layout";

class CreatePostPage extends Component {
  render() {
    return (
      <Layout>
        <CreatePost />
      </Layout>
    );
  }
}

export default CreatePostPage;
