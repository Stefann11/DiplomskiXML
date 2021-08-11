import React, { Component } from "react";
import ItemReview from "../components/Item/ItemReview";
import Layout from "../layouts/Layout";

class ReviewItemPage extends Component {
  render() {
    return (
      <Layout>
        <ItemReview />
      </Layout>
    );
  }
}

export default ReviewItemPage;
