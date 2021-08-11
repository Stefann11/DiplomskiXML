import React, { Component } from "react";
import {
  getUserNotificationSettings,
  editNotificationSettings,
} from "../../actions/actionsNotification";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";
import Switch from "react-switch";

class ChangeNotificationSettings extends Component {
  state = {
    isNotifiedByFollowRequests: false,
    isNotifiedByMessages: false,
    isNotifiedByPosts: false,
    isNotifiedByStories: false,
    isNotifiedByComments: false,
  };
  async componentDidMount() {
    debugger;
    await this.props.getUserNotificationSettings({
      loggedUserId: sessionStorage.getItem("userId"),
      notificationByUserId: sessionStorage.getItem("notificationByUserId"),
    });
    this.setState({
      isNotifiedByFollowRequests:
        this.props.userNotificationSettings.isNotifiedByFollowRequests,
      isNotifiedByMessages:
        this.props.userNotificationSettings.isNotifiedByMessages,
      isNotifiedByPosts: this.props.userNotificationSettings.isNotifiedByPosts,
      isNotifiedByStories:
        this.props.userNotificationSettings.isNotifiedByStories,
      isNotifiedByComments:
        this.props.userNotificationSettings.isNotifiedByComments,
    });
  }
  render() {
    if (this.props.userNotificationSettings === undefined) {
      return null;
    }

    const userNotificationSettings = this.props.userNotificationSettings;

    return (
      <div className="text-center">
        <h2>Change your notification settings for user </h2>
        <br />
        <label htmlFor="normal-switch" className="pt-4">
          <span>Notified By Follow Requests</span>
          <Switch
            onChange={this.handleChangeFollowRequests}
            checked={this.state.isNotifiedByFollowRequests}
            className="react-switch"
            id="normal-switch"
          />
        </label>
        <br />
        <label htmlFor="normal-switch" className="pt-4">
          <span>Notified By Messages</span>
          <Switch
            onChange={this.handleChangeMessages}
            checked={this.state.isNotifiedByMessages}
            className="react-switch"
            id="normal-switch"
          />
        </label>
        <br />
        <label htmlFor="normal-switch" className="pt-4">
          <span>Notified By Posts</span>
          <Switch
            onChange={this.handleChangePosts}
            checked={this.state.isNotifiedByPosts}
            className="react-switch"
            id="normal-switch"
          />
        </label>
        <br />
        <label htmlFor="normal-switch" className="pt-4">
          <span>Notified By Stories</span>
          <Switch
            onChange={this.handleChangeStories}
            checked={this.state.isNotifiedByStories}
            className="react-switch"
            id="normal-switch"
          />
        </label>
        <br />
        <label htmlFor="normal-switch" className="pt-4">
          <span>Notified By Comments</span>
          <Switch
            onChange={this.handleChangeComments}
            checked={this.state.isNotifiedByComments}
            className="react-switch"
            id="normal-switch"
          />
        </label>
        <br />
        <button
          className="btn btn-primary btn-block mt-4"
          onClick={() => {
            this.editNotificationSettings();
          }}
        >
          Change
        </button>
      </div>
    );
  }

  handleChangeFollowRequests = (checked) => {
    debugger;
    this.setState({ isNotifiedByFollowRequests: checked });
  };

  handleChangeMessages = (checked) => {
    debugger;
    this.setState({ isNotifiedByMessages: checked });
  };

  handleChangePosts = (checked) => {
    debugger;
    this.setState({ isNotifiedByPosts: checked });
  };

  handleChangeStories = (checked) => {
    debugger;
    this.setState({ isNotifiedByStories: checked });
  };

  handleChangeComments = (checked) => {
    debugger;
    this.setState({ isNotifiedByComments: checked });
  };

  async editNotificationSettings() {
    await this.props.editNotificationSettings({
      Id: this.props.userNotificationSettings.id,
      IsNotifiedByFollowRequests: this.state.isNotifiedByFollowRequests,
      IsNotifiedByMessages: this.state.isNotifiedByMessages,
      IsNotifiedByPosts: this.state.isNotifiedByPosts,
      IsNotifiedByStories: this.state.isNotifiedByStories,
      IsNotifiedByComments: this.state.isNotifiedByComments,
      LoggedUser: { id: sessionStorage.getItem("userId") },
      NotificationByUser: {
        id: sessionStorage.getItem("notificationByUserId"),
      },
    });
    window.location = "/profile/" + sessionStorage.getItem("userId");
  }
}

const mapStateToProps = (state) => ({
  userNotificationSettings: state.userNotificationSettings,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getUserNotificationSettings,
    editNotificationSettings,
  })
)(ChangeNotificationSettings);
