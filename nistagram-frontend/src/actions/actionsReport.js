import {
  REPORT_CONTENT,
  REPORT_CONTENT_ERROR,
  GET_REPORTS,
  GET_REPORTS_ERROR,
  EDIT_REPORT,
  EDIT_REPORT_ERROR,
} from "../types/types";
import axios from "axios";

export const reportContent = (report) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.post(
      "https://localhost:44355/api/reports",
      report,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: REPORT_CONTENT,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: REPORT_CONTENT_ERROR,
      payload: console.log(e),
    });
  }
};

export const getReports = () => async (dispatch) => {
  debugger;
  try {
    const response = await axios.get("https://localhost:44355/api/reports", {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    });
    dispatch({
      type: GET_REPORTS,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_REPORTS_ERROR,
      payload: console.log(e),
    });
  }
};

export const editReport = (report) => async (dispatch) => {
  debugger;
  try {
    const response = await axios.put(
      "https://localhost:44355/api/reports",
      report,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    );
    dispatch({
      type: EDIT_REPORT,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: EDIT_REPORT_ERROR,
      payload: console.log(e),
    });
  }
};
