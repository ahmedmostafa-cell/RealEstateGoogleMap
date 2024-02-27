
let items = [];
let ClsItems = {
    loaditems: function () {
        var objToday = new Date(),
            weekday = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'),
            dayOfWeek = weekday[objToday.getDay()],
            domEnder = function () { var a = objToday; if (/1/.test(parseInt((a + "").charAt(0)))) return "th"; a = parseInt((a + "").charAt(1)); return 1 == a ? "st" : 2 == a ? "nd" : 3 == a ? "rd" : "th" }(),
            dayOfMonth = today + (objToday.getDate() < 10) ? '0' + objToday.getDate() + domEnder : objToday.getDate() + domEnder,
            months = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'),
            curMonth = months[objToday.getMonth()],
            curYear = objToday.getFullYear(),
            curHour = objToday.getHours() > 12 ? objToday.getHours() - 12 : (objToday.getHours() < 10 ? "0" + objToday.getHours() : objToday.getHours()),
            curMinute = objToday.getMinutes() < 10 ? "0" + objToday.getMinutes() : objToday.getMinutes(),
            curSeconds = objToday.getSeconds() < 10 ? "0" + objToday.getSeconds() : objToday.getSeconds(),
            curMeridiem = objToday.getHours() > 12 ? "PM" : "AM";
        var today = curHour + ":" + curMinute + "." + curSeconds + curMeridiem + " " + dayOfWeek + " " + dayOfMonth + " of " + curMonth + ", " + curYear;
        console.log(today);
        console.log("aliiiiiiiiii");
        Helper.AjaxCallGet("/api/itemsapi", {}, "json", function (data) {
            let htmlData = "";
            console.log(data);
            items = data;
            for (let i = 0; i < data.length; i++) {
                console.log("fawzya");
                console.log(ClsItems.Template1(data[i]));
                htmlData = htmlData + ClsItems.Template1(data[i]);

            }
            console.log(htmlData);

            $("#DivTemplate1").html(htmlData);
        },
            function () {
            });

    },
    Template1: function (item) {
        console.log("fawzyaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        if (item.newSalesPrice !== null) {
            let itemhtmal = "<div  class='product product__style--3 col-lg-4 col-md-4 col-sm-6 col-12'>";
            itemhtmal = itemhtmal + " <div class='product__thumb'>";
            itemhtmal = itemhtmal + "<a class='first__img' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a>"
            itemhtmal = itemhtmal + " <a class='second__img animation1' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a>"
            itemhtmal = itemhtmal + " <div class='hot__box'> <span class='hot-label'>" + item.itemName + "</span></div></div><div class=product__content content--center'> <h4><a href='/Items/Detalis/" + item.itemId + "'>" + item.itemName + "</a></h4><ul class='prize d-flex'><li>" + item.newSalesPrice + "</li> <li class='old_prize'>" + item.salesPrice + "</li></ul>"
            itemhtmal = itemhtmal + "<div class='action'><div class='actions_inner'> <ul class='add_to_links'><li><a class='cart'  href='/Items/AddToCart/" + item.itemId + "'><i class='bi bi-shopping-cart-full'></i></a></li> <li><a class='wishlist' href='/WishList/AddToWishList/" + item.itemId + "'><i class='bi bi-heart-beat'></i></a></li></a></li></ul></div></div>"
            itemhtmal = itemhtmal + " <div class='product__hover--content'><ul class='rating d-flex'><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li></ul></div></div></div>"
            return itemhtmal;
        }
        else
        {
            let itemhtmal = "<div class='product product__style--3 col-lg-4 col-md-4 col-sm-6 col-12'>";
            itemhtmal = itemhtmal + " <div class='product__thumb'>";
            itemhtmal = itemhtmal + "<a class='first__img' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a>"
            itemhtmal = itemhtmal + " <a class='second__img animation1' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a>"
            itemhtmal = itemhtmal + " <div class='hot__box'> <span class='hot-label'>" + item.itemName + "</span></div></div><div class=product__content content--center'> <h4><a href='/Items/Detalis/" + item.itemId + "'>" + item.itemName + "</a></h4><ul class='prize d-flex'><li>" + item.salesPrice + "</li></ul>"
            itemhtmal = itemhtmal + "<div class='action'><div class='actions_inner'> <ul class='add_to_links'><li><a class='cart'  href='/Items/AddToCart/" + item.itemId + "'><i class='bi bi-shopping-cart-full'></i></a></li> <li><a class='wishlist' href='/WishList/AddToWishList/" + item.itemId + "'><i class='bi bi-heart-beat'></i></a></li></ul></div></div>"
            itemhtmal = itemhtmal + " <div class='product__hover--content'><ul class='rating d-flex'><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li></ul></div></div></div>"
            return itemhtmal;
        }
  

                
       
            
                
        
       
           
               

           
       
       
                  
									             
                                
                           
                        
                  
                
                
                   
                        
                        
                        
                        
                      
                    
                
            
        
    },
FilterItems: function (catId) {
    let newArray = $.grep(items, function (n, i) {
        return n.categoryId === catId;

    });
    console.log(newArray);
    let htmlData = "";

    for (let i = 0; i < newArray.length; i++) {

        console.log(ClsItems.Template1(newArray[i]));
        htmlData = htmlData + ClsItems.Template1(newArray[i]);

    }
    console.log(htmlData);

    $("#DivTemplate1").html(htmlData);




},
    Template2: function (item) {
        if (item.newSalesPrice !== null) {

            let itemhtmal = "<div  class='list__view' style='margin-bottom: 10px;'>";
            itemhtmal = itemhtmal + " <div class='thumb'><a class='first__img' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a><a class='second__img animation1' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a></div><div class='content'><h2><a href='/Items/Detalis/" + item.itemId + "'>" + item.itemName + "</a></h2>"
            itemhtmal = itemhtmal + "<ul class='rating d-flex'><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li></ul><ul class='prize d-flex'><li>" + item.newSalesPrice + "</li></ul>"
            itemhtmal = itemhtmal + "<ul class='cart__action d-flex'><li class='cart'><a href='/Items/AddToCart/" + item.itemId + "'>Add to cart</a></li><li class='wishlist'><a href='/WishList/AddToWishList/" + item.itemId + "'></a></li></ul></div></div>"
            return itemhtmal;
        }
        else
        {
            let itemhtmal = "<div class='list__view' style='margin-bottom: 10px;'>";
            itemhtmal = itemhtmal + " <div class='thumb'><a class='first__img' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a><a class='second__img animation1' href='/Items/Detalis/" + item.itemId + "'><img src='/Uploads/" + item.imageName + "' alt='product image'></a></div><div class='content'><h2><a href='/Items/Detalis/" + item.itemId + "'>" + item.itemName + "</a></h2>"
            itemhtmal = itemhtmal + "<ul class='rating d-flex'><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li class='on'><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li><li><i class='fa fa-star-o'></i></li></ul><ul class='prize d-flex'><li>" + item.salesPrice + "</li></ul>"
            itemhtmal = itemhtmal + "<ul class='cart__action d-flex'><li class='cart'><a href='/Items/AddToCart/" + item.itemId + "'>Add to cart</a></li><li class='wishlist'><a href='/WishList/AddToWishList/" + item.itemId + "'></a></li></ul></div></div>"
            return itemhtmal;

        }
    
      
                        
                    

                
									

















},
loaditems2: function () {

    console.log("Mohamed");
    Helper.AjaxCallGet("/api/itemsapi", {}, "json", function (data) {
        let htmlData = "";
        console.log(data);
        items = data;
        for (let i = 0; i < data.length; i++) {
            console.log("Ali");
            console.log(ClsItems.Template2(data[i]));
            htmlData = htmlData + ClsItems.Template2(data[i]);

        }
        console.log(htmlData);

        $("#DivTemplate2").html(htmlData);
    },
        function () {
        });

},
FilterItems2: function (catId) {
    let newArray = $.grep(items, function (n, i) {
        return n.categoryId === catId;

    });
    console.log(newArray);
    let htmlData = "";

    for (let i = 0; i < newArray.length; i++) {

        console.log(ClsItems.Template2(newArray[i]));
        htmlData = htmlData + ClsItems.Template2(newArray[i]);

    }
    console.log(htmlData);
    console.log(newArray.length);

    $("#DivTemplate2").html(htmlData);
    $("#categoryitemsno").html(newArray.length);
}

}

//let ClsItems = {
//    LoadItems: function () {
//        alert("kkhhj");
//        console.log('success');
//        $.ajax({
//            url: "https://localhost:44358/api/itemsapi",
//            type: 'GET',
//            dataType: 'Json',
//            data: JSON.stringify({}),
//            success: function (data) {
//                console.log('success', data);
//            }
//        });


//    }
//}

ClsItems.loaditems();
ClsItems.loaditems2();


									