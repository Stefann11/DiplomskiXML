import React, { Component } from "react";
import {
  getDislikedPosts,
  getAllImagesForCollection,
} from "../../actions/actions";
import PostModal from "../../components/Profile/PostModal";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";

class DislikedPosts extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getDislikedPosts();
    await this.getAllImages(this.props.dislikedPosts);
  }
  render() {
    if (this.props.dislikedPosts === undefined) {
      return null;
    }

    const dislikedPosts = this.props.dislikedPosts;

    if (this.props.collectionImages === undefined) {
      return null;
    }

    const Posts = () => {
      return dislikedPosts.map((post, i) =>
        this.props.collectionImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={
              "data:image/jpg;base64," +
              this.props.collectionImages[i].fileContents
            }
            onClick={() => this.displayModalPost(post)}
          />
        ) : (
          <video
            controls
            onClick={() => this.displayModalPost(post)}
            style={{ width: 367, height: 370 }}
            className="mb-3"
          >
            <source
              src={
                "data:video/mp4;base64," +
                this.props.collectionImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    };

    return (
      <div>
        {this.state.showPostModal ? (
          <PostModal
            show={this.state.showPostModal}
            postId={this.state.postId}
            personPhoto="/images/download.jfif"
            person={this.state.username}
            onShowChange={() => this.displayModalPost()}
          />
        ) : null}
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>𐄹 Saved Posts</GridControlBarItem>
          </GridControlBar>
          <Posts />
        </Grid>
      </div>
    );
  }

  displayModalPost = (post) => {
    if (post != undefined) {
      this.setState({
        postId: post.id,
        username: post.registeredUser.username,
      });
    }
    this.setState({
      showPostModal: !this.state.showPostModal,
    });
  };

  getAllImages = async (dislikedPosts) => {
    const paths = [];
    for (var i = 0; i < dislikedPosts.length; i++) {
      if (dislikedPosts[i].contentPath === undefined) {
        paths.push(dislikedPosts[i].contentPaths[0]);
      } else {
        paths.push(dislikedPosts[i].contentPath);
      }
    }
    await this.props.getAllImagesForCollection(paths);
  };
}

const mapStateToProps = (state) => ({
  dislikedPosts: state.dislikedPosts,
  collectionImages: state.collectionImages,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getDislikedPosts,
    getAllImagesForCollection,
  })
)(DislikedPosts);
