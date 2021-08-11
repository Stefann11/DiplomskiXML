import React, { Component } from "react";
import DatePicker from "react-datepicker";
import {
  editUser,
  editUserForPost,
  getLoggedUser,
  editUserForStory,
} from "../../actions/actionsUser";
import { connect } from "react-redux";
import { toast } from "react-toastify";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import "react-toastify/dist/ReactToastify.css";
import "react-datepicker/dist/react-datepicker.css";

class EditProfile extends Component {
  state = {
    id: 1,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    dateOfBirth: "",
    gender: "",
    username: "",
    bio: "",
    webSite: "",
    profilePicturePath: "",
    blockedUsers: [],
    blockedByUsers: [],
    mutedUsers: [],
    mutedByUsers: [],
    following: [],
    followers: [],
    myCloseFriends: [],
    closeFriendTo: [],
    isPrivate: false,
    isAcceptingMessages: false,
    isAcceptingTags: false,
    isBanned: false,
  };

  componentDidMount() {
    this.props.getLoggedUser();
  }

  changeState = (loggedUser) => {
    debugger;
    this.setState({
      id: loggedUser.id,
      firstName: loggedUser.firstName,
      lastName: loggedUser.lastName,
      email: loggedUser.emailAddress,
      phoneNumber: loggedUser.phoneNumber,
      dateOfBirth: new Date(loggedUser.dateOfBirth),
      gender: loggedUser.gender,
      username: loggedUser.username,
      bio: loggedUser.bio,
      webSite: loggedUser.websiteAddress,
      profilePicturePath: loggedUser.profilePicturePath,
      blockedUsers: loggedUser.blockedUsers,
      blockedByUsers: loggedUser.blockedByUsers,
      mutedUsers: loggedUser.mutedUsers,
      mutedByUsers: loggedUser.mutedByUsers,
      following: loggedUser.following,
      followers: loggedUser.followers,
      closeFriendTo: loggedUser.closeFriendTo,
      myCloseFriends: loggedUser.myCloseFriends,
      isPrivate: loggedUser.isPrivate,
      isAcceptingTags: loggedUser.isAcceptingTags,
      isAcceptingMessages: loggedUser.isAcceptingMessages,
    });
  };

  render() {
    debugger;
    if (this.props.loggedUser === undefined) {
      return null;
    }

    const loggedUser = this.props.loggedUser;
    if (this.state.firstName === "") {
      this.changeState(loggedUser);
    }
    return (
      <React.Fragment>
        <main className="main pt-0 pb-0" style={{ backgroundColor: "#4da3ff" }}>
          <div className="wrap bg-white">
            <div className="text-center pt-5">
              <img
                alt=""
                width="100"
                height="100"
                src="/images/iconfinder_00-ELASTOFONT-STORE-READY_user-circle_2703062.png"
              />
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">First Name:</label>
                  <input
                    type="text"
                    name="firstName"
                    value={this.state.firstName}
                    onChange={this.handleChange}
                    class="form-control"
                    id="firstName"
                    placeholder="Enter first name"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Last Name:</label>
                  <input
                    type="text"
                    name="lastName"
                    value={this.state.lastName}
                    onChange={this.handleChange}
                    class="form-control"
                    id="lastName"
                    placeholder="Enter last name"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Username:</label>
                  <input
                    type="text"
                    name="username"
                    value={this.state.username}
                    onChange={this.handleChange}
                    class="form-control"
                    id="username"
                    placeholder="Enter username"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Email:</label>
                  <input
                    type="text"
                    name="email"
                    value={this.state.email}
                    onChange={this.handleChange}
                    class="form-control"
                    id="email"
                    placeholder="Enter email"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="dateofbirth">Date of birth:</label>
                  <div className="d-block w-100">
                    <DatePicker
                      className="form-control w-100"
                      id="dateofbirth"
                      name="dueDate"
                      dateFormat="dd/MM/yyyy"
                      selected={this.state.dateOfBirth}
                      maxDate={new Date()}
                      onChange={(e) => this.handleChangeDate(e)}
                    />
                  </div>
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="telephone">Phone number:</label>
                  <input
                    type="text"
                    name="phoneNumber"
                    value={this.state.phoneNumber}
                    onChange={this.handleChange}
                    class="form-control"
                    id="telephone"
                    placeholder="Enter telephone number"
                  />
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Web site:</label>
                  <input
                    type="text"
                    name="webSite"
                    value={this.state.webSite}
                    onChange={this.handleChange}
                    class="form-control"
                    id="webSite"
                    placeholder="Enter webSite"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Gender:</label>
                  <select
                    value={this.state.gender}
                    class="form-control"
                    onChange={this.handleChange}
                    name="gender"
                  >
                    <option value=""> </option>
                    <option value="male">Male</option>
                    <option value="female">Female</option>
                  </select>
                </div>
              </div>
            </div>

            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Biography:</label>
                  <textarea
                    name="bio"
                    value={this.state.bio}
                    onChange={this.handleChange}
                    cols="40"
                    rows="5"
                    class="form-control"
                    placeholder="Enter biography"
                  ></textarea>
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <br />
                  <label className="mr-1" for="lastName">
                    Private profile:
                  </label>
                  <input
                    type="checkbox"
                    id="isPrivate"
                    name="isPrivate"
                    checked={this.state.isPrivate}
                    onChange={this.handleChangeCheckboxPrivate}
                  />
                  <br />
                  <label className="mr-1" for="lastName">
                    Accepting messages:
                  </label>
                  <input
                    type="checkbox"
                    id="isAcceptingMessages"
                    name="isAcceptingMessages"
                    checked={this.state.isAcceptingMessages}
                    onChange={this.handleChangeCheckboxMessages}
                  />
                  <br />
                  <label className="mr-1" for="lastName">
                    Accepting tags:
                  </label>
                  <input
                    type="checkbox"
                    id="isAcceptingTags"
                    name="isAcceptingTags"
                    checked={this.state.isAcceptingTags}
                    onChange={this.handleChangeCheckboxTags}
                  />
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button
                onClick={this.edit.bind(this)}
                className="btn btn-lg btn-primary btn-block"
              >
                Save changes
              </button>
            </div>
          </div>
        </main>
      </React.Fragment>
    );
  }

  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };

  handleChangeDate = (e) => {
    this.setState({
      dateOfBirth: e,
    });
  };

  handleChangeCheckboxPrivate = (e) => {
    debugger;
    this.setState({
      isPrivate: !this.state.isPrivate,
    });
    var test = this.state.isPrivate;
  };

  handleChangeCheckboxMessages = (e) => {
    debugger;
    this.setState({
      isAcceptingMessages: !this.state.isAcceptingMessages,
    });
  };

  handleChangeCheckboxTags = (e) => {
    debugger;
    this.setState({
      isAcceptingTags: !this.state.isAcceptingTags,
    });
  };

  async edit() {
    debugger;
    var successful = false;
    // successful = await this.props.editUserForStory({
    //   Id: this.state.id,
    //   Username: this.state.username,
    //   FirstName: this.state.firstName,
    //   LastName: this.state.lastName,
    //   IsPrivate: this.state.isPrivate,
    //   IsAcceptingTags: this.state.isAcceptingTags,
    //   IsAcceptingMessages: this.state.isAcceptingMessages,
    //   ProfilePicturePath: this.state.profilePicturePath,
    //   BlockedUsers: this.state.blockedUsers,
    //   BlockedByUsers: this.state.blockedByUsers,
    //   Following: this.state.following,
    //   Followers: this.state.followers,
    //   MyCloseFriends: this.state.myCloseFriends,
    //   CloseFriendTo: this.state.closeFriendTo,
    // });

    // successful = await this.props.editUserForPost({
    //   Id: this.state.id,
    //   Username: this.state.username,
    //   EmailAddress: this.state.email,
    //   FirstName: this.state.firstName,
    //   LastName: this.state.lastName,
    //   DateOfBirth: this.state.dateOfBirth,
    //   PhoneNumber: this.state.phoneNumber,
    //   Gender: this.state.gender,
    //   WebsiteAddress: this.state.webSite,
    //   Bio: this.state.bio,
    //   IsPrivate: this.state.isPrivate,
    //   IsAcceptingTags: this.state.isAcceptingTags,
    //   IsAcceptingMessages: this.state.IsAcceptingMessages,
    // });
    successful = await this.props.editUser({
      Id: this.state.id,
      Username: this.state.username,
      EmailAddress: this.state.email,
      FirstName: this.state.firstName,
      LastName: this.state.lastName,
      DateOfBirth: this.state.dateOfBirth,
      PhoneNumber: this.state.phoneNumber,
      Gender: this.state.gender,
      WebsiteAddress: this.state.webSite,
      Bio: this.state.bio,
      IsPrivate: this.state.isPrivate,
      IsAcceptingTags: this.state.isAcceptingTags,
      IsAcceptingMessages: this.state.isAcceptingMessages,
      IsBanned: this.state.isBanned,
    });

    if (successful === true) {
      this.props.history.replace({
        pathname: "/",
      });
    } else {
      toast.configure();
      toast.error("Unsuccessful edit!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }
}

const mapStateToProps = (state) => ({ loggedUser: state.loggedUser });

export default compose(
  withRouter,
  connect(mapStateToProps, {
    editUser,
    editUserForPost,
    getLoggedUser,
    editUserForStory,
  })
)(EditProfile);
