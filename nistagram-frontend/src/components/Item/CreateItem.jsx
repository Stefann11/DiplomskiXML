import React, { Component } from "react";

class CreateItem extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    name: "",
    price: "",
    availableCount: "",
  };
  render() {
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
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
          <button className="btn btn-primary btn-block">Create</button>
        </div>
      </React.Fragment>
    );
  }

  choosePost = async (event) => {
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
    });
  };

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };
}

export default CreateItem;
