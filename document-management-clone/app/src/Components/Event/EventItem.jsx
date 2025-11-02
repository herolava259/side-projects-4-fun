import React from "react";
import "./index.css";
import Image from "../../Base/Image";
export default class EventItem extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            order: this.props.order
        }
    }
    render() {
        return (
            <div className="event-item-wrapper">
                <div className="event-item-left">
                    <div className="event-day">
                        <span>
                            {this.props.day}
                        </span>
                    </div>
                    <div className="event-month">
                        <span>
                            {this.props.month}
                        </span>
                    </div>
                </div>
                <div className="event-item-right">
                    <div className="event-item-title">
                        {this.props.title}
                    </div>
                    <div className="event-item-time">
                        <img src={Image.clock} alt="" className="event-time-icon" />
                        <span>{this.props.fromTime}</span>-<span>{this.props.toTime}</span>
                    </div>
                </div>
            </div>
        );
    }
}