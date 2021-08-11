import React, { Component } from "react";
import { Card, CardBody } from "reactstrap";
import StoryList from "./StoryList";
const it = [...Array(20).keys()];
const items = it.map((item) => ({
  id: item,
  url: `https://loremflickr.com/150/150?random=${item}`,
}));

class StoryCard extends Component {
  state = {
    items,
  };
  render() {
    return (
      <Card>
        <CardBody style={{ zIndex: "0" }}>
          <StoryList items={this.props.users} onClick={this.handleClick} />
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

export default StoryCard;
