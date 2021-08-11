import React from "react";
import "./App.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import ProfilePage from "./pages/ProfilePage";
import PrivateProfilePage from "./pages/PrivateProfilePage";
import SomeoneProfilePage from "./pages/SomeoneProfilePage";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import CreatePostPage from "./pages/CreatePostPage";
import EditProfilePage from "./pages/EditProfilePage";
import VerificationRequestPage from "./pages/VerificationRequestPage";
import PrivacyPage from "./pages/PrivacyPage";
import RegistrationPage from "./pages/RegistrationPage";
import ApprovalAgentPage from "./pages/ApprovalAgentPage";
import CreateItemPage from "./pages/CreateItemPage";
import ReviewItemPage from "./pages/ReviewItemPage";
import Explore from "./pages/Explore";
import ProtectedRoute from "./routes/ProtectedRoute";
import PublicProfile from "./components/Profile/PublicProfile";
import CollectionsMenuPage from "./pages/CollectionsMenuPage";
import PostsInCollectionPage from "./pages/PostsInCollectionPage";
import FollowRequestPage from "./pages/FollowRequestsPage";
import CreateStoryPage from "./pages/CreateStoryPage";
import ChangeProfilePicturePage from "./pages/ChangeProfilePicturePage";
import SendVerificationRequestPage from "./pages/SendVerificationRequestPage";
import ViewVerificationRequestPage from "./pages/ViewVerificationRequestPage";
import LikedPostsPage from "./pages/LikedPostsPage";
import DislikedPostsPage from "./pages/DislikedPostsPage";
import ChangeNotificationSettingsPage from "./pages/ChangeNotificationSettingsPage";
import NotificationsPage from "./pages/NotificationsPage";
import ReportsPage from "./pages/ReportsPage";
import AgentRegistrationPage from "./pages/AgentRegistrationPage";
import NotLoggedAgentRegistrationPage from "./pages/NotLoggedAgentRegistrationPage";
import CreateCampaign from "./components/Campaign/CreateCampaign";
import CreateCampaignPage from "./pages/CreateCampaignPage";
import CampaignRequestsPage from "./pages/CampaignRequestsPage";
import EditCampaignPage from "./pages/EditCampaignPage"
import TokenPage from "./pages/TokenPage";
import AgentProtectedRoute from "./routes/AgentProtectedRoute";

function App() {
  return (
    <Router>
      <div>
        <Switch>
          <Route exact path="/registration">
            <RegistrationPage />
          </Route>
          <Route exact path="/login">
            <LoginPage />
          </Route>
          <Route exact path="/not-logged-agent-registration">
            <NotLoggedAgentRegistrationPage />
          </Route>
          <ProtectedRoute exact path="/post" component={CreatePostPage} />
          <ProtectedRoute exact path="/story" component={CreateStoryPage} />
          <ProtectedRoute
            exact
            path="/private"
            component={PrivateProfilePage}
          />
          <ProtectedRoute
            exact
            path="/someone"
            component={SomeoneProfilePage}
          />
          <ProtectedRoute exact path="/privacy" component={PrivacyPage} />
          <ProtectedRoute exact path="/edit" component={EditProfilePage} />
          <ProtectedRoute
            exact
            path="/verification"
            component={VerificationRequestPage}
          />
          <ProtectedRoute
            exact
            path="/approval"
            component={ApprovalAgentPage}
          />
          <ProtectedRoute
            exact
            path="/create-item"
            component={CreateItemPage}
          />
          <ProtectedRoute
            exact
            path="/review-item"
            component={ReviewItemPage}
          />
          <ProtectedRoute
            exact
            path="/collections"
            component={CollectionsMenuPage}
          />
          <ProtectedRoute
            exact
            path="/requests"
            component={FollowRequestPage}
          />
          <ProtectedRoute
            exact
            path="/sendVerificationRequest"
            component={SendVerificationRequestPage}
          />
          <ProtectedRoute exact path="/liked" component={LikedPostsPage} />
          <ProtectedRoute
            exact
            path="/disliked"
            component={DislikedPostsPage}
          />
          <Route exact path="/change-profile-picture">
            <ChangeProfilePicturePage />
          </Route>
          <Route exact path="/reports">
            <ReportsPage />
          </Route>
          <Route exact path="/agent-registration">
            <AgentRegistrationPage />
          </Route>
          <ProtectedRoute
            path="/change-notification-settings"
            component={ChangeNotificationSettingsPage}
          />
          <ProtectedRoute
            exact
            path="/notifications"
            component={NotificationsPage}
          />
          <Route path="/explore/:search">
            <Explore />
          </Route>
          <Route path="/profile/:username" component={ProfilePage}></Route>
          <ProtectedRoute
            path="/collection/:collectionId"
            component={PostsInCollectionPage}
          />
          <ProtectedRoute
            path="/viewVerificationRequests"
            component={ViewVerificationRequestPage}
          />
            <ProtectedRoute
            path="/edit-campaign"
            component={EditCampaignPage}
          />         
          <ProtectedRoute
            path="/campaign-requests"
            component={CampaignRequestsPage}
          />
          <AgentProtectedRoute
            exact
            path="/campaign"
            component={CreateCampaignPage}
          />
          <ProtectedRoute exact path="/token" component={TokenPage} />
          <ProtectedRoute exact path="/" component={HomePage} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
