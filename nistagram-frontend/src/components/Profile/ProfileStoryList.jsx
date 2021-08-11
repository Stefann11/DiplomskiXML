import React, { useEffect, useState } from "react";
import "./ProfileStoryCircle.css";
import ProfileStory from "./ProfileStory";
import { connect } from "react-redux";
import { loadProfileImagesStory } from "../../actions/actionsStory";
import { withRouter } from "react-router-dom";
import { compose } from "redux";

function ProfileProfileStoryList(props) {
  useEffect(async () => {
    await props.loadProfileImagesStory(createImagesList(props.highlights));
  }, [props.highlights]);

  useEffect(async () => {
    await props.loadProfileImagesStory(createImagesList(props.highlights));
  }, [props.activeStories]);

  const createImagesList = () => {
    debugger;
    var images = [];
    if (props.activeStories[0] !== undefined)
      images.push(props.activeStories[0].contentPath);
    props.highlights.forEach((element) => {
      images.push(element.stories[0].contentPath);
    });
    return images;
  };

  if (props.storyProfileImages === undefined || props.highlights === undefined)
    return null;
  debugger;
  var activeStoriesLength = props.activeStories.length > 0 ? 1 : 0;
  if (
    activeStoriesLength + props.highlights.length >
    props.storyProfileImages.length
  ) {
    return null;
  }
  debugger;
  return (
    <div className="story-wrapper">
      {props.activeStories === undefined ||
      props.activeStories.length === 0 ? null : (
        <ProfileStory
          user={props.user}
          first={true}
          activeStories={props.activeStories}
          profileImage={props.storyProfileImages[0].fileContents}
          highlights={props.highlights}
          i={0}
        />
      )}

      {props.highlights.map((highlight, i) => (
        <ProfileStory
          profileImage={
            props.storyProfileImages[
              props.activeStories.length === 0 ? i : i + 1
            ].fileContents
          }
          highlight={highlight}
          first={false}
        />
      ))}
    </div>
  );
}

const mapStateToProps = (state) => ({
  storyProfileImages: state.storyProfileImages,
});

export default compose(
  withRouter,
  connect(mapStateToProps, { loadProfileImagesStory })
)(ProfileProfileStoryList);
