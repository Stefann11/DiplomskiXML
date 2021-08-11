import React, { Component } from "react";
import { getReports, editReport } from "../../actions/actionsReport";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import NotLoggedPostModal from "../Profile/NotLoggedPostModal";
import StoryReportModal from "./StoryReportModal";
import moment from "moment";
import { banUser } from "../../actions/actionsUser";
import { banPost } from "../../actions/actions";
import { banStory } from "../../actions/actionsStory";

class Reports extends Component {
  state = {
    showPostModal: false,
    showStoryModal: false,
    postId: "",
    storyId: "",
    username: "",
  };

  componentDidMount() {
    debugger;
    this.props.getReports();
  }

  render() {
    debugger;
    if (this.props.reports === undefined) {
      return null;
    }
    const reports = this.props.reports;
    debugger;
    return (
      <div>
        {this.state.showPostModal ? (
          <NotLoggedPostModal
            show={this.state.showPostModal}
            postId={this.state.postId}
            personPhoto="/images/download.jfif"
            person={this.state.username}
            onShowChange={() => this.displayModalPost()}
          />
        ) : null}
        {this.state.showStoryModal ? (
          <StoryReportModal
            show={this.state.showStoryModal}
            storyId={this.state.storyId}
            onShowChange={() => this.displayModalStory()}
          />
        ) : null}
        <div className="wrap bg-white pt-3 pb-3" style={{ height: "100vh" }}>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
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
                    <th style={{ textAlign: "center" }}>Time Stamp</th>
                    <th style={{ textAlign: "center" }}>Report Reason</th>
                    <th style={{ textAlign: "center" }}>Registered User</th>
                    <th style={{ textAlign: "center" }}>View</th>
                    <th style={{ textAlign: "center" }}>Remove Content</th>
                    <th style={{ textAlign: "center" }}>Remove User</th>
                    <th style={{ textAlign: "center" }}>Skip</th>
                  </tr>
                </thead>
                <tbody>
                  {reports.map((f) => (
                    <tr>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {moment(f.timeStamp).format("DD/MM/YYYY HH:mm")}
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {f.reportReason}
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        {f.registeredUser.username}
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-3"
                          onClick={() => {
                            this.view(f);
                          }}
                        >
                          <img src="/images/view.png" />
                        </button>
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-3"
                          onClick={() => {
                            this.removeContent(f);
                          }}
                        >
                          <img
                            style={{ width: 24, height: 24 }}
                            src="/images/ban.png"
                          />
                        </button>
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-3"
                          onClick={() => {
                            this.removeUser(f);
                          }}
                        >
                          <img
                            style={{ width: 24, height: 24 }}
                            src="/images/ban.png"
                          />
                        </button>
                      </td>
                      <td
                        className="pl-4 pt-4 pb-4"
                        style={{ textAlign: "center" }}
                      >
                        <button
                          className="btn btn-primary mb-3"
                          onClick={() => {
                            this.skip(f);
                          }}
                        >
                          <img
                            style={{ width: 24, height: 24 }}
                            src="/images/minus.png"
                          />
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            </Card>
          </div>
        </div>
      </div>
    );
  }

  view = (f) => {
    if (f != undefined) {
      if (f.type === "Post" || f.type === "post") {
        this.displayModalPost(f);
      } else {
        this.displayModalStory(f);
      }
    }
  };

  displayModalPost = (f) => {
    if (f != undefined) {
      this.setState({
        postId: f.content.id,
        username: f.registeredUser.username,
      });
    }
    this.setState({
      showPostModal: !this.state.showPostModal,
    });
  };

  displayModalStory = (f) => {
    if (f != undefined) {
      this.setState({
        storyId: f.content.id,
      });
    }
    this.setState({
      showStoryModal: !this.state.showStoryModal,
    });
  };

  async removeUser(f) {
    await this.props.banUser(f.registeredUser.id);
    await this.props.editReport({
      Id: f.id,
      TimeStamp: f.timeStamp,
      ReportReason: f.reportReason,
      RegisteredUser: {
        id: f.registeredUser.id,
        username: f.registeredUser.username,
      },
      Content: { id: f.content.id },
      Type: f.type,
      ReportAction: "Ban",
    });
  }

  async skip(f) {
    await this.props.editReport({
      Id: f.id,
      TimeStamp: f.timeStamp,
      ReportReason: f.reportReason,
      RegisteredUser: {
        id: f.registeredUser.id,
        username: f.registeredUser.username,
      },
      Content: { id: f.content.id },
      Type: f.type,
      ReportAction: "NotBanned",
    });
  }

  async removeContent(f) {
    if (f != undefined) {
      if (f.type === "Post" || f.type === "post") {
        this.removePost(f);
      } else {
        this.removeStory(f);
      }
    }
  }

  async removePost(f) {
    await this.props.banPost(f.content.id);
    await this.props.editReport({
      Id: f.id,
      TimeStamp: f.timeStamp,
      ReportReason: f.reportReason,
      RegisteredUser: {
        id: f.registeredUser.id,
        username: f.registeredUser.username,
      },
      Content: { id: f.content.id },
      Type: f.type,
      ReportAction: "Ban",
    });
  }

  async removeStory(f) {
    await this.props.banStory(f.content.id);
    await this.props.editReport({
      Id: f.id,
      TimeStamp: f.timeStamp,
      ReportReason: f.reportReason,
      RegisteredUser: {
        id: f.registeredUser.id,
        username: f.registeredUser.username,
      },
      Content: { id: f.content.id },
      Type: f.type,
      ReportAction: "Ban",
    });
  }
}

const mapStateToProps = (state) => ({ reports: state.reports });

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getReports,
    editReport,
    banUser,
    banPost,
    banStory,
  })
)(Reports);
