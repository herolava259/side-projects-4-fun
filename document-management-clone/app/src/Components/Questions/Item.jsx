import React from "react";
import Image from "../../Base/Image";
export default class Item extends React.Component
{
    render()
    {
        const {question,answer}=this.props;
        
        return(
            <div className="questions-item">
            <div className="questions-item_how">
             
                <img src={Image.expand} alt="" className="question-image-bot"/>
             
                <img src={Image.collapse} alt="" className="question-image-top"/>
             
              <span>{question}</span>
            </div>
              <div className="questions-item_answer" >
                <span>A: </span>
                <span className="questions-item_answer-content">
                  {answer}
                </span>
              </div>
            
          </div>
        )
    }
}