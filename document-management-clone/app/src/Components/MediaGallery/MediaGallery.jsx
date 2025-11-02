import React from "react";
import Image from "../../Base/Image";
import './index.css';
import MediaItem from "./MediaItem";
import ShowMoreButton from "../ViewMore";
import videoGallery from "../Data/videoGallery.json"
import imageGallery from "../Data/imageGallery.json"
export default class MediaGallery extends React.Component {
    constructor(props) {
        super(props);
        
        if(this.props.isDynamic)
        {
            
            this.state ={
                gallery : videoGallery,
                icon: {play: Image.clock, pause: Image.collapse},
                displaySize: 4 > videoGallery.length ? videoGallery.length : 4,
            }
        }else{
            this.state = {
                gallery: imageGallery,
                icon: null,
                displaySize: 4 > imageGallery.length ? imageGallery.length : 4,
            }
        }

        this.handleShowMore = this.handleShowMore.bind(this);
        this.handleViewLess = this.handleViewLess.bind(this);

    }

    handleShowMore()
    {
        this.setState(state =>{
            let newSize = state.displaySize + 1 < state.gallery.length ? state.displaySize + 1 : state.gallery.length;

            let newState = {
                gallery: state.gallery,
                icon: state.icon,
                displaySize: newSize
            }

            return newState;
        });
    }

    handleViewLess()
    {
        this.setState(state =>{
            let newState = {
                gallery: state.gallery,
                icon: state.icon,
                displaySize: 4 > state.gallery.length ? state.gallery.length : 4
            }

            return newState;
        })
    }
    render() {
        
        let isMore = this.state.displaySize < this.state.gallery.length;
        return (
            
            <div className="gallery-container">
                <div className="gallery-row">
                    <h3>{this.props.isDynamic ? "Video Gallery" : "Image Gallery"}</h3>
                </div>
                <div className="gallery-row media-list">
                    {
                        this.state.gallery.slice(0,this.state.displaySize).map((item,index)=>{
                            return (
                                <div className="media-item">
                                    <MediaItem icon={this.state.icon} image={Image[item.image]} order={index} />
                                </div>
                            );
                        })
                    }
                </div>
                <div className="gallery-row">
                    <ShowMoreButton handleShowMore={this.handleShowMore} handleViewLess={this.handleViewLess} isMore = {isMore}/>
                </div>
            </div>
        )
    }
}