import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import PostModal from "./PostModal";
import ProfileHeader from "./ProfileHeader";

class MyProfile extends Component {
  state = {
    showPostModal: false,
  };
  render() {
    const photos = Array.from({ length: 5 }, (x, i) => (
      <Photo
        src="images/nature.jpg"
        onClick={() => {
          this.displayModalPost();
        }}
      />
    ));
    return (
      <div>
        {this.state.showPostModal ? (
          <PostModal
            show={this.state.showPostModal}
            post="images/nature.jpg"
            personPhoto="images/download.jfif"
            person="angusyoung"
            onShowChange={this.displayModalPost.bind(this)}
          />
        ) : null}
        <ProfileHeader />
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>𐄹 Posts</GridControlBarItem>
            <GridControlBarItem>웃 Tagged</GridControlBarItem>
          </GridControlBar>
          {photos}
        </Grid>
      </div>
    );
  }

  displayModalPost() {
    debugger;
    this.setState({
      showPostModal: !this.state.showPostModal,
    });
  }
}

export default MyProfile;
