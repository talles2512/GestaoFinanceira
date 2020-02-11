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


//função para resgatar o URL do provedor de conteúdo
function getScriptUrl() {
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

//função para resgatar os parâmetros contidos no URL
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

//função para carregar scripts auxiliares em arquivos externos
function loadScript(url, callback) {
    var script = document.createElement('script');
    script.async = true;
    script.src = url;

    var entry = document.getElementsByTagName('script')[0];
    entry.parentNode.insertBefore(script, entry);
    console.log('passei pelo loadScript');
    //script.onload = script.onreadystatechange = function () {
    //    var rdyState = script.readyState;
    //    if (!rdyState || /complete|loaded/.test(script.readyState)) {
    //        callback();
    //        script.onload = null;
    //        script.onreadystatechange = null;
    //    }
    //}
}

//função para carregar folhas de estilo
function loadStylesheet(url) {
    var link = document.createElement('link');

    link.rel = 'stylesheet';
    link.type = 'text/css';
    link.href = url;

    var entry = document.getElementsByTagName('script')[0];
    entry.parentNode.insertBefore(link, entry);
}

//função para injetar CSS na página
function injectCss(css) {
    var style = document.createElement('style');
    style.type = 'text/css';
    css = css.replace(/\}/g, "}\n");

    if (style.styleSheet) {
        style.styleSheet.cssText = css;
    }
    else {
        style.appendChild(document.createTextNode(css));
    }

    var entry = document.getElementsByTagName('script');
    entry.parentNode.insertBefore(style, entry);
}

//função para resgatar o estilo de um determinado elemento
function getStyle(node, property, camel) {
    var value;
    if (window.getComputedStyle) {
        value = document.defaultView
            .getComputedStyle(node, null)
            .getPropertyValue(property);
    }
    else if (node.currentStyle) {
        value = node.currentStyle[property] ? node.currentStyle[property] : node.currentStyle[camel];
    }

    if (value === '' || value === 'transparent' || value === 'rgba(0,0,0,0)') {
        return getStyle(node.parentNode, property, camel);
    }

    else {
        return value || '';
    }
}

//função para restagar o estilo básico de um container
function getBasicStyles(container) {
    var anchor = document.createElement('a');
    container.appendChild(anchor);

    function get(prop, camel) {
        return getStyle(container, prop, camel)
    }

    var styles = {
        anchorColor: getStyle(anchor, 'color'),
        fontColor: get('color'),
        backgroundColor: get('backgroud-color', 'backgroudColor'),
        direction: get('direction'),
        fontFamily: get('font-family', 'fontFamily').replace(/['"]/g, '')
    };

    anchor.parentNode.removeChild(anchor);
    return styles;
}

//função para aplicar estilos ao elemento body
function applyStyles(document, styles) {
    var body = document.getElementsByTagName('body')[0];
    for (var property in styles) {
        if (!styles.hasOwnProperty(property))
            return;

        body.style[property] = styles[pro]
    }
}
//applyStyles(iframe.contentWindow.document, getBasicStyles(container))

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

function retornarView(id) {
    var h1 = document.getElementsByTagName('h1');
    var ifr = document.createElement('iframe');
    ifr.style.width = '250px';
    ifr.style.height = '300px';
    ifr.style.border = 'none';
    ifr.style.paddingLeft = '350px';
    ifr.src = 'http://localhost:58980/Usuario/Teste?id=' + id;

    h1[0].insertAdjacentElement('afterend', ifr);
}

function retornarIframe() {
    var h1 = document.getElementsByTagName('h1');

    var div1 = document.createElement('div');
    div1.style.cssText = 'position: fixed; top: 0px; left: 0px; width: 100 %; height: 100 %; background: rgba(0, 0, 0, 0); text - align:'
        + 'center; z - index: 9999; transition: all 0.4s ease 0s;';

    h1[0].insertAdjacentElement('afterend', div1);

    var ifr = document.createElement('iframe');
    ifr.id = 'ifr';
    ifr.style.cssText = 'position: fixed; margin: 0px; border: 0px; right: 18px; bottom: 98px; height: 0px; opacity: 0; width: calc(100vw - 38px); max-width: 420px;'
        + 'border-radius: 10px; box-shadow: rgba(0, 0, 0, 0.4) 0px 8px 16px; z-index: 99999; transition: all 0.4s ease 0s; max-height: 620px; pointer-events: auto; ';
    ifr.src = 'http://localhost:59328/';

    div1.insertAdjacentElement('afterbegin',ifr);
}



function retornarBtnCircular() {
    var h1 = document.getElementsByTagName('h1');

    var div1 = document.createElement('div');
    div1.id = 'bmc-wbtn';
    div1.style.cssText = 'display: flex; align-items: center; justify-content: center; width: 64px; height: 64px; background: rgb(173, 223, 247); color: white;'
        + 'border-radius: 32px; position: fixed; right: 18px; bottom: 18px; box-shadow: rgba(0, 0, 0, 0.4) 0px 4px 8px; z-index: 999; cursor: pointer;'
        + 'font-weight: 600; transition: all 0.2s ease 0s; transform: scale(1);';
    h1[0].insertAdjacentElement('afterend', div1);

    var img = document.createElement('img');
    img.id = 'btncircular';
    img.src = 'http://localhost:58980/images/verisys.png';
    img.style.cssText = 'height: 50px; width: 50px; margin: 0; padding: 0;';

    div1.insertAdjacentElement('afterbegin', img);
}

function inserirScriptBotao() {
    var script = document.createElement('script');
    script.id = "oi";
    var entry = document.getElementsByTagName('script')[0];
    entry.parentNode.insertBefore(script, entry);
    var text = 'console.log("oi")';

    script.insertAdjacentText('afterbegin', text);


    //script.onload = script.onreadystatechange = function () {
    //    $('#bmc-wbtn').click(function () {
    //        var ifr = document.getElementById('ifr');
    //        ifr.style.cssText = 'position: fixed; margin: 0px; border: 0px; right: 18px; bottom: 98px; height: 0px; opacity: 0; width: calc(100vw-38px); max-width: 320px;'
    //            + 'border - radius: 10px; box - shadow: rgba(0, 0, 0, 0.4) 0px 8px 16px; background: url("https://marketplace.kony.com/static/dist/images/loader.svg)'
    //            + 'center center / 64px no - repeat rgb(255, 255, 255); z - index: 99999; transition: all 0.4s ease 0s; max - height: 620px; pointer - events: auto; ';
    //    })
    //}

}


var url = getScriptUrl();
console.log(url);
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
//getConta(id);
retornarBtnCircular();
retornarIframe();
loadScript('http://localhost:58980/js/teste.js', null);
// Write your JavaScript code.