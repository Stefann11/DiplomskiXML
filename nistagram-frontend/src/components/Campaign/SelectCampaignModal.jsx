import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  getCampaignsForAgent,
  createCampaignRequest,
} from "../../actions/actionsCampaign";
import { connect } from "react-redux";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

class SelectCampaignModal extends Component {
  state = {
    showCampaignModal: this.props.show,
    campaign: {},
    campaigns: [],
  };

  async componentDidMount() {
    debugger;
    await this.props.getCampaignsForAgent();
    this.setState({
      campaigns: this.props.campaignsForAgent,
    });
  }

  render() {
    if (this.props.campaignsForAgent === undefined) {
      return null;
    }
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showCampaignModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>Campaigns</ModalHeader>
        <ModalBody>
          <select
            className="form-control"
            onChange={this.handleChangeCollection}
          >
            <option>Select campaign</option>
            {this.state.campaigns.map((option, index) => (
              <option key={index} value={index}>
                {"Campaign: " + option.id}                
              </option>
            ))}
          </select>
        </ModalBody>
        <ModalFooter>
          <button
            disabled={
              this.state.campaign === undefined
                ? true
                : Object.keys(this.state.campaign).length === 0
            }
            className="btn btn-primary"
            onClick={() => {
              this.addPostToCollection();
            }}
          >
            Add
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  toggle() {
    debugger;
    this.setState({ showCampaignModal: false });
    this.props.onShowChange();
  }

  async addPostToCollection() {
    debugger;
    var result = false;
    result = await this.props.createCampaignRequest({
      IsApproved: false,
      Campaign: this.state.campaign,
      VerifiedUser: this.props.user,
      CampaignRequestAction: "created",
    });
    if (result === true) {
      this.toggle();
    } else {
      toast.configure();
      toast.error("You have already sent request for that campaign !", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
  }

  handleChangeCollection = (e) => {
    this.setState({ campaign: this.state.campaigns[e.target.value] });
  };
}

const mapStateToProps = (state) => ({
  campaignsForAgent: state.campaignsForAgent,
});

export default connect(mapStateToProps, {
  getCampaignsForAgent,
  createCampaignRequest,
})(SelectCampaignModal);
