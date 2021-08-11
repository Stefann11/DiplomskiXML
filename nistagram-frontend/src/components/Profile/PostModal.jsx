import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Favorite from "@material-ui/icons/Favorite";
import FavoriteBorder from "@material-ui/icons/FavoriteBorder";
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
} from "../../actions/actions";
import { loadImageProfile } from "../../actions/actionsUser";
import { connect } from "react-redux";
import TaggedUsersModal from "./TaggedUsersModal";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import ChooseCollectionModal from "../Collection/ChooseCollectionModal";
import ReportModal from "../Report/ReportModal";
import { createNotification } from "../../actions/actionsNotification";
import { getAdForContent } from "../../actions/actionsCampaign";
import LinkModal from "../Campaign/LinkModal";

class PostModal extends Component {
  state = {
    showPostModal: this.props.show,
    showTaggedModal: false,
    showChooseCollectionModal: false,
    showReportModal: false,
    showLinkModal: false,
    likesCount: 0,
    dislikesCount: 0,
    liked: false,
    disliked: false,
    commentText: "",
    comments: [],
    result: false,
    link: "",
  };

  async componentDidMount() {
    var result = false;
    result = await this.props.getAdForContent(this.props.postId);
    await this.props.getPost(this.props.postId);
    await this.props.loadImageProfile(this.props.personPhoto);
    this.setState({
      likesCount: this.props.post.likes.length,
      dislikesCount: this.props.post.dislikes.length,
      comments: this.props.post.comments,
      result: result,
    });
  }

  render() {
    if (this.props.post == undefined) {
      return null;
    }
    if (
      this.props.post.contentPath == undefined &&
      this.props.loadedImages == undefined
    ) {
      return null;
    }
    if (
      this.props.post.contentPaths == undefined &&
      this.props.loadedImage == undefined
    ) {
      return null;
    }

    debugger;
    var profileImage = "/images/user.png";
    if (this.props.profileImage !== undefined) {
      profileImage = this.props.profileImage;
    }
    const post = this.props.post;
    const loadedImage = this.props.loadedImage;
    const loadedImages = this.props.loadedImages;
    return (
      <Modal
        style={{
          maxWidth: "850px",
          width: "849px",
        }}
        isOpen={this.state.showPostModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          {this.state.showTaggedModal ? (
            <TaggedUsersModal
              show={this.state.showTaggedModal}
              postId={post.id}
              onShowChange={this.displayModalPost.bind(this)}
            />
          ) : null}
          {this.state.showChooseCollectionModal ? (
            <ChooseCollectionModal
              show={this.state.showChooseCollectionModal}
              postId={post.id}
              onShowChange={this.displayModalCollection.bind(this)}
            />
          ) : null}
          {this.state.showLinkModal ? (
            <LinkModal
              show={this.state.showLinkModal}
              link={this.state.link}
              onShowChange={this.displayModalLink.bind(this)}
            />
          ) : null}
          {this.state.showReportModal ? (
            <ReportModal
              show={this.state.showReportModal}
              contentId={post.id}
              type="post"
              registeredUser={post.registeredUser}
              onShowChange={this.displayModalReport.bind(this)}
            />
          ) : null}
          {profileImage === "/images/user.png" ? (
            <img
              src={profileImage}
              style={{ width: 32, height: 32, borderRadius: 50 }}
            />
          ) : (
            <img
              src={"data:image/jpg;base64," + profileImage}
              style={{ width: 32, height: 32, borderRadius: 50 }}
            />
          )}

          <span style={{ width: 15, display: "inline-block" }}></span>
          {this.props.person}
        </ModalHeader>
        <ModalBody>
          <div>
            {this.state.result === true ? (
              <div className="ml-4 mb-2" style={{ textAlign: "right" }}>
                {" "}
                <b>Sponsored</b>{" "}
                <img classNmae="ml-4 mb-4" src="/images/star.png" />
              </div>
            ) : (
              ""
            )}
            {post.contentPath == undefined ? (
              <Slide easing="ease">
                {loadedImages.map((f, i) =>
                  loadedImages[i].contentType === "image/jpeg" ? (
                    <div className="each-slide">
                      <img
                        onClick={() => {
                          this.displayModalPost();
                        }}
                        src={
                          loadedImages[i].contentType === "image/jpeg"
                            ? "data:image/jpg;base64," +
                              loadedImages[i].fileContents
                            : "data:video/mp4;base64," +
                              loadedImages[i].fileContents
                        }
                        style={{ width: 800, height: 320 }}
                        className="mb-3"
                      />
                    </div>
                  ) : (
                    <div className="each-slide">
                      <video
                        controls
                        onClick={() => {
                          this.displayModalPost();
                        }}
                        style={{ width: 800, height: 320 }}
                        className="mb-3"
                      >
                        <source
                          src={
                            "data:video/mp4;base64," +
                            loadedImages[i].fileContents
                          }
                          type="video/mp4"
                        ></source>
                      </video>
                    </div>
                  )
                )}
              </Slide>
            ) : loadedImage.contentType === "image/jpeg" ? (
              <img
                onClick={() => {
                  this.displayModalPost();
                }}
                src={"data:image/jpg;base64," + loadedImage.fileContents}
                style={{ width: 800, height: 320 }}
                className="mb-3"
              />
            ) : (
              <video
                controls
                onClick={() => {
                  this.displayModalPost();
                }}
                style={{ width: 800, height: 320 }}
                className="mb-3"
              >
                <source
                  src={"data:video/mp4;base64," + loadedImage.fileContents}
                  type="video/mp4"
                ></source>
              </video>
            )}
            <div>
              {post.hashTags.map((hashTag) => hashTag.hashTagText + " ")}
            </div>
            {post.description}
            <br />
            {moment(post.timeStamp).format("DD/MM/YYYY HH:mm")}
            {Object.entries(post.location).length !== 0
              ? ", " +
                post.location.street +
                " " +
                post.location.cityName +
                ", " +
                post.location.country
              : ""}
            <a
              style={{ float: "right" }}
              className="mr-4"
              href="javascript:;"
              onClick={() => {
                this.displayModalReport();
              }}
            >
              Report
            </a>
            <br />
            <br />
            <FormControlLabel
              style={{ width: 24, height: 24 }}
              control={
                <Checkbox
                  onClick={() => {
                    this.likePost();
                  }}
                  icon={<FavoriteBorder />}
                  checkedIcon={<Favorite />}
                  name="checkedH"
                />
              }
            />
            <img
              style={{ width: 24, height: 24 }}
              onClick={() => {
                this.dislikePost();
              }}
              src="/images/dislike.png"
            />
            <span style={{ width: 15, display: "inline-block" }}></span>
            <img src="/images/chat.png" />
            <span style={{ width: 15, display: "inline-block" }}></span>
            <img src="/images/send.png" />
            <span style={{ width: 15, display: "inline-block" }}></span>
            <img
              style={{ width: 24, height: 24 }}
              src="/images/collection.png"
              onClick={() => {
                this.displayModalCollection();
              }}
            />
            <br />
            <br />
            Likes:{" "}
            <a href="javascript:;" className="mr-2">
              {this.state.likesCount}
            </a>
            Dislikes: <a href="javascript:;">{this.state.dislikesCount}</a>{" "}
            <br />
            <hr />
            <div style={{ overflow: "scroll" }}>
              {this.state.comments.map((comment) => (
                <div>
                  <img src="/images/user.png" />{" "}
                  {comment.registeredUser.username + ": " + comment.commentText}
                  <br />
                  <hr />
                </div>
              ))}
            </div>
            <div style={{ float: "left" }}>
              <input
                name="commentText"
                value={this.state.commentText}
                onChange={this.handleChange}
                style={{ border: 0 }}
                type="text"
                placeholder="Add a comment..."
              />
            </div>
            <div style={{ float: "right" }}>
              <a
                onClick={() => {
                  this.commentPost();
                }}
                href=""
              >
                {" "}
                Post{" "}
              </a>
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

  async likePost() {
    debugger;
    if (this.state.liked == false) {
      this.setState((prevState) => {
        return { likesCount: prevState.likesCount + 1 };
      });
    } else {
      this.setState((prevState) => {
        return { likesCount: prevState.likesCount - 1 };
      });
    }

    await this.props.likePost({
      id: this.props.post.id,
      userId: sessionStorage.getItem("userId"),
    });
    this.setState({
      liked: !this.state.liked,
    });
  }

  async dislikePost() {
    debugger;
    if (this.state.disliked == false) {
      this.setState((prevState) => {
        return { dislikesCount: prevState.dislikesCount + 1 };
      });
    } else {
      this.setState((prevState) => {
        return { dislikesCount: prevState.dislikesCount - 1 };
      });
    }

    await this.props.dislikePost({
      id: this.props.post.id,
      userId: sessionStorage.getItem("userId"),
    });
    this.setState({
      disliked: !this.state.disliked,
    });
  }

  async commentPost() {
    this.sendNotification();
    const comment = {
      CommentText: this.state.commentText,
      RegisteredUser: { id: sessionStorage.getItem("userId") },
    };
    debugger;
    await this.props.commentPost({ id: this.props.post.id, comment: comment });
    toast.configure();
    toast.success("Commented successfully!", {
      position: toast.POSITION.TOP_RIGHT,
    });
  }

  sendNotification() {
    debugger;
    this.props.createNotification({
      Type: "Comment",
      ContentId: "12345678-1234-1234-1234-123456789123",
      RegisteredUser: { id: sessionStorage.getItem("userId") },
    });
  }

  toggle() {
    debugger;
    this.setState({ showPostModal: false });
    this.props.onShowChange();
  }

  displayModalPost() {
    if (this.state.result === true) {
      this.displayModalLink();
    } else {
      debugger;
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
}

const mapStateToProps = (state) => ({
  post: state.post,
  loadedImage: state.loadedImage,
  loadedImages: state.loadedImages,
  profileImage: state.profileImage,
  commentId: state.commentId,
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
  getAdForContent,
})(PostModal);
