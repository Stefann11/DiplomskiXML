import React, { useEffect, useState } from "react";
import {
  getPostsByCollectionAndUser,
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

function PostsInCollection(props) {
  const [postId, setPostId] = useState("");
  const [showPostModal, setShowPostModal] = useState(false);
  const [username, setUsername] = useState("");
  const initialUser = {};
  const collectionPosts = props.collectionPosts;

  useEffect(() => {
    props.getPostsByCollectionAndUser(props.location.pathname.slice(12));
  }, [props.location.pathname]);

  useEffect(() => {
    debugger;
    if (props.collectionPosts !== undefined)
      getAllImages(props.collectionPosts);
  }, [props.collectionPosts]);

  const getAllImages = async (collectionPosts) => {
    const paths = [];
    for (var i = 0; i < collectionPosts.length; i++) {
      if (collectionPosts[i].contentPath === undefined) {
        paths.push(collectionPosts[i].contentPaths[0]);
      } else {
        paths.push(collectionPosts[i].contentPath);
      }
    }
    await props.getAllImagesForCollection(paths);
  };

  const Posts = () => {
    if (props.collectionImages === undefined) {
      return null;
    }
    return collectionPosts.map((post, i) =>
      props.collectionImages[i].contentType === "image/jpeg" ? (
        <Photo
          src={
            "data:image/jpg;base64," + props.collectionImages[i].fileContents
          }
          onClick={() => displayModalPost(post)}
        />
      ) : (
        <video
          controls
          onClick={() => displayModalPost(post)}
          style={{ width: 367, height: 370 }}
          className="mb-3"
        >
          <source
            src={
              "data:video/mp4;base64," + props.collectionImages[i].fileContents
            }
            type="video/mp4"
          ></source>
        </video>
      )
    );
  };

  const displayModalPost = (post) => {
    if (post != undefined) {
      setPostId(post.id);
      setUsername(post.registeredUser.username);
    }
    setShowPostModal(!showPostModal);
  };

  if (props.collectionPosts === undefined) {
    return null;
  }

  return (
    <div>
      {showPostModal ? (
        <PostModal
          show={showPostModal}
          postId={postId}
          personPhoto="/images/download.jfif"
          person={username}
          onShowChange={() => displayModalPost()}
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

const mapStateToProps = (state) => ({
  collectionPosts: state.collectionPosts,
  collectionImages: state.collectionImages,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getPostsByCollectionAndUser,
    getAllImagesForCollection,
  })
)(PostsInCollection);
