import React from "react";
import "./layout.css";
import Navigation from "../Components/Navigation/Navigation";
import Announcement from "../Components/Announcement/Announcement";
import News from "../Components/News/News";
import MediaGallery from "../Components/MediaGallery/MediaGallery";
import DocumentGallery from "../Components/DocumentGallery/DocumentGallery";
import QuickLinks from "../Components/QuickLinks/QuickLinks";
import Events from "../Components/Event/Events";
import Questions from "../Components/Questions/Questions";
export default class Layout extends React.Component {
    render() {
        return (
            <div className="portal-homepage">
                <div className="wrap">
                    <div className="header">
                        <Navigation></Navigation>
                    </div>
                    <div className="content">
                        <div id="announcement">
                            <Announcement></Announcement>
                        </div>
                        <div id="news">
                            <News></News>
                        </div>
                        <div id="image-gallery">

                            <MediaGallery isDynamic = {false}/>
                        </div>
                        <div id="video-gallery">
                            <MediaGallery isDynamic = {true}/>
                        </div>
                        <div id="document-gallery">
                            <DocumentGallery></DocumentGallery>
                        </div>
                        <div id="quick-links">
                            <QuickLinks></QuickLinks>
                        </div>
                        <div id="events">
                            <Events></Events>
                        </div>
                        <div id="how-do-i">
                            <Questions></Questions>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}