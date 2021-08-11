import React, { Component } from "react";
import DateTimePicker from "react-datetime-picker";

class SelectOneTimeExposureDate extends Component {
  state = { exposureDate: this.props.exposureDate };
  render() {
    return (
      <div className="mt-5">
        <div className="form-group w-75" style={{ paddingLeft: 300 }}>
          <label className="label">Select exposure date and time:</label>
          <div className="w-100">
            <DateTimePicker
              value={this.props.exposureDate.Time}
              format="dd/MM/yyyy HH:mm"
              minDate={new Date()}
              onChange={this.props.handleChangeExposureDate}
            />
          </div>
        </div>
        <div
          className="pb-5"
          style={{
            width: "150px",
            position: "fixed",
            bottom: 0,
            right: 250,
          }}
        >
          <button
            disabled={this.props.exposureDate === ""}
            onClick={() => this.changeStep()}
            className="btn btn-primary btn-lg"
          >
            Next
          </button>
        </div>
        <div
          className="pb-5"
          style={{
            width: "150px",
            position: "fixed",
            bottom: 0,
            left: 250,
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
    this.props.addExposureDates(this.props.exposureDate);
    this.props.changeStep(2);
  }
}

export default SelectOneTimeExposureDate;
