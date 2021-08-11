import React, { Component } from "react";
import Stories from "react-insta-stories";
import { Modal, ModalBody } from "reactstrap";
import {
  getStoriesForModal,
  loadImagesStory,
} from "../../actions/actionsStory";
import { connect } from "react-redux";
import "../../css/story.css";
import TaggedUsersStoryModal from "./TaggedUsersStoryModal";
import ReportModal from "../Report/ReportModal";
import { getAdForContent } from "../../actions/actionsCampaign";
import StoryLinkModal from "../Campaign/StoryLinkModal";

class StoryModal extends Component {
  state = {
    showStoryModal: this.props.show,
    showTaggedUsersModal: false,
    showReportModal: false,
    users: [],
    story: {},
    showLinkModal: false,
    results: [],
    contentId: "",
  };

  async componentDidMount() {
    await this.props.getStoriesForModal(
      this.props.user,
      sessionStorage.getItem("userId")
    );
    await this.props.loadImagesStory(this.createImagesList(this.props.stories));
    var results2 = [];
    for (var i = 0; i < this.props.stories.length; i++) {
      var result = false;
      result = await this.props.getAdForContent(this.props.stories[i].id);
      results2.push(result);
    }
    this.setState({
      results: results2,
    });
  }

  render() {
    if (
      this.props.stories === undefined ||
      this.props.images === undefined ||
      this.props.stories.length !== this.props.images.length
    )
      return null;
    const stories = [];
    this.props.stories.forEach((story, i) => {
      stories.push({
        url:
          this.props.images[i].contentType === "image/jpeg"
            ? "data:image/jpg;base64," + this.props.images[i].fileContents
            : "data:video/mp4;base64," + this.props.images[i].fileContents,
        duration: 1000 * story.duration,
        type:
          this.props.images[i].contentType !== "image/jpeg" ? "video" : "image",
        header: {
          heading:
            story.registeredUser.firstName +
            " " +
            story.registeredUser.lastName,
          subheading: `Posted ${this.timeSince(story.timeStamp)} ago`,
          profileImage:
            "data:image/jpg;base64," + this.props.profileImage.fileContents,
        },
        seeMore: ({ close }) => {
          return (
            <div>
              {this.state.results[i] === true ? (
                <div className="ml-4 mb-2" style={{ textAlign: "right" }}>
                  {" "}
                  <b>Sponsored</b>{" "}
                  <img classNmae="ml-4 mb-4" src="/images/star.png" />
                </div>
              ) : (
                ""
              )}
              <div
                onClick={close}
                style={{
                  bottom: 0,
                  position: "absolute",
                  zIndex: 10,
                  width: "100%",
                  backgroundColor: "black",
                  opacity: "50%",
                  color: "white",
                  height: "300px",
                }}
              ></div>
              <div className="story-footer-description">
                {story.description}
              </div>
              <div className="story-footer-hashtag">
                {this.convertHashtagsToString(story.hashTags)}
              </div>
              {this.state.results[i] === true ? (
                <div className="story-footer-tagged">
                  <button
                    className="btn btn-sm btn-primary"
                    onClick={() => this.displayModalLink(story.id)}
                  >
                    Link:
                  </button>
                </div>
              ) : (
                <div className="story-footer-tagged">
                  <button
                    className="btn btn-sm btn-primary"
                    onClick={() => this.displayModalStory(story.taggedUsers)}
                  >
                    Tagged users:
                  </button>
                </div>
              )}
              {sessionStorage.getItem("userId") === "" ? null : (
                <div className="story-footer-collections">
                  <button
                    className="btn btn-sm btn-primary"
                    onClick={() => this.displayModalReport(story)}
                  >
                    Report
                  </button>
                </div>
              )}
            </div>
          );
        },
      });
    });
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "450px",
          maxHeight: "700px",
          width: "700px",
          borderRadius: "15px",
        }}
        className="text-center"
        isOpen={this.state.showStoryModal}
        toggle={this.toggle.bind(this)}
        centered={true}
        backdropClassName="story-modal-backdrop"
      >
        {this.state.showTaggedUsersModal ? (
          <TaggedUsersStoryModal
            show={this.state.showTaggedUsersModal}
            taggedUsers={this.state.users}
            onShowChange={this.displayModalStory.bind(this)}
          />
        ) : null}
        {this.state.showLinkModal ? (
          <StoryLinkModal
            show={this.state.showLinkModal}
            contentId={this.state.contentId}
            onShowChange={this.displayModalLink.bind(this)}
          />
        ) : null}
        {this.state.showReportModal ? (
          <ReportModal
            show={this.state.showReportModal}
            contentId={this.state.story.id}
            type="story"
            registeredUser={this.state.story.registeredUser}
            onShowChange={this.displayModalReport.bind(this)}
          />
        ) : null}
        <ModalBody
          className="story-modal-body"
          style={{ padding: "2px !important" }}
        >
          <Stories
            keyboardNavigation
            width="100%"
            height="670px"
            stories={stories}
            onAllStoriesEnd={(s, st) => this.toggle()}
            storyStyles={{ height: "670px" }}
          />
        </ModalBody>
      </Modal>
    );
  }

  timeSince(date) {
    var correctDate = new Date(Date.parse(date));
    var seconds = Math.floor(
      (new Date() - new Date(Date.parse(correctDate))) / 1000
    );

    var interval = seconds / 31536000;

    if (interval > 1) {
      return Math.floor(interval) + " years";
    }
    interval = seconds / 2592000;
    if (interval > 1) {
      return Math.floor(interval) + " months";
    }
    interval = seconds / 86400;
    if (interval > 1) {
      return Math.floor(interval) + " days";
    }
    interval = seconds / 3600;
    if (interval > 1) {
      return Math.floor(interval) + " hours";
    }
    interval = seconds / 60;
    if (interval > 1) {
      return Math.floor(interval) + " minutes";
    }
    return Math.floor(seconds) + " seconds";
  }

  convertHashtagsToString(hashtags) {
    debugger;
    var temp = [...hashtags];
    var text = temp[0];
    temp.shift();
    temp.forEach((element) => {
      text += " " + element;
    });
    return text;
  }

  createImagesList() {
    var images = [];
    this.props.stories.forEach((element) => {
      images.push(element.contentPath);
    });
    return images;
  }

  toggle() {
    debugger;
    this.setState({ showPostModal: false });
    this.props.onShowChange();
  }

  displayModalLink(contentId) {
    debugger;
    this.setState({
      contentId: contentId,
      showLinkModal: !this.state.showLinkModal,
    });
  }

  displayModalReport(story) {
    debugger;
    this.setState({
      story: story,
      showReportModal: !this.state.showReportModal,
    });
  }

  displayModalStory(users) {
    debugger;
    this.setState({
      showTaggedUsersModal: !this.state.showTaggedUsersModal,
      users: users,
    });
  }
}

const mapStateToProps = (state) => ({
  stories: state.storiesForModal,
  images: state.storyImages,
  ad: state.ad,
});

export default connect(mapStateToProps, {
  getStoriesForModal,
  loadImagesStory,
  getAdForContent,
})(StoryModal);
