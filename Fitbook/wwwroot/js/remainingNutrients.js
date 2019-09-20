(function () {
    console.log('hello world');
    let carbohydrates = document.getElementById("carbohydrates").innerText;
    let protein = document.getElementById("protein").innerText;
    let fat = document.getElementById("fat").innerText;
    let calories = document.getElementById("calories").innerText;
    let actualCarbohydrates = document.getElementById("actual-carbohydrates").innerText;
    let actualProtein = document.getElementById("actual-protein").innerText;
    let actualFat = document.getElementById("actual-fat").innerText;
    let actualCalories = document.getElementById("actual-calories").innerText;
    let date = document.getElementById("date");
    let today;

    console.log(carbohydrates);

    document.getElementById("remaining-carbohydrates").innerText = carbohydrates - actualCarbohydrates;
    document.getElementById("remaining-protein").innerText = protein - actualProtein
    document.getElementById("remaining-fat").innerText = fat - actualFat
    document.getElementById("remaining-calories").innerText = calories - actualCalories;

    today = new Date();
    date.innerText = `${(today.getMonth() + 1)}/${today.getDate()}/${today.getFullYear()}`;
})();