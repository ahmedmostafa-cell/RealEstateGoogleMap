myFunction();
function myFunction() {

    updateTable();
    updateTableDate1();
    updateTableDate2();
};

function myFunction2() {

    updateTable2();
    updateTableDate12();
    updateTableDate22();
};











function updateTable() {
    //const option = document.getElementById("employee").value;
    const option2 = document.getElementById("selledUnits");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/SelledUnitsApi`)
        .then(response => response.json())
        .then(data => {
            console.log(data);

            option2.innerHTML = data;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
   
}



function updateTable2() {
    const option = document.getElementById("exampleInputPassword1").value;
    const option1 = document.getElementById("exampleInputPassword2").value;
    const option2 = document.getElementById("selledUnits");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/SelledUnitsApi/${option}/${option1}`)
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
    //const option = document.getElementById("employee").value;
    const option2 = document.getElementById("unselledUnits");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/USelledUnitsApi`)
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
    //const option = document.getElementById("employee").value;
    const option2 = document.getElementById("unmovedUnits");
    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/UnMovedUnits`)
        .then(response => response.json())
        .then(data => {
            console.log(data);

            option2.innerHTML = data;

        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}