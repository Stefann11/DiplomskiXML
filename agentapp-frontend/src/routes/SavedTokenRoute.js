import React from "react";
import { Route, Redirect } from "react-router-dom";

function SavedTokenRoute({ component: Component, ...rest }) {
  debugger;
  const isAuthorized = sessionStorage.getItem("savedToken");
  return (
    <Route
      {...rest}
      render={(props) => {
        return isAuthorized ? (
          <Component />
        ) : (
          <Redirect
            to={{ pathname: "/token", state: { from: props.location } }}
          />
        );
      }}
    />
  );
}

export default SavedTokenRoute;
