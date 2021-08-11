import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import {
  getCampaignsForAgent,
  deleteCampaign,
} from "../../actions/actionsCampaign";
import moment from "moment";
import NotLoggedPostModal from "../Profile/NotLoggedPostModal";
import StoryAlbumReportModal from "../Report/StoryAlbumReportModal";
import EditCampaignModal from "./EditCampaignModal";
import { savePost, getPost } from "../../actions/actions";
import { createNotification } from "../../actions/actionsNotification";
import {
  getStoryById,
  saveStory,
  getUserForStory,
} from "../../actions/actionsStory";

class EditCampaign extends Component {
  state = {
    showEditModal: false,
    campaignId: "",
    agentId: "",
    type: "",
    startDate: "",
    endDate: "",
    dateOfChange: "",
    minDateOfBirth: "",
    maxDateOfBirth: "",
    gender: "",
  };

  async componentDidMount() {
    debugger;
    await this.props.getCampaignsForAgent();
    //await this.props.getUserForStory();
  }

  render() {
    if (this.props.campaignsForAgent === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        {this.state.showEditModal ? (
          <EditCampaignModal
            show={this.state.showEditModal}
            campaignId={this.state.campaignId}
            agentId={this.state.agentId}
            type={this.state.type}
            startDate={this.state.startDate}
            endDate={this.state.endDate}
            dateOfChange={this.state.dateOfChange}
            minDateOfBirth={this.state.minDateOfBirth}
            maxDateOfBirth={this.state.maxDateOfBirth}
            gender={this.state.gender}
            onShowChange={() => this.displayModalEdit()}
          />
        ) : null}
        <h3 className="mt-4" style={{ textAlign: "center" }}>
          Campaigns
        </h3>
        <hr />
        <div className="wrap bg-white pt-3">
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <Card
              className="mt-5"
              style={{
                shadowColor: "gray",
                boxShadow: "0 8px 6px -6px #999",
              }}
            >
              <div
                style={{
                  maxHeight: "440px",
                  overflowY: "auto",
                }}
              >
                <Table className="table allPrescriptions mb-0" striped>
                  <thead>
                    <tr>
                      <th style={{ textAlign: "center" }}>Campaign</th>
                      <th style={{ textAlign: "center" }}>Type</th>
                      <th style={{ textAlign: "center" }}>Gender</th>
                      <th style={{ textAlign: "center" }}>Min Age</th>
                      <th style={{ textAlign: "center" }}>Max Age</th>
                      <th style={{ textAlign: "center" }}></th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.campaignsForAgent.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>{f.id}</td>
                        <td style={{ textAlign: "center" }}>{f.type}</td>
                        <td style={{ textAlign: "center" }}>
                          {f.targetAudience.gender}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {moment(f.targetAudience.minDateOfBirth).format(
                            "DD/MM/YYYY"
                          )}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {moment(f.targetAudience.maxDateOfBirth).format(
                            "DD/MM/YYYY"
                          )}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          <button
                            className="btn btn-primary"
                            onClick={() => {
                              this.displayModalEdit(f);
                            }}
                          >
                            Edit
                          </button>
                        </td>
                        <td style={{ textAlign: "center" }}>
                          <button
                            className="btn btn-primary"
                            onClick={() => {
                              this.deleteCampaign(f);
                            }}
                          >
                            Delete
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </div>
            </Card>
          </div>
        </div>
      </div>
    );
  }

  deleteCampaign = async (f) => {
    await this.props.deleteCampaign(f);
    window.location = "edit-campaign";
  };

  displayModalEdit = (f) => {
    if (f != undefined) {
      this.setState({
        campaignId: f.id,
        agentId: f.agentId,
        type: f.type,
        startDate: f.startDate,
        endDate: f.endDate,
        dateOfChange: f.dateOfChange,
        minDateOfBirth: f.targetAudience.minDateOfBirth,
        maxDateOfBirth: f.targetAudience.maxDateOfBirth,
        gender: f.targetAudience.gender,
      });
    }
    this.setState({
      showEditModal: !this.state.showEditModal,
    });
  };
}

const mapStateToProps = (state) => ({
  campaignsForAgent: state.campaignsForAgent,
  post: state.post,
  storyById: state.storyById,
  story: state.story,
  storyOwner: state.registeredUser,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getCampaignsForAgent,
    savePost,
    createNotification,
    getPost,
    saveStory,
    getStoryById,
    getUserForStory,
    deleteCampaign,
  })
)(EditCampaign);
