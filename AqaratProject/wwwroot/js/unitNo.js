
function myFunction() {

    updateTable();
   
};













function updateTable() {
    const option = document.getElementById("employee").value;
    const option2 = document.getElementById("UnitNo");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`http://habibaahmedm-002-site3.atempurl.com/api/UnitNoApi/${option}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);

            option2.innerHTML = data;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
   
}




