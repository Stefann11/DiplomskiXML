import React, { Component } from "react";
import { Card, CardBody } from "reactstrap";
import ProfileStoryList from "./ProfileStoryList";
const it = [...Array(20).keys()];
const items = it.map((item) => ({
  id: item,
  url: `https://loremflickr.com/150/150?random=${item}`,
}));

class ProfileStoryCard extends Component {
  state = {
    items,
  };
  render() {
    if (
      this.props.highlights === undefined ||
      this.props.activeStories === undefined
    )
      return null;
    return (
      <Card>
        <CardBody className="card-body-profile" style={{ zIndex: "0" }}>
          <ProfileStoryList
            user={this.props.user}
            activeStories={this.props.activeStories}
            highlights={this.props.highlights}
            onClick={this.handleClick}
          />
        </CardBody>
      </Card>
    );
  }
  handleClick = (i) => {
    const toBeMoved = this.state.items[i];
    const newItems = this.state.items.filter((_, ind) => ind !== i);
    const items = [...newItems, toBeMoved];

    this.setState({
      items,
    });
  };
}

export default ProfileStoryCard;
