import React, { Component } from "react";
import { Card } from "react-bootstrap";
import { Table } from "reactstrap";
import { getUnapprovedVerificationRequests } from "../../actions/actionsUser";
import { connect } from "react-redux";
import VerificationRequestDetailsModal from "./VerificationRequestDetailsModal";

class ViewVerificationRequests extends Component {
  state = { showVerificationRequestModal: false, verificationRequest: {} };

  async componentDidMount() {
    await this.props.getUnapprovedVerificationRequests();
  }

  render() {
    debugger;
    if (this.props.unapprovedVerificationRequests === undefined) return null;
    return (
      <div>
        {this.state.showVerificationRequestModal === true ? (
          <VerificationRequestDetailsModal
            show={this.state.showVerificationRequestModal}
            onShowChange={this.displayModal.bind(this)}
            verificationRequest={this.state.verificationRequest}
          />
        ) : null}
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
                    <th style={{ textAlign: "center" }}>Details</th>
                  </tr>
                </thead>
                <tbody>
                  {this.props.unapprovedVerificationRequests.map((f) => (
                    <tr>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {f.registeredUser.firstName +
                          " " +
                          f.registeredUser.lastName +
                          " (" +
                          f.registeredUser.username +
                          ")"}
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-2"
                          onClick={() => {
                            this.displayModal(f);
                          }}
                        >
                          Details
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

  displayModal(verificationRequest) {
    debugger;
    this.setState({
      showVerificationRequestModal: !this.state.showVerificationRequestModal,
      verificationRequest: verificationRequest,
    });
  }
}

const mapStateToProps = (state) => ({
  unapprovedVerificationRequests: state.unapprovedVerificationRequests,
});
export default connect(mapStateToProps, { getUnapprovedVerificationRequests })(
  ViewVerificationRequests
);
