import React, { useEffect, useState } from "react";
import Stories from "react-insta-stories";
import { Modal, ModalBody } from "reactstrap";
import {
  getStoriesForModal,
  loadImagesStory,
} from "../../actions/actionsStory";
import { connect } from "react-redux";
import "../../css/story.css";
import TaggedUsersProfileStoryModal from "./TaggedUsersProfileStoryModal";
import CollectionsModal from "./CollectionsModal";
import ReportModal from "../Report/ReportModal";
import { getAdForContent } from "../../actions/actionsCampaign";
import StoryLinkModal from "../Campaign/StoryLinkModal";

function ProfileStoryModal(props) {
  const [showProfileStoryModal, setShowProfileStoryModal] = useState(
    props.show
  );
  const [showTaggedUsersModal, setShowTaggedUsersModal] = useState(false);
  const [showLinkModal, setShowLinkModal] = useState(false);
  const [showCollectionsModal, setShowCollectionsModal] = useState(false);
  const [showReportModal, setShowReportModal] = useState(false);
  const [users, setUsers] = useState([]);
  const [story, setStory] = useState({});
  const [results, setResults] = useState([]);
  const [contentId, setContentId] = useState("");
  var tempStories = [];

  const timeSince = (date) => {
    var correctDate = new Date(Date.parse(date));
    var seconds = Math.floor(
      (new Date() - new Date(Date.parse(correctDate))) / 1000
    );

    var interval = seconds / 31536000;

    if (interval > 1) {
      return Math.floor(interval) + " years";
    }
    interval = seconds / 2592000;
    if (interval > 1) {
      return Math.floor(interval) + " months";
    }
    interval = seconds / 86400;
    if (interval > 1) {
      return Math.floor(interval) + " days";
    }
    interval = seconds / 3600;
    if (interval > 1) {
      return Math.floor(interval) + " hours";
    }
    interval = seconds / 60;
    if (interval > 1) {
      return Math.floor(interval) + " minutes";
    }
    return Math.floor(seconds) + " seconds";
  };

  const convertHashtagsToString = (hashtags) => {
    debugger;
    var temp = [...hashtags];
    var text = temp[0];
    temp.shift();
    temp.forEach((element) => {
      text += " " + element;
    });
    return text;
  };

  const createImagesList = () => {
    var images = [];
    props.stories.forEach((element) => {
      images.push(element.contentPath);
    });
    return images;
  };

  const toggle = () => {
    debugger;
    setShowProfileStoryModal(false);
    props.onShowChange();
  };

  const displayModalStory = (users) => {
    debugger;
    setShowTaggedUsersModal(!showTaggedUsersModal);
    setUsers(users);
  };

  const displayModalLink = (contentId) => {
    debugger;
    setShowLinkModal(!showLinkModal);
    setContentId(contentId);
  };

  const displayModalReport = (story) => {
    debugger;
    setShowReportModal(!showReportModal);
    setStory(story);
  };

  const displayCollectionsModal = (story) => {
    debugger;
    setShowCollectionsModal(!showCollectionsModal);
    setStory(story);
  };

  useEffect(() => {
    debugger;
    props.loadImagesStory(createImagesList(props.stories));
  }, [props.stories]);

  useEffect(() => {
    async function fetchMyAPI() {
      var results2 = [];
      for (var i = 0; i < props.stories.length; i++) {
        var result = false;
        result = await props.getAdForContent(props.stories[i].id);
        results2.push(result);
      }
      debugger;
      setResults(results2);
    }

    fetchMyAPI();
  }, [props.stories]);

  useEffect(() => {
    debugger;
    props.loadImagesStory(createImagesList(props.stories));
    tempStories = props.images;
  }, [props.highlight]);

  if (
    props.stories === undefined ||
    props.images === undefined ||
    props.stories.length !== props.images.length
  )
    return null;
  debugger;
  const stories = [];
  props.stories.forEach((story, i) => {
    stories.push({
      url:
        props.images[i].contentType === "image/jpeg"
          ? "data:image/jpg;base64," + props.images[i].fileContents
          : "data:video/mp4;base64," + props.images[i].fileContents,
      duration: 1000 * story.duration,
      type: props.images[i].contentType !== "image/jpeg" ? "video" : "image",
      header: {
        heading:
          story.registeredUser.firstName + " " + story.registeredUser.lastName,
        subheading: `Posted ${timeSince(story.timeStamp)} ago`,
      },
      seeMore: ({ close }) => {
        return (
          <div>
            {results[i] === true ? (
              <div className="ml-4 mb-2" style={{ textAlign: "right" }}>
                {" "}
                <b>Sponsored</b>{" "}
                <img classNmae="ml-4 mb-4" src="/images/star.png" />
              </div>
            ) : (
              ""
            )}
            {showReportModal ? (
              <ReportModal
                show={showReportModal}
                contentId={story.id}
                type="story"
                registeredUser={story.registeredUser}
                onShowChange={displayModalReport}
              />
            ) : null}
            <div
              onClick={close}
              style={{
                bottom: 0,
                position: "absolute",
                zIndex: 10,
                width: "100%",
                backgroundColor: "black",
                opacity: "50%",
                color: "white",
                height: "300px",
              }}
            ></div>
            <div className="story-footer-description">{story.description}</div>
            <div className="story-footer-hashtag">
              {convertHashtagsToString(story.hashTags)}
            </div>
            {results[i] === true ? (
              <div className="story-footer-tagged">
                <button
                  className="btn btn-sm btn-primary"
                  onClick={() => displayModalLink(story.id)}
                >
                  Link:
                </button>
              </div>
            ) : (
              <div className="story-footer-tagged">
                <button
                  className="btn btn-sm btn-primary"
                  onClick={() => displayModalStory(story.taggedUsers)}
                >
                  Tagged users:
                </button>
              </div>
            )}

            {sessionStorage.getItem("token") === "" ? null : (
              <div className="story-footer-collections">
                <button
                  className="btn btn-sm btn-primary"
                  onClick={() => this.displayModalReport(story)}
                >
                  Report
                </button>
              </div>
            )}
            <div className="story-footer-collections">
              {props.isActiveStories &&
              props.user === sessionStorage.getItem("userId") &&
              sessionStorage.getItem("token") !== "" ? (
                <button
                  className="btn btn-sm btn-primary"
                  onClick={() => displayCollectionsModal(story)}
                >
                  Add to collection:
                </button>
              ) : null}
            </div>
          </div>
        );
      },
    });
  });
  return (
    <Modal
      style={{
        maxWidth: "450px",
        width: "450px",
        maxHeight: "700px",
        width: "700px",
        borderRadius: "15px",
      }}
      className="text-center"
      isOpen={showProfileStoryModal}
      toggle={toggle}
      centered={true}
      backdropClassName="story-modal-backdrop"
    >
      {showTaggedUsersModal ? (
        <TaggedUsersProfileStoryModal
          show={showTaggedUsersModal}
          taggedUsers={users}
          onShowChange={displayModalStory}
        />
      ) : null}
      {showLinkModal ? (
        <StoryLinkModal
          show={showLinkModal}
          contentId={contentId}
          onShowChange={displayModalLink}
        />
      ) : null}
      {showCollectionsModal ? (
        <CollectionsModal
          show={showCollectionsModal}
          highlights={props.highlights}
          story={story}
          onShowChange={displayCollectionsModal}
        />
      ) : null}
      <ModalBody
        className="story-modal-body"
        style={{ padding: "2px !important" }}
      >
        <Stories
          keyboardNavigation
          width="100%"
          height="670px"
          stories={stories}
          onAllStoriesEnd={(s, st) => toggle()}
          storyStyles={{ height: "670px" }}
        />
      </ModalBody>
    </Modal>
  );
}

const mapStateToProps = (state) => ({
  images: state.storyImages,
  allImages: state.allStoriesImages,
  ad: state.ad,
});

export default connect(mapStateToProps, {
  getStoriesForModal,
  loadImagesStory,
  getAdForContent,
})(ProfileStoryModal);
