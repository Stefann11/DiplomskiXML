import {
  GET_ITEMS,
  GET_ITEMS_ERROR,
  CREATE_ITEM,
  CREATE_ITEM_ERROR,
  GET_ITEM_BY_ID,
  GET_ITEM_BY_ID_ERROR,
  LOAD_IMAGE,
  LOAD_IMAGE_ERROR,
  EDIT_ITEM,
  EDIT_ITEM_ERROR,
  DELETE_ITEM,
  DELETE_ITEM_ERROR,
  BUY_ITEM,
  BUY_ITEM_ERROR,
} from "../types/types";
import axios from "axios";

export const getItems = () => async (dispatch) => {
  try {
    const response = await axios.get("http://localhost:55744/api/items", {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
      },
    });
    debugger;
    dispatch({
      type: GET_ITEMS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_ITEMS_ERROR,
      payload: console.log(e),
    });
  }
};

export const createItem = (item) => async (dispatch) => {
  try {
    const response = await axios.post(
      "http://localhost:55744/api/items",
      item,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    dispatch({
      type: CREATE_ITEM,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: CREATE_ITEM_ERROR,
      payload: console.log(e),
    });
  }
};

export const getItemById = (id) => async (dispatch) => {
  try {
    const response = await axios
      .get("http://localhost:55744/api/items/" + id, {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      })
      .then(async function (response) {
        dispatch({
          type: GET_ITEM_BY_ID,
          payload: response.data,
        });
        const response2 = axios
          .get(
            "http://localhost:55744/api/contents/" +
              response.data.itemImagePath,
            {
              headers: {
                "Access-Control-Allow-Origin": "*",
                Authorization:
                  "Bearer " + sessionStorage.getItem("tokenAgentApp"),
              },
            }
          )
          .then(function (response2) {
            dispatch({
              type: LOAD_IMAGE,
              payload: response2.data,
            });
          });
      });
  } catch (e) {
    dispatch({
      type: GET_ITEM_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const loadImage = (path) => async (dispatch) => {
  try {
    const response = await axios.get(
      "http://localhost:55744/api/contents/" + path,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
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

export const editItem = (item) => async (dispatch) => {
  try {
    const response = await axios.put("http://localhost:55744/api/items", item, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
      },
    });
    dispatch({
      type: EDIT_ITEM,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_ITEM_ERROR,
      payload: console.log(e),
    });
  }
};

export const deleteItem = (item) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.delete("http://localhost:55744/api/items", {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
      },
      data: item,
    });
    debugger;
    dispatch({
      type: DELETE_ITEM,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: DELETE_ITEM_ERROR,
      payload: console.log(e),
    });
  }
};

export const buyItem = (item) => async (dispatch) => {
  try {
    const response = await axios.put(
      "http://localhost:55744/api/items/buy",
      item,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    dispatch({
      type: BUY_ITEM,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: BUY_ITEM_ERROR,
      payload: console.log(e),
    });
  }
};
