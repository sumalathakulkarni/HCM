// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//All of the front-end javascript code for all of the views
const HCMModule = (function () {
    //Employee List initializing code:
    //Call to GetAllEmployees RESTAPI method and binds data to the datatable from the json response received
    InitializeEmployeeDirectory = function () {

        $.ajax({
            "type": "GET",
            "url": "/Employee/GetAllEmployees",
            contentType: "application/json; charset=utf-8",
            datatype: "json",

            success: function (data) {

                tblEmps = $("#tblEmps").DataTable({
                    "data": data,
                    scrollY: '400px',
                    scrollCollapse: true,
                    destroy: true,

                    "columnDefs": [
                        {
                            "targets": [0],
                            "data": "employeeID",
                            "visible": false,
                            "searchable": false,
                            "orderable": false,
                        },
                        {
                            "targets": [1],
                            "data": "firstName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [2],
                            "data": "lastName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [3],
                            "data": "emailAddress",
                            "className": "dt-left"
                        },
                        {
                            "targets": [4],
                            "data": "departmentName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [5],
                            "data": "roleName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [6],
                            "data": "manager",
                            "className": "dt-left"
                        },
                        {
                            "targets": [7],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Employee Details" name="ViewEmployee" id="viewDViewEmployeeetails"><i class="bi bi-file-check"></i></a>'
                        },
                        {
                            "targets": [8],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Employee Benefits" name="ViewBenefits" id="ViewBenefits"><i class="bi bi-list-task"></i></a>'
                        },
                        {
                            "targets": [9],
                            "data": "employeeID",
                            "orderable": false,
                            "searchable": false,
                            "render": function (data, type, row, meta) {
                                if (type === 'display') {
                                    data = '<a href="/Employee/EditEmployee?employeeID=' + data + '" class="btn btn-sm" role="button" title="Edit Details" name="Edit" id="Edit"><i class="bi bi-pencil"></i></a>';
                                }
                                return data;
                            }
                        },
                        {
                            "targets": [10],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Delete" name="Delete" id="Delete"><i class="bi bi-trash"></i></a>'
                        }
                    ]
                });

                //ViewDetails of selected employee icon button click:
                //Call to ViewEmployee RESTAPI call with EmployeeID as parameter
                //renders the json result received in a pop-up box 
                $("#tblEmps").on("click", ".btn-row", function (e) {

                    var action = $(this).attr("Name");
                    var tr = $(this).closest('tr');
                    var row = tblEmps.rows(tr);
                    var employeeID = row.data()[0].employeeID;
                    var name = row.data()[0].firstName;

                    if (action == "ViewEmployee") {
                        e.preventDefault();
                        $.ajax({
                            type: "GET",
                            url: "/Employee/ViewEmployee?employeeID=" + employeeID,
                            success: function (response) {
                                $('#dialog-view').html(response);

                                $("#dialog-view").dialog({
                                    title: "View Details",
                                    resizable: false,
                                    height: "auto",
                                    width: 700,
                                    height: 600,
                                    modal: true,
                                    buttons: {
                                        "Close": function () {
                                            $("#dialog-view").dialog("close");
                                        }
                                    }

                                });
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            },
                            error: function (response) {
                                alert(response.responseText);
                            }
                        });

                    //ViewBenefits of selected employee icon button click:
                    //Call to ViewBenefits RESTAPI call with EmployeeID as parameter
                    //renders the json result received in a pop-up box 
                    } else if (action == "ViewBenefits") {
                        e.preventDefault();
                        $.ajax({
                            type: "GET",
                            url: "/Employee/ViewBenefits?employeeID=" + employeeID,
                            success: function (response) {
                                $('#dialog-view').html(response);

                                $("#dialog-view").dialog({
                                    title: "View Benefits",
                                    resizable: false,
                                    height: "auto",
                                    width: 400,
                                    modal: true,
                                    buttons: {
                                        "Close": function () {
                                            $("#dialog-view").dialog("close");
                                        }
                                    }
                                });
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            },
                            error: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }

                    //Delete action of the selected employee icon button click:
                    //Call to DeleteEmployee RESTAPI method with EmployeeID as parameter
                    //displays a confirmation pop-up box 
                    //displays a success toaster message after the successful delete action
                    
                    else if (action == "Delete") {
                        e.preventDefault();

                        $("#dialog-confirm").html("<i style='color: red;' class='bi bi-exclamation-circle-fill'></i>&nbsp;&nbsp;Are you sure to delete the selected Employee?");

                        $("#dialog-confirm").dialog({
                            title: "Delete " + name,
                            resizable: false,
                            height: "auto",
                            width: 400,
                            modal: true,
                            buttons: {
                                "Yes": function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Employee/DeleteEmployee?employeeId=" + employeeID,
                                        success: function (response) {

                                            $("#dialog-confirm").dialog("close");
                                            InitializeEmployeeDirectory();
                                            toastr.success('Employee ' + name + ' is Deleted', 'Confirmation',)
                                        },
                                        failure: function (response) {
                                            alert(response.responseText);
                                        },
                                        error: function (response) {
                                            alert(response.responseText);
                                        }
                                    });
                                },
                                "No": function () {
                                    $("#dialog-confirm").dialog("close");
                                }
                            }
                        });
                    }
                });
            },
            error: function (ex) {
                alert('in error');
            }
        });
    }

    //Initializing required master data for the AddEmployee
    //Call to GetAllDepartments, GetallManagers, GetallManagers, GetAllRoles RESTAPI methods
    //binds the json results received to dropdown boxes
    InitializeAddEmployee = function () {

        var controller = 'Employee';
        LoadDropdown('DepartmentID', controller, 'GetAllDepartments', false);
        LoadDropdown('ManagerID', controller, 'GetallManagers', false);
        LoadDropdown('RoleID', controller, 'GetAllRoles', false);

        //Call to CheckEmployeeExists RESTAPI method on form submit
        //with the emailaddress value entered in the Add New Employee form as the parameter
        //returns an error message that is diplayed in a toaster message if the emailaddress already exists
        $("#frmAddEmployee").on("submit", function (event) {
            event.preventDefault();
            var emailAddress = $("#EmailAddress").val();

            var departmentID = $("#DepartmentID").val();
            var roleID = $("#RoleID").val();
            var managerID = $("#ManagerID").val();

            if (departmentID < 1) {
                toastr.error("Select department");
                $("#DepartmentID").focus();
                return false;
            }
            if (roleID < 1) {
                toastr.error("Select role");
                $("#RoleID").focus();
                return false;
            }
            if (managerID < 1) {
                toastr.error("Select manager");
                $("#ManagerID").focus();
                return false;
            }

            $.ajax({
                type: "GET",
                url: "/Employee/CheckEmployeeExists?emailAddress=" + emailAddress,
                success: function (response) {
                    if (response.isExisting) {
                        toastr.error('EmailAddress is already assigned to another employee', 'Invalid Email');
                        $("#EmailAddress").focus();
                        return false;
                    }
                    //Call to SaveEmployee RESTAPI method on form submit
                    //with all the input field values entered in the Add New Employee form as parameters
                    //displays a success message in a toaster message if the employee is added successfully
                    else {
                        $.ajax({
                            type: "POST",
                            url: "/Employee/SaveEmployee",
                            data: $("#frmAddEmployee").serialize(),

                            success: function (data) {
                                toastr.success('Employee ' + $("#FirstName").val() + " " + $("#LastName").val() + ' added successfully!', 'Confirmation');
                                window.location.href = 'EmployeeDirectory'
                            },
                            error: function (ex) {
                                alert('in error');
                            }
                        });
                    }
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
    };
    //Initializing required master data for the EditEmployee
    //Call to GetAllDepartments, GetallManagers, GetallManagers, GetAllRoles RESTAPI methods
    //binds the json results received to dropdown boxes
    InitializeEditEmployee = function () {

        debugger;
        var controller = 'Employee';
        LoadDropdown('DepartmentID', controller, 'GetAllDepartments', true);
        LoadDropdown('ManagerID', controller, 'GetallManagers', true);
        LoadDropdown('RoleID', controller, 'GetAllRoles', true);

        $("#frmEditEmployee").on("submit", function (event) {
            event.preventDefault();

            var departmentID = $("#DepartmentID").val();
            var roleID = $("#RoleID").val();
            var managerID = $("#ManagerID").val();

            if (departmentID < 1) {
                toastr.error("Select department");
                $("#DepartmentID").focus();
                return false;
            }
            if (roleID < 1) {
                toastr.error("Select role");
                $("#RoleID").focus();
                return false;
            }
            if (managerID < 1) {
                toastr.error("Select manager");
                $("#ManagerID").focus();
                return false;
            }

            //Call to SaveEmployee RESTAPI method on form submit
            //with all the input field values entered in the Edit Employee form as parameters for the selected employee 
            //displays a success message in a toaster message if the employee is updated successfully
            $.ajax({
                type: "POST",
                url: "/Employee/SaveEmployee",
                data: $("#frmEditEmployee").serialize(),

                success: function (data) {
                    toastr.success('Employee ' + $("#FirstName").val() + " " + $("#LastName").val() + ' updated successfully!', 'Confirmation');
                    window.location.href = 'EmployeeDirectory'
                },
                error: function (ex) {
                    alert('in error');
                }
            });
        });
    }
    //Initializing dropdown menu items and the all the relevant data for the menu items actions for the logged-in user
    InitializeUserMenu = function () {

        var bootstrapButton = $.fn.button.noConflict()
        $.fn.bootstrapBtn = bootstrapButton;
        //Call to EditBenefits RESTAPI method of the logged-in user
        //with all the employee's option choice values retrieved rendered in the EditBenefits pop-up box
        $("#btnEditBenefits").on("click", function (e) {

            var employeeID = $("#hdnEmployeeID").val();
            e.preventDefault();

            $.ajax({
                type: "GET",
                url: "/Employee/EditBenefits?employeeID=" + employeeID,
                success: function (response) {
                    $('#dialog-view').html(response);
                    $("#dialog-view").dialog({
                        title: "Edit Benefits",
                        resizable: false,
                        height: "auto",
                        width: 400,
                        modal: true
                    });
                    $("#btnSaveBenefits").on("click", function (e) {

                        var EmployeeMedical = $("#EmployeeMedical").prop("checked");
                        var EmployeeVision = $("#EmployeeVision").prop("checked");
                        var EmployeeDental = $("#EmployeeDental").prop("checked");
                        var EmployeeLife = $("#EmployeeLife").prop("checked");
                        var Employee401K = $("#Employee401K").prop("checked");

                        e.preventDefault();
                        //Call to SaveBenefits RESTAPI method of the logged-in user
                        //with the all the option choice values selected in the EditBenefits pop-up box as parameters
                        //displays a success message in a toaster message once the benefits choices are updated successfully
                        $.ajax({
                            type: "POST",
                            url: "/Employee/SaveBenefits",
                            data: {
                                EmployeeID: employeeID
                                , EmployeeMedical: EmployeeMedical
                                , EmployeeVision: EmployeeVision
                                , EmployeeDental: EmployeeDental
                                , EmployeeLife: EmployeeLife
                                , Employee401K: Employee401K
                            },

                            success: function (data) {
                                $("#dialog-view").dialog("close");
                                toastr.success('Selected benefits are updated', 'Confirmation');
                            },
                            error: function (ex) {
                                alert('in error');
                            }
                        })
                    })
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });

        //Call to EmployeeSkills RESTAPI method of the logged-in user with the EmployeeID as the parameter
        //with the all the employee skills previously added is displayed in the EmployeeSkills pop-up box
        $("#btnViewSkills").on("click", function (e) {

            var employeeID = $("#hdnEmployeeID").val();
            e.preventDefault();

            $.ajax({
                type: "GET",
                url: "/Employee/EmployeeSkills?employeeID=" + employeeID,
                success: function (response) {
                    $('#dialog-view').html(response);
                    $("#dialog-view").dialog({
                        title: "My Skills",
                        resizable: false,
                        height: "auto",
                        width: "auto",
                        modal: true
                    });
                    $("#btnAddSkill").on("click", function (e) {

                        var selectedOpts = $('#AllSkills option:selected');
                        if (selectedOpts.length > 0 && selectedOpts[0].value != -1) {
                            $('#EmployeeSkills').append($(selectedOpts).clone());
                            $(selectedOpts).remove();
                            toastr.success('Skill(s) added', 'Success');
                            e.preventDefault();
                        } else {
                            toastr.error('Select valid Skill(s)', 'Error');
                        }
                    });
                    $("#btnRemoveSkill").on("click", function (e) {

                        var selectedOpts = $('#EmployeeSkills option:selected');
                        if (selectedOpts.length > 0 && selectedOpts[0].value != -1) {
                            $('#AllSkills').append($(selectedOpts).clone());
                            $(selectedOpts).remove();
                            toastr.success('Skill(s) removed', 'Success');
                            e.preventDefault();
                        }
                        else {
                            toastr.error('Select valid Skill(s)', 'Error');
                        }
                    });
                    //Call to SaveEmployeeSkills RESTAPI method of the logged-in user
                    //with the all the employee skills previously added is displayed in the ViewSkills pop-up box
                    //and newly added skills list from the skills database as the parameter
                    //displays a success message in a toaster message once the skills are added successfully
                    $("#btnSaveSkills").on("click", function (e) {

                        selectedSkills = [];
                        $('#EmployeeSkills option').each(function () {
                            if (this.value > 0) {
                                selectedSkills.push({ Value: this.value, Text: this.text });
                            }
                        });
                        e.preventDefault();
                        $.ajax({
                            type: "POST",
                            url: "/Employee/SaveEmployeeSkills",
                            data: { EmployeeSkills: selectedSkills },
                            //data: $("#skillsForm").serialize(),
                            success: function (data) {
                                $("#dialog-view").dialog("close");
                                toastr.success('Successfully updated the employee skills', 'Confirmation');
                            },
                            error: function (ex) {
                                alert('in error');
                            }
                        })
                    })
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
        //Call to ViewPTO RESTAPI method of the logged-in user with the EmployeeID as the parameter
        //with the Manager, LeaveBalance, PTOType details displayed in the ViewPTO pop-up box
        $("#btnViewPTO").on("click", function (e) {

            var employeeID = $("#hdnEmployeeID").val();
            e.preventDefault();

            $.ajax({
                type: "GET",
                url: "/Employee/ViewPTO?employeeID=" + employeeID,
                success: function (response) {
                    $('#dialog-view').html(response);
                    $("#dialog-view").dialog({
                        title: "View PTO",
                        resizable: false,
                        height: "auto",
                        width: 500,
                        modal: true
                    });

                    function CalculatePTOCount() {
                        var start = $('#PTOStartDate').datepicker('getDate');
                        var end = $('#PTOEndDate').datepicker('getDate');
                        if (!start || !end)
                            return;
                        var days = 1 + (end - start) / 1000 / 60 / 60 / 24;
                        $("#NumDays").val(days);
                    }

                    $("#PTOStartDate").datepicker({ dateFormat: 'dd-mm-yy', onSelect: CalculatePTOCount });
                    $("#PTOEndDate").datepicker({ dateFormat: 'dd-mm-yy', onSelect: CalculatePTOCount });

                    //Call to SavePTO RESTAPI method (on form submit) of the logged-in user
                    //with the Manager, LeaveBalance, PTOType details, NumDays, Reason, 
                    //logged -in user's EmployeeID, Startdate, EndDate as the parameters
                    $("#empPTOForm").on("submit", function (event) {
                        
                        var PTOTypeID = $("#AllPTOTypes").val();
                        var ManagerID = $('#hdnManagerID').val();
                        var StartDate = $('#PTOStartDate').datepicker('getDate');
                        var EndDate = $('#PTOEndDate').datepicker('getDate');
                        CalculatePTOCount();
                        var NumDays = $("#NumDays").val();
                        var Reason = $("#Reason").val();
                        event.preventDefault();

                        if (PTOTypeID == -1) {
                            toastr.error('Select PTO Type', 'PTO Type');
                            return false;
                        }

                        if (NumDays < 1) {
                            toastr.error('End Date should be greater than or Equal to Start Date', 'Error');
                            return false;
                        }

                        $.ajax({
                            type: "POST",
                            url: "/Employee/SavePTO",
                            data: {
                                PTOTypeID: PTOTypeID,
                                ManagerID: ManagerID,
                                StartDate: StartDate.toDateString(),
                                EndDate: EndDate.toDateString(),
                                NumDays: NumDays,
                                Reason: Reason
                            },

                            success: function (data) {
                                $("#dialog-view").dialog("close");
                                toastr.success('PTO request submitted successfully', 'Confirmation');
                            },
                            error: function (ex) {
                                alert('in error');
                            }
                        })
                    })

                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
    }

    //Departments List initializing code:
    //Call to GetAllDepartments RESTAPI method and binds data to the datatable from the json response received
    InitializeDepartments = function () {

        $.ajax({
            "type": "GET",
            "url": "/Department/GetAllDepartments",
            contentType: "application/json; charset=utf-8",
            datatype: "json",

            success: function (data) {

                tblDepartments = $("#tblDepartments").DataTable({
                    "data": data,
                    scrollY: '400px',
                    scrollCollapse: true,
                    destroy: true,

                    "columnDefs": [
                        {
                            "targets": [0],
                            "data": "departmentID",
                            "searchable": false,
                            "orderable": false,
                        },
                        {
                            "targets": [1],
                            "data": "departmentName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [2],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Edit" name="Edit" id="Edit"><i class="bi bi-pencil"></i></a>'
                        },
                        {
                            "targets": [3],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Delete" name="Delete" id="Delete"><i class="bi bi-trash"></i></a>'
                        }
                    ]
                });

                //Call to AddDepartment RESTAPI method displays a pop-up box with input field for DepartmentName 
                $("#btnAddDepartment").on("click", function (e) {

                    e.preventDefault();
                    $.ajax({
                        type: "GET",
                        url: "/Department/AddDepartment",
                        success: function (response) {
                            $('#dialog-view').html(response);
                            $("#dialog-view").dialog({
                                title: "Add Department",
                                resizable: false,
                                height: "auto",
                                width: 400,
                                modal: true
                            });

                            //Call to SaveDepartment RESTAPI method on form submit
                            //with the DepartmentName input field value entered in the Add New Department pop-up box as the parameter
                            //displays a success message in a toaster message if the department is added successfully
                            $('#btnSaveNewDepartment').on("click", function (e) {

                                e.preventDefault();
                                var DepartmentID = 0;
                                var DepartmentName = $("#DepartmentName").val();
                                $.ajax({
                                    type: "POST",
                                    url: "/Department/SaveDepartment",
                                    data: { DepartmentID: DepartmentID, DepartmentName: DepartmentName },

                                    success: function (data) {
                                        $("#dialog-view").dialog("close");
                                        InitializeDepartments();
                                        toastr.success('Department ' + DepartmentName + ' is Created', 'Confirmation');
                                    },
                                    error: function (ex) {
                                        alert('in error');
                                    }
                                })
                            })
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        },
                        error: function (response) {
                            alert(response.responseText);
                        }
                    });
                });

                //Call to EditDepartment RESTAPI method to display a pop-up box with departmentname input field
                $("#tblDepartments").on("click", ".btn-row", function (e) {

                    var action = $(this).attr("Name");
                    var tr = $(this).closest('tr');
                    var row = tblDepartments.rows(tr);
                    var dept = row.data()[0];

                    if (action == "Edit") {
                        e.preventDefault();
                        $.ajax({
                            type: "POST",
                            url: "/Department/EditDepartment",
                            data: dept,
                            success: function (response) {
                                $('#dialog-view').html(response);
                                $("#dialog-view").dialog({
                                    title: "Edit Department",
                                    resizable: false,
                                    height: "auto",
                                    width: 400,
                                    modal: true
                                });

                                //Call to SaveDepartment RESTAPI method
                                //with the DepartmentName input field value entered in the EditDepartment pop-up box as the parameter
                                //displays a success message in a toaster message if the department name is updated successfully
                                $('#btnSaveExistingDepartment').on("click", function (e) {

                                    e.preventDefault();
                                    var DepartmentID = $("#DepartmentID").val();
                                    var DepartmentName = $("#DepartmentName").val();
                                    $.ajax({
                                        type: "POST",
                                        url: "/Department/SaveDepartment",
                                        data: { DepartmentID: DepartmentID, DepartmentName: DepartmentName },

                                        success: function (data) {
                                            $("#dialog-view").dialog("close");
                                            InitializeDepartments();
                                            toastr.success('Department ' + DepartmentName + ' is Updated', 'Confirmation');
                                        },
                                        error: function (ex) {
                                            alert('in error');
                                        }
                                    })
                                })

                            },
                            failure: function (response) {
                                alert(response.responseText);
                            },
                            error: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }

                    //Call to DeleteDepartment RESTAPI method for the selected department
                    //with the selected DepartmentID value as the parameter 
                    //displays a confirmation box
                    //displays a success message in a toaster message if the department is deleted successfully
                    else if (action == "Delete") {

                        e.preventDefault();
                        $("#dialog-confirm").html("<i style='color: red;' class='bi bi-exclamation-circle-fill'></i>&nbsp;&nbsp;Are you sure to delete the selected Department?");
                        $("#dialog-confirm").dialog({

                            title: "Delete " + dept.departmentName,
                            resizable: false,
                            height: "auto",
                            width: 400,
                            modal: true,
                            buttons: {
                                "Yes": function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Department/DeleteDepartment?departmentId=" + dept.departmentID,
                                        success: function (response) {
                                            $("#dialog-confirm").dialog("close");
                                            InitializeDepartments();
                                            toastr.success('Department ' + dept.departmentName + ' is Deleted', 'Confirmation');
                                        },
                                        failure: function (response) {
                                            alert(response.responseText);
                                        },
                                        error: function (response) {
                                            alert(response.responseText);
                                        }
                                    });
                                },
                                "No": function () {
                                    $("#dialog-confirm").dialog("close");
                                }
                            }
                        });
                    }
                });
            },
            error: function (ex) {
                alert('in error');
            }
        });
    }

    //Roles List initializing code:
    //Call to GetAllRoles RESTAPI method and binds data to the datatable from the json response received
    function InitializeRoles() {

        $.ajax({
            "type": "GET",
            "url": "/Role/GetAllRoles",
            contentType: "application/json; charset=utf-8",
            datatype: "json",

            success: function (data) {

                tblRoles = $("#tblRoles").DataTable({
                    "data": data,
                    scrollY: '400px',
                    scrollCollapse: true,
                    destroy: true,

                    "columnDefs": [
                        {
                            "targets": [0],
                            "data": "roleID",
                            "searchable": false,
                            "orderable": false,
                        },
                        {
                            "targets": [1],
                            "data": "roleName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [2],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Edit" name="Edit" id="Edit"><i class="bi bi-pencil"></i></a>'
                        },
                        {
                            "targets": [3],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Delete" name="Delete" id="Delete"><i class="bi bi-trash"></i></a>'
                        }
                    ]
                });

                //Call to AddRole RESTAPI method displays a pop-up box with input field for RoleName
                $("#btnAddRole").on("click", function (e) {

                    e.preventDefault();
                    $.ajax({
                        type: "GET",
                        url: "/Role/AddRole",
                        success: function (response) {
                            $('#dialog-view').html(response);
                            $("#dialog-view").dialog({
                                title: "Add Role",
                                resizable: false,
                                height: "auto",
                                width: 400,
                                modal: true
                            });

                            //Call to SaveRole RESTAPI method on form submit
                            //with the RoleName input field value entered in the Add New Role pop-up box as the parameter
                            //displays a success message in a toaster message if the role is added successfully
                            $('#btnSaveNewRole').on("click", function (e) {

                                e.preventDefault();
                                var RoleID = 0;
                                var RoleName = $("#RoleName").val();
                                $.ajax({
                                    type: "POST",
                                    url: "/Role/SaveRole",
                                    data: { RoleID: RoleID, RoleName: RoleName },
                                    success: function (data) {
                                        $("#dialog-view").dialog("close");
                                        InitializeRoles();
                                        toastr.success('Role ' + RoleName + ' is Created', 'Confirmation');
                                    },
                                    error: function (ex) {
                                        alert('in error');
                                    }
                                })
                            })
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        },
                        error: function (response) {
                            alert(response.responseText);
                        }
                    });
                });

                $("#tblRoles").on("click", ".btn-row", function (e) {

                    var action = $(this).attr("Name");
                    var tr = $(this).closest('tr');
                    var row = tblRoles.rows(tr);
                    var role = row.data()[0];
                    var roleID = row.data()[0].roleID;
                    var roleName = row.data()[0].roleName;

                    //Call to EditRole RESTAPI method to display a pop-up box with rolename input field
                    if (action == "Edit") {
                        e.preventDefault();
                        $.ajax({
                            type: "POST",
                            url: "/Role/EditRole",
                            data: role,
                            success: function (response) {
                                $('#dialog-view').html(response);
                                $("#dialog-view").dialog({
                                    title: "Edit Role",
                                    resizable: false,
                                    height: "auto",
                                    width: 400,
                                    modal: true
                                });

                                //Call to SaveRole RESTAPI method
                                //with the RoleName input field value entered in the EditRole pop-up box as the parameter
                                //displays a success message in a toaster message if the role name is updated successfully
                                $('#btnSaveExistingRole').on("click", function (e) {

                                    e.preventDefault();
                                    var RoleID = $("#RoleID").val();
                                    var RoleName = $("#RoleName").val();
                                    $.ajax({
                                        type: "POST",
                                        url: "/Role/SaveRole",
                                        data: { RoleID: RoleID, RoleName: RoleName },

                                        success: function (data) {
                                            $("#dialog-view").dialog("close");
                                            InitializeRoles();
                                            toastr.success('Role ' + RoleName + ' is Updated', 'Confirmation');
                                        },
                                        error: function (ex) {
                                            alert('in error');
                                        }
                                    })
                                })

                            },
                            failure: function (response) {
                                alert(response.responseText);
                            },
                            error: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }

                    //Call to DeleteRole RESTAPI method for the selected role
                    //with the selected RoleID value as the parameter
                    //displays a confirmation box
                    //displays a success message in a toaster message if the role is deleted successfully
                    else if (action == "Delete") {

                        e.preventDefault();

                        $("#dialog-confirm").html("<i style='color: red;' class='bi bi-exclamation-circle-fill'></i>&nbsp;&nbsp;Are you sure to delete the selected Role?");
                        $("#dialog-confirm").dialog({
                            title: "Delete the role '" + roleName + "'",
                            resizable: false,
                            height: "auto",
                            width: 400,
                            modal: true,
                            buttons: {
                                "Yes": function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Role/DeleteRole?RoleId=" + roleID,
                                        success: function (response) {
                                            $("#dialog-confirm").dialog("close");
                                            InitializeRoles();
                                            toastr.success('Role ' + roleName + ' is Deleted', 'Confirmation');
                                        },
                                        failure: function (response) {
                                            alert(response.responseText);
                                        },
                                        error: function (response) {
                                            alert(response.responseText);
                                        }
                                    });
                                },
                                "No": function () {
                                    $("#dialog-confirm").dialog("close");
                                }
                            }
                        });
                    }
                });
            },
            error: function (ex) {
                alert('in error');
            }
        });
    }
    //Skills List initializing code:
    //Call to GetAllSkills RESTAPI method and binds data to the datatable from the json response received
    function InitializeSkills() {

        $.ajax({
            "type": "GET",
            "url": "/Skill/GetAllSkills",
            contentType: "application/json; charset=utf-8",
            datatype: "json",

            success: function (data) {

                tblSkills = $("#tblSkills").DataTable({
                    "data": data,
                    scrollY: '400px',
                    scrollCollapse: true,
                    destroy: true,

                    "columnDefs": [
                        {
                            "targets": [0],
                            "data": "skillID",
                            "searchable": false,
                            "orderable": false,
                        },
                        {
                            "targets": [1],
                            "data": "skillName",
                            "className": "dt-left"
                        },
                        {
                            "targets": [2],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Edit" name="Edit" id="Edit"><i class="bi bi-pencil"></i></a>'
                        },
                        {
                            "targets": [3],
                            "data": null,
                            "orderable": false,
                            "searchable": false,
                            "defaultContent": '<a href="#" class="btn btn-sm btn-row" role="button" title="Delete" name="Delete" id="Delete"><i class="bi bi-trash"></i></a>'
                        }
                    ]
                });

                //Call to AddSkill RESTAPI method displays a pop-up box with input field for SkillName
                $("#btnAddNewSkill").on("click", function (e) {

                    e.preventDefault();
                    $.ajax({
                        type: "GET",
                        url: "/Skill/AddSkill",
                        success: function (response) {
                            $('#dialog-view').html(response);
                            $("#dialog-view").dialog({
                                title: "Add Skill",
                                resizable: false,
                                height: "auto",
                                width: 400,
                                modal: true
                            });

                            //Call to SaveSkill RESTAPI method on form submit
                            //with the SkillName input field value entered in the Add New Skill pop-up box as the parameter
                            //displays a success message in a toaster message if the skill is added successfully
                            $("#btnSaveNewSkill").on("click", function (e) {

                                e.preventDefault();
                                var SkillID = 0;
                                var SkillName = $("#SkillName").val();
                                $.ajax({
                                    type: "POST",
                                    url: "/Skill/SaveSkill",
                                    data: { SkillID: SkillID, SkillName: SkillName },

                                    success: function (data) {
                                        $("#dialog-view").dialog("close");
                                        InitializeSkills();
                                        toastr.success('Skill ' + SkillName + ' is Created', 'Confirmation');
                                    },
                                    error: function (ex) {
                                        alert('in error');
                                    }
                                })
                            })
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        },
                        error: function (response) {
                            alert(response.responseText);
                        }
                    });
                });

                $("#tblSkills").on("click", ".btn-row", function (e) {

                    var action = $(this).attr("Name");

                    var tr = $(this).closest('tr');
                    var row = tblSkills.rows(tr);
                    var skill = row.data()[0];
                    var skillID = row.data()[0].skillID;
                    var skillName = row.data()[0].skillName;

                    //Call to EditSkill RESTAPI method to display a pop-up box with skillname input field
                    if (action == "Edit") {
                        e.preventDefault();
                        $.ajax({
                            type: "POST",
                            url: "/Skill/EditSkill",
                            data: skill,
                            success: function (response) {
                                $('#dialog-view').html(response);
                                $("#dialog-view").dialog({
                                    title: "Edit Skill",
                                    resizable: false,
                                    height: "auto",
                                    width: 400,
                                    modal: true
                                });

                                //Call to SaveSkill RESTAPI method
                                //with the RoleName input field value entered in the EditSkill pop-up box as the parameter
                                //displays a success message in a toaster message if the skill name is updated successfully
                                $('#btnSaveExistingSkill').on("click", function (e) {

                                    e.preventDefault();
                                    var SkillID = $("#SkillID").val();
                                    var SkillName = $("#SkillName").val();
                                    $.ajax({
                                        type: "POST",
                                        url: "/Skill/SaveSkill",
                                        data: { SkillID: SkillID, SkillName: SkillName },

                                        success: function (data) {
                                            $("#dialog-view").dialog("close");
                                            InitializeSkills();
                                            toastr.success('Skill ' + SkillName + ' is Updated', 'Confirmation');
                                        },
                                        error: function (ex) {
                                            alert('in error');
                                        }
                                    })
                                })

                            },
                            failure: function (response) {
                                alert(response.responseText);
                            },
                            error: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }

                    //Call to DeleteSkill RESTAPI method for the selected role
                    //with the selected SkillID value as the parameter
                    //displays a confirmation box
                    //displays a success message in a toaster message if the skill is deleted successfully
                    else if (action == "Delete") {

                        e.preventDefault();

                        $("#dialog-confirm").html("<i style='color: red;' class='bi bi-exclamation-circle-fill'></i>&nbsp;&nbsp;Are you sure to delete the selected Skill?");
                        $("#dialog-confirm").dialog({
                            title: "Delete the skill '" + skillName + "'",
                            resizable: false,
                            height: "auto",
                            width: 400,
                            modal: true,
                            buttons: {
                                "Yes": function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Skill/DeleteSkill?SkillId=" + skillID,
                                        success: function (response) {
                                            $("#dialog-confirm").dialog("close");
                                            InitializeSkills();
                                            toastr.success('Skill ' + skillName + ' is Deleted', 'Confirmation');
                                        },
                                        failure: function (response) {
                                            alert(response.responseText);
                                        },
                                        error: function (response) {
                                            alert(response.responseText);
                                        }
                                    });
                                },
                                "No": function () {
                                    $("#dialog-confirm").dialog("close");
                                }
                            }
                        });
                    }
                });
            },
            error: function (ex) {
                alert('in error');
            }
        });
    }

    //Utility method for loading dropdown boxes across the application
    function LoadDropdown(id, controller, operation, isEdit) {

        $.ajax({
            "type": "GET",
            "url": "/" + controller + "/" + operation,
            contentType: "application/json; charset=utf-8",
            datatype: "json",

            success: function (data) {
                $('#' + id).empty();
                $('#' + id).append($('<option>').text('---').attr('value', "-1"));

                $.each(data, function (index, item) {
                    $('#' + id).append($('<option>').text(item.text).attr('value', item.value));
                });
                if (isEdit) {
                    var selectedValue = $('#hdn' + id).val();
                    $('#' + id).val(selectedValue);
                }
            },
            error: function (ex) {
                alert('in error');
            }
        });
    }

    return {
        InitializeEmployeeDirectory,
        InitializeAddEmployee,
        InitializeEditEmployee,
        InitializeUserMenu,
        InitializeDepartments,
        InitializeRoles,
        InitializeSkills
    };

})();