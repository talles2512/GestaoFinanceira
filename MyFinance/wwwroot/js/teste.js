$(document).ready(function () {

    $('#bmc-wbtn').click(function () {
        var ifr = document.getElementById('ifr');
        var img = document.getElementById('btncircular');


        if (ifr.style.border == 'white') {
            ifr.style.cssText = 'position: fixed; margin: 0px; border: 0px; right: 18px; bottom: 98px; height: 0px; opacity: 0; width: calc(100vw-38px); max-width: 320px;'
                + 'border - radius: 10px; box - shadow: rgba(0, 0, 0, 0.4) 0px 8px 16px; z-index: 99999; transition: all 0.4s ease 0s; max - height: 620px; pointer - events: auto; ';
        }

        else {
            ifr.style.cssText = 'position: fixed; margin: 0px; border: white; right: 18px; bottom: 98px; height: calc(100% - 140px);'
                + 'opacity: 1; width: calc(100vw - 48px); max-width: 420px; border-radius: 10px; box-shadow: rgba(0, 0, 0, 0.4) 0px 8px 16px;'
                + 'z-index: 99999; transition: all 0.4s ease 0s; max-height: 620px; pointer-events: auto;';
        }
        })
})