import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { getItems } from "../../actions/actions";

class ItemsTable extends Component {
  state = {
    showPostModal: false,
    showEditModal: false,
    itemToEdit: {},
  };

  async componentDidMount() {
    debugger;
    await this.props.getItems();
  }

  render() {
    if (this.props.items === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        <h3 className="mt-4" style={{ textAlign: "center" }}>
          Items
        </h3>
        <hr />
        <div className="wrap bg-white pt-3">
          <div style={{ textAlign: "right" }}>
            {sessionStorage.getItem("roleAgentApp") === "Agent" ? (
              <button
                type="button"
                className="btn-primary btn"
                onClick={() => {
                  this.createNew();
                }}
              >
                Create new item
              </button>
            ) : (
              ""
            )}
          </div>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <Card
              className="mt-5"
              style={{
                shadowColor: "gray",
                boxShadow: "0 8px 6px -6px #999",
              }}
            >
              <div
                style={{
                  maxHeight: "440px",
                  overflowY: "auto",
                }}
              >
                <Table className="table allPrescriptions mb-0" striped>
                  <thead>
                    <tr>
                      <th style={{ textAlign: "center" }}>Name</th>
                      <th style={{ textAlign: "center" }}>Price</th>
                      <th style={{ textAlign: "center" }}>Available Count</th>
                      <th style={{ textAlign: "center" }}></th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.items.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>{f.name}</td>
                        <td style={{ textAlign: "center" }}>{f.price}</td>
                        <td style={{ textAlign: "center" }}>
                          {f.availableCount}
                        </td>

                        <td style={{ textAlign: "center" }}>
                          <img
                            onClick={() => {
                              this.view(f);
                            }}
                            src="/images/analytics.png"
                          />
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </div>
            </Card>
          </div>
        </div>
      </div>
    );
  }

  edit(item) {
    this.setState({
      itemToEdit: item,
    });
    this.displayModalEdit();
  }

  async delete(item) {
    debugger;
    window.location = "/";
  }

  createNew() {
    window.location = "/create";
  }

  view(f) {
    window.location = "/items/" + f.id;
  }

  displayModalPost() {
    this.setState({
      showPostModal: !this.state.showPostModal,
    });
  }

  displayModalEdit() {
    this.setState({
      showEditModal: !this.state.showEditModal,
    });
  }
}

const mapStateToProps = (state) => ({
  items: state.items,
});

export default compose(
  withRouter,
  connect(mapStateToProps, { getItems })
)(ItemsTable);
