import React, { Component } from "react";
import OptionsButton from "./OptionsButton";
import ProfileHeader from "./ProfileHeader";

class PrivateProfile extends Component {
  state = {};
  render() {
    return (
      <div>
        <OptionsButton />
        <ProfileHeader />
        <hr />
        <div
          className="pt-4"
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <p style={{ textAlign: "center" }}>
            <b>This Account Is Private</b>
            <br />
            Follow to see their photos and videos.
            <br />
            <button
              style={{ float: "center" }}
              className="btn btn-primary btn-block btn-md mt-4"
            >
              Follow
            </button>
          </p>
        </div>
      </div>
    );
  }
}

export default PrivateProfile;
