import React, { Component } from "react";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { compose } from "redux";

class Token extends Component {
  state = {
    showCreateCollectionModal: false,
  };

  componentDidMount() {}

  render() {
    return (
      <div>
        <div className="wrap bg-white pt-3 pb-3" style={{ height: "100vh" }}>
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <div className="text-center">
              <label className="label">Token:</label>
              <textarea
                name="bio"
                value={sessionStorage.getItem("token")}
                cols="40"
                rows="4"
                class="form-control"
                disabled={true}
              ></textarea>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => ({});

export default compose(withRouter, connect(mapStateToProps, {}))(Token);
