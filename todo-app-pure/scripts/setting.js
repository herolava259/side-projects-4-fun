
const btnSave = document.querySelector('#btn-submit');
const msgSetting = document.querySelector('#msg-state');

const newPerPage = document.querySelector('#input-page-size');

const category = document.querySelector('#input-category');

function showError(msg){
    msgSetting.setAttribute('style', 'color: #e74c3c');

    msgSetting.innerText = msg;
}

function checkPageSizeInputError()
{
    let pageSize = parseInt(newPerPage.value);

    if(pageSize < 0){
        showError("So trang khong the la so am");
        return true;
    }else if(pageSize == 0)
    {
        showError("So trang khong the la 0");
        return true;
    }

    return false;
}

btnSave.onclick = function(){

    isPageSizeError = checkPageSizeInputError();

    if(isPageSizeError)
    {
        return ;
    }

    const userInfo = getFromStorage(CurrentUserInfoKey)||[];
    console.log(userInfo.userName);

    let settingInfo = {name:userInfo.username,pagesize:parseInt(newPerPage.value),category:category.value};

    saveSettingInfo(settingInfo,UserSettingKey);

    window.location.href = "../index.html";

}