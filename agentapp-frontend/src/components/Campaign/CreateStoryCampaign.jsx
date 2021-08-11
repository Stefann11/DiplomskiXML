import React, { Component } from "react";
import {
  saveStory,
  getLocationsForStory,
  getTaggableForStory,
  getUserForStory,
} from "../../actions/actionsStory";
import { connect } from "react-redux";
import axios from "axios";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { createNotification } from "../../actions/actionsNotification";
import { createCampaign } from "../../actions/actionsCampaign";

class CreateStoryCampaign extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    description: "",
    location: {},
    locations: [],
    hashTagText: "",
    contentPath: "",
    registeredUser: { id: sessionStorage.getItem("savedUserId") },
    taggableUsers: [],
    taggedUsers: [],
    taggableUser: {},
    leftTaggableUsers: [],
    fileUrls: [],
    contentPaths: [],
    storyOwner: {},
    duration: 1,
    isCloseFriendsStory: false,
    link: "",
    campaignType: "",
  };

  async componentDidMount() {
    await this.props.getLocationsForStory();
    this.setState({
      locations: this.props.locations,
    });

    await this.props.getTaggableForStory();
    this.setState({
      taggableUsers: this.props.taggableUsers,
    });

    await this.props.getUserForStory();

    this.createType();
  }

  render() {
    debugger;
    if (
      this.props.locations === undefined ||
      this.props.taggableUsers === undefined ||
      this.props.storyOwner === undefined
    ) {
      return null;
    }
    debugger;
    if (this.state.leftTaggableUsers.length == 0) {
      this.setState({
        leftTaggableUsers: this.props.taggableUsers,
      });
    }
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <input type="file" onChange={this.choosePost} multiple />
              {this.state.fileType === "video/mp4" ? (
                <video width="500" height="300" controls>
                  <source src={this.state.fileUrl} type="video/mp4" />
                </video>
              ) : (
                <img
                  src={this.state.fileUrl}
                  style={{ width: 500, height: 300 }}
                />
              )}
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label className="label">Description:</label>
              <textarea
                name="description"
                value={this.state.description}
                onChange={this.handleChange}
                cols="40"
                rows="5"
                class="form-control"
                placeholder="Enter description"
              ></textarea>
              <br></br>
              <div>
                <label className="label">Link:</label>
                <input
                  name="link"
                  value={this.state.link}
                  onChange={this.handleChange}
                  cols="40"
                  rows="5"
                  class="form-control"
                  placeholder="Enter link"
                ></input>
              </div>
              <br></br>
              <div>
                <div class="form-group w-100 pr-5">
                  <label for="location">Duration:</label>
                  <select
                    className="form-control"
                    onChange={this.handleChangeDuration}
                  >
                    <option>Select duration</option>
                    <option value={1}>1s</option>
                    <option value={2}>2s</option>
                    <option value={3}>3s</option>
                    <option value={4}>4s</option>
                    <option value={5}>5s</option>
                    <option value={6}>6s</option>
                    <option value={7}>7s</option>
                    <option value={8}>8s</option>
                    <option value={9}>9s</option>
                    <option value={10}>10s</option>
                    <option value={11}>11s</option>
                    <option value={12}>12s</option>
                    <option value={13}>13s</option>
                    <option value={14}>14s</option>
                    <option value={15}>15s</option>
                  </select>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="location">Location:</label>
              <select
                className="form-control"
                onChange={this.handleChangeLocation}
              >
                <option>Select location</option>
                {this.state.locations.map((option, index) => (
                  <option key={index} value={index}>
                    {option.street +
                      ", " +
                      option.cityName +
                      ", " +
                      option.country}
                  </option>
                ))}
              </select>
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label className="label">Hash Tags:</label>
              <textarea
                name="hashTagText"
                value={this.state.hashTagText}
                onChange={this.handleChange}
                cols="40"
                rows="5"
                class="form-control"
                placeholder="Enter hash tags, seperated by space"
              ></textarea>
            </div>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            disabled={this.state.contentPaths.length === 0}
            className="btn btn-primary btn-block"
            onClick={() => {
              this.createPost();
            }}
          >
            Post
          </button>
        </div>
      </React.Fragment>
    );
  }

  addTagged() {
    var listLeft = this.state.leftTaggableUsers;
    var listTagged = this.state.taggedUsers;
    const taggedUser = this.state.taggableUser;
    listTagged.push(this.state.taggableUser);
    debugger;
    const listLeft2 = listLeft.filter(function (el) {
      return el.id != taggedUser.id;
    });
    this.setState({
      leftTaggableUsers: listLeft2,
      taggedUsers: listTagged,
    });
  }

  async createPost() {
    const hashTagsObjects = this.state.hashTagText.split(" ");
    var stories = [];
    for (var i = 0; i < this.state.contentPaths.length; i++) {
      debugger;
      await this.props.saveStory({
        Description: this.state.description,
        RegisteredUser: this.props.storyOwner,
        Location: this.state.location,
        ContentPath: this.state.contentPaths[i],
        HashTags: hashTagsObjects,
        TaggedUsers: this.state.taggedUsers,
        SeenByUsers: [],
        Duration: this.state.duration,
        Type:
          this.state.isCloseFriendsStory === true
            ? "CloseFriendStory"
            : "Story",
        IsBanned: false,
      });
      debugger;
      stories.push(this.props.story);
    }
    debugger;
    let ads = [];
    stories.forEach((story) => {
      ads.push({
        ContentId: story.id,
        Type: this.props.type,
        Link: this.state.link,
        ClickCount: 0,
        ProfileOwnerId: sessionStorage.getItem("savedUserId"),
      });
    });
    debugger;
    await this.props.createCampaign(
      this.props.startDate === "" && this.props.endDate === ""
        ? {
            Type: this.state.campaignType,
            TargetAudience: {
              MinDateOfBirth: this.props.minDateOfBirth,
              MaxDateOfBirth: this.props.maxDateOfBirth,
              Gender: this.props.gender,
            },
            AgentId: sessionStorage.getItem("savedUserId"),
            ExposureDates: this.props.exposureDates,
            Ads: ads,
          }
        : {
            Type: this.state.campaignType,
            TargetAudience: {
              MinDateOfBirth: this.props.minDateOfBirth,
              MaxDateOfBirth: this.props.maxDateOfBirth,
              Gender: this.props.gender,
            },
            AgentId: sessionStorage.getItem("savedUserId"),
            StartDate: this.props.startDate,
            EndDate: this.props.endDate,
            ExposureDates: this.props.exposureDates,
            Ads: ads,
          }
    );

    window.location = "/";
  }

  createType() {
    if (this.props.recurrence === "oneTimeCampaign") {
      if (this.props.type === "Post")
        this.setState({ campaignType: "OneTimePostCampaign" });
      else this.setState({ campaignType: "OneTimeStoryCampaign" });
    } else {
      if (this.props.type === "Post")
        this.setState({ campaignType: "RecurringPostCampaign" });
      else this.setState({ campaignType: "RecurringStoryCampaign" });
    }
  }

  handleChangeCheckboxCloseFriendsStory = (e) => {
    debugger;
    this.setState({
      isCloseFriendsStory: !this.state.isCloseFriendsStory,
    });
  };

  handleChangeUser = (e) => {
    this.setState({
      taggableUser: this.state.leftTaggableUsers[e.target.value],
    });
  };

  handleChangeLocation = (e) => {
    this.setState({ location: this.state.locations[e.target.value] });
  };

  handleChangeDuration = (e) => {
    debugger;
    this.setState({ duration: e.target.value });
  };

  choosePost = async (event) => {
    debugger;
    var result = [];
    var files = event.target.files;
    Array.prototype.forEach.call(files, (file) => {
      result.push(URL.createObjectURL(file));
    });
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
      fileUrls: result,
      fileType: files[0].type,
    });

    var contentPaths = [];
    var i;

    for (i = 0; i < files.length; i++) {
      debugger;
      const formData = new FormData();

      formData.append("formFile", files[i]);
      formData.append("fileName", files[i].name);

      var dummyThis = this;
      try {
        const res = await axios({
          method: "post",
          url: "https://localhost:44355/api/contents",
          data: formData,
          headers: {
            "Content-Type": "multipart/form-data",
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
          },
        })
          .then(function (response) {
            var contentPath = { ...dummyThis.state.contentPath };
            contentPath = response.data;
            dummyThis.setState({ contentPath });
          })
          .catch(function (response) {
            console.log(response);
          });
      } catch (ex) {
        console.log(ex);
      }
      contentPaths.push(this.state.contentPath);
      this.setState({
        contentPaths: contentPaths,
      });
    }
  };

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };
}

const mapStateToProps = (state) => ({
  locations: state.locations,
  taggableUsers: state.taggableUsers,
  storyOwner: state.registeredUser,
  story: state.story,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    saveStory,
    getLocationsForStory,
    getTaggableForStory,
    getUserForStory,
    createNotification,
    createCampaign,
  })
)(CreateStoryCampaign);
