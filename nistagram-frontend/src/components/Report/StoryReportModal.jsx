import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { getStoryById } from "../../actions/actionsStory";
import moment from "moment";

class StoryReportModal extends Component {
  state = {
    showStoryModal: this.props.show,
  };

  async componentDidMount() {
    await this.props.getStoryById(this.props.storyId);
  }

  render() {
    if (this.props.storyById === undefined) {
      return null;
    }

    if (this.props.imageForStory === undefined) {
      return null;
    }

    debugger;
    return (
      <Modal
        style={{
          maxWidth: "850px",
          width: "849px",
        }}
        isOpen={this.state.showStoryModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          Reported story
        </ModalHeader>
        <ModalBody>
          <div>
            {this.props.imageForStory.contentType === "image/jpeg" ? (
              <img
                src={
                  "data:image/jpg;base64," +
                  this.props.imageForStory.fileContents
                }
                style={{ width: 800, height: 320 }}
                className="mb-3"
              />
            ) : (
              <video
                controls
                style={{ width: 800, height: 320 }}
                className="mb-3"
              >
                <source
                  src={
                    "data:video/mp4;base64," +
                    this.props.imageForStory.fileContents
                  }
                  type="video/mp4"
                ></source>
              </video>
            )}
            <br />
            <div>Description: {"    " + this.props.storyById.description}</div>
            <hr />
            <div>
              Posted on:
              {"    " +
                moment(this.props.storyById.timeStamp).format(
                  "DD/MM/YYYY HH:mm"
                )}
            </div>
          </div>
        </ModalBody>
        <ModalFooter></ModalFooter>
      </Modal>
    );
  }

  toggle() {
    debugger;
    this.setState({ showStoryModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({
  storyById: state.storyById,
  imageForStory: state.imageForStory,
});

export default connect(mapStateToProps, { getStoryById })(StoryReportModal);
