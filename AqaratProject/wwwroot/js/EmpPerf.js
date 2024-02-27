
function myFunction() {

    updateTable();
    updateTableDate1();
    updateTableDate2();
};













function updateTable() {
    const option = document.getElementById("employee").value;
    const option2 = document.getElementById("BookingNo");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/EmployessPerfApi/${option}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);

            option2.innerHTML = data;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
   
}





function updateTableDate1() {
    const option = document.getElementById("employee").value;
    const option2 = document.getElementById("SalesNo");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/EmployessSalesNoApi/${option}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);

            option2.innerHTML = data;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}




function updateTableDate2() {
    const option = document.getElementById("employee").value;
    const option2 = document.getElementById("OfferNo");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/EmployeeOffersNoApi/${option}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);

            option2.innerHTML = data;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}