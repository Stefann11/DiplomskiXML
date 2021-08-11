import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import {
  getCampaignRequests,
  updateCampaignRequest,
  createAd,
} from "../../actions/actionsCampaign";
import moment from "moment";
import NotLoggedPostModal from "../Profile/NotLoggedPostModal";
import StoryAlbumReportModal from "../Report/StoryAlbumReportModal";
import { savePost, getPost } from "../../actions/actions";
import { createNotification } from "../../actions/actionsNotification";
import {
  getStoryById,
  saveStory,
  getUserForStory,
} from "../../actions/actionsStory";

class CampaignRequests extends Component {
  state = {
    showPostModal: false,
    showStoryModal: false,
    postId: "",
    storyId: "",
    storyIds: [],
    username: "",
    storyOwner: {},
    campaignId: "",
  };

  async componentDidMount() {
    debugger;
    await this.props.getCampaignRequests();
    await this.props.getUserForStory();
    this.setState({
      storyOwner: this.props.storyOwner,
    });
  }

  render() {
    if (this.props.campaignRequests === undefined) {
      return null;
    }
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
          <StoryAlbumReportModal
            show={this.state.showStoryModal}
            storyIds={this.state.storyIds}
            onShowChange={() => this.displayModalStory()}
          />
        ) : null}
        <h3 className="mt-4" style={{ textAlign: "center" }}>
          Campaign Requests
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
                      <th style={{ textAlign: "center" }}>Agent</th>
                      <th style={{ textAlign: "center" }}>Type</th>
                      <th style={{ textAlign: "center" }}>Exposure Dates</th>
                      <th style={{ textAlign: "center" }}></th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.campaignRequests.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>{f.campaign.id}</td>
                        <td style={{ textAlign: "center" }}>
                          {f.campaign.agent.firstName +
                            " " +
                            f.campaign.agent.lastName}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.campaign.type}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          {f.campaign.exposureDates.map((date, i) =>
                            i === f.campaign.exposureDates.length - 1
                              ? moment(date.time).format("DD/MM/YYYY HH:mm")
                              : moment(date.time).format("DD/MM/YYYY HH:mm") +
                                ", "
                          )}
                        </td>
                        <td style={{ textAlign: "center" }}>
                          <img
                            onClick={() => {
                              this.accept(f);
                            }}
                            src="/images/checked.png"
                          />
                        </td>
                        <td style={{ textAlign: "center" }}>
                          <img
                            onClick={() => {
                              this.reject(f);
                            }}
                            src="/images/cancel.png"
                          />
                        </td>
                        <td style={{ textAlign: "center" }}>
                          <img
                            onClick={() => {
                              this.view(f);
                            }}
                            src="/images/analytics.png"
                          />
                         
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

  async accept(f) {
    await this.props.updateCampaignRequest({
      Id: f.id,
      IsApproved: true,
      Campaign: f.campaign,
      VerifiedUser: f.verifiedUser,
      CampaignRequestAction: "accepted",
    });
    this.setState({
      campaignId: f.campaign.id,
    });
    if (
      f.campaign.type === "OneTimePostCampaign" ||
      f.campaign.type === "RecurringPostCampaign"
    ) {
      await this.createPost(f.campaign.ads[0]);
    } else {
      for (var j = 0; j < f.campaign.ads.length; j++) {
        debugger;
        await this.createStory(f.campaign.ads[j]);
      }
    }
  }

  async createPost(ad) {
    await this.props.getPost(ad.contentId);
    debugger;
    var contentPaths = [];
    if (this.props.post.contentPaths === undefined) {
      contentPaths.push(this.props.post.contentPath);
    } else {
      contentPaths = this.props.post.contentPaths;
    }

    await this.props.savePost({
      Description: this.props.post.description,
      RegisteredUser: { id: sessionStorage.getItem("userId") },
      Location: this.props.post.location,
      ContentPaths: contentPaths,
      HashTags: this.props.post.hashTags,
      Taggedusers: this.props.post.taggedUsers,
    });
    var ad = {
      ContentId: this.props.post.id,
      Type: "Post",
      Link: ad.link,
      ClickCount: 0,
      ProfileOwnerId: sessionStorage.getItem("userId"),
    };
    await this.props.createAd({ Ad: ad, CampaignId: this.state.campaignId });
    await this.props.createNotification({
      Type: "Post",
      ContentId: this.props.post.id,
      RegisteredUser: { id: sessionStorage.getItem("userId") },
    });
  }

  async createStory(ad) {
    debugger;
    await this.props.getStoryById(ad.contentId);
    debugger;

    await this.props.saveStory({
      Description: this.props.storyById.description,
      RegisteredUser: this.state.storyOwner,
      Location: this.props.storyById.location,
      ContentPath: this.props.storyById.contentPath,
      HashTags: this.props.storyById.hashTags,
      TaggedUsers: this.props.storyById.taggedUsers,
      SeenByUsers: [],
      Duration: this.props.storyById.duration,
      Type: "Story",
      IsBanned: false,
    });
    debugger;
    var ad = {
      ContentId: this.props.story.id,
      Type: "Story",
      Link: ad.link,
      ClickCount: 0,
      ProfileOwnerId: sessionStorage.getItem("userId"),
    };
    await this.props.createAd({ Ad: ad, CampaignId: this.state.campaignId });
    debugger;
    await this.props.createNotification({
      Type: "Story",
      ContentId: "12345678-1234-1234-1234-123456789123",
      RegisteredUser: { id: sessionStorage.getItem("userId") },
    });
  }

  async reject(f) {
    await this.props.updateCampaignRequest({
      Id: f.id,
      IsApproved: false,
      Campaign: f.campaign,
      VerifiedUser: f.verifiedUser,
      CampaignRequestAction: "rejected",
    });
  }

  view = (f) => {
    debugger;
    if (f != undefined) {
      if (
        f.campaign.type === "OneTimePostCampaign" ||
        f.campaign.type === "RecurringPostCampaign"
      ) {
        this.displayModalPost(f);
      } else {
        this.displayModalStory(f);
      }
    }
  };

  displayModalPost = (f) => {
    if (f != undefined) {
      this.setState({
        postId: f.campaign.ads[0].contentId,
        username: f.campaign.agent.username,
      });
    }
    this.setState({
      showPostModal: !this.state.showPostModal,
    });
  };

  displayModalStory = (f) => {
    var storyIds = [];
    if (f != undefined) {
      for (var i = 0; i < f.campaign.ads.length; i++) {
        storyIds.push(f.campaign.ads[i].contentId);
      }
      this.setState({
        storyIds: storyIds,
      });
    }
    this.setState({
      showStoryModal: !this.state.showStoryModal,
    });
  };
}

const mapStateToProps = (state) => ({
  campaignRequests: state.campaignRequests,
  post: state.post,
  storyById: state.storyById,
  story: state.story,
  storyOwner: state.registeredUser,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getCampaignRequests,
    updateCampaignRequest,
    savePost,
    createNotification,
    getPost,
    saveStory,
    getStoryById,
    getUserForStory,
    createAd,
  })
)(CampaignRequests);
