(function () {
    // grab elements
    let showCustomSplitButton = document.getElementById("show-custom-split-form");
    let prebuiltMacroSplitsForm = document.getElementById("prebuilt-macro-splits-form");
    let customMacroSplitsForm = document.getElementById("custom-macro-split-form");

    console.log(showCustomSplitButton);

    // add eventlisteners
    showCustomSplitButton.addEventListener('click', () => {
        console.log('button clicked');
        if (customMacroSplitsForm.style.display === "none") {
            customMacroSplitsForm.style.display = "block";
            prebuiltMacroSplitsForm.style.display = "none";
            showCustomSplitButton.innerText = "Show Recommended Splits";
        } else {
            customMacroSplitsForm.style.display = "none";
            prebuiltMacroSplitsForm.style.display = "block";
        }
    });
})();