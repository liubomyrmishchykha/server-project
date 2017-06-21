
//filter for data in trNgGrid 
instanceApp.filter("dateFilter", function () {
    var re = /^\/Date\((d|-|.*)\)[\/|\\]$/;

    return function (fieldValueUnused, item) {
        if (item != undefined) {
            var matches = item.match(re);
        }
        if (matches) {
            var parsed_date = new Date(parseInt(matches[1]));
            var curr_date = parsed_date.getDate();
            var curr_month = parsed_date.getMonth() + 1;
            var curr_year = parsed_date.getFullYear();
            return curr_year + "-" + curr_month + "-" + curr_date;
        }
        else return null;
    }
});

//filter for data on page
instanceApp.filter("dateFilterForPage", function () {
    var re = /^\/Date\((d|-|.*)\)[\/|\\]$/;
    return function (item) {
        if (item != undefined) {
            var matches = item.match(re);
        }
        if (matches) return new Date(parseInt(matches[1]));
        else return null;
    };
});

//filter for database status
instanceApp.filter("databaseStatus", function () {
    return function (item) {
        if (item == 0) {
            return "online";
        }
        else return "unavailable";
    }
});

//filter for inst status inside trNgGrid
instanceApp.filter("instStatus", function () {
    return function (fieldValueUnused, item) {
        if (item == "1") {
            return "online";
        }
        else return "ofline";
    }
});


