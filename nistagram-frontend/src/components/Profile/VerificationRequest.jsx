import React, { Component } from "react";

class VerificationRequest extends Component {
  state = {
    id: 1,
    name: "",
    category: "",
    file: null,
    fileName: "",
    fileUrl: null,
  };

  render() {
    return (
      <React.Fragment>
        <main className="main pt-0 pb-0" style={{ backgroundColor: "#4da3ff" }}>
          <div className="wrap bg-white">
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="firstName">Name:</label>
                  <input
                    type="text"
                    name="name"
                    value={this.state.name}
                    onChange={this.handleChange}
                    class="form-control"
                    id="name"
                    placeholder="Enter name"
                  />
                </div>
              </div>
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label for="lastName">Category:</label>
                  <select
                    value={this.state.category}
                    class="form-control"
                    onChange={this.handleChange}
                    name="category"
                  >
                    <option value=""> </option>
                    <option value="Sport">Sport </option>
                    <option value="Influencer">Influencer </option>
                    <option value="New/Media">New/Media </option>
                    <option value="Business">Business </option>
                    <option value="Brand">Brand </option>
                    <option value="Organization">Organization </option>
                  </select>
                </div>
              </div>
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <input type="file" onChange={this.choosePost} />
                </div>
                <div class="form-group w-100 pr-5">
                  <img
                    src={this.state.fileUrl}
                    style={{ width: 300, height: 200 }}
                  />
                </div>
              </div>
            </div>
            <div className="mt-5 pb-5">
              <button className="btn btn-lg btn-primary btn-block">
                Send verification request
              </button>
            </div>
          </div>
        </main>
      </React.Fragment>
    );
  }

  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };

  choosePost = async (event) => {
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
    });
  };
}

export default VerificationRequest;
