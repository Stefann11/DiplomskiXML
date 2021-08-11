import React, { Component } from "react";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import {
  getFollowing,
  getFollowingWithoutMuted,
} from "../../actions/actionsUser";
import {
  getNotificationsForFollowing,
  getUserNotificationSettings,
} from "../../actions/actionsNotification";
import {
  NotificationContainer,
  NotificationManager,
} from "react-notifications";
import "react-notifications/lib/notifications.css";
import moment from "moment";

class Notifications extends Component {
  state = {};
  async componentDidMount() {
    debugger;
    await this.props.getFollowingWithoutMuted();
    await this.props.getUserNotificationSettings();
    await this.props.getNotificationsForFollowing({
      LoggedUser: { id: sessionStorage.getItem("userId") },
      RegisteredUsers: this.props.following,
    });
    debugger;
    var notificationsForFollowing = [...this.props.notificationsForFollowing];
    notificationsForFollowing.sort(function compare(a, b) {
      var dateA = new Date(a.timeStamp);
      var dateB = new Date(b.timeStamp);
      return dateA - dateB;
    });
    notificationsForFollowing.forEach((element) =>
      NotificationManager.success(
        element.type === "Post"
          ? element.registeredUser.username + " Has Created New Post!"
          : element.type === "Story"
          ? element.registeredUser.username + " Has Created New Story!"
          : element.registeredUser.username + " Has Commented On Post!",
        element.type
      )
    );
  }
  render() {
    if (this.props.following === undefined) {
      return null;
    }

    if (this.props.notificationsForFollowing === undefined) {
      return null;
    }

    var notificationsForFollowing = [...this.props.notificationsForFollowing];
    notificationsForFollowing.sort(function compare(a, b) {
      var dateA = new Date(a.timeStamp);
      var dateB = new Date(b.timeStamp);
      return dateA - dateB;
    });

    debugger;
    return (
      <div>
        <div className="wrap bg-white pt-3 pb-3" style={{ height: "100vh" }}>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <h2>Notifications</h2>
            <Card
              className="mt-5"
              style={{
                shadowColor: "gray",
                boxShadow: "0 8px 6px -6px #999",
              }}
            >
              <Table className="table allPrescriptions mb-0" striped>
                <thead>
                  <tr>
                    <th style={{ textAlign: "center" }}>Activity</th>
                  </tr>
                </thead>
                <tbody>
                  {notificationsForFollowing.map((f) => (
                    <tr>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {f.registeredUser.username + " "}
                        {f.type === "Post"
                          ? " Has Created New Post!"
                          : f.type === "Story"
                          ? " Has Created New Story!"
                          : f.type === "Comment"
                          ? " Has Commented On Post!"
                          : ""}
                      </td>
                      <td>{moment(f.timeStamp).format("DD/MM/YYYY HH:mm")}</td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            </Card>
          </div>
          <NotificationContainer />
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => ({
  following: state.following,
  userNotificationSettings: state.userNotificationSettings,
  notificationsForFollowing: state.notificationsForFollowing,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getFollowingWithoutMuted,
    getFollowing,
    getUserNotificationSettings,
    getNotificationsForFollowing,
  })
)(Notifications);
