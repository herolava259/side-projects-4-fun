import React from "react";
import './index.css';
import QuickLinkItem from "./QuickLinkItem"
import quicklinks from "../Data/quicklinks.json";
import Image from "../../Base/Image";


export default class QuickLinks extends React.Component {
    constructor(props) {
        super(props);

        this.state ={
            qLinks: quicklinks
        }
    }

    render() {
        return (
            <div className="quick-links-wrapper">
                <div className="quick-links-title">
                    <h3>Quick Links</h3>
                </div>
                <div className="quick-links-list">
                    {
                        this.state.qLinks.map((item,index)=>{
                            return (
                                <div key={index} className="quick-link-item">
                                    <QuickLinkItem icon={Image[item.img]} title={item.title} order={index}/>
                                </div>
                            );
                        })
                    }
                </div>
            </div>
        );
    }
}