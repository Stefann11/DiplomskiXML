import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { buyItem } from "../../actions/actions";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

class BuyItemModal extends Component {
  state = {
    showBuyModal: this.props.show,
    item: this.props.item,
    quantity: 0,
  };

  render() {
    debugger;
    return (
      <Modal
        style={{
          maxWidth: "450px",
          width: "449px",
        }}
        isOpen={this.state.showBuyModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>
          Enter quantity
        </ModalHeader>
        <ModalBody>
          <div>
            <input
              type="number"
              name="quantity"
              value={this.state.quantity}
              onChange={this.handleChange}
              class="form-control"
              id="quantity"
              placeholder="Enter quantity you want to buy"
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            disabled={this.state.quantity <= 0}
            className="btn btn-primary"
            onClick={() => {
              this.buy();
            }}
          >
            Buy
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  async buy() {
    var result = false;
    result = await this.props.buyItem({
      Item: this.state.item,
      Quantity: this.state.quantity,
    });
    if (result === true) {
      window.location = "/items/" + this.state.item.id;
    } else {
      toast.configure();
      toast.error("Quantity is too big!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    }
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

  toggle() {
    debugger;
    this.setState({ showBuyModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { buyItem })(BuyItemModal);
