import React from "react";
import "./App.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import ItemsTablePage from "./pages/ItemsTablePage";
import CreateItemPage from "./pages/CreateItemPage";
import ReviewItemPage from "./pages/ReviewItemPage";
import EditItemPage from "./pages/EditItemPage";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";
import ProtectedRoute from "./routes/ProtectedRoute";
import CreateCampaignPage from "./pages/CreateCampaignPage";
import EnterTokenPage from "./pages/EnterTokenPage";
import SavedTokenRoute from "./routes/SavedTokenRoute";

function App() {
  return (
    <Router>
      <div>
        <Switch>
          <ProtectedRoute exact path="/" component={ItemsTablePage} />
          <ProtectedRoute exact path="/create" component={CreateItemPage} />
          <Route path="/items/:itemId" component={ReviewItemPage} />
          <ProtectedRoute exact path="/edit-item" component={EditItemPage} />
          <SavedTokenRoute
            exact
            path="/campaign"
            component={CreateCampaignPage}
          />
          <ProtectedRoute exact path="/token" component={EnterTokenPage} />
          <Route exact path="/login" component={LoginPage} />
          <Route exact path="/registration" component={RegistrationPage} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
