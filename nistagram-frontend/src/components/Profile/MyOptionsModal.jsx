import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { connect } from "react-redux";
import { loadImagesForArchive } from "../../actions/actionsStory";
import ProfileStoryModal from "./ProfileStoryModal";

class MyOptionsModal extends Component {
  state = {
    showOptionsModal: this.props.show,
    showProfileStoryModal: false,
    user: "",
  };

  async componentDidMount() {
    this.props.loadImagesForArchive(
      this.createImagesList(this.props.allStories)
    );
  }

  render() {
    debugger;
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showOptionsModal}
        centered={true}
      >
        {this.state.showProfileStoryModal ? (
          <ProfileStoryModal
            show={this.state.showProfileStoryModal}
            onShowChange={this.displayModalProfileStory.bind(this)}
            user={this.state.user}
            profileImage={this.props.profileImage}
            isActiveStories={true}
            isArchive={true}
            highlights={this.props.highlights}
            stories={this.props.allStories}
          />
        ) : null}
        <ModalHeader toggle={this.toggle.bind(this)}></ModalHeader>
        <ModalBody>
          <button
            onClick={() => this.collections()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Collections</label>
          </button>
          <hr />
          <button
            disabled={this.props.allStories.length === 0}
            onClick={() =>
              this.displayModalProfileStory(sessionStorage.getItem("userId"))
            }
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Archive</label>
          </button>
          <hr />
          <button
            onClick={() => this.edit()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Change profile picture</label>
          </button>
          <hr />
          <button
            onClick={() => this.likedPosts()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Liked posts</label>
          </button>
          <hr />
          <button
            onClick={() => this.dislikedPosts()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Disliked posts</label>
          </button>

          {sessionStorage.getItem("role") === "RegisteredUser" ? (
            <React.Fragment>
              {" "}
              <hr />
              <button
                style={{
                  height: "100%",
                  width: "100%",
                  alignSelf: "stretch",
                  float: "center",
                }}
                className="btn btn-block btn-primary btn-md mb-2"
                onClick={() => {
                  window.location = "/sendVerificationRequest";
                }}
              >
                <label>Send verification request</label>
              </button>
            </React.Fragment>
          ) : null}

          {sessionStorage.getItem("role") !== "Agent" ? (
            <React.Fragment>
              <hr />
              <button
                style={{
                  height: "100%",
                  width: "100%",
                  alignSelf: "stretch",
                  float: "center",
                }}
                className="btn btn-block btn-primary btn-md mb-2"
                onClick={() => {
                  window.location = "/agent-registration";
                }}
              >
                <label>Send agent request</label>
              </button>
            </React.Fragment>
          ) : null}
          {sessionStorage.getItem("role") === "VerifiedUser" ? (
            <React.Fragment>
              <hr />
              <button
                style={{
                  height: "100%",
                  width: "100%",
                  alignSelf: "stretch",
                  float: "center",
                }}
                className="btn btn-block btn-primary btn-md mb-2"
                onClick={() => {
                  window.location = "/campaign-requests";
                }}
              >
                <label>Campaign requests</label>
              </button>
            </React.Fragment>
          ) : null}
          {sessionStorage.getItem("role") === "Agent" ? (
            <React.Fragment>
              <hr />
              <button
                style={{
                  height: "100%",
                  width: "100%",
                  alignSelf: "stretch",
                  float: "center",
                }}
                className="btn btn-block btn-primary btn-md mb-2"
                onClick={() => {
                  window.location = "/edit-campaign";
                }}
              >
                <label>My campaigns</label>
              </button>
            </React.Fragment>
          ) : null}
        </ModalBody>
      </Modal>
    );
  }

  collections() {
    this.props.history.replace({
      pathname: "/collections",
    });
  }

  edit() {
    this.props.history.replace({
      pathname: "/change-profile-picture",
    });
  }

  likedPosts() {
    this.props.history.replace({
      pathname: "/liked",
    });
  }

  dislikedPosts() {
    this.props.history.replace({
      pathname: "/disliked",
    });
  }

  mute() {
    alert("mute");
  }

  toggle() {
    debugger;
    this.setState({ showOptionsModal: false });
    this.props.onShowChange();
  }

  createImagesList() {
    debugger;
    var images = [];
    this.props.allStories.forEach((element) => {
      images.push(element.contentPath);
    });
    return images;
  }

  displayModalProfileStory(userid) {
    this.setState({
      showProfileStoryModal: !this.state.showProfileStoryModal,
      user: userid,
    });
  }
}

const mapStateToProps = (state) => ({
  allStories: state.allStories,
});

export default compose(
  withRouter,
  connect(mapStateToProps, { loadImagesForArchive })
)(MyOptionsModal);
