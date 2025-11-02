import React from "react";
import "./index.css";
export default class AnnouncementItem extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            title: this.props.title,
            imgUrl: this.props.imgUrl,
            occurDate: this.props.occur,
            description: this.props.desp,
            department: this.props.dept,
            order: this.props.order,
            }
    }
    render() {
        return (
            <div className="ann-item-wrapper">
                <div className="left-part">
                    <img src={this.state.imgUrl} alt="" className="ann-img" />
                </div>
                <div className="right-part">
                    <div className="ann-title">
                        <h3>{this.state.title}</h3>
                    </div>
                    <div className="ann-body">
                        <p>{this.state.description}</p>
                    </div>
                    <div className="ann-footer">
                        <div className="occur">
                            <i className="fa"></i>
                            <span>{this.state.occurDate}</span>
                        </div>
                        <div className="dept-badge">
                            <span>{this.state.department}</span>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}