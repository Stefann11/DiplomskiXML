import React, { Component } from "react";
import ItemsTable from "../components/Item/ItemsTable";
import Layout from "../layouts/Layout";

class ItemsTablePage extends Component {
  render() {
    return (
      <Layout>
        <ItemsTable />
      </Layout>
    );
  }
}

export default ItemsTablePage;
