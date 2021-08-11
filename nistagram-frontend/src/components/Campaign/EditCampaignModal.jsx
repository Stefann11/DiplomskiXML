import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import moment from "moment";
import { Slide } from "react-slideshow-image";
import "react-slideshow-image/dist/styles.css";
import {
  getPost,
  loadImage,
  likePost,
  dislikePost,
  commentPost,
  loadImages,
  getAllImagesForProfile,
} from "../../actions/actions";
import { loadImageProfile } from "../../actions/actionsUser";
import { connect } from "react-redux";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { createNotification } from "../../actions/actionsNotification";
import { getAdForContent, editCampaign } from "../../actions/actionsCampaign";
import LinkModal from "../Campaign/LinkModal";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";

class EditCampaignModal extends Component {
  state = {
    showEditModal: this.props.show,
    campaignId: this.props.campaignId,
    agentId: this.props.agentId,
    type: this.props.type,
    startDate: new Date(this.props.startDate),
    endDate: new Date(this.props.endDate),
    dateOfChange: new Date(this.props.dateOfChange),
    minDateOfBirth: new Date(this.props.minDateOfBirth),
    maxDateOfBirth: new Date(this.props.maxDateOfBirth),
    gender: this.props.gender,
  };

  async componentDidMount() {
   debugger;
  }

  render() {
    if (this.props.minDateOfBirth === undefined) {
        return null;
      }
    if (this.props.maxDateOfBirth === undefined) {
    return null;
    }
    if (this.props.gender === undefined) {
    return null;
    }
    // if (this.props.post == undefined) {
    //   return null;
    // }
    // if (
    //   this.props.post.contentPath == undefined &&
    //   this.props.loadedImages == undefined
    // ) {
    //   return null;
    // }
    // if (
    //   this.props.post.contentPaths == undefined &&
    //   this.props.loadedImage == undefined
    // ) {
    //   return null;
    // }
    // debugger;
    // var profileImage = "/images/user.png";
    // if (this.props.profileImage !== undefined) {
    //   profileImage = this.props.profileImage;
    // }
    // const post = this.props.post;
    // const loadedImage = this.props.loadedImage;
    // const loadedImages = this.props.loadedImages;
    return (
      <Modal
        style={{
          maxWidth: "850px",
          width: "849px",
        }}
        isOpen={this.state.showEditModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
        Edit
        </ModalHeader>
        <ModalBody>
        <div className="mt-5">
        <div className="form-group w-75" style={{ paddingLeft: 300 }}>
          <label className="label">Select max date of birth:</label>
          <div className="w-100">
            <DatePicker
              className="form-control w-100"
              id="maxDateOfBirth"
              name="maxDateOfBirth"
              dateFormat="dd/MM/yyyy"
              selected={this.state.maxDateOfBirth}
              maxDate={new Date()}
              minDate={this.state.minDateOfBirth}
              onChange={(e) => this.handleChangeMaxDate(e)}
            />
          </div>
        </div>
        <div className="form-group w-75 pt-3" style={{ paddingLeft: 300 }}>
          <label className="label">Select min date of birth:</label>
          <div className="w-100">
            <DatePicker
              className="form-control w-100"
              id="minDateOfBirth"
              name="minDateOfBirth"
              dateFormat="dd/MM/yyyy"
              selected={this.state.minDateOfBirth}
              maxDate={
                this.state.maxDateOfBirth === ""
                  ? new Date()
                  : this.state.maxDateOfBirth
              }
              onChange={(e) => this.handleChangeMinDate(e)}
            />
          </div>
          <div className="form-group w-100 pt-3">
            <label for="gender">Gender:</label>
            <select
              className="w-100"
              value={this.state.gender}
              class="form-control"
              onChange={this.handleChange}
              name="gender"
            >
              <option value=""></option>
              <option value="male">Male</option>
              <option value="female">Female</option>
            </select>
          </div>
        </div>
        <div
          className="pb-5"          
        >
          <button           
            onClick={() => this.edit()}
            className="btn btn-primary btn-lg"
          >
            Edit
          </button>
        </div>       
      </div>
        </ModalBody>
        <ModalFooter></ModalFooter>
      </Modal>
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

  

  
   
  handleChangeMinDate = (e) => {
    debugger;
    this.setState({
      minDateOfBirth: e,
    });
  };

  handleChangeMaxDate = (e) => {
    debugger;
    this.setState({
      maxDateOfBirth: e,
    });
  };
  

  

  toggle() {
    debugger;
    this.setState({ showNotLoggedPostModal: false });
    this.props.onShowChange();
  }

  displayModalPost() {
    debugger;
    if (this.state.result === true) {
      this.displayModalLink();
    } else {
      this.setState({
        showTaggedModal: !this.state.showTaggedModal,
      });
    }
  }

  displayModalLink() {
    debugger;
    this.setState({
      showLinkModal: !this.state.showLinkModal,
      link: this.props.ad.link,
    });
  }

  displayModalReport() {
    debugger;
    this.setState({
      showReportModal: !this.state.showReportModal,
    });
  }

  displayModalCollection() {
    debugger;
    this.setState({
      showChooseCollectionModal: !this.state.showChooseCollectionModal,
    });
  }

  edit() {
    debugger; 
    this.props.editCampaign({
      Id: this.state.campaignId,
      AgentId : this.state.agentId,
      Type : this.state.type,
      StartDate : this.state.startDate,
      EndDate : this.state.endDate,
      DateOfChange : this.state.dateOfChange,
      TargetAudience: {
            MinDateOfBirth : this.state.minDateOfBirth,
            MaxDateOfBirth : this.state.maxDateOfBirth,
            Gender : this.state.gender
      }
    });
    window.location = "/edit-campaign";
  }
  
}

const mapStateToProps = (state) => ({
  post: state.post,
  loadedImage: state.loadedImage,
  loadedImages: state.loadedImages,
  profileImage: state.profileImage,
  commentId: state.commentId,
  profileImages: state.profileImages,
  ad: state.ad,
});

export default connect(mapStateToProps, {
  getPost,
  loadImage,
  likePost,
  dislikePost,
  commentPost,
  loadImages,
  loadImageProfile,
  createNotification,
  getAllImagesForProfile,
  getAdForContent,
  editCampaign,
})(EditCampaignModal);
