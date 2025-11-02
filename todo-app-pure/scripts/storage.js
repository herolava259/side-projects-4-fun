
const CurrentUserInfoKey = "CURRENT_USER";
const UserArrKey = "USER_ARR";
const UserSettingKey = "USER_SETTING_ARR";
const ToDoArrKey = "TODO_KEY";

var database = `[{"username": ""tungld"}]`;

function getFromStorage(key)
{
    return localStorage.getItem(key);
}


function saveCurrentUserInfo(userInfo, key = CurrentUserInfoKey)
{
    localStorage.setItem(key, JSON.stringify(userInfo));
}

function setFromStorage(obj, key)
{
    localStorage.setItem(key, JSON.stringify(obj));
}


function removeItem(key)
{
    localStorage.removeItem(key);
}

function setUserInfoToStorage(userObj, key = UserArrKey)
{
    let userArr = localStorage.getItem(key) ? JSON.parse(localStorage.getItem(key)) : [];


    //console.log(userObj);
    userArr.push(userObj);

    //console.log(JSON.stringify(userArr))

    localStorage.setItem(key, JSON.stringify(userArr));

}

function saveSettingInfo(settingObj, key=UserSettingKey)
{
    let userSettingArr = localStorage.getItem(key) ? JSON.parse(localStorage.getItem(key)) : [];

    for(let i =0; i < userSettingArr.length; i++)
    {
        if(userSettingArr[i].name === settingObj.name)
        {
            userSettingArr.splice(i,1);
            break;
        }
    }

    userSettingArr.push(settingObj);

    localStorage.setItem(key, JSON.stringify(userSettingArr));
}


function getSettingByUserName(name, key = UserSettingKey)
{
    let userSettingArr = localStorage.getItem(key) ? JSON.parse(localStorage.getItem(key)) : [];

    for(let stt of userSettingArr)
    {
        if(name === stt.username)
        {
            return stt;
        }
    }

    return NaN;
}

function getToDoListOfCurrUser(userKey = CurrentUserInfoKey){

    const userInfo = localStorage.getItem(userKey) ? JSON.parse(localStorage.getItem(userKey)) : [];
    let toDoList = {owner:userInfo.username,toDoList:[]};

    if(userInfo.length == 0){
        return NaN;
    }

    const toDoListArr = localStorage.getItem(ToDoArrKey) ? JSON.parse(localStorage.getItem(ToDoArrKey)) : [];


    
    for(let i =0; i < toDoListArr.length; i++)
    {
        if(toDoListArr[i].owner === userInfo.username)
        {
            toDoList = toDoListArr[i];
            toDoListArr.splice(i,1);
            break;
        }
    }

    toDoListArr.push(toDoList);
    localStorage.setItem(ToDoArrKey, JSON.stringify(toDoListArr));
    return toDoList;
}

function addToDoItemToStorage(toDoItem, toDoKey = ToDoArrKey, userKey = CurrentUserInfoKey)
{
    let toDoListArr = localStorage.getItem(toDoKey) ? JSON.parse(localStorage.getItem(toDoKey)) : [];

    let userInfo = localStorage.getItem(userKey) ? JSON.parse(localStorage.getItem(userKey)) : [];

    if(userInfo.length == 0)
    {
        return;
    }
    
    var i = 0;
    for(; i < toDoListArr.length; i++)
    {
        if(toDoListArr[i].owner === userInfo.username)
        {
            const list = toDoListArr[i].toDoList;
            for(let item of list)
            {
                if(item.task == toDoItem.task)
                {
                    return;
                }
            }
            list.push(toDoItem);
            break;
        }
    }
    // console.log(toDoListArr.length);
    // console.log(i);
    if(i == toDoListArr.length)
    {
        //console.log("in todo");
        toDoListArr.push({owner:userInfo.username, toDoList:[toDoItem]});
    }
    localStorage.setItem(toDoKey, JSON.stringify(toDoListArr));
    
    
}


function getToDoListToStorage(userKey = CurrentUserInfoKey, toDoArrKey = ToDoArrKey)
{
    let userInfo = localStorage.getItem(userKey) ? JSON.parse(localStorage.getItem(userKey)) : [];

    if(userInfo.length == 0)
    {
        return NaN;
    }

    const toDoListArr = localStorage.getItem(ToDoArrKey) ? JSON.parse(localStorage.getItem(ToDoArrKey)) : [];

    for(let list of toDoListArr)
    {
        if(list.owner === userInfo.username)
        {
            return list;
        }
    }

    return NaN;
}


function removeToDoItem(nameTask, userKey = CurrentUserInfoKey)
{
    
    let toDoList = getToDoListOfCurrUser(userKey);
    console.log(nameTask);
    console.log(toDoList.toDoList.length);
    for(let i = 0 ; i < toDoList.toDoList.length; i++)
    {
        let taskItem =  toDoList.toDoList[i].task;
        //console.log(toDoList.toDoList[i].task);
        if(nameTask === taskItem)
        {

            console.log(nameTask);
            toDoList.toDoList.splice(i,1);
        }
    }

    const toDoListArr = localStorage.getItem(ToDoArrKey) ? JSON.parse(localStorage.getItem(ToDoArrKey)) : [];

    for(let i=0; i<toDoListArr.length;i++)
    {
        if(toDoList.owner === toDoListArr[i].owner)
        {
            toDoListArr.splice(i,1);
            toDoListArr.push(toDoList);
            localStorage.setItem(ToDoArrKey,JSON.stringify(toDoListArr));
            return;
        }
    }
}

function replaceToDoItem(name ,isDone,  userKey= CurrentUserInfoKey)
{
    let userInfo = localStorage.getItem(userKey) ? JSON.parse(localStorage.getItem(userKey)) : [];

    const toDoListArr = localStorage.getItem(ToDoArrKey) ? JSON.parse(localStorage.getItem(ToDoArrKey)) : [];

    for(let list of toDoListArr)
    {
        if(list.owner === userInfo.username)
        {
            for(let item of list.toDoList)
            {
                if(item.task == name)
                {
                    item.isDone = isDone;
                    localStorage.setItem(ToDoArrKey, JSON.stringify(toDoListArr));
                    return;
                }
            }
        }
    }

    return ;

}