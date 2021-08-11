import React, { Component } from "react";
import "./StoryCircle.css";
import Story from "./Story";
import { connect } from "react-redux";
import { loadProfileImagesStory } from "../../actions/actionsStory";

class StoryList extends Component {
  state = {};

  componentDidMount() {
    this.props.loadProfileImagesStory(this.createImagesList(this.props.items));
  }

  render() {
    debugger;
    if (this.props.storyProfileImages === undefined) return null;
    return (
      <div className="story-wrapper">
        <Story first={true} item={{}} i={0} />
        {this.props.stories.length === 0
          ? null
          : this.props.items.map((item, i) => (
              <Story
                profileImage={this.props.storyProfileImages[i]}
                item={item}
                first={false}
              />
            ))}
      </div>
    );
  }

  createImagesList() {
    var images = [];
    this.props.items.forEach((element) => {
      images.push(element.profilePicturePath);
    });
    return images;
  }
}

const mapStateToProps = (state) => ({
  storyProfileImages: state.storyProfileImages,
  stories: state.stories,
});

export default connect(mapStateToProps, { loadProfileImagesStory })(StoryList);
