import React, { Component } from "react";
import { getFollowRequests, handleRequests } from "../../actions/actionsUser";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { withRouter } from "react-router-dom";
import { compose } from "redux";

class FollowRequestsTable extends Component {
  state = {};

  componentDidMount() {
    debugger;
    this.props.getFollowRequests();
  }

  render() {
    debugger;
    if (this.props.followRequests === undefined) {
      return null;
    }
    const followRequests = this.props.followRequests;
    return (
      <div>
        <div className="wrap bg-white pt-3 pb-3" style={{ height: "100vh" }}>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <div className="text-center">
              <img style={{ width: 32, height: 32 }} src="/images/user.png" />
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
                    <th style={{ textAlign: "center" }}>Request by</th>
                    <th style={{ textAlign: "center" }}>Accept</th>
                    <th style={{ textAlign: "center" }}>Reject</th>
                  </tr>
                </thead>
                <tbody>
                  {followRequests.map((f) => (
                    <tr>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {f.requestsFollow.firstName +
                          " " +
                          f.requestsFollow.lastName +
                          " (" +
                          f.requestsFollow.username +
                          ")"}
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-2"
                          onClick={() => {
                            this.accept(f);
                          }}
                        >
                          Accept
                        </button>
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-2"
                          onClick={() => {
                            this.reject(f);
                          }}
                        >
                          Reject
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

  async accept(f) {
    this.props.handleRequests({
      Id: f.id,
      FollowedById: f.requestsFollow.id,
      FollowingId: sessionStorage.getItem("userId"),
      Type: "approve",
      IsApproved: true,
    });
  }

  async reject(f) {
    this.props.handleRequests({
      Id: f.id,
      FollowedById: f.requestsFollow.id,
      FollowingId: sessionStorage.getItem("userId"),
      Type: "decline",
      IsApproved: false,
    });
  }
}

const mapStateToProps = (state) => ({ followRequests: state.followRequests });

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getFollowRequests,
    handleRequests,
  })
)(FollowRequestsTable);
