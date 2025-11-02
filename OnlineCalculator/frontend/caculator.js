
const display = document.querySelector('.display-text');

const digitList = document.querySelectorAll('.digit');

const multiply = document.querySelector('.multiply');

const add = document.querySelector('.add');

const minus = document.querySelector('.minus');

const divide = document.querySelector('.divide');

const equal = document.querySelector('.equal');

const dot = document.querySelector('.dot');

const ac = document.querySelector('.ac');

const de = document.querySelector('.de');

const hisBtn = document.querySelector('.histories-btn');

const hisList = document.querySelector('.history-list');

const clearHisList = document.querySelector('.clear-histories-btn')

// init global variable
var currOperator = '';

var prevOperand = '';

var currOperand = '';
var currOperatorIndex = -1;

digitList.forEach( key =>{
    key.onclick = function() {
        display.value += this.innerHTML;
        currOperand += this.innerHTML;
    }
})

ac.onclick = function(){
    display.value = display.value.slice(0,-1);
    debugger;
    if(currOperand.length > 0)
    {
        currOperand = currOperand.slice(0,-1);
    }
    else if(currOperand.length==0){
        currOperator = '';
        currOperatorIndex = -1;
        currOperand = prevOperand;
    }
}

de.onclick = function(){
    display.value = '';
    reset();
}
const operatorPriority = {'+':2, '-':2, '*':1, '/':1}
const operatorArr = '+-*/'

const domainPath = 'https://localhost:5001/api/calculator';
// binding event when click operator 

function reset(){
    currOperator = '';

    prevOperand = '';

    currOperand = '';
    currOperatorIndex = -1;
}

function showHistoriesList(resp)
{
    debugger;
    arr = JSON.parse(resp);
    elemArr = arr.map(item =>{
        return `<p class="history-item">${item.firstOperand} ${item.algOperator} ${item.lastOperand} = ${item.result} </p>`;
    });

    hisList.innerHTML = elemArr.join("\n");
}

function loadHistories()
{
    let path = domainPath + '/histories';
    let xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function()
    {
        if(this.readyState == 4)
        {
            if(this.status == 200){
                showHistoriesList(this.responseText)
            }else{
                console.log(this.responseText);
            }
        }
    }

    xhttp.open("GET", path, false);
    xhttp.send();
}
hisBtn.addEventListener('click', ()=>{loadHistories();});
clearHisList.addEventListener('click', ()=>{
    hisList.innerHTML = '';
})
function processingCalculation(elem){


    let resp = requestCaculation();
    let respObj = JSON.parse(resp);
    prevOperand = respObj.result;
    currOperand = '';
    currOperator = elem.innerHTML;
    display.value = prevOperand + currOperator;
    currOperatorIndex = display.value.length-1;
    
}

function onClickOperandBtn(elem){
    //debugger;
    const _last = display.value[display.value.length-1];
    if(operatorArr.includes(_last) || display.value === ''){
        return ;
    }
    display.value += elem.innerHTML;
    if(currOperator === ''){
        currOperator = elem.innerHTML;
        currOperatorIndex = display.value.length-1;
        prevOperand = currOperand
        currOperand = '';
    }
    else{
        processingCalculation(elem);
    }
}

add.addEventListener('click', () => {onClickOperandBtn(add)});
minus.addEventListener('click', () => {onClickOperandBtn(minus)});
multiply.addEventListener('click', () => {onClickOperandBtn(multiply)});
divide.addEventListener('click', () => {onClickOperandBtn(divide)});


function requestCaculation()
{
    let path = domainPath + '/addcalculation';
    var respCalc = '';

    const req = {
        firstOperand:prevOperand,
        lastOperand:currOperand,
        algOperator: currOperator, 
    }

    const xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function()
    {
        if(this.readyState == 4)
        {
            if(this.status == 200){
                respCalc = this.responseText;
            }else{
                console.log(this.responseText);
            }
        }
    }

    xhttp.open("POST", path, false);
    xhttp.setRequestHeader('Content-Type','application/json; charset=utf-8');
    xhttp.send(JSON.stringify(req));

    return respCalc;
}

equal.onclick = function(){

    if(currOperatorIndex == display.value.length-1)
    {
        return;
    }
    debugger;
    let resp = requestCaculation();
    let respObj = JSON.parse(resp);

    currOperand = respObj.result.toString();
    prevOperand = '';
    currOperator = '';
    currOperatorIndex = -1;
    display.value = currOperand + currOperator;
    
}

