import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { addCloseFriendStory } from "../../actions/actionsStory";
import {
  addCloseFriend,
  muteProfile,
  blockProfile,
} from "../../actions/actionsUser";

class OptionsModal extends Component {
  state = {
    showOptionsModal: this.props.show,
  };

  render() {
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showOptionsModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}></ModalHeader>
        <ModalBody>
          <button
            onClick={() => this.block()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Block this user</label>
          </button>
          <hr />
          <button
            onClick={() => this.mute()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Mute this user</label>
          </button>
          <hr />
          <button
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Report this user</label>
          </button>
          <hr />
          <button
            onClick={() => this.changeNotificationSettings()}
            style={{
              height: "100%",
              width: "100%",
              alignSelf: "stretch",
              float: "center",
            }}
            className="btn btn-block btn-primary btn-md mb-2"
          >
            <label>Change notification settings for this user</label>
          </button>
          <hr />
          {this.displayCloseFriendButton()}
        </ModalBody>
      </Modal>
    );
  }

  displayCloseFriendButton() {
    var display = true;
    var follower = false;
    debugger;
    for (var i = 0; i < this.props.user.closeFriendTo.length; i++) {
      if (
        this.props.user.closeFriendTo[i].id === sessionStorage.getItem("userId")
      ) {
        display = false;
        break;
      }
    }
    for (var i = 0; i < this.props.user.followers.length; i++) {
      if (
        this.props.user.followers[i].id === sessionStorage.getItem("userId")
      ) {
        follower = true;
        break;
      }
    }
    if (display && follower) {
      return (
        <button
          style={{
            height: "100%",
            width: "100%",
            alignSelf: "stretch",
            float: "center",
          }}
          className="btn btn-block btn-md mb-2"
          onClick={() => this.addCloseFriend()}
        >
          Add to Close friends
        </button>
      );
    } else {
      return "";
    }
  }

  async addCloseFriend() {
    await this.props.addCloseFriendStory(this.props.user.id);
    await this.props.addCloseFriend(this.props.user.id);
    debugger;
    window.location = "/profile/" + this.props.user.id;
  }

  async mute() {
    await this.props.muteProfile({
      MutedById: sessionStorage.getItem("userId"),
      MutingId: this.props.user.id,
    });
    window.location = "/profile/" + this.props.user.id;
  }

  async block() {
    await this.props.blockProfile({
      BlockedById: sessionStorage.getItem("userId"),
      BlockingId: this.props.user.id,
    });
    window.location = "/profile/" + this.props.user.id;
  }

  changeNotificationSettings() {
    sessionStorage.setItem("notificationByUserId", this.props.user.id);
    window.location = "/change-notification-settings";
  }

  toggle() {
    debugger;
    this.setState({ showOptionsModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = () => {};

export default connect(mapStateToProps, {
  addCloseFriendStory,
  addCloseFriend,
  muteProfile,
  blockProfile,
})(OptionsModal);
