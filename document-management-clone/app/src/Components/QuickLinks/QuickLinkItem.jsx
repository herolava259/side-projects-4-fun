import React from "react";
import "./index.css";

export default class QuickLinkItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            order: this.props.order
        };
    }
    render() {
       
        return (
            <div className="ql-item-container" style={{backgroundColor:"#0074BD"}}>
                <img src={this.props.icon} alt="" className="quick-link-img" />
                <h4 className="quick-link-it-title">{this.props.title}</h4>
            </div>
        );
    }
}