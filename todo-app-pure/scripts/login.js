
var username = document.querySelector('#username');
var password = document.querySelector('#password');
var form = document.querySelector('form');

function showError(input, msg)
{
    const parent = input.parentElement;

    const small = parent.querySelector('small');

    small.classList.add('error');

    small.innerText = msg;
}

function showSuccess(input)
{
    const parent = input.parentElement;

    const small = parent.querySelector('small');

    small.classList.remove('error');
}

function checkLengthError(input, min, max){

    input.value = input.value.trim();

    if(input.value.length < min){
        showError(input, `Phai co it nhat ${min} ky tu`);
        return true;
    }


    if(input.value.length > max)
    {
        showError(input, `Phai co it nhat ${min} ky tu`);
        return true;
    }

    showSuccess(input);
    return false;
}

function checkEmptyError(listInput){

    let isEmptyError = false;
    listInput.forEach(input=>{
        input.value = input.value.trim();

        if(!input.value){
            isEmptyError = true;
            showError(input, "Khong duoc bo trong");
        }else{
            showSuccess(input);
        }
    });
    return isEmptyError;
}



function checkAccountInStorage()
{
    let check = false;
    const userArr = JSON.parse(getFromStorage(UserArrKey))||[];


    for(let account of userArr)
    {
        if(account.username===username.value && account.password === password.value)
        {
            check = true;
            break;
        }
    }

    if(check == false){
        showError(username, 'Tai Khoan khong ton tai hoac mat khau khong chinh xac');
    }
    return check;
}

form.addEventListener('submit', function(e){
    e.preventDefault();

    let isEmptyError = checkEmptyError([username, password]);
   
    let isUserNameLengthError = checkLengthError(username, 3, 40);
    let isPasswordLengthError = checkLengthError(password, 4, 50);
    let isAccountInStorageError = !checkAccountInStorage();

    if(isEmptyError || isUserNameLengthError || isPasswordLengthError || isAccountInStorageError){
        // do nothing
    }else{

        saveCurrentUserInfo({username:username.value, password:password.value});
        window.location.href = "../index.html";
    }

});