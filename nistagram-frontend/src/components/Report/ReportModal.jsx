import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { reportContent } from "../../actions/actionsReport";

class ReportModal extends Component {
  state = {
    showReportModal: this.props.show,
    name: "",
  };

  render() {
    debugger;
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showReportModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          Reason for reporting
        </ModalHeader>
        <ModalBody>
          <div>
            <input
              type="text"
              name="name"
              value={this.state.name}
              onChange={this.handleChange}
              class="form-control"
              id="name"
              placeholder="Enter reason for reporting"
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            disabled={this.state.name.length === 0}
            className="btn btn-primary"
            onClick={() => {
              this.create();
            }}
          >
            Report
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  async create() {
    await this.props.reportContent({
      ReportReason: this.state.name,
      RegisteredUser: {
        id: this.props.registeredUser.id,
        username: this.props.registeredUser.username,
      },
      Content: { id: this.props.contentId },
      Type: this.props.type,
    });
    this.toggle();
  }

  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };

  toggle() {
    debugger;
    this.setState({ showReportModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { reportContent })(ReportModal);
