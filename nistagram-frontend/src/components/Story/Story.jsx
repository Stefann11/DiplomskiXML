import React, { Component } from "react";
import StoryModal from "./StoryModal";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";
import "./StoryCircle.css";

class Story extends Component {
  state = {
    showStoryModal: false,
    user: {},
  };

  render() {
    if (this.props.profileImage === undefined && this.props.first === false)
      return null;
    return (
      <div>
        {this.state.showStoryModal ? (
          <StoryModal
            show={this.state.showStoryModal}
            onShowChange={this.displayModalStory.bind(this)}
            user={this.state.user}
            profileImage={this.props.profileImage}
          />
        ) : null}
        <div
          className="story-circle"
          style={{
            backgroundImage: this.props.first
              ? `url("/images/white_plus2.png")`
              : `url("data:image/jpg;base64,${this.props.profileImage.fileContents}"`,
            backgroundRepeat: "no-repeat",
            paddingBottom: "51px",
            backgroundSize: "cover",
            paddingRight: "51px",
            backgroundPosition: "center",
          }}
          key={this.props.item.id}
          onClick={() => {
            debugger;
            if (this.props.first === true)
              this.props.history.push({
                pathname: "/story",
              });
            else this.displayModalStory(this.props.item.id);
          }}
        />
      </div>
    );
  }

  displayModalStory(userid) {
    debugger;
    this.setState({
      showStoryModal: !this.state.showStoryModal,
      user: userid,
    });
  }
}

const mapStateToProps = (state) => ({
  image: state.storyImage,
});

export default compose(withRouter, connect(mapStateToProps, {}))(Story);
