import React, { Component } from "react";
import AsyncSelect from "react-select/async";
import { getHashTagsByText, getLocationsByText } from "../../actions/actions";
import { getUsersByName } from "../../actions/actionsUser";
import { connect } from "react-redux";
import { compose } from "redux";
import { withRouter } from "react-router-dom";

class SearchBar extends Component {
  state = { inputValue: "", type: "" };
  render() {
    return (
      <AsyncSelect
        cacheOptions
        loadOptions={this.loadOptions}
        defaultOptions
        onInputChange={this.handleInputChange}
        onChange={this.search}
        defaultValue={""}
        placeholder="Search"
      />
    );
  }

  search = (value) => {
    if (value.type === "hashtag") {
      this.props.history.replace({
        pathname: "/explore/tag/" + value.label,
        state: {
          searchObject: value,
        },
      });
    } else if (value.type === "location") {
      this.props.history.replace({
        pathname: "/explore/location/" + value.value,
        state: {
          searchObject: value,
        },
      });
    } else {
      window.location = "/profile/" + value.value.id;
    }
  };

  handleInputChange = (newValue) => {
    const inputValue = newValue.replace(/\W/g, "");
    this.setState({ inputValue });
    return inputValue;
  };

  loadOptions = async (inputValue, callback) => {
    await this.props.getHashTagsByText(inputValue);
    var valueList = [];
    this.props.hashtags.forEach((element) => {
      valueList.push({
        value: element.hashTagText,
        label: element.hashTagText,
        type: "hashtag",
      });
    });
    await this.props.getLocationsByText(inputValue);
    this.props.locations.forEach((element) => {
      valueList.push({
        value: this.createLocationValue(element),
        label: this.createLocationLabel(element),
        type: "location",
      });
    });
    await this.props.getUsersByName(inputValue);
    this.props.users.forEach((element) => {
      valueList.push({
        value: element,
        label: element.username,
        type: "user",
      });
    });
    callback(this.filterOptions(valueList));
  };

  filterOptions = (valueList) => {
    return valueList;
  };

  createLocationValue(element) {
    if (element.street !== "")
      return element.street + "-" + element.cityName + "-" + element.country;
    if (element.cityName !== "")
      return element.cityName + "-" + element.country;
    else return element.country;
  }

  createLocationLabel(element) {
    if (element.street !== "") return element.street + ", " + element.cityName;
    if (element.cityName !== "")
      return element.cityName + ", " + element.country;
    else return element.country;
  }
}

const mapStateToProps = (state) => ({
  hashtags: state.hashtags,
  locations: state.locations,
  users: state.users,
});

export default compose(
  withRouter,
  connect(mapStateToProps, {
    getHashTagsByText,
    getLocationsByText,
    getUsersByName,
  })
)(SearchBar);
