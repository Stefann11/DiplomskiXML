import React from "react";
import { Route, Redirect } from "react-router-dom";

function AgentProtectedRoute({ component: Component, ...rest }) {
  debugger;
  const isAuthorized = sessionStorage.getItem("role") === "Agent";
  return (
    <Route
      {...rest}
      render={(props) => {
        return isAuthorized ? (
          <Component />
        ) : (
          <Redirect to={{ pathname: "/", state: { from: props.location } }} />
        );
      }}
    />
  );
}

export default AgentProtectedRoute;
