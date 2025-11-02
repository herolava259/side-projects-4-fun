import React from "react";
import "./index.css";
import events from "../Data/events.json"
import EventItem from "./EventItem";
import ShowMoreButton from "../ViewMore";
export default class Events extends React.Component {
    constructor(props) {
        super(props);
        let disSize = 4;
        let isMore = true;
        if(disSize > events.length)
        {
            disSize = events.length;
            isMore = false;
        }
        this.state = 
        {
            events: events,
            displaySize: disSize,
            isMore: isMore
        }
        
        this.handleShowMore = this.handleShowMore.bind(this);
        this.handleViewLess = this.handleViewLess.bind(this);

    }

    handleShowMore()
    {
        this.setState((state, props)=>{
            let newSize = state.displaySize + 1;
            let isMore = true;

            if(newSize > state.events.length)
            {
                newSize = state.events.length;
                isMore = false;
            }

            return {
                events: state.events,
                displaySize: newSize,
                isMore: isMore
            };
        });
    }

    handleViewLess()
    {
        this.setState(state =>{
            let disSize = 4;
            let isMore = true;
            if(disSize > state.events.length)
            {
                disSize = state.events.length;
                isMore = false;
            }

            return {
                events: events,
                displaySize: disSize,
                isMore: isMore
            }
            
        });
    }

    render() {
        return (
            <div className="event-wrapper">
                <div className="event-title">
                    <h3>Events</h3>
                </div>
                <div className="event-list">
                    {
                        this.state.events.slice(0, this.state.displaySize)
                                         .map((item, index) =>
                                         {
                                            return (
                                                <div key = {index} className="event-item">
                                                    <EventItem 
                                                               title={item.title}
                                                               day={item.date}
                                                               month={item.month}
                                                               fromTime={item.time}
                                                               toTime={item.timeend}
                                                               order = {index}
                                                    />
                                                </div>
                                            );
                                         })
                    }
                </div>

                <div className="event-footer">
                    <ShowMoreButton handleShowMore={this.handleShowMore} handleViewLess={this.handleViewLess} isMore={this.state.isMore}/>
                </div>
            </div>
        )
    }
}