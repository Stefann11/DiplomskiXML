import React, { Component } from "react";
import TimePicker from "react-time-picker";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import { intersection } from "lodash";

class SelectRecurringExposureDate extends Component {
  state = {
    exposureTimes: this.props.exposureTimes,
    maxExposureDates: 5,
    startDate: this.props.startDate,
    endDate: this.props.endDate,
    validTimes: this.props.validTimes,
  };
  render() {
    debugger;
    return (
      <div className="mt-5">
        <div className="form-group w-75" style={{ paddingLeft: 300 }}>
          <label className="label">Select start date:</label>
          <DatePicker
            className="form-control w-100"
            id="startDate"
            name="startDate"
            dateFormat="dd/MM/yyyy"
            selected={this.props.startDate}
            minDate={new Date()}
            maxDate={this.props.endDate === "" ? null : this.props.endDate}
            onChange={(e) => this.props.handleChangeStartDate(e)}
          />
        </div>
        <div className="form-group w-75" style={{ paddingLeft: 300 }}>
          <label className="label">Select end date:</label>
          <DatePicker
            className="form-control w-100"
            id="endDate"
            name="endDate"
            dateFormat="dd/MM/yyyy"
            selected={this.props.endDate}
            minDate={
              this.props.startDate === "" ? new Date() : this.props.startDate
            }
            onChange={(e) => this.props.handleChangeEndDate(e)}
          />
        </div>
        {this.props.exposureTimes.map((date, i) => {
          return (
            <div className="form-group w-75" style={{ paddingLeft: 300 }}>
              <label className="label">Select exposure date:</label>
              <div className="w-100 d-flex">
                <TimePicker
                  value={date}
                  format="HH:mm"
                  onChange={(e) => this.props.handleChangeExposureTime(e, i)}
                />
                {i === 0 ? (
                  <div className="pl-4">
                    <button
                      className="btn btn-primary"
                      style={{ width: 50 }}
                      onClick={() => this.props.addExposureTime()}
                    >
                      +
                    </button>
                  </div>
                ) : null}
                {this.props.exposureTimes.length > 1 && i !== 0 ? (
                  <div className="pl-4">
                    <button
                      className="btn btn-primary"
                      style={{ width: 50 }}
                      onClick={() => this.props.removeExposureTime(i)}
                    >
                      -
                    </button>
                  </div>
                ) : null}
              </div>
            </div>
          );
        })}
        <div
          className="pb-5 mb-5 mt-5"
          style={{
            width: "150px",
            position: "relative",
            float: "right",
          }}
        >
          <button
            disabled={
              this.props.minDateOfBirth === "" ||
              this.props.maxDateOfBirth === "" ||
              this.props.gender === ""
            }
            onClick={() => this.changeStep()}
            className="btn btn-primary btn-lg"
          >
            Next
          </button>
        </div>
        <div
          className="pb-5 mb-5 mt-5"
          style={{
            width: "150px",
            position: "relative",
            float: "left",
          }}
        >
          <button
            onClick={() => this.stepBack()}
            className="btn btn-warning btn-lg"
          >
            Back
          </button>
        </div>
      </div>
    );
  }

  stepBack() {
    this.props.changeStep(0);
  }

  changeStep() {
    debugger;
    let exposureDates = [];
    let tempDate = new Date(this.props.startDate);
    let endDate = new Date(this.props.endDate);
    while (tempDate <= endDate) {
      this.props.exposureTimes.forEach((time) => {
        let timeToAdd = new Date(tempDate);
        let timeArray = time.split(":");
        let hours = parseInt(timeArray[0]);
        let minutes = parseInt(timeArray[1]);
        timeToAdd.setHours(timeToAdd.getHours() + hours);
        timeToAdd.setMinutes(timeToAdd.getMinutes() + minutes);
        let dateToAdd = { Time: timeToAdd, SeenByIds: [] };
        exposureDates.push(dateToAdd);
      });
      tempDate.setDate(tempDate.getDate() + 1);
    }
    this.props.addExposureDates(exposureDates);
    this.props.changeStep(2);
  }
}

export default SelectRecurringExposureDate;
