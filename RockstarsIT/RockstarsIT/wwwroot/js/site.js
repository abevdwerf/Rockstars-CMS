// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function OpenEditor(updatefield) {
    let editelements = document.getElementsByName(updatefield);
    for (let i = 0; i < editelements.length; i++) {
        if (editelements[i].hidden == true) {
            editelements[i].hidden = false;
            document.getElementById("editimg").src = '/icons/x-circle.svg'
        }
        else {
            editelements[i].hidden = true;
            document.getElementById("editimg").src = '/icons/Pen-geel.svg'
        }
    }
}