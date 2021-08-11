import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  getPost,
  getCollectionsByUser,
  addPostToCollection,
} from "../../actions/actions";
import { connect } from "react-redux";

class ChooseCollectionModal extends Component {
  state = {
    showChooseCollectionModal: this.props.show,
    collection: {},
    collections: [],
  };

  async componentDidMount() {
    debugger;
    await this.props.getPost(this.props.postId);
    await this.props.getCollectionsByUser();
    this.setState({
      collections: this.props.collections,
    });
  }

  render() {
    if (this.props.post === undefined || this.props.collections === undefined) {
      return null;
    }
    const post = this.props.post;
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showChooseCollectionModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>Collections</ModalHeader>
        <ModalBody>
          <select
            className="form-control"
            onChange={this.handleChangeCollection}
          >
            <option>Select collection</option>
            {this.state.collections.map((option, index) => (
              <option key={index} value={index}>
                {option.collectionName}
              </option>
            ))}
          </select>
        </ModalBody>
        <ModalFooter>
          <button
            disabled={
              this.state.collection === undefined
                ? true
                : Object.keys(this.state.collection).length === 0
            }
            className="btn btn-primary"
            onClick={() => {
              this.addPostToCollection();
            }}
          >
            Add
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  toggle() {
    debugger;
    this.setState({ showChooseCollectionModal: false });
    this.props.onShowChange();
  }

  async addPostToCollection() {
    debugger;
    await this.props.addPostToCollection({
      id: this.state.collection.id,
      post: this.props.post,
    });
    this.toggle();
  }

  handleChangeCollection = (e) => {
    this.setState({ collection: this.state.collections[e.target.value] });
  };
}

const mapStateToProps = (state) => ({
  post: state.post,
  collections: state.collections,
});

export default connect(mapStateToProps, {
  getPost,
  getCollectionsByUser,
  addPostToCollection,
})(ChooseCollectionModal);
