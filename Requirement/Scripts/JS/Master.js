

var baseurl = document.baseURI;
$(function () {
    jQuery('.numbersOnly').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });

    //$('.datepicker').datepicker({
    //    dateFormat: 'dd-M-yy',
    //    maxDate: '0',
    //    //maxDate: "+1M +10D",
    //    changeMonth: true,
    //    changeYear: true,
    //});

});

/* Only Digit Allowed */
function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

$(".numberonly").on("input", function (evt) {
    var self = $(this);
    self.val(self.val().replace(/[^0-9.]/g, ''));
    if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
        evt.preventDefault();
    }
});

/* -------------------------------------------- Date Range -------------------------------*/
function GetCurrentMonth() {
    var d = new Date(),
        n = d.getMonth(),
        n = n + 1;
    console.log(n)
    return n;
}
function GetCurrentYear() {
    var d = new Date(),
        y = d.getFullYear();
    console.log(y)
    return y;
}
function toDate(dateString) {
    debugger;
    var parts = dateString.split('-');
    return new Date(parts[2] + "-" + parts[1] + "-" + parts[0]);
}
function getAge(dateString) {
    debugger;
    if (dateString != null && dateString != '' && dateString != undefined) {
        const dates = new Date(dateString); // {object Date}            

        const covdate = new Intl.DateTimeFormat("en-US", {
            year: "numeric",
            month: "2-digit",
            day: "2-digit"
        }).format(dates);
        dateString = covdate;

        var now = new Date();

        var yearNow = now.getFullYear();
        var monthNow = now.getMonth();
        var dateNow = now.getDate();
        //date must be mm/dd/yyyy
        var dob = new Date(dateString.substring(6, 10),
            dateString.substring(0, 2) - 1,
            dateString.substring(3, 5)
        );

        var yearDob = dob.getFullYear();
        var monthDob = dob.getMonth();
        var dateDob = dob.getDate();
        var age = {};
        var ageString = "";
        var yearString = "";
        var monthString = "";
        var dayString = "";


        yearAge = yearNow - yearDob;

        if (monthNow >= monthDob)
            var monthAge = monthNow - monthDob;
        else {
            yearAge--;
            var monthAge = 12 + monthNow - monthDob;
        }

        if (dateNow >= dateDob)
            var dateAge = dateNow - dateDob;
        else {
            monthAge--;
            var dateAge = 31 + dateNow - dateDob;

            if (monthAge < 0) {
                monthAge = 11;
                yearAge--;
            }
        }

        age = {
            years: yearAge,
            months: monthAge,
            days: dateAge
        };

        if (age.years > 1) yearString = " years";
        else yearString = " year";
        if (age.months > 1) monthString = " months";
        else monthString = " month";
        if (age.days > 1) dayString = " days";
        else dayString = " day";



        if ((age.years > 15) && (age.months > 0) && (age.days > 0))
            ageString = age.years + yearString + ", " + age.months + monthString + ", and " + age.days + dayString + " old.";
        else if ((age.years > 0) && (age.months > 0) && (age.days > 0))
            ageString = age.years + yearString + ", " + age.months + monthString + ", and " + age.days + dayString + " old.";
        else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
            ageString = "Only " + age.days + dayString + " old!";
        else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
            ageString = age.years + yearString + " old. Happy Birthday!!";
        else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
            ageString = age.years + yearString + " and " + age.months + monthString + " old.";
        else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
            ageString = age.months + monthString + " and " + age.days + dayString + " old.";
        else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
            ageString = age.years + yearString + " and " + age.days + dayString + " old.";
        else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
            ageString = age.months + monthString + " old.";
        // else ageString = "Oops! Could not calculate age!";
        else ageString = ""; //toastr.error("Error", "Can't be valid date.");

        return (age.years) + "/" + ageString;
    }
    else {
        alert('Please Select Date Of Birth.');
    }
}

function BindYearList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetYearList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                if (SelectAll) {
                    $('#' + ElementId).append($("<option>").val('0').text('All'));
                }
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                //var yr = GetCurrentYear();
                var ymax = data.length - 1;
                $('#' + ElementId + ' option[value="' + ymax + '"]').prop('selected', true);
                // $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}

function BindMonthList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetMonthList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                if (SelectAll) {
                    $('#' + ElementId).append($("<option>").val('0').text('All'));
                }
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId + ' option[value="' + GetCurrentMonth() + '"]').prop('selected', true);
                //$('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
    $('#' + ElementId).trigger("chosen:updated");
}
function BindGetAllUserist(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetAllUserist",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindStateList(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetStateList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindDistrictList(ElementId, SelectedValue, SelectAll, Para = 0) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "Master/GetDistrictList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'SId': Para }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindBlockList(ElementId, SelectedValue, SelectAll, Para, Para1) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "Master/GetBlockList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'SId': Para, 'DId': Para1 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindPanchayatList(ElementId, SelectedValue, SelectAll, Para, Para1, Para2) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "Master/GetPanchayatList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'SId': Para, 'DId': Para1, 'BId': Para2 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindVillageList(ElementId, SelectedValue, SelectAll, Para, Para1, Para2, Para3) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "Master/GetVillageList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'SId': Para, 'DId': Para1, 'BId': Para2, 'PId': Para3 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindCLFList(ElementId, SelectedValue, SelectAll, Para, Para1, Para2, Para3) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "Master/GetCLFList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'SId': Para, 'DId': Para1, 'BId': Para2, 'PId': Para3 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindVOList(ElementId, SelectedValue, SelectAll, Para, Para1, Para2, Para3, Para4, Para5) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        url: document.baseURI + "Master/GetVOList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'SId': Para, 'DId': Para1, 'BId': Para2, 'PId': Para3, 'CId': Para4, 'VId': Para5 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}


function BindGetCourses(ElementId, SelectedValue, SelectAll, Para1) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetCourses",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'DistrictId': Para1 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindGetCoursesPCIEdubridge(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetCoursesPCIEdubridge",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindTrainingAgency(ElementId, SelectedValue, SelectAll) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTrainingAgencyList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindTrainingCenter(ElementId, SelectedValue, SelectAll, Para1, Para2) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTrainingCenterList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'DistrictId': Para1, 'TrainingAgencyId': Para2 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindTrainingCentersWiseLists(ElementId, SelectedValue, SelectAll, Para, Para1, Para2) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTrainingCenters",
        type: "Post",
        data: JSON.stringify({ 'IsAll': SelectAll, 'StateId': Para, 'Roles': Para1, 'TCIds': Para2 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindTainerAtCenter(ElementId, SelectedValue, SelectAll, Para) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTrainerAtCenterList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'TCenterId': Para }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindCourseSessionTopices(ElementId, SelectedValue, SelectAll, Para) {
    // debugger;
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetCourseSessionTopicesList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'CourseIds': Para }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindModuleWiseBatches(ElementId, SelectedValue, SelectAll, Para1, Para2, Para3, Para4) {
    // debugger;
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetModuleWiseBatches",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'ModuleType': Para1, 'CourseId': Para2, 'BatchId': Para3, 'TrainingCenterId': Para4 }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}
function BindBatchWiseCourseList(ElementId, SelectedValue, SelectAll, Para) {
    debugger;
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetBatchWiseCourseList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'BatchIds': Para }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}

function BindGetTainerWiseBatchesList(ElementId, SelectedValue, SelectAll, Para) {
    $('#' + ElementId).empty();
    $('#' + ElementId).prop("disabled", false);
    //$('#' + ElementId).append($("<option>").val('0').text('Select'));
    $.ajax({
        //url: document.baseURI + "/Master/GetHSCDistrict",
        url: document.baseURI + "Master/GetTainerWiseBatchesList",
        type: "Post",
        data: JSON.stringify({ 'SelectAll': SelectAll, 'TarinerIds': Para }),
        contentType: "application/json; charset=utf-8",
        //global: false,
        async: false,
        dataType: "json",
        success: function (resp) {
            if (resp.IsSuccess) {
                var data = JSON.parse(resp.res);
                $.each(data, function (i, exp) {
                    $('#' + ElementId).append($("<option>").val(exp.Value).text(exp.Text));
                });
                $('#' + ElementId).val(SelectedValue);
            }
            else {
                //alert(resp.IsSuccess);
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });

    //console.log('select value-'+SelectedValue);
    $('#' + ElementId).trigger("chosen:updated");
}


function parseToNumber(str) {
    var val = parseFloat(str);
    return val ? val : 0;
}
(function ($) {
    $.fn.validateMinMaxDigits = function (options) {
        // Default settings
        var settings = $.extend({
            minDigits: 1,       // Minimum digits allowed
            maxDigits: 10,      // Maximum digits allowed
            errorMessage: "Input must be between {min} and {max} digits."
        }, options);

        // Apply validation to each element in the jQuery collection
        return this.each(function () {
            var $input = $(this);

            $input.on("input", function () {
                var value = $input.val();
                var digits = value.replace(/\D/g, "").length; // Count digits only

                if (digits < settings.minDigits || digits > settings.maxDigits) {
                    $input.addClass("error");
                    alert(
                        settings.errorMessage
                            .replace("{min}", settings.minDigits)
                            .replace("{max}", settings.maxDigits)
                    );
                } else {
                    $input.removeClass("error");
                }
            });
        });
    };
})(jQuery);
function validateNumber(event) {
    const inputValue = event.target.value;
    if (/[^0-9]/g.test(inputValue)) {
        event.target.value = inputValue.replace(/[^0-9]/g, ''); // Remove non-numeric characters
    }
}
function validateDecimalNumber(event) {
    const inputValue = event.target.value;
    // Allow only numbers and one decimal point
    if (/[^0-9.]/g.test(inputValue) || (inputValue.split('.').length > 2)) {
        event.target.value = inputValue.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');
    }
}

function InputAllowcharacter(intput) {
    $('.' + intput).on('keypress', function (e) {
        // Get the key code of the pressed key
        var charCode = e.which || e.keyCode;
        // Allow letters (A-Z, a-z), numbers (0-9), space (32)
        if (
            (charCode >= 65 && charCode <= 90) ||  // A-Z
            (charCode >= 97 && charCode <= 122) || // a-z
            (charCode >= 48 && charCode <= 57) ||  // 0-9
            charCode === 32 // Space
        ) {
            return true;  // Allow the character
        } else {
            return false; // Block special characters
        }
    });
}
function validateDecimal(input) {
    // Replace any non-decimal characters (including multiple decimal points) with an empty string
    input.value = input.value.replace(/[^0-9.]/g, '');

    // Ensure only one decimal point is allowed
    let decimalCount = (input.value.match(/\./g) || []).length;
    if (decimalCount > 1) {
        input.value = input.value.slice(0, -1); // Remove the last invalid character
    }
}

