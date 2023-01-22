let rangeInput = document.getElementById("rangeError");
let textInput = document.getElementById("textError");
rangeInput.addEventListener("input", function() {
    textInput.value = rangeInput.value.toString();
});

let changeRange = () => rangeInput.value = parseInt(textInput.value) < 10 ?
    textInput.value.toString() : rangeInput.value = 10;

textInput.addEventListener("input", changeRange);

changeRange();



