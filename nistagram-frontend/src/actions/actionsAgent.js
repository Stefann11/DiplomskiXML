import {
  CREATE_AGENT_REQUEST,
  CREATE_AGENT_REQUEST_ERROR,
  GET_AGENT_REQUESTS,
  GET_AGENT_REQUESTS_ERROR,
  UPDATE_AGENT_REQUEST,
  UPDATE_AGENT_REQUEST_ERROR,
  CREATE_AGENT_NOT_EXISTING,
  CREATE_AGENT_NOT_EXISTING_ERROR,
} from "../types/types";
import axios from "axios";

export const createAgentRequest = (request) => async (dispatch) => {
  try {
    const response = await axios.post(
      `https://localhost:44355/api/agentRequests`,
      request,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: CREATE_AGENT_REQUEST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_AGENT_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const getAgentRequests = () => async (dispatch) => {
  try {
    const response = await axios.get(
      `https://localhost:44355/api/agentRequests`,
      {
        params: { "is-approved": "false" },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: GET_AGENT_REQUESTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_AGENT_REQUESTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const updateAgentRequest = (agentRequest) => async (dispatch) => {
  try {
    const response = await axios.put(
      `https://localhost:44355/api/agentRequests`,
      agentRequest,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: UPDATE_AGENT_REQUEST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: UPDATE_AGENT_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};
