
updateTableWithoutParameter();
function myFunction() {
    
    updateTable();
};


function myFunctionDate1() {

    updateTableDate1();
};




function myFunctionDate2() {

    updateTableDate1();
};





function updateTable() {
    const option = document.getElementById("employee").value;

    // Make an API call to fetch data based on the selected option //https://localhost:44347/
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/LogHistoryApi/${option}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example1");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            data.forEach(rowData => {
                console.log(rowData);
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");



                column1.innerText = rowData.updatedBy;
                column2.innerText = rowData.createdDate;


                row.appendChild(column1);
                row.appendChild(column2);


                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}


function updateTableWithoutParameter() {
    
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/LogHistoryApi/`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example1");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            data.forEach(rowData => {
                console.log(rowData);
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");



                column1.innerText = rowData.updatedBy;
                column2.innerText = rowData.createdDate;


                row.appendChild(column1);
                row.appendChild(column2);


                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}



function updateTableDate1() {
    const option = document.getElementById("employee").value;
    const optionDate1 = document.getElementById("exampleInputPassword1").value;
    const optionDate2 = document.getElementById("exampleInputPassword2").value;
    // Make an API call to fetch data based on the selected option
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/LogHistoryApi/${option}/${optionDate1}/${optionDate2}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const table = document.getElementById("example1");
            const tbody = table.getElementsByTagName("tbody")[0];

            // Clear existing table rows
            tbody.innerHTML = "";

            // Add new rows based on the fetched data
            data.forEach(rowData => {
                console.log(rowData);
                const row = document.createElement("tr");
                const column1 = document.createElement("td");
                const column2 = document.createElement("td");



                column1.innerText = rowData.updatedBy;
                column2.innerText = rowData.createdDate;


                row.appendChild(column1);
                row.appendChild(column2);


                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}