import React, { Component } from "react";
import CreateStory from "../components/Story/CreateStory";
import Layout from "../layouts/Layout";

class CreateStoryPage extends Component {
  render() {
    return (
      <Layout>
        <CreateStory />
      </Layout>
    );
  }
}

export default CreateStoryPage;
