var images = [
    "/Uploads/1.jpg",
    "/Uploads/2.jpg",
    "/Uploads/3.jpg",
    "/Uploads/4.jpg",
  
];

var currentIndex = 0;
var backgroundContainer = document.querySelector(".background-container");

function changeBackground() {
    var nextIndex = (currentIndex + 1) % images.length;
    var nextImage = new Image();
    nextImage.onload = function () {
        backgroundContainer.style.transition = "background-image 1s ease-out";
        backgroundContainer.style.backgroundImage = "url(" + images[nextIndex] + ")";
        currentIndex = nextIndex;
    };
    nextImage.src = images[nextIndex];
}

setInterval(changeBackground, 3000); // Change the background every 5 seconds