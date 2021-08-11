import React, { Component } from "react";
import ReviewItem from "../components/Item/ReviewItem";
import Layout from "../layouts/Layout";

class ReviewItemPage extends Component {
  render() {
    return (
      <Layout>
        <ReviewItem />
      </Layout>
    );
  }
}

export default ReviewItemPage;
