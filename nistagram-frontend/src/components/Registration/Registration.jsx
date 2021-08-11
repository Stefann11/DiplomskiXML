import React, { Component } from "react";
import {
  userRegistration,
  userRegistrationForPost,
  userRegistrationForStory,
} from "../../actions/actionsUser";
import DatePicker from "react-datepicker";
import { connect } from "react-redux";
import { toast } from "react-toastify";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import "react-toastify/dist/ReactToastify.css";
import "react-datepicker/dist/react-datepicker-cssmodules.css";

class Registration extends Component {
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
    password: "",
    repeatPassword: "",
    isPrivate: false,
    isAcceptingMessages: false,
    isAcceptingTags: false,
  };

  render() {
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
                  <label for="firstName">Password:</label>
                  <input
                    type="password"
                    name="password"
                    value={this.state.password}
                    onChange={this.handleChange}
                    class="form-control"
                    id="password"
                    placeholder="Enter password"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Repeat password:</label>
                  <input
                    type="password"
                    name="repeatPassword"
                    value={this.state.repeatPassword}
                    onChange={this.handleChange}
                    class="form-control"
                    id="repeatPassword"
                    placeholder="Repeat password"
                  />
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
                    onChange={this.handleChangeCheckboxTags}
                  />
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button
                disabled={this.state.password != this.state.repeatPassword}
                className="btn btn-lg btn-primary btn-block"
                onClick={this.register.bind(this)}
              >
                Register
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

  handleChangeCheckboxPrivate = (e) => {
    this.setState({
      isPrivate: !this.state.isPrivate,
    });
  };

  handleChangeCheckboxMessages = (e) => {
    this.setState({
      isAcceptingMessages: !this.state.isAcceptingMessages,
    });
  };

  handleChangeCheckboxTags = (e) => {
    this.setState({
      isAcceptingTags: !this.state.isAcceptingTags,
    });
  };

  handleChangeDate = (e) => {
    this.setState({
      dateOfBirth: e,
    });
  };

  async register() {
    debugger;
    var successful = false;
    successful = await this.props.userRegistration({
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
      IsAcceptingMessages: this.state.isAcceptingMessages,
      IsAcceptingTags: this.state.isAcceptingTags,
      Password: this.state.password,
    });

    // successful = this.props.userRegistrationForPost( this.props.registeredUser)
    //   "Username" : this.state.username,
    //   "EmailAddress" : this.state.email,
    //   "FirstName" : this.state.firstName,
    //   "LastName" : this.state.lastName,
    //   "DateOfBirth" : this.state.dateOfBirth,
    //   "PhoneNumber" : this.state.phoneNumber,
    //   "Gender" : this.state.gender,
    //   "WebsiteAddress" : this.state.webSite,
    //   "Bio" : this.state.bio,
    //   "IsPrivate" : this.state.isPrivate,
    //   "IsAcceptingMessages" : this.state.isAcceptingMessages,
    //   "IsAcceptingTags" : this.state.isAcceptingTags,
    //   "Password" : this.state.password
    // })

    //successful = await this.props.userRegistrationForStory( this.props.registeredUser)
    //ZA STORY KOJI JE NO SQL
    // this.props.userRegistrationForStory({ "Username" : this.state.username,
    //   "EmailAddress" : this.state.email,
    //   "FirstName" : this.state.firstName,
    //   "LastName" : this.state.lastName,
    //   "DateOfBirth" : this.state.dateOfBirth,
    //   "PhoneNumber" : this.state.phoneNumber,
    //   "Gender" : this.state.gender,
    //   "WebsiteAddress" : this.state.webSite,
    //   "Bio" : this.state.bio,
    //   "IsPrivate" : this.state.isPrivate,
    //   "IsAcceptingMessages" : this.state.isAcceptingMessages,
    //   "IsAcceptingTags" : this.state.isAcceptingTags,
    //   "Password" : this.state.password
    // })
    if (successful === true) {
      this.props.history.replace({
        pathname: "/login",
      });
    } else {
      toast.configure();
      toast.error("Unsuccessful registration!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }
}

const mapStateToProps = (state) => ({
  userRegistration: state.userRegistration,
  userRegistrationForPost: state.userRegistrationForPost,
  userRegistrationForStory: state.userRegistrationForStory,
  registeredUser: state.registeredUser,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    userRegistration,
    userRegistrationForPost,
    userRegistrationForStory,
  })
)(Registration);
