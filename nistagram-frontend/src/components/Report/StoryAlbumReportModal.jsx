import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { getStoryById } from "../../actions/actionsStory";
import moment from "moment";
import { Slide } from "react-slideshow-image";
import "react-slideshow-image/dist/styles.css";

class StoryAlbumReportModal extends Component {
  state = {
    showStoryModal: this.props.show,
    stories: [],
    imagesForStories: [],
  };

  async componentDidMount() {
    var stories = [];
    var imagesForStories = [];
    debugger;
    for (var i = 0; i < this.props.storyIds.length; i++) {
      await this.props.getStoryById(this.props.storyIds[i]);
      stories.push(this.props.storyById);
      imagesForStories.push(this.props.imageForStory);
    }
    this.setState({
      stories: stories,
      imagesForStories: imagesForStories,
    });
  }

  render() {
    if (this.props.storyById === undefined) {
      return null;
    }

    if (this.props.imageForStory === undefined) {
      return null;
    }

    if (this.state.stories.length === 0) {
      return null;
    }

    debugger;
    console.log(this.state);
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
            {this.state.imagesForStories.length > 1 ? (
              <Slide easing="ease">
                {this.state.imagesForStories.map((f, i) =>
                  this.state.imagesForStories[i].contentType ===
                  "image/jpeg" ? (
                    <div className="each-slide">
                      <img
                        src={
                          "data:image/jpg;base64," +
                          this.state.imagesForStories[i].fileContents
                        }
                        style={{ width: 800, height: 320 }}
                        className="mb-3"
                      />
                    </div>
                  ) : (
                    <div className="each-slide">
                      <video
                        controls
                        style={{ width: 800, height: 320 }}
                        className="mb-3"
                      >
                        <source
                          src={
                            "data:video/mp4;base64," +
                            this.state.imagesForStories[i].fileContents
                          }
                          type="video/mp4"
                        ></source>
                      </video>
                    </div>
                  )
                )}
              </Slide>
            ) : this.props.imageForStory.contentType === "image/jpeg" ? (
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

export default connect(mapStateToProps, { getStoryById })(
  StoryAlbumReportModal
);
