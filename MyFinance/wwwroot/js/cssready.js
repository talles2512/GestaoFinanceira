//função para verificar e exibir se um arquivo de folha de estilo foi carregado
function isCssReady(callback) {
    var testElem = document.createElement('span');
    testElem.id = 'stork-css-ready';
    testElem.style = 'color: #fff';
    var entry = document.getElementsByTagName('script')[0];
    entry.parentNode.insertBefore(testElem,entry);
    (function poll(){
        var node = document.getElementById('stork-css-ready');
        var value;

        if(window.getComputedStyle){
            value = document.defaultView
            .getComputedStyle(testElem, null)
            .getPropertyValue('color');
        }

        else if(node.currentStyle){
            value = node.currentStyle.color;
        }

        if(value && value === 'rgb(186, 218, 85)' ||
            value.toLowerCase() === '#bada55'){
                callback();
            }
        else{
            setTimeout(poll, 500);
        }
    })();
}

