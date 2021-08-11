import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { Table } from "reactstrap";
import { Card, CardBody, CardFooter, CardHeader } from "reactstrap";
import { getPost } from "../../actions/actions";
import { connect } from "react-redux";

class TaggedUsersModal extends Component {
  state = {
    showPostModal: this.props.show,
  };

  componentDidMount() {
    debugger;
    this.props.getPost(this.props.postId);
  }

  render() {
    if (this.props.post == undefined) {
      return null;
    }
    const post = this.props.post;
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showPostModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>Tagged people</ModalHeader>
        <ModalBody>
          <Card
            className="mt-5"
            style={{ shadowColor: "gray", boxShadow: "0 8px 6px -6px #999" }}
          >
            <Table className="table allPrescriptions mb-0">
              <tbody>
                {post.taggedUsers.map((f) => (
                  <tr>
                    <td style={{ textAlign: "left" }}>
                      <img src="/images/user.png" />
                      <label className="ml-3">
                        {f.firstName + " " + f.lastName}
                      </label>
                      <label className="ml-2">({f.username})</label>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Card>
        </ModalBody>
        <ModalFooter></ModalFooter>
      </Modal>
    );
  }

  toggle() {
    debugger;
    this.setState({ showPostModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({ post: state.post });

export default connect(mapStateToProps, { getPost })(TaggedUsersModal);
