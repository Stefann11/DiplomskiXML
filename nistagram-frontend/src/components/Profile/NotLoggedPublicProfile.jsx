import React, { useEffect, useState } from "react";
import {
  getPostsByUserId,
  getAllImagesForProfile,
} from "../../actions/actions";
import ProfileHeader from "./ProfileHeader";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";
import { getUserById, followProfile } from "../../actions/actionsUser";
import NotLoggedPostModal from "./NotLoggedPostModal";
import ProfileStoryCard from "./ProfileStoryCard";
import {
  getHighlights,
  getStoriesForNotLogged,
} from "../../actions/actionsStory";

function PublicProfile(props) {
  const [postId, setPostId] = useState("");
  const [showPostModal, setShowPostModal] = useState(false);
  const [username, setUsername] = useState("");
  const [followedById, setFollowedById] = useState(0);
  const [followingId, setFollowingId] = useState(0);
  const user = props.user;
  const initialUser = {};
  useEffect(async () => {
    props.getUserById(props.location.pathname.slice(9));
    props.getPostsByUserId(props.location.pathname.slice(9));
    await props.getHighlights(props.location.pathname.slice(9));
    await props.getStoriesForNotLogged(props.location.pathname.slice(9));
  }, [props.location.pathname]);

  useEffect(() => {
    debugger;
    if (props.posts !== undefined) getAllImages(props.posts);
  }, [props.posts]);

  const follow = () => {
    props.followProfile({
      FollowedById: sessionStorage.getItem("userId"),
      FollowingId: props.location.pathname.slice(9),
    });
  };

  const Posts = () => {
    debugger;
    var shouldDisplayPosts = false;
    if (props.location.pathname.slice(9) === sessionStorage.getItem("userId")) {
      shouldDisplayPosts = true;
    } else {
      if (user.isPrivate === false) {
        shouldDisplayPosts = true;
      } else {
        for (var i = 0; i < user.followers.length; i++) {
          if (user.followers[i].id === sessionStorage.getItem("userId")) {
            shouldDisplayPosts = true;
            break;
          }
        }
      }
    }
    if (props.profileImages === undefined) {
      return null;
    }
    if (props.profileImages.length === 0) {
      return null;
    }
    debugger;
    if (shouldDisplayPosts === true) {
      return props.posts.map((post, i) =>
        props.profileImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={"data:image/jpg;base64," + props.profileImages[i].fileContents}
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
                "data:video/mp4;base64," + props.profileImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    } else {
      return (
        <div className="text-center pt-5">
          <img src="/images/padlock.png" />
          <br />
          <h4>This Account Is Private</h4>
        </div>
      );
    }
  };

  var shouldDisplayStories = false;
  const displayStories = () => {
    debugger;
    if (props.location.pathname.slice(9) === sessionStorage.getItem("userId")) {
      shouldDisplayStories = true;
    } else {
      if (user.isPrivate === false) {
        shouldDisplayStories = true;
      } else {
        for (var i = 0; i < user.followers.length; i++) {
          if (user.followers[i].id === sessionStorage.getItem("userId")) {
            shouldDisplayStories = true;
            break;
          }
        }
      }
    }
  };

  const displayModalPost = (post) => {
    if (post != undefined) {
      setPostId(post.id);
      setUsername(post.registeredUser.username);
    }
    setShowPostModal(!showPostModal);
  };

  const getAllImages = async (profilePosts) => {
    const paths = [];
    for (var i = 0; i < profilePosts.length; i++) {
      if (profilePosts[i].contentPath === undefined) {
        paths.push(profilePosts[i].contentPaths[0]);
      } else {
        paths.push(profilePosts[i].contentPath);
      }
    }
    await props.getAllImagesForProfile(paths);
  };

  debugger;
  if (
    props.posts === undefined ||
    user === undefined ||
    props.highlights === undefined ||
    props.stories === undefined
  ) {
    return null;
  }

  if (user !== undefined) {
    displayStories();
  }

  return (
    <div>
      {showPostModal ? (
        <NotLoggedPostModal
          show={showPostModal}
          postId={postId}
          personPhoto="/images/download.jfif"
          person={username}
          onShowChange={() => displayModalPost()}
        />
      ) : null}
      <ProfileHeader
        user={user}
        userid={user.id}
        postsCount={props.posts.length}
      />
      {props.user.category !== undefined ? (
        <div
          style={{
            marginLeft: "340px",
            marginTop: "-50px",
            marginBottom: "50px",
          }}
        >
          <img
            style={{ width: "30px", height: "30px", marginRight: "10px" }}
            src="/images/correct.png"
          />
          <b>{props.user.category}</b>
        </div>
      ) : null}
      {shouldDisplayStories ? (
        <ProfileStoryCard
          user={props.user}
          activeStories={props.stories}
          highlights={props.highlights}
        />
      ) : null}
      <Grid>
        <GridControlBar>
          <GridControlBarItem isActive>𐄹 Posts</GridControlBarItem>
          <GridControlBarItem>웃 Tagged</GridControlBarItem>
        </GridControlBar>
        {user.isPrivate === true ? (
          <div className="text-center mt-3">
            <img src="/images/padlock.png" />
            <br />
            <h4>This Account Is Private</h4>
          </div>
        ) : (
          <Posts />
        )}
      </Grid>
    </div>
  );
}

const mapStateToProps = (state) => ({
  posts: state.profilePosts,
  user: state.registeredUser,
  profileImages: state.profileImages,
  highlights: state.highlights,
  stories: state.notLoggedInUserStories,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getPostsByUserId,
    getUserById,
    followProfile,
    getAllImagesForProfile,
    getHighlights,
    getStoriesForNotLogged,
  })
)(PublicProfile);
