import React, { Component } from "react";
import {
  savePost,
  getLocationsByText,
  getTaggableUsers,
} from "../../actions/actions";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { createNotification } from "../../actions/actionsNotification";
import Stepper from "react-stepper-horizontal";
import SelectCampaignType from "./SelectCampaignType";
import SelectTargetAudience from "./SelectTargetAudience";
import SelectOneTimeExposureDate from "./SelectOneTimeExposureDate";
import SelectRecurringExposureDate from "./SelectRecurringExposureDate";
import CreatePostCampaign from "./CreatePostCampaign";
import CreateStoryCampaign from "./CreateStoryCampaign";

class CreatePost extends Component {
  state = {
    activeStep: 0,
    recurrence: "",
    type: "",
    minDateOfBirth: "",
    maxDateOfBirth: "",
    gender: "",
    exposureDates: [],
    exposureTimes: [""],
    startDate: "",
    endDate: "",
    exposureDate: "",
  };

  render() {
    debugger;
    return (
      <React.Fragment>
        <div className="mt-5">
          <Stepper
            steps={[
              { title: "Select Campaign Type" },
              { title: "Select Exposure Date" },
              { title: "Select Target Audience" },
              { title: "Create Ads" },
            ]}
            activeStep={this.state.activeStep}
          />
          {this.state.activeStep === 0 ? (
            <SelectCampaignType
              recurrence={this.state.recurrence}
              type={this.state.type}
              handleChange={this.handleChange.bind(this)}
              changeStep={this.changeStep.bind(this)}
            />
          ) : null}
          {this.state.activeStep === 1 &&
          this.state.recurrence === "oneTimeCampaign" ? (
            <SelectOneTimeExposureDate
              addExposureDates={this.addExposureDates.bind(this)}
              changeStep={this.changeStep.bind(this)}
              exposureDate={this.state.exposureDate}
              handleChangeExposureDate={this.handleChangeExposureDate.bind(
                this
              )}
            />
          ) : null}
          {this.state.activeStep === 1 &&
          this.state.recurrence === "recurringCampaign" ? (
            <SelectRecurringExposureDate
              addExposureDates={this.addExposureDates.bind(this)}
              changeStep={this.changeStep.bind(this)}
              exposureTimes={this.state.exposureTimes}
              startDate={this.state.startDate}
              endDate={this.state.endDate}
              handleChangeStartDate={this.handleChangeStartDate.bind(this)}
              handleChangeEndDate={this.handleChangeEndDate.bind(this)}
              handleChangeExposureTime={this.handleChangeExposureTime.bind(
                this
              )}
              addExposureTime={this.addExposureTime.bind(this)}
              removeExposureTime={this.removeExposureTime.bind(this)}
            />
          ) : null}
          {this.state.activeStep === 2 ? (
            <SelectTargetAudience
              minDateOfBirth={this.state.minDateOfBirth}
              maxDateOfBirth={this.state.maxDateOfBirth}
              gender={this.state.gender}
              handleChange={this.handleChange.bind(this)}
              handleChangeMinDate={this.handleChangeMinDate.bind(this)}
              handleChangeMaxDate={this.handleChangeMaxDate.bind(this)}
              changeStep={this.changeStep.bind(this)}
            />
          ) : null}
          {this.state.activeStep === 3 && this.state.type === "Post" ? (
            <CreatePostCampaign
              recurrence={this.state.recurrence}
              type={this.state.type}
              startDate={this.state.startDate}
              endDate={this.state.endDate}
              minDateOfBirth={this.state.minDateOfBirth}
              maxDateOfBirth={this.state.maxDateOfBirth}
              gender={this.state.gender}
              exposureDates={this.state.exposureDates}
              changeStep={this.changeStep.bind(this)}
            />
          ) : null}
          {this.state.activeStep === 3 && this.state.type === "Story" ? (
            <CreateStoryCampaign
              recurrence={this.state.recurrence}
              type={this.state.type}
              startDate={this.state.startDate}
              endDate={this.state.endDate}
              minDateOfBirth={this.state.minDateOfBirth}
              maxDateOfBirth={this.state.maxDateOfBirth}
              gender={this.state.gender}
              exposureDates={this.state.exposureDates}
              changeStep={this.changeStep.bind(this)}
            />
          ) : null}
        </div>
      </React.Fragment>
    );
  }

  async addExposureDates(dates) {
    debugger;
    var datesArray = [];
    if (dates.length === undefined) datesArray.push(dates);
    else datesArray = dates;
    await this.setState({
      exposureDates: datesArray,
    });
  }

  changeStep(step) {
    this.setState({ activeStep: step });
    console.log(this.state);
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

  handleChangeMinDate = (e) => {
    debugger;
    this.setState({
      minDateOfBirth: e,
    });
  };

  handleChangeMaxDate = (e) => {
    debugger;
    this.setState({
      maxDateOfBirth: e,
    });
  };

  handleChangeStartDate = (e) => {
    debugger;
    this.setState({
      startDate: e,
    });
  };

  handleChangeEndDate = (e) => {
    debugger;
    this.setState({
      endDate: e,
    });
  };

  handleChangeExposureTime = (e, i) => {
    this.setState({
      exposureTimes: this.state.exposureTimes.map((element, j) =>
        j === i ? e : element
      ),
      validTimes: this.checkIfAllTimesNotEmpty(),
    });
  };

  addExposureTime() {
    this.setState({
      exposureTimes: this.state.exposureTimes.concat(""),
      validTimes: this.checkIfAllTimesNotEmpty(),
    });
  }

  removeExposureTime(i) {
    debugger;
    this.setState({
      exposureTimes: this.state.exposureTimes.filter((_, j) => j !== i),
    });
  }

  checkIfAllTimesNotEmpty() {
    let valid = true;
    this.state.exposureTimes.forEach((time) =>
      time === "" ? (valid = false) : null
    );
    return valid;
  }

  handleChangeExposureDate = (e) => {
    debugger;
    this.setState({
      exposureDate: { Time: e, SeenByIds: [] },
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
