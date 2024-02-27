
updateTableWithoutParameter();









function updateTableWithoutParameter() {
    
    // Make an API call to fetch data based on the selected option  //https://eibtek2-001-site1.atempurl.com
    fetch(`https://habibaahmedm-002-site3.atempurl.com/api/ActiveUsersApi/`)
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
                const column3 = document.createElement("td");


                column1.innerText = rowData.loggedUserName;
                column2.innerText = rowData.createdBy;
                column3.innerText = rowData.updatedBy;

                row.appendChild(column1);
                row.appendChild(column2);
                row.appendChild(column3);

                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error fetching data:", error);
        });
}



