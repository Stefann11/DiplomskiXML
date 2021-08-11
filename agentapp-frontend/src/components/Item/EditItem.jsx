import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import { getItemById, editItem } from "../../actions/actions";

class EditItem extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    price: "",
    availableCount: "",
  };

  async componentDidMount() {
    await this.props.getItemById(sessionStorage.getItem("itemId"));
    debugger;
    this.setState({
      name: this.props.item.name,
      price: this.props.item.price,
      availableCount: this.props.item.availableCount,
    });
  }

  render() {
    if (this.props.item === undefined) {
      return null;
    }

    if (this.props.loadedImage === undefined) {
      return null;
    }
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <img
                onClick={() => {
                  this.displayModalPost();
                }}
                src={
                  "data:image/jpg;base64," + this.props.loadedImage.fileContents
                }
                style={{ width: 600, height: 320 }}
                className="mb-3"
              />
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label>You can choose new photo if you want to: </label>
              <input type="file" onChange={this.choosePost} />
              <img
                src={this.state.fileUrl}
                style={{ width: 500, height: 300 }}
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label className="label">Name:</label>
              <input
                type="text"
                name="name"
                value={this.state.name}
                onChange={this.handleChange}
                class="form-control"
                id="name"
                placeholder="Enter name of product"
              />
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="tag">Price:</label>
              <input
                type="number"
                name="price"
                value={this.state.price}
                onChange={this.handleChange}
                class="form-control"
                id="price"
                placeholder="Enter price"
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="location">Available count:</label>
              <input
                type="number"
                name="availableCount"
                value={this.state.availableCount}
                onChange={this.handleChange}
                class="form-control"
                id="availableCount"
                placeholder="Enter availableCount"
              />
            </div>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            onClick={() => {
              this.createItem();
            }}
            className="btn btn-primary btn-block"
          >
            Edit
          </button>
        </div>
      </React.Fragment>
    );
  }

  async createItem() {
    debugger;
    await this.props.editItem({
      Id: this.props.item.id,
      Name: this.state.name,
      ItemImagePath:
        this.state.contentPath === ""
          ? this.props.item.itemImagePath
          : this.state.contentPath,
      Price: this.state.price,
      AvailableCount: this.state.availableCount,
    });
    window.location = "/";
  }

  choosePost = async (event) => {
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
      fileType: event.target.files[0].type,
    });
    const formData = new FormData();

    formData.append("formFile", event.target.files[0]);
    formData.append("fileName", event.target.files[0].name);

    var dummyThis = this;
    try {
      const res = await axios({
        method: "post",
        url: "http://localhost:55744/api/contents",
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      })
        .then(function (response) {
          var contentPath = { ...dummyThis.state.contentPath };
          contentPath = response.data;
          dummyThis.setState({ contentPath });
          console.log(response);
        })
        .catch(function (response) {
          console.log(response);
        });
      console.log(res);
      debugger;
      console.log(this.state.contentPath);
    } catch (ex) {
      console.log(ex);
    }
  };

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };
}

const mapStateToProps = (state) => ({
  item: state.item,
  loadedImage: state.loadedImage,
});

export default compose(
  withRouter,
  connect(mapStateToProps, { getItemById, editItem })
)(EditItem);
