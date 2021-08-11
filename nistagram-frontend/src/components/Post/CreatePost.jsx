import React, { Component } from "react";
import {
  savePost,
  getLocationsByText,
  getTaggableUsers,
} from "../../actions/actions";
import { connect } from "react-redux";
import axios from "axios";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { createNotification } from "../../actions/actionsNotification";

class CreatePost extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    description: "",
    location: undefined,
    locations: [],
    hashTagText: "",
    contentPath: "",
    registeredUser: { id: sessionStorage.getItem("userId") },
    taggableUsers: [],
    taggedUsers: [],
    taggableUser: {},
    leftTaggableUsers: [],
    fileUrls: [],
    contentPaths: [],
  };

  async componentDidMount() {
    await this.props.getLocationsByText("");
    this.setState({
      locations: this.props.locations,
    });

    await this.props.getTaggableUsers();
    this.setState({
      taggableUsers: this.props.taggableUsers,
    });
  }

  render() {
    debugger;
    if (
      this.props.locations == undefined ||
      this.props.taggableUsers == undefined
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
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="tag">People in this photo:</label>
              <select className="form-control" onChange={this.handleChangeUser}>
                <option>Select user</option>
                {this.state.leftTaggableUsers.map((option, index) => (
                  <option key={index} value={index}>
                    {option.firstName +
                      " " +
                      option.lastName +
                      "(" +
                      option.username +
                      ")"}
                  </option>
                ))}
              </select>
              <button
                className="btn btn-primary "
                onClick={() => {
                  this.addTagged();
                }}
              >
                Add
              </button>
            </div>
          </div>
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
            disabled={
              this.state.contentPaths.length === 0 ||
              this.state.location === undefined
            }
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
    const res = this.state.hashTagText.split(" ");
    var hashTagsObjects = [];
    res.forEach((element) => hashTagsObjects.push({ HashTagText: element }));
    await this.props.savePost({
      Description: this.state.description,
      RegisteredUser: { id: this.state.registeredUser.id },
      Location: this.state.location,
      ContentPaths: this.state.contentPaths,
      HashTags: hashTagsObjects,
      Taggedusers: this.state.taggedUsers,
    });
    await this.props.createNotification({
      Type: "Post",
      ContentId: this.props.post.id,
      RegisteredUser: { id: this.state.registeredUser.id },
    });
    window.location = "/profile/" + sessionStorage.getItem("userId");
  }

  handleChangeUser = (e) => {
    this.setState({
      taggableUser: this.state.leftTaggableUsers[e.target.value],
    });
  };

  handleChangeLocation = (e) => {
    this.setState({ location: this.state.locations[e.target.value] });
  };

  choosePost = async (event) => {
    var result = [];
    var files = event.target.files;
    debugger;
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
          url: "https://localhost:44355/api/posts/contents",
          data: formData,
          headers: {
            "Content-Type": "multipart/form-data",
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("token"),
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
  post: state.post,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    savePost,
    getLocationsByText,
    getTaggableUsers,
    createNotification,
  })
)(CreatePost);
