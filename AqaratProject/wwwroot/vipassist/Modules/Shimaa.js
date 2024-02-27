
let ClsCategories = {

    LoadCategories2: function () {

        Helper.AjaxCallGet("/api/categoryapi", {}, "json", function (data) {
            let htmlData = "";
            console.log(data);
            for (let i = 0; i < data.length; i++) {
                console.log("Mostafa");
                console.log(ClsCategories.Template2(data[i]));
                htmlData = htmlData + ClsCategories.Template2(data[i]);

            }
            console.log(htmlData);

            $("#DivCategories").html(htmlData);
        },
            function () {
            });

    },
    Template2: function (cat) {
        let cattegory = "<li><a  style='cursor: pointer;' class='title' onclick='ClsItems.FilterItems2(" + cat.categoryId + "); ClsItems.FilterItems(" + cat.categoryId + ");'>" + cat.categoryName + "<span>" + cat.numberOfItems +"</span></a></li>";
        return cattegory;

    }
   

    
}

ClsCategories.LoadCategories2();
