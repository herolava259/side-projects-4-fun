import React from "react";
import Image from "../../Base/Image";
import "./index.css";
import Item from "./Item";
import ShowMoreButton from "../ViewMore";
import questions from "../Data/questions.json"
export default class Questions extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
        questions
    }
}

filterQuestions = (event) => {
  
  if (event.target.value === "") {
    this.setState({ questions: questions });
  } else {
    this.setState(
      {
        questions: questions.filter((item) =>
          item.question.toLowerCase().includes(event.target.value.toLowerCase())
        )
      },
      //gọi hàm callback sau khi trạng thái được cập nhật
      () => {
        console.log(this.state.questions);
      }
    );
  }
};


  render() {
    //question,answer
    return (
      <div>
        <div className="questions">
          <span className="questions-title">How do I</span>
          <div className="questions-input">
            <img src={Image.search} alt="" />
            <input type={"text"} placeholder="Find question" onChange={this.filterQuestions}/>
          </div>


          <div className="questions-wrap">
          {
            //Lấy ra 5 item
                        this.state.questions.slice(0,5).map((item, index) => {
                            return (
                              <div key={index}>
                              <Item 
                                question={item.question}
                                answer={item.answer}
                              />

                              </div>
                            )
                        })
          }
         
          <ShowMoreButton
            listitem=   {
            //Lấy ra các item còn lại
                        this.state.questions.slice(5,this.state.questions.length).map((item, index) => {
                            return (
                              <div key={index}>
                              <Item 
                                question={item.question}
                                answer={item.answer}
                              />

                              </div>
                            )
                        })
          }
          />
          </div>
        </div>
      </div>
    );
  }
}
