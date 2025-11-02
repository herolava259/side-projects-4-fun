import React from "react";
import Image from "../../Base/Image";
import './index.css';
import ShowMoreButton from '../ViewMore';
import news from "../Data/news.json"
import NewItem from "./NewItem"



export default class News extends React.Component {
    constructor(props) {
        super(props);

        let maxSize = news.length;
        this.state = {
            news: news,
            displaySize: maxSize >= 4 ? 4: maxSize,
            maxItemSize: maxSize,
            isMore: maxSize > 4
        }
        this.handleShowMore = this.handleShowMore.bind(this);
        this.handleViewLess = this.handleViewLess.bind(this);
    }

    handleShowMore()
    {
       
        this.setState(state =>{
            
            let newState = {
                news: state.news,
                displaySize: state.displaySize + 1 > state.maxItemSize  ? state.maxItemSize: state.displaySize + 1,
                maxItemSize: state.maxItemSize,
                isMore: state.isMore
            }

            newState.isMore = newState.displaySize < newState.maxItemSize;
            return newState;
        });
    }

    handleViewLess()
    {
        this.setState(state =>({
            news: state.news,
            displaySize: state.maxItemSize >= 4 ? 4: state.maxItemSize,
            maxItemSize:state.maxItemSize,
            isMore: state.maxItemSize > 4
        }));
    }
    render() {
        return (
            <div className="announcement-wrapper">
                <div className="announcement-header">
                    <h2>News</h2>
                </div>
                <div className="annoucement-body announcement-list">
                    {
                        this.state.news.slice(0,this.state.displaySize)
                                               .map((item,index)=>
                                               {
                                                return(
                                                <div key={index} className="announcement-item">
                                                    <NewItem title={item.title}
                                                          imgUrl={Image[item.img]}
                                                          occur = {item.date}
                                                          desp = {item.content}
                                                          order = {index}
                                                    />
                                                </div>
                                                );
                                               })
                    }
                </div>
                <div className="annoucement-footer">
                    
                    <ShowMoreButton handleShowMore={this.handleShowMore} handleViewLess={this.handleViewLess} isMore = {this.state.isMore}/>
                </div>
            </div>
        )
    }
}