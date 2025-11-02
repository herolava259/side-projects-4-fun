import React from "react";
import "./index.css";
import Image from "../../Base/Image";
import ShowMoreButton from "../ViewMore";
export default class TopicGallery extends React.Component {
    constructor(props) {
        super(props);

        this.handleShowMore = this.handleShowMore.bind(this);
        this.handleViewLess = this.handleViewLess.bind(this);

        let numFile = this.props.file.length;
        let displayFile;
        let isMore = true;
        if(4 < numFile)
        {
            displayFile = this.props.file.slice(0,4);
            isMore = true;

        }else{
            displayFile = this.props.file;
            isMore = false;
        };

        this.state = {
            docs: displayFile,
            isMore: isMore,
        };

    }

    handleShowMore()
    {
        this.setState((state,props)=>
            {
                if(state.docs.length+1 >= props.file.length)
                {
                    return {
                        docs: props.file,
                        isMore: false,
                    };
                }else{
                    let slicedSize = state.docs.length + 1;

                    return{
                        docs: props.file.slice(0,slicedSize),
                        isMore: true
                    };
                }
            });
    }

    handleViewLess()
    {
        this.setState((state, props)=>{

            let numFile = this.props.file.length;
            let displayFile;
            let isMore = true;
            if(4 < numFile)
            {
                displayFile = this.props.file.slice(0,4);
                isMore = true;

            }else{
                displayFile = this.props.file;
                isMore = false;
            };

            return {
                docs: displayFile,
                isMore: isMore,
            }; 
        });
    }

    render() {
        return (
            <div key={this.props.order} className="topic-gallery-wrapper">
                <div className="name-topic">
                    <h3>{this.props.name}</h3>
                </div>
                <div className="file-list">
                    {
                        this.state.docs.map((item, index)=>{
                            return (
                                <div key={index} className="file-item">
                                    <img src={Image[item.icon]} alt="" className="file-icon" />
                                    <span className="file-title">{item.title}</span>
                                </div>
                            );
                        })
                    }
                </div>
                <div className="show-more-btn">
                    <ShowMoreButton handleShowMore={this.handleShowMore} handleViewLess={this.handleViewLess} isMore={this.state.isMore}/>
                </div>
            </div>
        );
    }
}