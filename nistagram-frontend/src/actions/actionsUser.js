import {
  REGISTER_USER,
  REGISTER_USER_ERROR,
  GET_LOGGED_USER,
  GET_LOGGED_USER_ERROR,
  EDIT_USER,
  EDIT_USER_ERROR,
  GET_USERS_BY_NAME,
  GET_USERS_BY_NAME_ERROR,
  GET_USER_BY_ID,
  GET_USER_BY_ID_ERROR,
  EDIT_USER_POST,
  EDIT_USER_POST_ERROR,
  EDIT_USER_STORY,
  EDIT_USER_STORY_ERROR,
  FOLLOW_PROFILE,
  FOLLOW_PROFILE_ERROR,
  GET_FOLLOW_REQUESTS,
  GET_FOLLOW_REQUESTS_ERROR,
  HANDLE_REQUESTS,
  HANDLE_REQUESTS_ERROR,
  GET_FOLLOWING,
  GET_FOLLOWING_ERROR,
  CHANGE_PROFILE_PICTURE_USERMICROSERVICE,
  CHANGE_PROFILE_PICTURE_USERMICROSERVICE_ERROR,
  LOAD_IMAGE_PROFILE,
  LOAD_IMAGE_PROFILE_ERROR,
  ADD_CLOSE_FRIEND,
  ADD_CLOSE_FRIEND_ERROR,
  SEND_VERIFICATION_REQUEST,
  SEND_VERIFICATION_REQUEST_ERROR,
  GET_UNAPPROVED_VERIFICATION_REQUESTS,
  GET_UNAPPROVED_VERIFICATION_REQUESTS_ERROR,
  GET_FOLLOWING_WITHOUT_MUTED,
  GET_FOLLOWING_WITHOUT_MUTED_ERROR,
  MUTE_PROFILE,
  MUTE_PROFILE_ERROR,
  BLOCK_PROFILE,
  BLOCK_PROFILE_ERROR,
  DELETE_VERIFICATION_REQUEST,
  DELETE_VERIFICATION_REQUEST_ERROR,
  VERIFY_USER,
  VERIFY_USER_ERROR,
  BAN_USER,
  BAN_USER_ERROR,
} from "../types/types";
import axios from "axios";

export const userRegistration = (user) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/users",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: REGISTER_USER,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: REGISTER_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const userRegistrationForPost = (user) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/usersforpost",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: REGISTER_USER,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: REGISTER_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const userRegistrationForStory = (user) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/usersforstory",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: REGISTER_USER,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: REGISTER_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const userLoggedIn = (user) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/users/login",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    var parts = response.data.token.split("."); // header, payload, signature
    var userInfo = JSON.parse(atob(parts[1]));
    sessionStorage.setItem("token", response.data.token);
    sessionStorage.setItem("userId", userInfo.user_id);
    sessionStorage.setItem("role", userInfo.role);
    sessionStorage.setItem("username", userInfo.username);
    return true;
  } catch (e) {
    console.log(e);
  }
};

export const getLoggedUser = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/" + sessionStorage.getItem("userId"),
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_LOGGED_USER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_LOGGED_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const editUser = (user) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/users/edit",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: EDIT_USER,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: EDIT_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const editUserForPost = (user) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/users/editforpost",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: EDIT_USER_POST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: EDIT_USER_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const editUserForStory = (user) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/story-users/" +
        sessionStorage.getItem("userId"),
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: EDIT_USER_STORY,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: EDIT_USER_STORY_ERROR,
      payload: console.log(e),
    });
  }
};

export const getUsersByName = (name) => async (dispatch) => {
  try {
    var response = {};
    if (sessionStorage.getItem("token") === "") {
      response = await axios.get("https://localhost:44355/api/users?", {
        params: {
          name: name,
          access: "public",
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      });
    } else {
      response = await axios.get("https://localhost:44355/api/users?", {
        params: {
          id: sessionStorage.getItem("userId"),
          name: name,
          access: "public",
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      });
    }
    dispatch({
      type: GET_USERS_BY_NAME,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_USERS_BY_NAME_ERROR,
      payload: console.log(e),
    });
  }
};

export const getUserById = (id) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/" + id,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_USER_BY_ID,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_USER_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const getUserByIdWithoutBlocked = (id) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/" +
        sessionStorage.getItem("userId") +
        "/logged/" +
        id +
        "/user",
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_USER_BY_ID,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_USER_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const followProfile = (follow) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/users/follow",
      follow,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: FOLLOW_PROFILE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: FOLLOW_PROFILE_ERROR,
      payload: console.log(e),
    });
  }
};

export const getFollowRequests = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/" +
        sessionStorage.getItem("userId") +
        "/followRequests",
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_FOLLOW_REQUESTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_FOLLOW_REQUESTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const handleRequests = (follow) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/users/handlerequest",
      follow,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: HANDLE_REQUESTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: HANDLE_REQUESTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const getFollowing = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/" +
        sessionStorage.getItem("userId") +
        "/following",
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_FOLLOWING,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_FOLLOWING_ERROR,
      payload: console.log(e),
    });
  }
};

export const getFollowingWithoutMuted = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/" +
        sessionStorage.getItem("userId") +
        "/following-without-muted",
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_FOLLOWING_WITHOUT_MUTED,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_FOLLOWING_WITHOUT_MUTED_ERROR,
      payload: console.log(e),
    });
  }
};

export const changeProfilePictureUsermicroservice =
  (picture) => async (dispatch) => {
    try {
      const response = await axios.put(
        "https://localhost:44355/api/users/" +
          sessionStorage.getItem("userId") +
          "/profile-picture/" +
          picture,
        {},
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("token"),
          },
        }
      );

      dispatch({
        type: CHANGE_PROFILE_PICTURE_USERMICROSERVICE,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: CHANGE_PROFILE_PICTURE_USERMICROSERVICE_ERROR,
        payload: console.log(e),
      });
    }
  };

export const loadImageProfile = (path) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users/contents/" + path,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );

    dispatch({
      type: LOAD_IMAGE_PROFILE,
      payload: response.data.fileContents,
    });
  } catch (e) {
    dispatch({
      type: LOAD_IMAGE_PROFILE_ERROR,
      payload: console.log(e),
    });
  }
};

export const addCloseFriend = (userId) => async (dispatch) => {
  try {
    const response = await axios.put(
      `https://localhost:44355/api/users/${sessionStorage.getItem(
        "userId"
      )}/close-friends/${userId}`,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: ADD_CLOSE_FRIEND,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: ADD_CLOSE_FRIEND_ERROR,
      payload: console.log(e),
    });
  }
};

export const muteProfile = (mute) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/users/mute",
      mute,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: MUTE_PROFILE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: MUTE_PROFILE_ERROR,
      payload: console.log(e),
    });
  }
};

export const blockProfile = (block) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/users/block",
      block,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: BLOCK_PROFILE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: BLOCK_PROFILE_ERROR,
      payload: console.log(e),
    });
  }
};

export const sendVerificationRequest = (request) => async (dispatch) => {
  try {
    const response = await axios.post(
      `https://localhost:44355/api/VerificationRequests`,
      request,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: SEND_VERIFICATION_REQUEST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: SEND_VERIFICATION_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const getUnapprovedVerificationRequests = () => async (dispatch) => {
  try {
    const response = await axios.get(
      `https://localhost:44355/api/VerificationRequests`,
      {
        params: { "is-approved": "false" },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_UNAPPROVED_VERIFICATION_REQUESTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_UNAPPROVED_VERIFICATION_REQUESTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const deleteVerificationRequest = (request) => async (dispatch) => {
  try {
    const response = await axios.delete(
      `https://localhost:44355/api/VerificationRequests/${request.id}`,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: DELETE_VERIFICATION_REQUEST,
      payload: request,
    });
  } catch (e) {
    dispatch({
      type: DELETE_VERIFICATION_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const verifyUser = (request) => async (dispatch) => {
  request.registeredUserId = request.registeredUser.id;
  try {
    const response = await axios.put(
      `https://localhost:44355/api/VerificationRequests/${request.id}`,
      request,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: VERIFY_USER,
      payload: request,
    });
  } catch (e) {
    dispatch({
      type: VERIFY_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const banUser = (id) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/users/" + id + "/ban",
      {},
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: BAN_USER,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: BAN_USER_ERROR,
      payload: console.log(e),
    });
  }
};
