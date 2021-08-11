import React, { useState, useEffect } from "react";
import Layout from "../layouts/Layout";
import { Grid, Photo } from "react-instagram-ui-kit";
import {
  getPostsByHashTag,
  getPostsByLocation,
  getAllImagesForSearch,
} from "../actions/actions";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";
import PostModal from "../components/Profile/PostModal";
import _ from "lodash";

function Explore(props) {
  const [postId, setPostId] = useState("");
  const [showPostModal, setShowPostModal] = useState(false);
  const [username, setUsername] = useState("");

  const getAllImages = async (posts) => {
    const paths = [];
    for (var i = 0; i < posts.length; i++) {
      if (posts[i].contentPath === undefined) {
        paths.push(posts[i].contentPaths[0]);
      } else {
        paths.push(posts[i].contentPath);
      }
    }
    await props.getAllImagesForSearch(paths);
  };

  useEffect(() => {
    var test = props.location.pathname.slice(18);
    var testArray = test.split("-");
    if (props.location.state.searchObject.type === "hashtag") {
      debugger;
      if (props.location.hash !== "")
        props.getPostsByHashTag(props.location.hash);
      else props.getPostsByHashTag(props.location.pathname.slice(13));
    } else if (props.location.state.searchObject.type === "location") {
      debugger;
      var locationArray = props.location.pathname.slice(18).split("-");
      if (locationArray.length === 1)
        props.getPostsByLocation(locationArray[0], "", "");
      else if (locationArray.length === 2)
        props.getPostsByLocation(locationArray[1], locationArray[0], "");
      else
        props.getPostsByLocation(
          locationArray[2],
          locationArray[1],
          locationArray[0]
        );
    }
  }, [props.location.pathname]);

  useEffect(() => {
    debugger;
    if (props.posts !== undefined) getAllImages(props.posts);
  }, [props.posts]);

  debugger;
  if (props.posts === undefined || props.images === undefined) {
    return null;
  }
  if (props.images.length !== props.posts.length) return null;
  const Posts = () =>
    props.posts.map((post, i) => (
      <Photo
        src={"data:image/jpg;base64," + props.images[i].fileContents}
        onClick={() => displayModalPost(post)}
      />
    ));

  const displayModalPost = (post) => {
    debugger;
    if (post != undefined) {
      setPostId(post.id);
      setUsername(post.registeredUser.username);
    }
    setShowPostModal(!showPostModal);
  };

  return (
    <Layout>
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
        <Posts />
      </Grid>
    </Layout>
  );
}

const mapStateToProps = (state) => ({
  posts: state.explorePosts,
  images: state.searchImages,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getPostsByHashTag,
    getPostsByLocation,
    getAllImagesForSearch,
  })
)(Explore);
