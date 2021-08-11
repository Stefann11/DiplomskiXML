import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import OptionsButton from "./OptionsButton";
import PostModal from "./PostModal";
import ProfileHeader from "./ProfileHeader";
import { getPostsByUserId } from "../../actions/actions";
import { connect } from "react-redux";
import { VerifiedUser } from "@material-ui/icons";

class SomeoneProfile extends Component {
  state = {
    showPostModal: false,
    posts: [],
    postId: "",
  };

  componentDidMount() {
    debugger;
    this.props.getPostsByUserId(sessionStorage.getItem("userId"));
  }

  render() {
    if (this.props.posts == undefined) {
      return null;
    }
    const posts = this.props.posts;
    const user = posts[0].registeredUser;
    debugger;
    const Posts = () =>
      posts.map((post) => (
        <Photo
          src="images/nature.jpg"
          onClick={() => {
            this.displayModalPost(post);
          }}
        />
      ));
    return (
      <div>
        {this.state.showPostModal ? (
          <PostModal
            show={this.state.showPostModal}
            postId={this.state.postId}
            personPhoto="images/download.jfif"
            person={user.username}
            onShowChange={this.displayModalPost.bind(this)}
          />
        ) : null}
        <OptionsButton />
        <ProfileHeader user={user} postsCount={posts.length} />
        <button
          style={{ float: "right" }}
          className="btn btn-block btn-primary btn-md mt-4 mb-4"
        >
          Follow
        </button>
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>𐄹 Posts</GridControlBarItem>
            <GridControlBarItem>웃 Tagged</GridControlBarItem>
          </GridControlBar>
          <Posts />
        </Grid>
      </div>
    );
  }

  displayModalPost(post) {
    debugger;
    if (post != undefined) {
      this.setState({
        postId: post.id,
      });
    }
    this.setState({
      showPostModal: !this.state.showPostModal,
    });
  }
}

const mapStateToProps = (state) => ({ posts: state.posts });

export default connect(mapStateToProps, { getPostsByUserId })(SomeoneProfile);
