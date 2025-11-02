import React from "react";
import './index.css';
import TopicGallery from "./TopicGallery";
import documentGallery from "../Data/documentGallery.json";


export default class DocumentGallery extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            documents: documentGallery,
        }
    }
    render() {
        return (
            <div className="doc-gallery-wrapper">
                <div className="document-title">
                    <h2>Document Gallery</h2>
                </div>
                <div className="topic-list">
                    {
                        this.state.documents.map((item, index) =>{
                            return (
                                <div key={index} className="topic-item">
                                    <TopicGallery name={item.name} file={item.file} order={index}/>
                                </div>
                            );
                        })
                    }
                </div>
            </div>
        );
    }
}