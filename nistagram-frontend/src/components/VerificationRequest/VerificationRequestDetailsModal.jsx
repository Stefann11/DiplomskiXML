import { Modal, ModalBody, ModalHeader } from "reactstrap";
import React, { Component } from "react";
import { connect } from "react-redux";
import {
  loadImageProfile,
  deleteVerificationRequest,
  verifyUser,
} from "../../actions/actionsUser";
import { textAlign } from "@material-ui/system";

class VerificationRequestDetailsModal extends Component {
  state = { showModal: this.props.show };

  async componentDidMount() {
    await this.props.loadImageProfile(
      this.props.verificationRequest.documentImagePath
    );
  }

  render() {
    debugger;
    return (
      <Modal
        style={{
          maxWidth: "1000px",
          width: "999px",
        }}
        isOpen={this.state.showModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          Verification Request
        </ModalHeader>
        <ModalBody>
          <div>
            <div>
              <img
                width="50%"
                style={{
                  marginLeft: "auto",
                  marginRight: "auto",
                  display: "block",
                  maxHeight: "799px",
                  maxWidth: "599px",
                }}
                src={"data:image/jpg;base64," + this.props.image}
              />
            </div>
            <div className="ml-5 mr-5 mt-3 text-center">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">First name:</label>
                  <div className="d-block w-100">
                    <label name="firstName">
                      {this.props.verificationRequest.firstName}
                    </label>
                  </div>
                </div>
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Last name:</label>
                  <div className="d-block w-100">
                    <label name="firstName">
                      {this.props.verificationRequest.lastName}
                    </label>
                  </div>
                </div>
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Category:</label>
                  <div className="d-block w-100">
                    <label name="firstName">
                      {this.props.verificationRequest.category}
                    </label>
                  </div>
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5 ml-5 mr-5">
              <button
                className="btn btn-lg btn-success w-25 float-left"
                onClick={() => {
                  this.verifyUser();
                }}
              >
                Accept
              </button>
              <button
                className="btn btn-lg btn-danger w-25 float-right"
                onClick={() => {
                  this.rejectRequest();
                }}
              >
                Reject
              </button>
            </div>
          </div>
        </ModalBody>
      </Modal>
    );
  }

  async rejectRequest() {
    await this.props.deleteVerificationRequest(this.props.verificationRequest);
    this.toggle();
  }

  async verifyUser() {
    await this.props.verifyUser(this.props.verificationRequest);
    this.toggle();
  }

  toggle() {
    debugger;
    this.setState({ showModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({ image: state.profileImage });

export default connect(mapStateToProps, {
  loadImageProfile,
  deleteVerificationRequest,
  verifyUser,
})(VerificationRequestDetailsModal);
