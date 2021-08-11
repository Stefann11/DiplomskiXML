import React, { Component } from "react";
import ProfileStoryModal from "./ProfileStoryModal";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";

class ProfileStory extends Component {
  state = {
    showProfileStoryModal: false,
    user: {},
  };

  render() {
    debugger;
    return (
      <div>
        {this.state.showProfileStoryModal ? (
          <ProfileStoryModal
            show={this.state.showProfileStoryModal}
            onShowChange={this.displayModalProfileStory.bind(this)}
            user={this.state.user}
            profileImage={this.props.profileImage}
            isActiveStories={this.props.first}
            highlights={this.props.first ? this.props.highlights : null}
            stories={
              this.props.first
                ? this.props.activeStories
                : this.props.highlight.stories
            }
          />
        ) : null}
        <div
          className="story-circle"
          style={{
            backgroundImage:
              this.props.first === true
                ? `url("data:image/jpg;base64,${this.props.profileImage}"`
                : `url("data:image/jpg;base64,${this.props.profileImage}"`,
            backgroundRepeat: "no-repeat",
            paddingBottom: "51px",
            backgroundSize: "cover",
            paddingRight: "51px",
            backgroundPosition: "center",
          }}
          onClick={() => {
            debugger;
            if (this.props.first === true)
              this.displayModalProfileStory(this.props.user.id);
            else this.displayModalProfileStory(this.props.highlight.id);
          }}
        />
        <label
          style={{ fontSize: "10px", textAlign: "center", paddingTop: "5px" }}
        >
          {this.props.highlight !== undefined
            ? this.props.highlight.highlightName
            : "Active stories"}
        </label>
      </div>
    );
  }

  displayModalProfileStory(userid) {
    this.setState({
      showProfileStoryModal: !this.state.showProfileStoryModal,
      user: userid,
    });
  }
}

const mapStateToProps = (state) => ({
  image: state.ProfileStoryImage,
});

export default compose(withRouter, connect(mapStateToProps, {}))(ProfileStory);
