import React from "react";
import { Route, Redirect } from "react-router-dom";

function ProtectedRoute({ component: Component, ...rest }) {
  debugger;
  const isAuthorized = sessionStorage.getItem("tokenAgentApp");
  return (
    <Route
      {...rest}
      render={(props) => {
        return isAuthorized ? (
          <Component />
        ) : (
          <Redirect
            to={{ pathname: "/login", state: { from: props.location } }}
          />
        );
      }}
    />
  );
}

export default ProtectedRoute;
