<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="PersonnelRequisitionSystem.LoginPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Personnel Requisition Management System</title>

    <link rel="shortcut icon" href="../images/sohbiicon.ico" />

    <link href="https://fonts.googleapis.com/css?family=Varela+Round" rel="stylesheet" />

    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <style type="text/css">
        body {
            font-family: 'Varela Round', sans-serif;
        }

        .form-control {
            box-shadow: none;
            font-weight: normal;
            font-size: 13px;
        }

            .form-control:focus {
                border-color: #33cabb;
                box-shadow: 0 0 8px rgba(0,0,0,0.1);
            }

        .navbar-header.col {
            padding: 0 !important;
        }

        .navbar {
            background: #fff;
            padding-left: 16px;
            padding-right: 16px;
            border-bottom: 1px solid #dfe3e8;
            border-radius: 0;
        }

        .nav-link img {
            border-radius: 50%;
            width: 36px;
            height: 36px;
            margin: -8px 0;
            float: left;
            margin-right: 10px;
        }

        .navbar .navbar-brand, .navbar .navbar-brand:hover, .navbar .navbar-brand:focus {
            padding-left: 0;
            font-size: 20px;
            padding-right: 50px;
        }

            .navbar .navbar-brand b {
                font-weight: bold;
                color: #33cabb;
            }

        .navbar .form-inline {
            display: inline-block;
        }

        .navbar .nav li {
            position: relative;
        }

            .navbar .nav li a {
                color: #888;
            }

        .search-box {
            position: relative;
        }

            .search-box input {
                padding-right: 35px;
                border-color: #dfe3e8;
                border-radius: 4px !important;
                box-shadow: none;
            }

            .search-box .input-group-addon {
                min-width: 35px;
                border: none;
                background: transparent;
                position: absolute;
                right: 0;
                z-index: 9;
                padding: 7px;
                height: 100%;
            }

            .search-box i {
                color: #a0a5b1;
                font-size: 19px;
            }

        .navbar .nav .btn-primary, .navbar .nav .btn-primary:active {
            color: #fff;
            background: #33cabb;
            padding-top: 8px;
            padding-bottom: 6px;
            vertical-align: middle;
            border: none;
        }

            .navbar .nav .btn-primary:hover, .navbar .nav .btn-primary:focus {
                color: #fff;
                outline: none;
                background: #31bfb1;
            }

        .navbar .navbar-right li:first-child a {
            padding-right: 30px;
        }

        .navbar .nav-item i {
            font-size: 18px;
        }

        .navbar .dropdown-item i {
            font-size: 16px;
            min-width: 22px;
        }

        .navbar ul.nav li.active a, .navbar ul.nav li.open > a {
            background: transparent !important;
        }

        .navbar .nav .get-started-btn {
            min-width: 120px;
            margin-top: 8px;
            margin-bottom: 8px;
        }

        .navbar ul.nav li.open > a.get-started-btn {
            color: #fff;
            background: #31bfb1 !important;
        }

        .navbar .dropdown-menu {
            border-radius: 1px;
            border-color: #e5e5e5;
            box-shadow: 0 2px 8px rgba(0,0,0,.05);
        }

        .navbar .nav .dropdown-menu li {
            color: #999;
            font-weight: normal;
        }

            .navbar .nav .dropdown-menu li a, .navbar .nav .dropdown-menu li a:hover, .navbar .nav .dropdown-menu li a:focus {
                padding: 8px 20px;
                line-height: normal;
            }

        .navbar .navbar-form {
            border: none;
        }

        .navbar .dropdown-menu.form-wrapper {
            width: 280px;
            padding: 20px;
            left: auto;
            right: 0;
            font-size: 14px;
        }

            .navbar .dropdown-menu.form-wrapper a {
                color: #33cabb;
                padding: 0 !important;
            }

                .navbar .dropdown-menu.form-wrapper a:hover {
                    text-decoration: underline;
                }

        .navbar .form-wrapper .hint-text {
            text-align: center;
            margin-bottom: 15px;
            font-size: 20px;
        }

        .navbar .form-wrapper .social-btn .btn, .navbar .form-wrapper .social-btn .btn:hover {
            color: #fff;
            margin: 0;
            padding: 0 !important;
            font-size: 13px;
            border: none;
            transition: all 0.4s;
            text-align: center;
            line-height: 34px;
            width: 47%;
            text-decoration: none;
        }

        .navbar .social-btn .btn-primary {
            background: #507cc0;
        }

            .navbar .social-btn .btn-primary:hover {
                background: #4676bd;
            }

        .navbar .social-btn .btn-info {
            background: #64ccf1;
        }

            .navbar .social-btn .btn-info:hover {
                background: #4ec7ef;
            }

        .navbar .social-btn .btn i {
            margin-right: 5px;
            font-size: 16px;
            position: relative;
            top: 2px;
        }

        .navbar .form-wrapper .form-footer {
            text-align: center;
            padding-top: 10px;
            font-size: 13px;
        }

            .navbar .form-wrapper .form-footer a:hover {
                text-decoration: underline;
            }

        .navbar .form-wrapper .checkbox-inline input {
            margin-top: 3px;
        }

        .or-seperator {
            margin-top: 32px;
            text-align: center;
            border-top: 1px solid #e0e0e0;
        }

            .or-seperator b {
                color: #666;
                padding: 0 8px;
                width: 30px;
                height: 30px;
                font-size: 13px;
                text-align: center;
                line-height: 26px;
                background: #fff;
                display: inline-block;
                border: 1px solid #e0e0e0;
                border-radius: 50%;
                position: relative;
                top: -15px;
                z-index: 1;
            }

        .navbar .checkbox-inline {
            font-size: 13px;
        }

        .navbar .navbar-right .dropdown-toggle::after {
            display: none;
        }

        @media (min-width: 1200px) {
            .form-inline .input-group {
                width: 300px;
                margin-left: 30px;
            }
        }

        @media (max-width: 768px) {
            .navbar .dropdown-menu.form-wrapper {
                width: 100%;
                padding: 10px 15px;
                background: transparent;
                border: none;
            }

            .navbar .form-inline {
                display: block;
            }

            .navbar .input-group {
                width: 100%;
            }

            .navbar .nav .btn-primary, .navbar .nav .btn-primary:active {
                display: block;
            }
        }
    </style>

    <%--  <script type="text/javascript">
        //// Prevent dropdown menu from closing when click inside the form
        $(document).on("click", ".navbar-right .dropdown-menu", function (e) {
            e.stopPropagation();
        });

        //function clearTextBox(textBoxID) {
        //    document.getElementById(textBoxID).value = "";
        //}

        function OnFocusDocumentControllerFields() {
            document.getElementById("tbDCSmartUsername").value = "";
            document.getElementById("tbDCSmartPass").value = "";
        }

        function OnFocusSmartFields() {
            document.getElementById("tbDCUsername").value = "";
            document.getElementById("tbDCPassword").value = "";
        }

        
    </script>--%>

</head>
<body style="background-image: url(images/Sohbi.JPG); background-repeat: no-repeat; background-size: cover;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="SM1"></asp:ScriptManager>

        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false">
            <TargetControls>
                <telerik:TargetControl Skin="Material" ControlsToApplySkin="RadWindow" />
            </TargetControls>
        </telerik:RadSkinManager>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>

        <nav class="navbar navbar-default navbar-expand-lg navbar-LIGHT" style="background-color: #222d32; border-color: transparent">
            <div class="navbar-header d-flex col">
                <a class="navbar-brand" href="#"><b style="color: white">MANPOWER REQUEST AND MONITORING SYSTEM</b></a>
                <button type="button" data-target="#navbarCollapse" data-toggle="collapse" class="navbar-toggle navbar-toggler ml-auto">
                    <span class="navbar-toggler-icon"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
            </div>
            <!-- Collection of nav links, forms, and other content for toggling -->
            <div id="navbarCollapse" class="collapse navbar-collapse justify-content-start">

                <ul class="nav navbar-nav navbar-right ml-auto">
                    
                    <li class="nav-item" style="width: 150px;">
                        
                        <a runat="server" id="lnkcontroller" data-toggle="dropdown" class=" nav-link dropdown-toggle"
                            style="color: white; text-align: center; cursor: pointer; background-color: rgba(192,192,192,.1)">REQUESTOR</a>

                        <ul class="dropdown-menu form-wrapper" style="border-color: transparent;">
                            
                            <li>
                                
                                <div>

                                    <p class="hint-text">LOGIN</p>

                                    <div class="form-group">
                                        <%--<input type="text" class="form-control" placeholder="Username" required="required" />--%>
                                        <asp:TextBox ID="tbUsername" runat="server" onkeydown="return (event.keyCode!=13);"
                                            CssClass="form-control" placeholder="Enter Username..." />
                                    </div>
                                    <div class="form-group">
                                        <%--<input type="password" class="form-control" placeholder="Password" required="required" />--%>
                                        <asp:TextBox ID="tbPassword" runat="server" placeholder="Enter Password..."
                                            CssClass="form-control" TextMode="Password" />
                                    </div>
                                    <%--<input type="submit" class="btn btn-primary btn-block" value="Sign in" />--%>
                                    <asp:Button runat="server" ID="btnLogin" Width="100%"
                                        CssClass="btn btn-primary btn-block" Text="Sign in" OnClick="Login" />
                                </div>

                            </li>

                        </ul>

                    </li>

                    <li class="nav-item" style="width: 150px;">
                        
                        <a runat="server" id="A1" data-toggle="dropdown" class=" nav-link dropdown-toggle"
                            style="color: white; text-align: center; cursor: pointer; background-color: rgba(192,192,192,.1)">HRGA</a>

                        <ul class="dropdown-menu form-wrapper" style="border-color: transparent;">
                            
                            <li>
                                <div>

                                    <p class="hint-text">LOGIN</p>

                                    <div class="form-group">
                                        <%--<input type="text" class="form-control" placeholder="Username" required="required" />--%>
                                        <asp:TextBox ID="tbHRUsername" runat="server" onkeydown="return (event.keyCode!=13);"
                                            CssClass="form-control" placeholder="Enter Username..." />
                                    </div>
                                    <div class="form-group">
                                        <%--<input type="password" class="form-control" placeholder="Password" required="required" />--%>
                                        <asp:TextBox ID="tbHRUserpass" runat="server" placeholder="Enter Password..."
                                            CssClass="form-control" TextMode="Password" OnTextChanged="LoginHR" />
                                    </div>
                                    <%--<input type="submit" class="btn btn-primary btn-block" value="Sign in" />--%>
                                    <asp:Button runat="server" ID="Button1" Width="100%"
                                        CssClass="btn btn-primary btn-block" Text="Sign in" OnClick="LoginHR" />
                                </div>

                            </li>

                        </ul>

                    </li>

                </ul>
            </div>
        </nav>

        <div style="margin-top: 5%; margin-bottom: 5%; text-align: center">
            <img src="../images/skpilogopic.png" style="width: 300px; height: 250px;" />

            <p style="color: white; font-size: 60px"><b>Sohbi Kohgei (Phils.), Inc.</b></p>

            <p style="color: white; font-size: 30px">Special Economic Zone, Lima Technology Center, Lipa City, Batangas</p>
        </div>
    </form>
</body>
</html>
