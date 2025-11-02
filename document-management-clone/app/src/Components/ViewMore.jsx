import React from 'react';


export default class ShowMoreButton extends React.Component
{
    constructor(props)
    {
        super(props);
        
    }

    render()
    {
        
        let currEvent = this.props.isMore ? this.props.handleShowMore : this.props.handleViewLess;
        let sign = this.props.isMore ? 'Show more >>' : 'View Less <<'; 
        return (
            <button class="show-more-btn" onClick={currEvent}>{sign}</button>
        );
    }
}