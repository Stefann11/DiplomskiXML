import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { createNewCollection } from "../../actions/actions";
import { connect } from "react-redux";

class CreateCollectionModal extends Component {
  state = {
    showCreateCollectionModal: this.props.show,
    name: "",
  };

  render() {
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showCreateCollectionModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          Create new collection
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
              placeholder="Enter collection name"
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
            Create
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  async create() {
    await this.props.createNewCollection({
      CollectionName: this.state.name,
      RegisteredUser: { id: sessionStorage.getItem("userId") },
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
    this.setState({ showCreateCollectionModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { createNewCollection })(
  CreateCollectionModal
);
