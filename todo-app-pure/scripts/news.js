
const url = "https://newsapi.org/v2/everything?q=Apple&from=2022-09-14&sortBy=popularity&apiKey=73f7d3ad61e34d1abdbc2be3c9a1cbac"

const btnPrev = document.querySelector('#btn-prev');

const btnNext = document.querySelector('#btn-next');

const pageInterval = document.querySelector('#page-num');

const newsContainer = document.querySelector('#news-container');

var newStart = 0;

var perPage = 5;

const settingJsonArr = localStorage.getItem(UserSettingKey)? JSON.parse(localStorage.getItem(UserSettingKey)) : [];
const userInfo = localStorage.getItem(CurrentUserInfoKey)? JSON.parse(localStorage.getItem(CurrentUserInfoKey)) : [];

var settingInfo;

for(let it of settingJsonArr)
{
    if(it.name = userInfo.username)
    {
        settingInfo = it;
        break;
    }
}

perPage = settingInfo.pagesize;



const xhttp = new XMLHttpRequest();

var responseJson;

xhttp.onload = function(){
    responseJson = this.responseText;
    console.log(this.responseText);
}



xhttp.open("GET", url, false);

xhttp.send();

const respObj = JSON.parse(responseJson);

const numPage = respObj.totalResults;
console.log(numPage);

const articles = respObj.articles;

function createPageItem(urlImg,altImg, title, description, urlPaper)
{
    urlImg = urlImg ? urlImg : "";
    altImg = altImg ? altImg : "";
    title = title ? title : "";
    description = description ? description : "";
    urlPaper = urlPaper ? urlPaper : "";
    return  `<div class="card flex-row flex-wrap">
    <div class="card mb-3" style="">
        <div class="row no-gutters">
            <div class="col-md-4">
                <img src="${urlImg}" class="card-img"
                    alt="${altImg}" style="width:max-width; height:auto">
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <h5 class="card-title">"${title}"</h5>
                    <p class="card-text">${description}</p>
                    <a href="${urlPaper}"
                        class="btn btn-primary">View</a>
                </div>
            </div>
        </div>
    </div>
    </div>`;
}

function doNextPapers()
{
    if(newStart >= numPage)
    {
        return;
    }
    
   
    let upper = newStart+perPage > numPage ? numPage : newStart+perPage;
    if(upper == newStart){
        return;
    }
    pageInterval.innerText = upper;
    newsContainer.innerHTML = "";
    for(let i = newStart ;i < upper; i++)
    {
        let article = articles[i];
        newsContainer.innerHTML += createPageItem(article.urlToImage,article.author,article.title,article.description,article.url);
    }
    
    newStart = upper;
    
}


function doPrevPapers()
{
    if(newStart <= perPage)
    {
        return;
    }
    
    let lower = newStart - perPage > 0 ? newStart - perPage : 0;
    let begin = lower-perPage > 0 ? lower-perPage : 0;

    if(lower == newStart)
    {
        return;
    }
    pageInterval.innerText = lower;
    newsContainer.innerHTML = "";
    for(let i = begin ;i < lower; i++)
    {
        let article = articles[i];
        newsContainer.innerHTML += createPageItem(article.urlToImage,article.author,article.title,article.description,article.url);
    }
    
    newStart = lower;
    
}


btnPrev.addEventListener('click', function(){
    doPrevPapers();
});

btnNext.addEventListener('click', function(){
    doNextPapers();
});

doNextPapers();