//var Finance = (function (window, undefined) {
//    var Finance = {};

//    function loadSupportingFiles(callback) { }
//    function getContaParams() { }
//    function getDados(params, callback) { }
//    function retornarAcao() { }

//    loadSupportingFiles(function () {
//        var params = getContaParams();
//        getDados(params, function () {
//            retornarAcao();
//        });
//    });

//    return Finance;
//})(window);

function getScriptUrl() {
    console.log('passei pelo script.url');
    var script = document.getElementsByTagName('script');
    var element;
    var src;

    for (var i = 0; i < script.length; i++) {
        console.log(script);
        element = script[i];
        src = element.src;
        console.log(src);
        return src;
    }
}

function getQueryParams(query) {
    var args = query.split('&');
    var params = {};
    var pair;
    var key;
    var value;

    function decode(string) {
        return decodeURIComponent(string || "").replace('+', ' ');
    }

    for (var i = 0; i < args.length; i++) {
        pair = args[i].split('=');
        key = decode(pair[0]);
        value = decode(pair[1]);
        params[key] = value;
    }

    return params;
}

function getConta(id) {
    fetch('https://localhost:44307/conta?id=' + id)
        .then(function (response) {
            if (response.ok) {
                response.json().then(function (data) {
                    console.log(data);
                    retornarDados(data);
                });
            }
        })
        .catch(function (err) {
            console.error('Failed retrieving information', err);
        });
}

function retornarDados(data) {
    var h1 = document.getElementsByTagName('h1');
    var table = '<table class="table table-bordered">' +
        '<thead>' +
        '<tr>' +
        '    <th>Id</th> ' +
        '    <th>Nome</th>' +
        '    <th>Saldo</th>' +
        '</tr> ' +
        '</thead>' +
        '<tbody>' +
        '<tr> ' +
        '    <td>' + data.id + '</td> ' +
        '    <td>' + data.nome + '</td>' +
        '    <td>' + data.saldo + '</td>' +
        '</tr> ' +
        '</tbody >' +
        '</table >';


    h1[0].insertAdjacentHTML('afterend', table);


}


var url = getScriptUrl();
var parametros;

for (var i = 0; i < url.length; i++) {
    if (url[i] == '#') {
        parametros = url.substring(i, url.length);
    }
}

console.log(parametros);

var params = getQueryParams(parametros.replace(/^.*\#/, ''));
var id = params.id;
console.log(id);
getConta(id);

// Write your JavaScript code.