import React, { Component } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker-cssmodules.css";
import "../../css/checkbox.css";

class Privacy extends Component {
  state = {
    id: 1,
    private: true,
    receive: true,
    tagable: true,
  };

  render() {
    return (
      <React.Fragment>
        <main className="main pt-0 pb-0" style={{ backgroundColor: "#4da3ff" }}>
          <div className="wrap bg-white">
            <div className="text-center pt-5">
              <img
                alt=""
                width="100"
                height="100"
                src="/images/iconfinder_00-ELASTOFONT-STORE-READY_user-circle_2703062.png"
              />
            </div>
            <div className="mt-5">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <label className="pr-5" for="firstName">
                    Private:
                  </label>
                  <span className="checkbox">
                    <input type="checkbox" checked={this.state.private} />
                    <span
                      className="wrapper"
                      onClick={() => this.toggleCheckPrivate("private")}
                    >
                      <span className="knob"></span>
                    </span>
                  </span>
                </div>
              </div>
              <div className="mt-5">
                <div className="d-inline-flex w-50">
                  <div class="form-group w-100 pr-5">
                    <label className="pr-5" for="firstName">
                      Receive messages from not followed profiles:
                    </label>
                    <span className="checkbox">
                      <input type="checkbox" checked={this.state.receive} />
                      <span
                        className="wrapper"
                        onClick={() => this.toggleCheckReceive("receive")}
                      >
                        <span className="knob"></span>
                      </span>
                    </span>
                  </div>
                </div>
              </div>
              <div className="mt-5">
                <div className="d-inline-flex w-50">
                  <div class="form-group w-100 pr-5">
                    <label className="pr-5" for="firstName">
                      Can other profiles tag you:
                    </label>
                    <span className="checkbox">
                      <input type="checkbox" checked={this.state.tagable} />
                      <span
                        className="wrapper"
                        onClick={() => this.toggleCheckTag("tagable")}
                      >
                        <span className="knob"></span>
                      </span>
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </main>
      </React.Fragment>
    );
  }

  toggleCheckPrivate = (event) => {
    this.setState({ private: !this.state[event] });
  };

  toggleCheckReceive = (event) => {
    this.setState({ receive: !this.state[event] });
  };

  toggleCheckTag = (event) => {
    this.setState({ tagable: !this.state[event] });
  };

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
}

export default Privacy;
