import React, { Component } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";

class SelectTargetAudience extends Component {
  state = {};
  render() {
    debugger;
    return (
      <div className="mt-5">
        <div className="form-group w-75" style={{ paddingLeft: 300 }}>
          <label className="label">Select max date of birth:</label>
          <div className="w-100">
            <DatePicker
              className="form-control w-100"
              id="maxDateOfBirth"
              name="maxDateOfBirth"
              dateFormat="dd/MM/yyyy"
              selected={this.props.maxDateOfBirth}
              maxDate={new Date()}
              onChange={(e) => this.props.handleChangeMaxDate(e)}
            />
          </div>
        </div>
        <div className="form-group w-75 pt-3" style={{ paddingLeft: 300 }}>
          <label className="label">Select min date of birth:</label>
          <div className="w-100">
            <DatePicker
              className="form-control w-100"
              id="minDateOfBirth"
              name="minDateOfBirth"
              dateFormat="dd/MM/yyyy"
              selected={this.props.minDateOfBirth}
              maxDate={
                this.props.maxDateOfBirth === ""
                  ? new Date()
                  : this.props.maxDateOfBirth
              }
              onChange={(e) => this.props.handleChangeMinDate(e)}
            />
          </div>
          <div className="form-group w-100 pt-3">
            <label for="lastName">Gender:</label>
            <select
              className="w-100"
              value={this.props.gender}
              class="form-control"
              onChange={this.props.handleChange}
              name="gender"
            >
              <option value=""></option>
              <option value="male">Male</option>
              <option value="female">Female</option>
            </select>
          </div>
        </div>
        <div
          className="pb-5"
          style={{ width: "150px", position: "fixed", bottom: 0, right: 250 }}
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
    this.props.changeStep(1);
  }

  changeStep() {
    this.props.changeStep(3);
  }
}

export default SelectTargetAudience;
