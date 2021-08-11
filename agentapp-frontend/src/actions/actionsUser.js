import {
  GET_LOGGED_USER,
  GET_LOGGED_USER_ERROR,
  REGISTER_USER,
  REGISTER_USER_ERROR,
} from "../types/types";
import axios from "axios";

export const userRegistration = (user) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "http://localhost:55744/api/users",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
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
      "http://localhost:55744/api/users/login",
      user,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    var parts = response.data.token.split("."); // header, payload, signature
    var userInfo = JSON.parse(atob(parts[1]));
    sessionStorage.setItem("tokenAgentApp", response.data.token);
    sessionStorage.setItem("userIdAgentApp", userInfo.user_id);
    sessionStorage.setItem("roleAgentApp", userInfo.role);
    sessionStorage.setItem("usernameAgentApp", userInfo.username);
    debugger;
    return true;
  } catch (e) {
    console.log(e);
  }
};
