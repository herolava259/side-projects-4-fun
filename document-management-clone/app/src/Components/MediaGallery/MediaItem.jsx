import React from "react";
import './index.css';
export default class MediaItem extends React.Component
{
    constructor(props){
        super(props);
    }

    render()
    {
        const {image,icon}=this.props;
        if(icon==null)
        {
            return (
                <div className="media-item-wrapper">
                    <img className="media-image" src={image} alt="" />
                </div>
            );
        }
        else{
            return (
                <div className="media-item-wrapper">
                    <img className="icon-overlapper icon-play" src={icon.play} alt="" />
                    <img className="icon-overlapper icon-pause" src={icon.pause} alt="" />
                    <img className="media-image" src={image} alt="" />
                </div>
            );
        }
        
    }
}