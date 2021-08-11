import React, { Component } from "react";

class SelectCampaignType extends Component {
  state = { recurrence: this.props.recurrence, type: this.props.type };
  render() {
    return (
      <div className="mt-5">
        <div className="form-group w-75" style={{ paddingLeft: 150 }}>
          <label className="label">Select recurrence of campaign:</label>
          <div className="w-100 d-flex pt-3" onChange={this.props.handleChange}>
            <div style={{ width: 400 }}>
              <input
                className="mr-3"
                type="radio"
                value="oneTimeCampaign"
                name="recurrence"
              />
              One Time Campaign
            </div>
            <div className="w-100" style={{ paddingLeft: 300 }}>
              <input
                className="mr-3"
                type="radio"
                value="recurringCampaign"
                name="recurrence"
              />
              Recurring Campaign
            </div>
          </div>
        </div>
        <div className="form-group w-75 pt-5" style={{ paddingLeft: 150 }}>
          <label className="label">Select type of campaign:</label>
          <div className="w-100 pt-3 d-flex" onChange={this.props.handleChange}>
            <div style={{ width: 400 }}>
              <input className="mr-3" type="radio" value="Post" name="type" />
              Post Campaign
            </div>
            <div className="w-100" style={{ paddingLeft: 300 }}>
              <input className="mr-3" type="radio" value="Story" name="type" />
              Story Campaign
            </div>
          </div>
        </div>
        <div
          className="pb-5"
          style={{ width: "150px", position: "fixed", bottom: 0, right: 250 }}
        >
          <button
            disabled={this.props.recurrence === "" || this.props.type === ""}
            onClick={() => this.props.changeStep(1)}
            className="btn btn-primary btn-lg"
          >
            Next
          </button>
        </div>
      </div>
    );
  }
}

export default SelectCampaignType;
