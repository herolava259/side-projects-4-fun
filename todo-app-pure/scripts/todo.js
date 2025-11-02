
const todoInput = document.querySelector("#input-task");
const todoList = document.querySelector('#todo-list');
const btnAdd = document.querySelector('#btn-add');


const uToDoList = getToDoListOfCurrUser();

uToDoList.toDoList.forEach(item=>{
    createToDoItem(item.task, item.isDone);
})

btnAdd.onclick = function(){

    let name = todoInput.value;
    if(name === "")
    {
        return;
    }
    createToDoItem(name,false);
    let toDoItem = {task:todoInput.value, isDone:0};
    addToDoItemToStorage(toDoItem);
}


function removeItem(input)
{
    var taskName = input.parentElement.innerText;
    

    taskName = taskName.substring(0,taskName.lastIndexOf("×")).trim();
    //console.log(taskName);
    removeToDoItem(taskName);

    input.parentElement.remove();
}

function createToDoItem(name, isDone)
{
    const newItem = document.createElement("li");

    if(isDone == 1)
    {
        newItem.classList.add('checked');
    }
    
    newItem.innerText = name;
    newItem.addEventListener("click",function(){

        console.log("click");
        const toDoList = getToDoListOfCurrUser();
        console.log(toDoList);
        
        for(let item of toDoList.toDoList)
        {
            if(item.task === name)
            {
                console.log(name);
                if(item.isDone == 0){
                    
                    replaceToDoItem(name,1);
                    newItem.classList.add('checked');
                }else{
                    replaceToDoItem(name,0);
                    newItem.classList.remove('checked');
                }
                return;
            }
        }
    });
    
    const closeBtn = document.createElement('span');

    closeBtn.innerText = "×";
    closeBtn.classList.add('close');

    closeBtn.onclick = function(){removeItem(this)};

    newItem.appendChild(closeBtn);

    todoList.appendChild(newItem);
}
