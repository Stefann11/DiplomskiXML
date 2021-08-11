import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { Table } from "reactstrap";
import { Card, CardBody, CardFooter, CardHeader } from "reactstrap";
import {
  addStoryToHighlight,
  getHighlights,
  createHighlight,
  loadProfileImagesStory,
} from "../../actions/actionsStory";
import { connect } from "react-redux";

class CollectionsModal extends Component {
  state = {
    showPostModal: this.props.show,
    name: "",
  };

  render() {
    if (this.props.highlights == undefined) {
      return null;
    }
    return (
      <Modal
        style={{
          maxWidth: "600px",
          width: "599px",
        }}
        isOpen={this.state.showPostModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>Collections</ModalHeader>
        <ModalBody>
          <Card
            className="mt-5"
            style={{ shadowColor: "gray", boxShadow: "0 8px 6px -6px #999" }}
          >
            <Table className="table allPrescriptions mb-0">
              <tbody>
                <tr>
                  <td style={{ textAlign: "left" }}>
                    <label className="ml-2">Create new highlight:</label>
                    <div className="float-right">
                      <button
                        onClick={() => this.createHighlight()}
                        className="btn btn-sm btn-primary"
                      >
                        +
                      </button>
                    </div>
                    <input
                      type="text"
                      name="name"
                      value={this.state.name}
                      onChange={this.handleChange}
                      className="form-control float-right w-50 h-75 mr-4"
                      id="name"
                      placeholder="Enter highlight name"
                    />
                  </td>
                </tr>
                {this.props.highlights.map((f) => (
                  <tr>
                    <td style={{ textAlign: "left" }}>
                      <label className="ml-2">{f.highlightName}</label>
                      <div className="float-right">
                        <button
                          onClick={() => this.addStoryToHighlight(f.id)}
                          className="btn btn-sm btn-primary"
                        >
                          +
                        </button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Card>
        </ModalBody>
        <ModalFooter></ModalFooter>
      </Modal>
    );
  }

  handleChange = (event) => {
    this.setState({
      name: event.target.value,
    });
  };

  async createHighlight() {
    var highlight = {
      HighlightName: this.state.name,
      Stories: [this.props.story],
      RegisteredUser: this.props.story.registeredUser,
    };
    await this.props.createHighlight(highlight);
    await this.props.getHighlights(this.props.story.registeredUser.id);
    await this.props.loadProfileImagesStory(this.createImagesList());
    window.location.reload(false);
  }

  async addStoryToHighlight(highlightId) {
    await this.props.addStoryToHighlight(highlightId, this.props.story);
    await this.props.getHighlights(this.props.story.registeredUser.id);
    this.toggle();
  }

  createImagesList() {
    debugger;
    var images = [];
    this.props.newHighlights.forEach((element) => {
      images.push(element.stories[0].contentPath);
    });
    return images;
  }

  toggle() {
    this.setState({ showPostModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({ newHighlights: state.highlights });

export default connect(mapStateToProps, {
  addStoryToHighlight,
  getHighlights,
  createHighlight,
  loadProfileImagesStory,
})(CollectionsModal);
