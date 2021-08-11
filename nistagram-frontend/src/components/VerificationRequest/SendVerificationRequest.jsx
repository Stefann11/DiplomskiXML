import React, { Component } from "react";
import { sendVerificationRequest } from "../../actions/actionsUser";
import { connect } from "react-redux";
import axios from "axios";
import { withRouter } from "react-router-dom";
import { compose } from "redux";

class SendVerificationRequest extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    registeredUser: { id: sessionStorage.getItem("userId") },
    fileUrls: [],
    contentPaths: [],
    firstName: "",
    lastName: "",
    category: "",
  };

  render() {
    debugger;
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <input type="file" onChange={this.choosePost} />
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
              <label className="label">First name:</label>
              <input
                name="firstName"
                value={this.state.firstName}
                onChange={this.handleChange}
                cols="40"
                rows="5"
                class="form-control"
                placeholder="Enter real first name"
              ></input>
              <br></br>
              <div>
                <label className="label">Last name:</label>
                <input
                  name="lastName"
                  value={this.state.lastName}
                  onChange={this.handleChange}
                  cols="40"
                  rows="5"
                  class="form-control"
                  placeholder="Enter real last name"
                ></input>
              </div>
              <br></br>
              <div>
                <label for="location">Category:</label>
                <select
                  className="form-control"
                  onChange={this.handleChangeCategory}
                >
                  <option>Select category</option>
                  <option value="Influencer">Influencer</option>
                  <option value="Sports">Sports</option>
                  <option value="News">News/Media</option>
                  <option value="Brand">Brand</option>
                  <option value="Organization">Organization</option>
                </select>
              </div>
            </div>
            <br></br>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            disabled={
              this.state.contentPaths.length === 0 ||
              this.state.category === "" ||
              this.state.firstName === "" ||
              this.state.lastName === ""
            }
            className="btn btn-primary btn-block"
            onClick={() => {
              this.sendVerificationRequst();
            }}
          >
            Send Verification request
          </button>
        </div>
      </React.Fragment>
    );
  }

  async sendVerificationRequst() {
    debugger;
    await this.props.sendVerificationRequest({
      RegisteredUserId: sessionStorage.getItem("userId"),
      FirstName: this.state.firstName,
      LastName: this.state.lastName,
      Category: this.state.category,
      DocumentImagePath: this.state.contentPaths[0],
    });
    window.location = "/";
  }

  handleChangeCategory = (e) => {
    this.setState({ category: e.target.value });
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

const mapStateToProps = (state) => ({});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    sendVerificationRequest,
  })
)(SendVerificationRequest);
