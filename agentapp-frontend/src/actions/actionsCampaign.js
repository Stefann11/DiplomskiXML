import {
  GET_CAMPAIGNS_FOR_AGENT,
  GET_CAMPAIGNS_FOR_AGENT_ERROR,
  CREATE_CAMPAIGN_REQUEST,
  CREATE_CAMPAIGN_REQUEST_ERROR,
  GET_CAMPAIGN_REQUESTS,
  GET_CAMPAIGN_REQUESTS_ERROR,
  UPDATE_CAMPAIGN_REQUEST,
  UPDATE_CAMPAIGN_REQUEST_ERROR,
  GET_CAMPAIGN_REQUESTS_FOR_USER_PROFILE,
  GET_CAMPAIGN_REQUESTS_FOR_USER_PROFILE_ERROR,
  CREATE_CAMPAIGN,
  CREATE_CAMPAIGN_ERROR,
  GET_AD_FOR_CONTENT,
  GET_AD_FOR_CONTENT_ERROR,
  CREATE_AD,
  CREATE_AD_ERROR,
} from "../types/types";
import axios from "axios";

export const getCampaignsForAgent = () => async (dispatch) => {
  try {
    const response = await axios.get(`https://localhost:44355/api/campaigns`, {
      params: { agentId: sessionStorage.getItem("savedUserId") },
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
      },
    });
    dispatch({
      type: GET_CAMPAIGNS_FOR_AGENT,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_CAMPAIGNS_FOR_AGENT_ERROR,
      payload: console.log(e),
    });
  }
};

export const createCampaignRequest = (campaignRequest) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44355/api/campaignRequests",
      campaignRequest,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
        },
      }
    );
    dispatch({
      type: CREATE_CAMPAIGN_REQUEST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: CREATE_CAMPAIGN_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const getCampaignRequests = () => async (dispatch) => {
  try {
    const response = await axios.get(
      `https://localhost:44355/api/campaignRequests`,
      {
        params: {
          userId: sessionStorage.getItem("savedUserId"),
          "is-approved": "false",
          action: "created",
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
        },
      }
    );
    dispatch({
      type: GET_CAMPAIGN_REQUESTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_CAMPAIGN_REQUESTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const updateCampaignRequest = (campaignRequest) => async (dispatch) => {
  try {
    const response = await axios.put(
      "https://localhost:44355/api/campaignRequests",
      campaignRequest,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
        },
      }
    );
    dispatch({
      type: UPDATE_CAMPAIGN_REQUEST,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: UPDATE_CAMPAIGN_REQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const createCampaign = (campaign) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.post(
      `https://localhost:44355/api/campaigns`,
      campaign,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
        },
      }
    );
    debugger;
    dispatch({
      type: CREATE_CAMPAIGN,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_CAMPAIGN_ERROR,
      payload: console.log(e),
    });
  }
};

export const getCampaignRequestsForUserProfile =
  (parameters) => async (dispatch) => {
    try {
      const response = await axios.get(
        `https://localhost:44355/api/campaignRequests`,
        {
          params: {
            userId: parameters.userId,
            "is-approved": parameters.isApproved,
            action: parameters.action,
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
          },
        }
      );
      dispatch({
        type: GET_CAMPAIGN_REQUESTS_FOR_USER_PROFILE,
        payload: response.data,
      });
    } catch (e) {
      dispatch({
        type: GET_CAMPAIGN_REQUESTS_FOR_USER_PROFILE_ERROR,
        payload: console.log(e),
      });
    }
  };

export const getAdForContent = (contentId) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get(`https://localhost:44355/api/ads`, {
      params: {
        contentId: contentId,
      },
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
      },
    });
    dispatch({
      type: GET_AD_FOR_CONTENT,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: GET_AD_FOR_CONTENT_ERROR,
      payload: console.log(e),
    });
  }
};

export const createAd = (ad) => async (dispatch) => {
  try {
    const response = await axios.post("https://localhost:44355/api/ads", ad, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("savedToken"),
      },
    });
    dispatch({
      type: CREATE_AD,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_AD_ERROR,
      payload: console.log(e),
    });
  }
};
