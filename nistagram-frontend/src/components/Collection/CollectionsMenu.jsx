import React, { Component } from "react";
import { getCollectionsByUser } from "../../actions/actions";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import CreateCollectionModal from "./CreateCollectionModal";

class CollectionsMenu extends Component {
  state = {
    showCreateCollectionModal: false,
  };

  componentDidMount() {
    debugger;
    this.props.getCollectionsByUser();
  }

  render() {
    debugger;
    if (this.props.collections === undefined) {
      return null;
    }
    const collections = this.props.collections;
    return (
      <div>
        {this.state.showCreateCollectionModal ? (
          <CreateCollectionModal
            show={this.state.showCreateCollectionModal}
            onShowChange={this.displayModalPost.bind(this)}
          />
        ) : null}
        <div className="wrap bg-white pt-3 pb-3" style={{ height: "100vh" }}>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <div className="text-center">
              <img
                style={{ width: 48, height: 48 }}
                src="/images/collection.png"
              />
            </div>
            <div style={{ width: 200, maxWidth: 200, float: "right" }}>
              <button
                className="btn btn-primary"
                onClick={() => {
                  this.displayModalPost();
                }}
              >
                Create new collection
              </button>
            </div>

            <Card
              className="mt-5"
              style={{
                shadowColor: "gray",
                boxShadow: "0 8px 6px -6px #999",
              }}
            >
              <Table className="table allPrescriptions mb-0" striped>
                <thead>
                  <tr>
                    <th style={{ textAlign: "center" }}>Collection Name</th>
                    <th style={{ textAlign: "center" }}>View</th>
                  </tr>
                </thead>
                <tbody>
                  {collections.map((f) => (
                    <tr>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {f.collectionName}
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-2"
                          onClick={() => {
                            this.view(f);
                          }}
                        >
                          View
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            </Card>
          </div>
        </div>
      </div>
    );
  }

  view(f) {
    window.location = "/collection/" + f.id;
  }

  displayModalPost() {
    this.setState({
      showCreateCollectionModal: !this.state.showCreateCollectionModal,
    });
  }
}

const mapStateToProps = (state) => ({ collections: state.collections });

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getCollectionsByUser,
  })
)(CollectionsMenu);
