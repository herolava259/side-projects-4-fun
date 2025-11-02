var username = document.querySelector('#username');
var firstname = document.querySelector('#firstname');
var lastname = document.querySelector('#lastname');
var password = document.querySelector('#password');
var confirmPassword = document.querySelector('#confirm-password');
var form = document.querySelector('form');

function showError(input, message){

    let parent = input.parentElement;

    let small = parent.querySelector('small');
    parent.classList.add('error');

    small.innerText = message;
    
}

function showSuccess(input)
{
    let parent = input.parentElement;
    let small = parent.querySelector('small');
    parent.classList.remove('error');
    small.innerText = '';
}


function checkEmptyError(listInput){

    let isEmptyError = false;
    listInput.forEach(input=>{
        input.value = input.value.trim();

        if(!input.value){
            isEmptyError = true;
            showError(input, 'Khong duoc de trong')
        }else{
            showSuccess(input)
        }
    });


    return isEmptyError;
}


function checkMatchPasswordError(passwordInput, cfpasswordInput)
{
    if(passwordInput.value !== cfpasswordInput.value){
        showError(cfpasswordInput, 'Password is not match with the confirm');
        return true;
    }

    return false;
}

function checkLengthError(input, min, max){

    input.value = input.value.trim();

    if(input.value.length < min){
        showError(input, `Phai co it nhat ${min} ky tu`);
        return true;
    }


    if(input.value.length > max)
    {
        showError(input, `Khong duoc qua ${max} ky tu`);
        return true;
    }

    showSuccess(input);
    return false;
}

//Get du lieu de so sanh 






function checkUserNameSakeError(){

    const nameStr = username.value;
    const userArr = JSON.parse(getFromStorage(UserArrKey))||[];
    if(userArr.length == 0)
    {
        return false;
    }
    for(let uInfo of userArr)
    {
        if(uInfo["username"] === nameStr){
            showError(username, "Username is exist");
            return true;
        }
    }

    return false;
}

function saveUserInfo()
{
    var userInf = {firstname: firstname.value, lastname: lastname.value, username: username.value, password: password.value };
    console.log(JSON.stringify(userInf));
    setUserInfoToStorage(userInf);
}

form.addEventListener('submit', function(e){
    e.preventDefault();
    let isEmptyError = checkEmptyError([username,firstname, lastname, password, confirmPassword]);
   
    let isUserNameLengthError = checkLengthError(username, 3, 40);
    let isPasswordLengthError = checkLengthError(password, 4, 50);
    


    let isMatchPassword = checkMatchPasswordError(password, confirmPassword);
    let isUserNameSake = checkUserNameSakeError();

    if(isEmptyError || isUserNameLengthError || isPasswordLengthError || isMatchPassword || isUserNameSake){
        // do nothing
    }else{

        saveUserInfo();
        window.location.href = "../index.html";

    }

});


