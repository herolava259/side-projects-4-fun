

const logoutBtn = document.querySelector('#btn-logout');
const loginMsg = document.querySelector('#welcome-message');

var userInfo = getFromStorage(CurrentUserInfoKey)||[];

if(userInfo.length == 0){
    loginMsg.innerText = "Ban chua dang nhap";
}else{
    userInfo = JSON.parse(userInfo);
    loginMsg.innerText = `Chao mung ${userInfo.username} da den trang web cua chung toi`;
}

logoutBtn.onclick = function(){
    userInfo = getFromStorage(CurrentUserInfoKey)||[];
    if(userInfo.length == 0)
    {
        loginMsg = 'Ban chua dang nhap';
        return;
    }

    removeItem(CurrentUserInfoKey);
    loginMsg.innerText = "Ban da dang xuat thanh cong";
}
