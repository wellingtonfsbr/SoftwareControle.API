﻿
function toggleLightMode() {
    let body = document.querySelector('body');
    if (body.classList.contains('light')) {
        body.classList.remove('light');
    } else {
        body.classList.add('light');
        theme = "light";
    }
}