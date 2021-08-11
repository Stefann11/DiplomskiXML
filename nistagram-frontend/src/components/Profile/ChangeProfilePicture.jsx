import React, { Component } from "react";
import { connect } from "react-redux";
import axios from "axios";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { changeProfilePictureUsermicroservice } from "../../actions/actionsUser";
import { changeProfilePicturePostmicroservice } from "../../actions/actions";

class ChangeProfilePicture extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    description: "",
    location: {},
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

  render() {
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <input type="file" onChange={this.choosePost} />
              <img
                src={this.state.fileUrl}
                style={{ width: 500, height: 300 }}
              />
            </div>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            disabled={this.state.contentPath.length === 0}
            className="btn btn-primary btn-block"
            onClick={() => {
              this.createPost();
            }}
          >
            Change profile picture
          </button>
        </div>
      </React.Fragment>
    );
  }

  async createPost() {
    debugger;
    await this.props.changeProfilePictureUsermicroservice(
      this.state.contentPath
    );
    await this.props.changeProfilePicturePostmicroservice(
      this.state.contentPath
    );
    this.props.history.replace({
      pathname: "/profile/" + sessionStorage.getItem("userId"),
    });
  }

  choosePost = async (event) => {
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
    });
    const formData = new FormData();

    formData.append("formFile", event.target.files[0]);
    formData.append("fileName", event.target.files[0].name);

    var dummyThis = this;
    try {
      const res = await axios({
        method: "post",
        url: "https://localhost:44355/api/users/contents",
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
          console.log(response);
        })
        .catch(function (response) {
          console.log(response);
        });
      console.log(res);
    } catch (ex) {
      console.log(ex);
    }

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
          console.log(response);
        })
        .catch(function (response) {
          console.log(response);
        });
      console.log(res);
    } catch (ex) {
      console.log(ex);
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
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    changeProfilePictureUsermicroservice,
    changeProfilePicturePostmicroservice,
  })
)(ChangeProfilePicture);
