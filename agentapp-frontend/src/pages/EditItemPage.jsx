import React, { Component } from "react";
import EditItem from "../components/Item/EditItem";
import Layout from "../layouts/Layout";

class EditItemPage extends Component {
  render() {
    return (
      <Layout>
        <EditItem />
      </Layout>
    );
  }
}

export default EditItemPage;
