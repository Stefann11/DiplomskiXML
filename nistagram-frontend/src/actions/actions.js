import {
  GET_POSTS_BY_USER_ID,
  GET_POSTS_BY_USER_ID_ERROR,
  GET_POST,
  GET_POST_ERROR,
  GET_HASHTAGS_BY_TEXT_ERROR,
  GET_POSTS_BY_HASHTAG,
  GET_POSTS_BY_HASHTAG_ERROR,
  GET_HASHTAGS_BY_TEXT,
  LOAD_IMAGE,
  LOAD_IMAGE_ERROR,
  SAVE_POST,
  SAVE_POST_ERROR,
  GET_LOCATIONS,
  GET_LOCATIONS_ERROR,
  GET_TAGGABLE_USERS,
  GET_TAGGABLE_USERS_ERROR,
  LIKE_POST,
  LIKE_POST_ERROR,
  DISLIKE_POST,
  DISLIKE_POST_ERROR,
  COMMENT_POST,
  COMMENT_POST_ERROR,
  GET_LOCATIONS_BY_TEXT,
  GET_LOCATIONS_BY_TEXT_ERROR,
  GET_POSTS_BY_LOCATION,
  GET_POSTS_BY_LOCATION_ERROR,
  LOAD_IMAGES,
  LOAD_IMAGES_ERROR,
  CLEAR_IMAGES,
  GET_USERS_BY_NAME,
  GET_USERS_BY_NAME_ERROR,
  GET_USER_BY_ID,
  GET_USER_BY_ID_ERROR,
  GET_COLLECTIONS_BY_USER,
  GET_COLLECTIONS_BY_USER_ERROR,
  ADD_POST_TO_COLLECTION,
  ADD_POST_TO_COLLECTION_ERROR,
  GET_POSTS_BY_COLLECTION_AND_USER,
  GET_POSTS_BY_COLLECTION_AND_USER_ERROR,
  GET_POSTS_FOR_FOLLOWING,
  GET_POSTS_FOR_FOLLOWING_ERROR,
  GET_ALL_IMAGES,
  GET_ALL_IMAGES_ERROR,
  CHANGE_PROFILE_PICTURE_POSTMICROSERVICE,
  CHANGE_PROFILE_PICTURE_POSTMICROSERVICE_ERROR,
  GET_ALL_IMAGES_FOR_SEARCH,
  GET_ALL_IMAGES_FOR_SEARCH_ERROR,
  GET_ALL_IMAGES_FOR_PROFILE,
  GET_ALL_IMAGES_FOR_PROFILE_ERROR,
  GET_ALL_IMAGES_FOR_COLLECTION,
  GET_ALL_IMAGES_FOR_COLLECTION_ERROR,
  CREATE_NEW_COLLECTION,
  CREATE_NEW_COLLECTION_ERROR,
  GET_LIKED_POSTS,
  GET_LIKED_POSTS_ERROR,
  GET_DISLIKED_POSTS,
  GET_DISLIKED_POSTS_ERROR,
  BAN_POST,
  BAN_POST_ERROR,
  GET_POSTS_FOR_CAMPAIGN_ERROR,
  GET_POSTS_FOR_CAMPAIGN,
} from "../types/types";
import axios from "axios";
import { getAdForContent } from "./actionsCampaign";

export const getPostsByUserId = (id) => async (dispatch) => {
  try {
    const response = await axios.get("https://localhost:44355/api/posts?", {
      params: { userid: id },
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    });
    dispatch({
      type: GET_POSTS_BY_USER_ID,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_POSTS_BY_USER_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const getPostsByHashTag = (hashtag) => async (dispatch) => {
  try {
    const response = await axios.get("https://localhost:44355/api/posts?", {
      params: { hashtag: hashtag, access: "public" },
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    });
    dispatch({
      type: GET_POSTS_BY_HASHTAG,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_POSTS_BY_HASHTAG_ERROR,
      payload: console.log(e),
    });
  }
};

export const getPostsByLocation =
  (country, city, street) => async (dispatch) => {
    try {
      const response = await axios.get("https://localhost:44355/api/posts?", {
        params: {
          country: country,
          city: city,
          street: street,
          access: "public",
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      });
      dispatch({
        type: GET_POSTS_BY_LOCATION,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: GET_POSTS_BY_LOCATION_ERROR,
        payload: console.log(e),
      });
    }
  };

export const getPost = (id) => async (dispatch) => {
  try {
    const response = await axios
      .get("https://localhost:44355/api/posts/" + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      })
      .then(async function (response) {
        dispatch({ type: CLEAR_IMAGES });
        dispatch({
          type: GET_POST,
          payload: response.data,
        });
        if (response.data.contentPath == undefined) {
          var i;
          for (i = 0; i < response.data.contentPaths.length; i++) {
            const response2 = await axios
              .get(
                "https://localhost:44355/api/posts/contents/" +
                  response.data.contentPaths[i],
                {
                  headers: {
                    "Access-Control-Allow-Origin": "*",
                    Authorization: "Bearer " + sessionStorage.getItem("token"),
                  },
                }
              )
              .then(function (response2) {
                dispatch({
                  type: LOAD_IMAGES,
                  payload: response2.data,
                });
              });
          }
        } else {
          const response2 = await axios
            .get(
              "https://localhost:44355/api/posts/contents/" +
                response.data.contentPath,
              {
                headers: {
                  "Access-Control-Allow-Origin": "*",
                  Authorization: "Bearer " + sessionStorage.getItem("token"),
                },
              }
            )
            .then(function (response2) {
              dispatch({
                type: LOAD_IMAGE,
                payload: response2.data,
              });
            });
        }
      });
  } catch (e) {
    dispatch({
      type: GET_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const loadImage = (path) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/posts/contents/" + path,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: LOAD_IMAGE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: LOAD_IMAGE_ERROR,
      payload: console.log(e),
    });
  }
};

export const loadImages = (path) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/posts/contents/" + path,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: LOAD_IMAGES,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: LOAD_IMAGES_ERROR,
      payload: console.log(e),
    });
  }
};

export const savePost = (post) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/posts/",
      post,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: SAVE_POST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: SAVE_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const getHashTagsByText = (text) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/hashtags?text=" + text,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_HASHTAGS_BY_TEXT,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_HASHTAGS_BY_TEXT_ERROR,
      payload: console.log(e),
    });
  }
};

export const getLocationsByText = (text) => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/locations?text=" + text,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_LOCATIONS_BY_TEXT,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_LOCATIONS_BY_TEXT_ERROR,
      payload: console.log(e),
    });
  }
};

export const getLocations = () => async (dispatch) => {
  try {
    const response = await axios.get("https://localhost:44355/api/locations", {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    });
    dispatch({
      type: GET_LOCATIONS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_LOCATIONS_ERROR,
      payload: console.log(e),
    });
  }
};

export const getTaggableUsers = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/users?isTaggable=" + true,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_TAGGABLE_USERS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_TAGGABLE_USERS_ERROR,
      payload: console.log(e),
    });
  }
};

export const likePost = (dto) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/posts/" +
        dto.id +
        "/users/" +
        dto.userId +
        "/likes",
      {},
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: LIKE_POST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: LIKE_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const dislikePost = (dto) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/posts/" +
        dto.id +
        "/users/" +
        dto.userId +
        "/dislikes",
      {},
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: DISLIKE_POST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: DISLIKE_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const commentPost = (dto) => async (dispatch) => {
  try {
    const comment = dto.comment;
    const response = await axios.post(
      "https://localhost:44355/api/posts/" + dto.id + "/comments",
      comment,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: COMMENT_POST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: COMMENT_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const getCollectionsByUser = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/collections?userId=" +
        sessionStorage.getItem("userId"),
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_COLLECTIONS_BY_USER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_COLLECTIONS_BY_USER_ERROR,
      payload: console.log(e),
    });
  }
};

export const addPostToCollection = (dto) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/collections/" +
        dto.id +
        "/posts/" +
        dto.post.id,
      {},
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: ADD_POST_TO_COLLECTION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: ADD_POST_TO_COLLECTION_ERROR,
      payload: console.log(e),
    });
  }
};

export const getPostsByCollectionAndUser =
  (collectionId) => async (dispatch) => {
    try {
      const response = await axios.get(
        "https://localhost:44355/api/posts/collections/" +
          collectionId +
          "/users/" +
          sessionStorage.getItem("userId"),
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("token"),
          },
        }
      );
      dispatch({
        type: GET_POSTS_BY_COLLECTION_AND_USER,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: GET_POSTS_BY_COLLECTION_AND_USER_ERROR,
        payload: console.log(e),
      });
    }
  };

export const getPostsForFollowing = (users) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/posts/following",
      users,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_POSTS_FOR_FOLLOWING,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_POSTS_FOR_FOLLOWING_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAllImages = (posts) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/posts/images",
      posts,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_ALL_IMAGES,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ALL_IMAGES_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAllImagesForSearch = (posts) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/posts/images",
      posts,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_ALL_IMAGES_FOR_SEARCH,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ALL_IMAGES_FOR_SEARCH_ERROR,
      payload: console.log(e),
    });
  }
};

export const changeProfilePicturePostmicroservice =
  (picture) => async (dispatch) => {
    try {
      const response = await axios.put(
        "https://localhost:44355/api/post-microservice/users/" +
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
        type: CHANGE_PROFILE_PICTURE_POSTMICROSERVICE,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: CHANGE_PROFILE_PICTURE_POSTMICROSERVICE_ERROR,
        payload: console.log(e),
      });
    }
  };

export const getAllImagesForProfile = (posts) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/posts/images",
      posts,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_ALL_IMAGES_FOR_PROFILE,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ALL_IMAGES_FOR_PROFILE_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAllImagesForCollection = (posts) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/posts/images",
      posts,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_ALL_IMAGES_FOR_COLLECTION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ALL_IMAGES_FOR_COLLECTION_ERROR,
      payload: console.log(e),
    });
  }
};

export const createNewCollection = (collection) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/collections",
      collection,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: CREATE_NEW_COLLECTION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_NEW_COLLECTION_ERROR,
      payload: console.log(e),
    });
  }
};

export const getLikedPosts = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/posts/liked/" +
        sessionStorage.getItem("userId"),
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_LIKED_POSTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_LIKED_POSTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const getDislikedPosts = () => async (dispatch) => {
  try {
    const response = await axios.get(
      "https://localhost:44355/api/posts/disliked/" +
        sessionStorage.getItem("userId"),
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_DISLIKED_POSTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_DISLIKED_POSTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const banPost = (id) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/posts/" + id + "/ban",
      {},
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: BAN_POST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: BAN_POST_ERROR,
      payload: console.log(e),
    });
  }
};

export const getPostsForCampaign = (id) => async (dispatch) => {
  try {
    const response = await axios.get(
      `https://localhost:44355/api/posts/${id}`,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    const response2 = await axios.get(`https://localhost:44355/api/ads`, {
      params: {
        contentId: response.data.id,
      },
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    });
    var payload = response.data;
    payload.link = response2.data.link;
    dispatch({
      type: GET_POSTS_FOR_CAMPAIGN,
      payload: payload,
    });
  } catch (e) {
    dispatch({
      type: GET_POSTS_FOR_CAMPAIGN_ERROR,
      payload: console.log(e),
    });
  }
};
