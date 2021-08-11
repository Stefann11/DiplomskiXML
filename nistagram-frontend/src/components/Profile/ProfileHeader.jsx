import React, { useEffect, useState } from "react";
import { Profile } from "react-instagram-ui-kit";
import { getUserById, loadImageProfile } from "../../actions/actionsUser";
import { connect } from "react-redux";

function ProfileHeader(props) {
  const user = props.user;
  const postsCount = props.postsCount;

  useEffect(() => {
    props.loadImageProfile(props.user.profilePicturePath);
  }, [props.user]);

  debugger;
  if (props.user === undefined) return null;
  if (props.user.profilePicturePath !== "") {
    if (props.profileImage === undefined) return null;
  }

  return (
    <Profile
      bio={props.user.bio}
      pictureSrc={
        props.profileImage !== undefined
          ? "data:image/jpg;base64," + props.profileImage
          : "/images/user.png"
      }
      username={props.user.username}
      followersData={[
        postsCount,
        props.user.followers === undefined ? 0 : props.user.followers.length,
        props.user.following === undefined ? 0 : props.user.following.length,
      ]}
      fullname={props.user.firstName + " " + props.user.lastName}
    />
  );
}

const mapStateToProps = (state) => ({
  registeredUser: state.registeredUser,
  profileImage: state.profileImage,
});

export default connect(mapStateToProps, {
  getUserById,
  loadImageProfile,
})(ProfileHeader);
