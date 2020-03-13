class Stock extends React.Component {
    constructor() {
        super();
        this.state = { stocks: [] };

        this.change = this.change.bind(this);
        this.add = this.add.bind(this);
        this.del = this.del.bind(this);
        this.edit = this.edit.bind(this);
    }

    async resp() {
        await fetch('/stock', {
            method: 'GET',
            headers: {
                'Authorization': `any`,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('Request successful', q);
            this?.setState({ stocks: q });
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    componentDidMount() {
       this?.resp();   
    }

    async add(e) {
        await fetch("/stock", {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: document.getElementById("in").value,
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ add stock');
            document.getElementById("in").value = "";

            this?.resp();
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async del(e) {
        await fetch("/stock/" + e.currentTarget.parentNode.id, {
            method: "DELETE",
            headers: {
                "Accept": "application/json"
            }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ del ');
            this?.resp();
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });

    }

    async edit(e) {
        var id = e.currentTarget.parentNode.id;

        await fetch("/stock", {
            method: "PUT",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: parseInt(id, 10),
                name: e.currentTarget.parentNode.children.namedItem("txt" + id).value,
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ edit ');
            this.resp();
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    change(e) {
        var stcks = this.state.stocks;
        var index = stcks.findIndex(q => q.id == e.currentTarget.parentNode.id);
        stcks[index].name = event.target.value;
        this.setState({ stocks: stcks });
    }

    render() {

        const divStyle = {
            margin: '0.2%',
            border: '0.5px solid black',
            padding: '1%',
        };

        return (
            <div>
                {
                    this?.state.stocks.length == 0 ?
                        <label>  </label> :
                        this.state.stocks.map(item =>
                            <div id={item.id} key={item.id} style={divStyle}>
                                <label>{item.id}</label>
                                <input type="text" id={"txt" + item.id} onChange={this.change} value={item.name} ></input>
                                <button id="edit" onClick={this.edit} style={{ float: 'right' }}>Редактировать</button>
                                <button  onClick={this.del} style={{ float: 'right' }}> Удалить</button>
                                <br />
                            </div>)
                }   

                <div style={divStyle}>
                    <input id="in" type="text"></input> <button onClick={this.add}> Добавить склад </button>
                </div>
            </div>
        );
    }
}

class Goods extends React.Component {
    constructor() {
        super();
        this.state = { goods: [] };

        this.change = this.change.bind(this);
        this.add = this.add.bind(this);
        this.del = this.del.bind(this);
        this.edit = this.edit.bind(this);
    }

    resp() {
        fetch('/goods', {
            method: 'GET',
            headers: {
                'Authorization': `any`,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('Request successful', q);
            this?.setState({ goods: q });
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    componentDidMount() {
        this?.resp();
    }

    async add(e) {
        await fetch("/goods", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                name: document.getElementById("inName").value,
                article: parseInt(document.getElementById("inArt").value, 10),
                weight: parseInt(document.getElementById("inWeig").value, 10),
                size: parseInt(document.getElementById("inSiz").value, 10),
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ add product');
            document.getElementById("inName").value = "";
            document.getElementById("inArt").value = "";
            document.getElementById("inWeig").value = "";
            document.getElementById("inSiz").value = "";
            this?.resp();
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async del(e) {
        await fetch("/goods/" + e.currentTarget.parentNode.id, {
            method: "DELETE",
            headers: { "Accept": "application/json" }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ del ');
            this?.resp();
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async edit(e) {
        var id = e.currentTarget.parentNode.id;
        await fetch("/goods", {
            method: "PUT",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                id: parseInt(id, 10),
                name: e.currentTarget.parentNode.children.namedItem("name" + id).value,
                article: parseInt(e.currentTarget.parentNode.children.namedItem("art" + id).value, 10),
                weight: parseInt(e.currentTarget.parentNode.children.namedItem("weig" + id).value, 10),
                size: parseInt(e.currentTarget.parentNode.children.namedItem("siz" + id).value, 10),
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ edit ');
            this?.resp();
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    change(e) {
        var gds = this?.state.goods;
        var id = e.currentTarget.parentNode.id;
        var index = gds.findIndex(q => q.id == id);
        switch (e.currentTarget.id) {
            case "name" + id:
                gds[index].name = event.target.value;
                break;
            case "art" + id:
                gds[index].article = event.target.value;
                break;
            case "weig" + id:
                gds[index].weight = event.target.value;
                break;
            case "siz" + id:
                gds[index].size = event.target.value;
                break;
        }
        
        this.setState({ goods: gds });
    }

    render() {

        const divStyle = {
            margin: '0.2%',
            border: '0.5px solid black',
            padding: '1%',
        };

        return (
            <div>
                {
                    this?.state.goods.length == 0 ?
                        <label>  </label> :
                        this.state.goods.map(item =>
                            <div id={item.id} key={item.id} style={divStyle}>
                                <label style={{ margin: '0.2%' }}>{item.id}</label>
                                <label style={{ margin: '0.2%' }}>Название</label>
                                <input type="text" id={"name" + item.id} onChange={this.change} value={item.name} ></input>
                                <label style={{ margin: '0.2%' }}>Артикл</label>
                                <input type="number" id={"art" + item.id} onChange={this.change} value={item.article} ></input>
                                <label style={{ margin: '0.2%' }}>Вес</label>
                                <input type="number" id={"weig" + item.id} onChange={this.change} value={item.weight} ></input>
                                <label style={{ margin: '0.2%' }}>Обьем</label>
                                <input type="number" id={"siz" + item.id} onChange={this.change} value={item.size} ></input>


                                <button id="edit" onClick={this.edit} style={{ float: 'right' }}>Редактировать</button>
                                <button onClick={this.del} style={{ float: 'right' }}> Удалить</button>
                                <br />
                            </div>)
                }

                <div style={divStyle}>
                    <label style={{ margin: '0.2%' }}>Название</label>
                    <input style={{ margin: '0.2%' }} id="inName" type="text"></input>
                    <label style={{ margin: '0.2%' }}>Артикл</label>
                    <input style={{ margin: '0.2%' }} id="inArt" type="number" defaultValue="0"></input>
                    <label style={{ margin: '0.2%' }}>Вес</label>
                    <input style={{ margin: '0.2%' }} id="inWeig" type="number" defaultValue="0"></input>
                    <label style={{ margin: '0.2%' }}>Обьем</label>
                    <input style={{ margin: '0.2%' }} id="inSiz" type="number" defaultValue="0"></input>

                    <button style={{ margin: '0.2%' }} onClick={this.add}> Добавить продукт </button>

                </div>
            </div>
        );
    }
}

class StockInGoods extends React.Component {
    constructor() {
        super();
        this.state = {
            stocks: [],
            goods: [], 
            stockingoods: [],  
        };

        this.change = this.change.bind(this);
        this.addnew = this.addnew.bind(this);
        this.del = this.del.bind(this);
        this.editCur = this.editCur.bind(this);

        this.transf = this.transf.bind(this);

        this.handleChange = this.handleChange.bind(this);
    }

    async resp(api) {
        await fetch(api, {
            method: 'GET',
            headers: {
                'Authorization': `any`,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {            
            switch (api) {
                case '/goods':
                    console.log('Request successful goods', q);
                    this.setState({ goods: q });
                    break;
                case '/stock':
                    console.log('Request successful stocks', q);
                    this.setState({ stocks: q });
                    break;
                case '/stockingoods':
                    console.log('Request successful stockingoods', q);
                    this.setState({ stockingoods: q });
                    break;
            }
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async componentDidMount() {
         this.resp('/goods');
         this.resp('/stock');
         this.resp('/stockingoods');
    }

    async add(st, pr, num) {

        await fetch("/stockingoods", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                stockId: st,
                goodsId: pr,
                number: num,
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ add stockingoods');
            this?.resp('/stockingoods');
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async addoperation(typeId, ofSt, inSt, gd, num) {
        await fetch("/operation", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                operationTypeId: typeId,
                ofStock: ofSt,
                inStock: inSt,
                goods: gd,
                number: num,
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ add operation');
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async addnew(e) {
        if (parseInt(document.getElementById("inNum").value, 10) >= 0) {
            this.addoperation(1, " ", this.state.stocks.find(q => q.id == parseInt(document.getElementById("inStock").value, 10))?.name,
                this.state.goods.find(q => q.id == parseInt(document.getElementById("inProd").value, 10))?.name, parseInt(document.getElementById("inNum").value, 10));

            this.add(parseInt(document.getElementById("inStock").value, 10), parseInt(document.getElementById("inProd").value, 10), parseInt(document.getElementById("inNum").value, 10));
        }
    }

    transf(e) {
        var id = e.currentTarget.parentNode.parentNode.id;
        var item = this.state.stockingoods.find(q => q.id == parseInt(id, 10));

        var numtr = parseInt(document.getElementById("trnum" + e.currentTarget.parentNode.parentNode.id).value, 10);
        var numcr = parseInt(document.getElementById("num" + id).value, 10);

        var stcr = item.stockId;
        var sttr = parseInt(document.getElementById("trst" + id).value, 10);

        var prod = item.goodsId;

        if (numtr <= 0 || numtr > numcr || numcr - numtr < 0)
            return;

        this.add(sttr, prod, numtr);
        this.edit(parseInt(id, 10), stcr, prod, numcr - numtr);

        this.addoperation(3, document.getElementById("st" + id).value,
            this.state.stocks.find(q => q.id == sttr).name, document.getElementById("pr" + id).value, numtr);
    }

    async edit(id, stId, gdId, num) {
        await fetch("/stockingoods", {
            method: "PUT",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                id: id,
                stockId: stId,
                goodsId: gdId,
                number: num,
            })
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ edit ');
            this?.resp('/stockingoods');
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    async editCur(e) {
        var id = e.currentTarget.parentNode.parentNode.id;
        var item = this.state.stockingoods.find(q => q.id == parseInt(id, 10));
        alert(document.getElementById("num" + id).value + "  " + document.getElementById("num" + id).name);
        var cur = parseInt(document.getElementById("num" + id).value, 10);
        var old = parseInt(document.getElementById("num" + id).placeholder, 10);

        this.addoperation(0, document.getElementById("st" + id).value," ",
                document.getElementById("pr" + id).value, cur );

        this.edit(parseInt(id, 10), item.stockId, item.goodsId, parseInt(document.getElementById("num" + id).value, 10));
    }

    async del(e) {
        var id = e.currentTarget.parentNode.parentNode.id;
        await fetch("/stockingoods/" + id, {
            method: "DELETE",
            headers: { "Accept": "application/json" }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            console.log('succ del ');
            this.addoperation(2, document.getElementById("st" + id).value,
                " ", document.getElementById("pr" + id).value, parseInt(document.getElementById("num" + id).value, 10));
            this?.resp('/stockingoods');
            
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    change(e) {
        var stgds = this?.state.stockingoods;
        var id = e.currentTarget.parentNode.parentNode.parentNode.id;
        var index = stgds.findIndex(q => q.id == id);

        switch (e.currentTarget.id) {
            case "num" + id:
                stgds[index].number = event.target.value;
                break;
        }

        this.setState({ stockingoods: stgds });
    }

    handleChange(event) {
        if (event.currentTarget.id == "inStock") {
            this.setState({ valInStock: event.currentTarget.value });
        }
        else if (event.currentTarget.id == "inProd") {
            this.setState({ valInGoods: event.currentTarget.value });
        }
    }

   

    render() {

        const divStyle = {
            margin: '0.1%',
            border: '0.5px solid black',
            padding: '0.1%',
        };

        const divBl = {
            margin: '0.3%',
            display: 'inline-block',
        };
    
        return (
            <div>
                {
                    (this?.state.stockingoods.length == 0 || this?.state.goods.length == 0 || this?.state.stocks.length == 0) ?
                        <label>  </label> :
                        this.state.stockingoods.sort((a, b) => a.id > b.id ? 1 : -1).map(item =>
                            <div id={item.id} key={item.id} style={divStyle}>

                                <div style={{ border: '0.5px solid gray', display: 'inline-block', margin: '0.1%', width: '54%', padding: '0.1%' }}>

                                    <label>{item.id}</label>

                                    <div style={divBl}>
                                        <label>Склад</label><br/>
                                        <input type="text" id={"st" + item.id} disabled value={this.state.stocks.find(st => st.id == item.stockId)?.name} ></input>
                                    </div>

                                    <div style={divBl}>
                                        <label>Товар</label><br />
                                        <input type="text" id={"pr" + item.id} disabled value={this.state.goods.find(st => st.id == item.goodsId)?.name}></input>
                                    </div>

                                    <div style={divBl}>
                                        <label>Количество</label><br />
                                        <input type="number" id={"num" + item.id} style={{ width: '90px' }} onChange={this.change} value={item.number}></input>
                                    </div>

                                    <button id="edit" onClick={this.editCur} >Редактировать</button>
                                    <button onClick={this.del} > Удалить</button>
                                </div>

                                <div style={{ border: '0.5px solid gray', display: 'inline-block', margin: '0.2%', width: '44%', padding: '0.1%'}}>
                                    <div style={divBl}>
                                        <label>Переместить на</label><br />
                                        <select id={"trst" + item.id} multiple={false}>
                                            {
                                                this.state.stocks.map(q => {
                                                    if (q.id != item.stockId)
                                                       return <option key={q.id} value={q.id}>{q.name}</option>
                                                })}
                                        </select>
                                    </div>

                                    <div style={divBl} >
                                        <label>Количество</label><br />
                                        <input type="number" style={{ width: '90px' }} id={"trnum" + item.id} defaultValue="0" ></input>
                                    </div>

                                    <button id="transf" onClick={this.transf} style={{ display: 'inline-block'}}>Переместить</button>
                                </div>
                                
                            </div>)
                }

                <div style={divStyle}>
                    <label style={{ margin: '0.2%' }}>Склад</label>
                    <select id="inStock"  multiple={false}>
                        {this.state.stocks.map(q => <option key={q.id} value={q.id}>{q.name}</option>)}
                    </select>

                    <label style={{ margin: '0.2%' }}>Товар</label>
                    <select id="inProd" multiple={false}>
                        {this.state.goods.map(q => <option key={q.id} value={q.id}>{q.name}</option>)}
                    </select>

                    <label style={{ margin: '0.2%' }}>Количество</label>
                    <input style={{ margin: '0.2%', width: '90px'}} id="inNum" type="number" defaultValue="0"></input>

                    <button style={{ margin: '0.2%' }} onClick={this.addnew}>Добавить товар на склад</button>

                </div>
            </div>
        );
    }
}

class Operation extends React.Component {
    constructor() {
        super();
        this.state = {
            operation: [],
            type : [],
        };
    }

    componentDidMount() {
        this.resp('/operation');
        this.resp('/operationtype');
    }

    async resp(api) {
        await fetch(api, {
            method: 'GET',
            headers: {
                'Authorization': `any`,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            return response.json();
        })
        .then(q => {
            switch (api) {
                case '/operation':
                    console.log('Request successful', q);
                    this?.setState({ operation: q });
                    break;
                case '/operationtype':
                    console.log('Request successful', q);
                    this?.setState({ type: q });
                    break;
            }
            
        })
        .catch(function (error) {
            console.log('Request failed', error)
        });
    }

    render() {

        const divStyle = {
            margin: '0.2%',
            border: '0.5px solid black',
        };

        const divBl = {
            margin: '0.1%',
            display: 'inline-block',
        };

        return (
            <div>
                {
                    (this.state.operation.length == 0 || this.state.type.length != 3) ?
                        <label>Операции не произведены</label> :
                        this.state.operation.reverse().map(item =>
                            <div id={item.id} key={item.id} style={divStyle}>
                                <label style={{ margin: '0.2%' }}>{item.id}</label>

                                <div style={divBl}>
                                    <label>Тип операции</label><br />
                                    <input type="text" id={"tp" + item.id} disabled value={this.state.type.find(q => q.id == item.operationTypeId)?.nameType} ></input> 
                                    
                                </div>

                                <div style={divBl}>
                                    <label>Со склада</label><br/>
                                    <input type="text" id={"ofst" + item.id} disabled value={item.ofStock}></input>
                                </div>

                                <div style={divBl}>
                                    <label>На склад</label><br />
                                    <input type="text" id={"inst" + item.id} disabled value={item.inStock} ></input>
                                </div>

                                <div style={divBl}>
                                    <label>Продукт</label><br />
                                    <input type="text" id={"gd" + item.id} disabled value={item.goods} ></input>
                                </div>

                                <div style={divBl}>
                                    <label>Количество</label><br />
                                    <input type="number" id={"num" + item.id} disabled value={item.number} ></input>
                                </div>

                                <div style={divBl}>
                                    <label>Дата</label><br />
                                    <input type="text" id={"date" + item.id} disabled value={item.date} ></input>
                                </div>

                            </div>)
                }
            </div>
        );
    }
}

function selected(e) {

    var active = function (e) {
        document.getElementById("content").style.display = "block";

        var tablinks = document.getElementsByClassName("tablinks");
        for (i = 0; i < tablinks.length; i++)
            tablinks[i].className = tablinks[i].className.replace(" active", "");

        e.className += " active";
    }

    switch (e.id) {
        case "St":
            ReactDOM.render(
                <Stock />,
                document.getElementById("content")
            );
            active(e);
            break;
        case "Pr":
            ReactDOM.render(
                <Goods />,
                document.getElementById("content")
            );
            active(e);            
            break;
        case "PrSt":
            ReactDOM.render(
                <StockInGoods />,
                document.getElementById("content")
            );
            active(e);
            break;
        case "Op":
            ReactDOM.render(
                <Operation />,
                document.getElementById("content")
            );
            active(e);
            break;
    }
}