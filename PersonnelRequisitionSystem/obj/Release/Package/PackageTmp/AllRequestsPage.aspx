<%@ Page Title="All Requests" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AllRequestsPage.aspx.cs"
    Inherits="PersonnelRequisitionSystem.AllRequestsPage" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        @media (min-width: 992px) {
            .steps {
                padding: 0px;
                background: transparent;
                list-style: none;
                overflow: hidden;
                margin-top: 20px;
                margin-bottom: 20px;
                border-radius: 4px;
                display: table-row;
            }

                .steps > li {
                    display: table-cell;
                    vertical-align: middle;
                    width: 1%;
                    height: 0;
                }

                    .steps > li + li:before {
                        padding: 0;
                        content: "";
                    }

                .steps li a {
                    color: white;
                    text-decoration: none;
                    padding: 10px 0 10px 35px;
                    position: relative;
                    display: inline-block;
                    width: calc(100% - 10px);
                    background-color: #999;
                    text-align: center;
                    height: 100%;
                }

                .steps li.completed a {
                    /*background: #3c763d;*/
                    background: #00a65a;
                }

                    .steps li.completed a:after {
                        /*border-left: 30px solid #3c763d;*/
                        border-left: 30px solid #00a65a;
                    }

                .steps li.active a {
                    background: #3c8dbc;
                }

                    .steps li.active a:after {
                        border-left: 30px solid #3c8dbc;
                    }

                .steps li.rejected_cancelled a {
                    background: #dd4b39;
                }

                    .steps li.rejected_cancelled a:after {
                        border-left: 30px solid #dd4b39;
                    }

                .steps li.pending a {
                    background: #ffa500;
                }

                    .steps li.pending a:after {
                        border-left: 30px solid #ffa500;
                    }

                .steps li:first-child a {
                    padding-left: 15px;
                }

                .steps li:last-of-type a {
                    width: calc(100% - 38px);
                }

                .steps li a:before {
                    content: " ";
                    display: block;
                    width: 0;
                    height: 0;
                    border-top: 50px solid transparent; /* height not equal parent */
                    border-bottom: 50px solid transparent; /* height not equal parent */
                    border-left: 30px solid white;
                    position: absolute;
                    top: 50%;
                    margin-top: -50px; /* height not equal parent */
                    margin-left: 1px;
                    left: 100%;
                    z-index: 1;
                }

                .steps li a:after {
                    content: " ";
                    display: block;
                    width: 0;
                    height: 0;
                    border-top: 50px solid transparent; /* height not equal parent */
                    border-bottom: 50px solid transparent; /* height not equal parent */
                    border-left: 30px solid #999;
                    position: absolute;
                    top: 50%;
                    margin-top: -50px; /* height not equal parent */
                    left: 100%;
                    z-index: 2;
                }
        }

        @media (max-width: 991px) {
            .steps {
                padding: 8px 15px;
                margin-bottom: 20px;
                list-style: none;
                background-color: #f5f5f5;
                border-radius: 4px;
                overflow: auto;
            }

                .steps > li {
                    display: block;
                }

                .steps li a {
                    color: #777;
                }

                .steps > li:before {
                    padding: 0 5px;
                    color: #ccc;
                    content: "\e080";
                    font-family: 'Glyphicons Halflings';
                    font-style: normal;
                    font-weight: 400;
                    line-height: 1;
                    -webkit-font-smoothing: antialiased;
                    -moz-osx-font-smoothing: grayscale;
                }

                .steps li.completed:before {
                    content: "\e013";
                    color: #3c763d;
                    font-family: 'Glyphicons Halflings';
                    font-style: normal;
                    font-weight: 400;
                    line-height: 1;
                    -webkit-font-smoothing: antialiased;
                    -moz-osx-font-smoothing: grayscale;
                }

                .steps li.completed a {
                    color: inherit;
                }

                .steps li.active:before {
                    color: #8a6d3b;
                }

                .steps > .active {
                    color: #999;
                }

                .steps li:first-child a {
                    padding-left: inherit;
                }

                .steps li:last-of-type a {
                    width: inherit;
                }
        }
    </style>

    <style>
        .alignright {
            text-align: right;
        }

        .aligncenter {
            text-align: center;
        }

        .headers {
            color: white;
            text-align: center;
            font-size: 19px;
            padding: 5px;
            background-color: #3c8dbc;
            border: none;
        }

        .items {
            text-align: left;
            font-size: 15px;
            word-wrap: break-word;
            padding: 5px;
        }

        .items2 {
            text-align: center;
            font-size: 15px;
            word-wrap: break-word;
            padding: 5px;
        }
    </style>

    <style type="text/css">
        .orderText {
            font: normal 12px Arial,Verdana;
            margin-top: 6px;
        }

        .RadWindow .rwTitleWrapper {
            box-sizing: content-box;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" SkinID="WebBlue">
    </telerik:RadWindowManager>

    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Position="Center"
        Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000" AutoCloseDelay="3000">
    </telerik:RadNotification>

    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function UpdateItemCountField(sender, args) {
                //Set the footer text.
                sender.get_dropDownElement().lastChild.innerHTML = "A total of " + sender.get_items().get_count() + " items";
            }
        </script>
    </telerik:RadScriptBlock>

    <div class="content-wrapper">

        <div class="container">

            <!-- Content Header (Page header) -->
            <section class="content-header">

                <h1>ALL REQUESTS
                </h1>

                <ol class="breadcrumb">

                    <li><a runat="server"><i class="glyphicon glyphicon-home"></i>Home</a></li>

                    <li class="active">ALL REQUESTS</li>

                </ol>

            </section>

            <!-- Main content -->
            <section class="content">

                <div class="row" runat="server" id="rowRecords" visible="true">

                    <div class="col-lg-lg-12">

                        <div class="row">

                            <div class="col-lg-12">

                                <fieldset>

                                    <legend>Legend</legend>

                                    <table style="width: 100%; margin-top: -25px">
                                        <tr>
                                            <td>
                                                <div class="bg-orange" style="width: 30px; height: 30px"></div>
                                            </td>
                                            <td style="width: 120px; padding-left: 10px">
                                                <h5>Cancelled</h5>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <div class="bg-red" style="width: 30px; height: 30px"></div>
                                            </td>
                                            <td style="width: 120px; padding-left: 10px">
                                                <h5>Rejected</h5>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <div class="bg-green-active" style="width: 30px; height: 30px"></div>
                                            </td>
                                            <td style="width: 120px; padding-left: 10px">
                                                <h5>ACTIVE/ON-GOING</h5>
                                            </td>
                                            <td style="width: 70%; padding-left: 10px">
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="150px"
                                                    CssClass="btn btn-flat btn-primary bg-blue-gradient pull-right"
                                                    Style="vertical-align: middle; margin-top: 15px; margin-bottom: 15px;"
                                                    OnClick="RefreshGrid" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>

                            </div>

                        </div>

                        <telerik:RadGrid RenderMode="Lightweight" ID="gridRequests" AllowFilteringByColumn="True" Height="600px"
                            AllowSorting="True" AllowPaging="True" PageSize="30" runat="server" AutoGenerateColumns="False"
                            EnableLinqExpressions="false" ShowStatusBar="true" ShowGroupPanel="false" OnItemDataBound="gridRequests_ItemDataBound">

                            <MasterTableView AutoGenerateColumns="false" EditMode="Batch" AllowFilteringByColumn="true">

                                <BatchEditingSettings EditType="Cell" OpenEditingEvent="Click" />
                                <CommandItemSettings ShowRefreshButton="true" ShowAddNewRecordButton="true" ShowSaveChangesButton="true" />

                                <Columns>

                                    <%--<telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" AllowFiltering="false"
                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="CounterColumn" HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="numberLabel" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>

                                    <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" AllowFiltering="false"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="85%"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewDetails" runat="server" CommandArgument='<%# Eval("UniqueCode")%>' Width="100%"
                                                Text="View" CssClass="btn btn-primary btn-flat bg-blue-gradient" Style="color: white"
                                                OnClick="ViewDetails"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ControlNo"
                                        HeaderStyle-Width="150px" ItemStyle-Width="150px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                        FilterControlAltText="Filter Control No" HeaderText="Control No" SortExpression="ControlNo"
                                        UniqueName="ControlNo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="FullName_LnameFirst"
                                        HeaderStyle-Width="150px" ItemStyle-Width="150px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                        FilterControlAltText="Filter FullName_LnameFirst" HeaderText="Requestor" SortExpression="FullName_LnameFirst"
                                        UniqueName="FullName_LnameFirst" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Dept_Desc"
                                        HeaderStyle-Width="250px" ItemStyle-Width="250px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" Display="false"
                                        FilterControlAltText="Filter Department" HeaderText="Department" SortExpression="Dept_Desc"
                                        UniqueName="Dept_Desc" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">

                                        <FilterTemplate>
                                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxview" DataSourceID="SqlDataSource1"
                                                DataTextField="Description"
                                                DataValueField="Description" Width="100%" AppendDataBoundItems="true" Skin="Metro"
                                                SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Dept_Desc").CurrentFilterValue %>'
                                                runat="server" OnClientSelectedIndexChanged="CodeIndexChangedStat">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="All" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlock5" runat="server">
                                                <script type="text/javascript">
                                                    function CodeIndexChangedview(sender, args) {
                                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                        tableView.filter("Dept_Desc", args.get_item().get_value(), "EqualTo");
                                                    }
                                                </script>
                                            </telerik:RadScriptBlock>
                                        </FilterTemplate>


                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="MaleCount" AllowFiltering="false"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                        FilterControlAltText="Filter Male" HeaderText="Male" SortExpression="MaleCount"
                                        UniqueName="MaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="FemaleCount" AllowFiltering="false"
                                        HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                        FilterControlAltText="Filter Female" HeaderText="Female" SortExpression="FemaleCount"
                                        UniqueName="FemaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridDateTimeColumn DataField="DateNeeded" HeaderText="Date Needed" FilterDateFormat="dd/MMM/yyyy"
                                        SortExpression="DateNeeded" PickerType="DatePicker" EnableTimeIndependentFiltering="true" AllowFiltering="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-Width="150px" ItemStyle-Width="150px" CurrentFilterFunction="EqualTo" UniqueName="DateNeeded"
                                        DataFormatString="{0:dd/MMM/yyyy}" AutoPostBackOnFilter="true">
                                    </telerik:GridDateTimeColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="SIGNSTAT"
                                        HeaderStyle-Width="200px" ItemStyle-Width="200px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" AllowFiltering="false"
                                        FilterControlAltText="Filter Remarks" HeaderText="Remarks" SortExpression="SIGNSTAT"
                                        UniqueName="SIGNSTAT" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="SigningRemarks" Display="false"
                                        HeaderStyle-Width="200px" ItemStyle-Width="200px" FilterControlWidth="100%" ShowFilterIcon="false"
                                        HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" AllowFiltering="false"
                                        FilterControlAltText="Filter Remarks" HeaderText="Remarks" SortExpression="SigningRemarks"
                                        UniqueName="SigningRemarks" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>

                                </Columns>

                            </MasterTableView>

                            <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="3"></Scrolling>
                                <Selecting AllowRowSelect="false"></Selecting>
                            </ClientSettings>

                            <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>

                            <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#006699" />

                            <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>

                        </telerik:RadGrid>

                    </div>

                </div>

                <div class="row" runat="server" id="rowDetails" visible="false">

                    <div class="col-lg-12">

                        <div class="box" runat="server" id="cardReceive" visible="false">
                            <div class="box-header" style="background-color: rgb(11, 163, 217);">
                                <h3 class="box-title" style="color: white; margin-top: 7px">RECORD DETAILS</h3>
                                <%--<div class="card-options">
                                    <a runat="server" class="card-options-remove" style="color: white"><i class="fe fe-x"></i></a>
                                </div>--%>
                                <button type="button" runat="server"
                                    id="btncloseviewing" class="btn btn-default btn-box-tool pull-right aligncenter"
                                    style="width: 25px" onserverclick="CloseForm">
                                    <i class="fa fa-times"></i>
                                </button>
                            </div>
                            <div class="box-body">

                                <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip2"
                                    MultiPageID="RadMultiPage2" SelectedIndex="0" Font-Size="Smaller">

                                    <Tabs>

                                        <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                                        <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                                    </Tabs>

                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage2" SelectedIndex="0">

                                    <telerik:RadPageView runat="server" ID="RadPageView1">

                                        <div class="box">

                                            <div class="box-body">

                                                <div class="row">

                                                    <div class="col-lg-12">

                                                        <div class="row">
                                                            <div class="col-lg-3 alignright">
                                                                <b>Prepared by</b> :
                                                            </div>
                                                            <div class="col-lg-9">
                                                                <label id="lblSignRequestor" runat="server" style="font-weight: normal;"></label>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-3 alignright">
                                                                <b>Noted by(Dept. Manager)</b> :
                                                            </div>
                                                            <div class="col-lg-9">
                                                                <label id="lblSignDeptManager" runat="server" style="font-weight: normal;"></label>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-3 alignright">
                                                                <b>Noted by(HRGA. Manager)</b> :
                                                            </div>
                                                            <div class="col-lg-9">
                                                                <label id="lblSignHRManager" runat="server" style="font-weight: normal;"></label>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-lg-3 alignright">
                                                                <b>Noted by(GM/Factory Manager)</b> :
                                                            </div>
                                                            <div class="col-lg-9">
                                                                <label id="lblSignGM_FactoryManager" runat="server" style="font-weight: normal;"></label>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-3 alignright">
                                                                <b>Approver - VP</b> :
                                                            </div>

                                                            <div class="col-lg-9">
                                                                <label runat="server" id="lblSignVP" style="font-weight: normal;"></label>
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-lg-3 alignright">
                                                                <b>Received by</b> :
                                                            </div>

                                                            <div class="col-lg-9">
                                                                <label runat="server" style="font-weight: normal;">HRGA</label>
                                                            </div>

                                                        </div>


                                                    </div>

                                                </div>

                                                <ul class="steps">

                                                    <li id="liSignPreparer" runat="server"><a href="#">
                                                        <label style="font-weight: normal;">1. Requesting</label><br />
                                                        <label id="lblRemarksSubmittion" runat="server" style="font-weight: normal;"></label>
                                                    </a></li>

                                                    <li id="liSignChecker" runat="server"><a href="#">
                                                        <label style="font-weight: normal;">2. Note By Dept. Manager)</label><br />
                                                        <label id="lblRemarksChecking" runat="server" style="font-weight: normal;"></label>
                                                    </a></li>

                                                    <li id="liSignNoter" runat="server"><a href="#">
                                                        <label style="font-weight: normal;">3. Note By HRGA Manager</label><br />
                                                        <label id="lblRemarksNoted" runat="server" style="font-weight: normal;"></label>
                                                    </a></li>

                                                    <li id="liSignSecondNoter" runat="server"><a href="#">
                                                        <label style="font-weight: normal;">4. Note By GM/Factory Manager</label><br />
                                                        <label id="lblRemarksSecondNoted" runat="server" style="font-weight: normal;"></label>
                                                    </a></li>

                                                    <li id="liSignVPApproval" runat="server"><a href="#">
                                                        <label style="font-weight: normal;">5. Approval - VP</label><br />
                                                        <label id="lblRemarksVPApproval" runat="server" style="font-weight: normal;"></label>
                                                    </a></li>

                                                    <li id="liSignHRGA" runat="server"><a href="#">
                                                        <label style="font-weight: normal;">6. Received(HRGA)</label><br />
                                                        <label id="lblRemarksHRGA" runat="server" style="font-weight: normal;"></label>
                                                    </a></li>


                                                </ul>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Control No:</label>

                                                        <asp:TextBox ID="tbRecControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Date Filed:</label>

                                                        <asp:TextBox ID="tbRecDateFiled" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Requestor Name:</label>

                                                        <asp:TextBox ID="tbRecEmpName" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Department:</label>

                                                        <asp:TextBox ID="tbRecDepartment" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Section:</label>

                                                        <asp:TextBox ID="tbRecSection" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Position:</label>

                                                        <asp:TextBox ID="tbRecPosition" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <div class="row">
                                                            <div class="col-lg-4">

                                                                <label class="docconlabels">Male:</label>

                                                                <asp:TextBox ID="tbRecMaleCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                            </div>

                                                            <div class="col-lg-4">

                                                                <label class="docconlabels">Female:</label>

                                                                <asp:TextBox ID="tbRecFemaleCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                            </div>

                                                            <div class="col-lg-4">

                                                                <label class="docconlabels">Total:</label>

                                                                <asp:TextBox ID="tbRecTotalCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Date Needed:</label>

                                                        <asp:TextBox ID="tbRecDateNeeded" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Job Description</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">

                                                    <div class="col-lg-12">

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <label class="docconlabels">Brief Description of Duties:</label>

                                                                <asp:TextBox ID="tbRecBriefDescriptionofDuties" runat="server" ReadOnly="true" placeholder="Enter Text Here..."
                                                                    CssClass="form-control" Height="100px" TextMode="MultiLine" Visible="false"></asp:TextBox>

                                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridRecBriefDesc" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                    AutoGenerateColumns="False" OnItemDataBound="gridRecBriefDesc_ItemDataBound"
                                                                    DataSourceID="SqlDSBriefDescOfDuties" Skin="Metro" ShowHeader="false">

                                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false" />

                                                                        <SortExpressions>

                                                                            <telerik:GridSortExpression FieldName="ID" SortOrder="Ascending" />

                                                                        </SortExpressions>

                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px"
                                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="TemplateColumn" HeaderText="#"
                                                                                Display="false">

                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="numberLabel" runat="server" />

                                                                                </ItemTemplate>

                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-HorizontalAlign="Center" Display="false"
                                                                                SortExpression="ID" UniqueName="ID">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn DataField="BriefDesc" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-CssClass="wrapword maximize" ItemStyle-HorizontalAlign="Left" HeaderText="Brief Description "
                                                                                SortExpression="BriefDesc" UniqueName="BriefDesc" HeaderStyle-Font-Size="15">

                                                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                                                    <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                                                    </RequiredFieldValidator>
                                                                                </ColumnValidationSettings>
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                    </MasterTableView>

                                                                    <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                                                </telerik:RadGrid>

                                                            </div>

                                                        </div>

                                                        <br />

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                                                <asp:TextBox ID="tbRecSpecialSkills_QualificationsRequired" ReadOnly="true" runat="server" Visible="false"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridRecSpecialSkills_QualificationsRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                    AutoGenerateColumns="False" OnItemDataBound="gridRecSpecialSkills_QualificationsRequired_ItemDataBound"
                                                                    Skin="Metro" ShowHeader="false" DataSourceID="SqlDSSpecialSkills_QualificationsReq">

                                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false" />

                                                                        <SortExpressions>

                                                                            <telerik:GridSortExpression FieldName="ID" SortOrder="Ascending" />

                                                                        </SortExpressions>

                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" Display="false"
                                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="TemplateColumn" HeaderText="#">

                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="numberLabel" runat="server" />

                                                                                </ItemTemplate>

                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-HorizontalAlign="Center" Display="false"
                                                                                SortExpression="ID" UniqueName="ID">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn DataField="Skills_Qualifications" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-CssClass="wrapword" ItemStyle-HorizontalAlign="Left" HeaderText="Skills_Qualifications"
                                                                                SortExpression="Skills_Qualifications" UniqueName="Skills_Qualifications" HeaderStyle-Font-Size="15">

                                                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                                                    <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                                                    </RequiredFieldValidator>
                                                                                </ColumnValidationSettings>
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                    </MasterTableView>

                                                                    <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                                                </telerik:RadGrid>


                                                            </div>

                                                        </div>

                                                        <br />

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <label class="docconlabels">Education Required:</label>

                                                                <asp:TextBox ID="tbRecEducationRequired" runat="server" ReadOnly="true" Visible="false"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridRecEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                    AutoGenerateColumns="False" OnItemDataBound="gridRecEducationRequired_ItemDataBound"
                                                                    Skin="Metro" ShowHeader="false"
                                                                    DataSourceID="SQLDSEducationRequired">

                                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false" />

                                                                        <SortExpressions>

                                                                            <telerik:GridSortExpression FieldName="ID" SortOrder="Ascending" />

                                                                        </SortExpressions>

                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px"
                                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="TemplateColumn" HeaderText="#"
                                                                                Display="false">

                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="numberLabel" runat="server" />

                                                                                </ItemTemplate>

                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-HorizontalAlign="Center" Display="false"
                                                                                SortExpression="ID" UniqueName="ID">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn DataField="EducationRequired" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-CssClass="wrapword" ItemStyle-HorizontalAlign="Left" HeaderText="EducationRequired"
                                                                                SortExpression="EducationRequired" UniqueName="EducationRequired" HeaderStyle-Font-Size="15">

                                                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                                                    <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                                                    </RequiredFieldValidator>
                                                                                </ColumnValidationSettings>
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>

                                                                    <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                                                </telerik:RadGrid>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Employment/Work Status</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="tbRecWorkStatus" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                            TextMode="SingleLine"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Justification and History</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">

                                                    <div class="col-lg-4">

                                                        <asp:TextBox ID="tbRecJustification" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-8">

                                                        <asp:TextBox ID="tbRecHistory" runat="server" placeholder="Enter Text Here..." Enabled="false" CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Attachment</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px;">

                                                    <div class="col-lg-12">

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <table>

                                                                    <tr>

                                                                        <td style="padding: 10px">
                                                                            <b>Select File:</b>
                                                                        </td>

                                                                        <td style="padding: 10px">
                                                                            <telerik:RadAsyncUpload ID="RadAsyncUpload1" Skin="Metro"
                                                                                Style="padding-top: 8px" MultipleFileSelection="Disabled" runat="server" Width="100%">
                                                                            </telerik:RadAsyncUpload>
                                                                        </td>

                                                                        <td style="padding: 10px">
                                                                            <asp:LinkButton ID="linkUpload" runat="server" Style="font-size: 10px;"
                                                                                class="btn btn-primary btn-flat" OnClientClick="return confirm('Do you want to proceed?')"
                                                                                OnClick="UploadFile"><i class="glyphicon glyphicon-upload"></i> Upload</asp:LinkButton>
                                                                        </td>

                                                                    </tr>

                                                                </table>

                                                            </div>

                                                        </div>

                                                        <br />


                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <asp:GridView ID="gridRecAttachment" runat="server" Width="100%"
                                                                    CssClass="table table-outline table-vcenter text-nowrap card-table"
                                                                    AutoGenerateColumns="false" ShowFooter="false" EmptyDataText="No Records Found"
                                                                    OnPreRender="gridRecAttachment_PreRender">

                                                                    <Columns>

                                                                        <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="95%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                                            <ItemTemplate>

                                                                                <%# Eval("attachmentname") %>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20px">

                                                                            <ItemTemplate>

                                                                                <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                                                    CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile"
                                                                                    Text=" Download" Width="100%" />

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField ItemStyle-CssClass="items2" HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-Width="25px" HeaderStyle-Width="25px">

                                                                            <ItemTemplate>

                                                                                <asp:LinkButton ID="lnkRemoveAttachment" runat="server" class="btn btn-success glyphicon glyphicon-trash aligncenter"
                                                                                    CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to delete?')"
                                                                                    OnClick="lnkRemoveAttachment_Click" />

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                    </Columns>

                                                                    <AlternatingRowStyle />

                                                                    <EmptyDataRowStyle CssClass="items2" />

                                                                </asp:GridView>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>


                                                <div class="row" style="margin-top: 10px;" runat="server" visible="true">

                                                    <div class="col-lg-12">

                                                        <div class="row" id="divApprover" runat="server" visible ="false">
                                                            <div class="col-lg-4"></div>
                                                            <div class="col-lg-4">

                                                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="RadcboApprover"
                                                                    MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px" Label="Approver"
                                                                    HighlightTemplatedItems="true" OnDataBound="RadcboApprover_DataBound"
                                                                    OnClientItemsRequested="UpdateItemCountField" OnItemDataBound="RadcboApprover_ItemDataBound"
                                                                    AutoPostBack="true"
                                                                    EmptyMessage="Select Approver"
                                                                    Filter="Contains">
                                                                    <HeaderTemplate>
                                                                        <ul>
                                                                            <li class="col1">Employee ID</li>
                                                                            <li class="col2_2">Employee Name</li>
                                                                        </ul>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <ul>
                                                                            <li class="col1">
                                                                                <%# DataBinder.Eval(Container.DataItem, "EmpID") %></li>
                                                                            <li class="col2">
                                                                                <%# DataBinder.Eval(Container.DataItem, "FullName_LnameFirst") %></li>

                                                                        </ul>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        A total of
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                        items
                                                                    </FooterTemplate>
                                                                </telerik:RadComboBox>

                                                                <br />

                                                                <br />

                                                                <br />
                                                            </div>
                                                            <div class="col-lg-4"></div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-lg-12 aligncenter">

                                                                <telerik:RadButton ID="btnReActivate" runat="server"
                                                                    Text="Re-Activate Application" SingleClick="true"
                                                                    Primary="true" RenderMode="Lightweight" Enabled="true"
                                                                    Style="width: 150px; height: 50px; margin-left: 10px; margin-right: 10px;"
                                                                    SingleClickText="Processing..." OnClick="btnReActivate_Click">

                                                                    <ConfirmSettings ConfirmText="Are you sure you want to re-activate the application?" Title="Confirm Action" />

                                                                </telerik:RadButton>

                                                                <telerik:RadButton ID="btnResend" runat="server"
                                                                    Text="Resend Application" SingleClick="true"
                                                                    Primary="true" RenderMode="Lightweight" Enabled="true"
                                                                    Style="width: 150px; height: 50px; margin-left: 10px; margin-right: 10px;"
                                                                    SingleClickText="Sending..." OnClick="btnResend_Click">

                                                                    <ConfirmSettings ConfirmText="Are you sure you want to resend the application?" Title="Confirm Action" />
                                                                </telerik:RadButton>

                                                                <button type="button" id="btnCancel" runat="server" class="btn btn-warning"
                                                                    style="width: 150px; height: 50px; margin-left: 10px; margin-right: 10px;"
                                                                    data-toggle="modal" data-target="#modalCancel">
                                                                    Cancel Application</button>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>

                                            </div>

                                        </div>
                                    </telerik:RadPageView>

                                    <telerik:RadPageView runat="server" ID="RadPageView2">

                                        <%= GetCommentThreads()%>

                                        <div class="row" runat="server" id="rowNewMessage">
                                            <div class="col-lg-12">
                                                <hr style="margin-top: 10px" />
                                                <div class="input-group" style="margin-top: -10px">
                                                    <input runat="server" id="tbPendingMessage" type="text" name="message" placeholder="Type Message ..." class="form-control" />
                                                    <span class="input-group-btn">
                                                        <button type="button" class="btn btn-primary btn-flat" runat="server" onserverclick="btnHold_Click">Send</button>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>

                                    </telerik:RadPageView>

                                </telerik:RadMultiPage>

                            </div>



                        </div>

                        <div class="box" runat="server" id="cardViewRequestDetails" visible="false">

                            <div class="box-header" style="background-color: rgb(11, 163, 217);">

                                <h3 class="box-title" style="color: white; margin-top: 7px">RECORD DETAILS</h3>

                                <button type="button" runat="server"
                                    id="Button1" class="btn btn-default btn-box-tool pull-right aligncenter"
                                    style="width: 25px" onserverclick="CloseForm">
                                    <i class="fa fa-times"></i>
                                </button>

                            </div>

                            <div class="box-body">

                                <div class="box">

                                    <div class="box-header bg-gray-light" style="color: black">

                                        <h3 class="box-title" style="color: black;">Application Status</h3>

                                    </div>

                                    <div class="box-body">

                                        <div class="row" runat="server" id="AlertCancelled" visible="false" style="margin-top: 5px;">

                                            <div class="col-lg-12">

                                                <%-- <%=CancelAlert(Session["ApplicationRemarks"].ToString().Trim()) %>--%>
                                            </div>

                                        </div>

                                        <div class="row" style="margin-top: 10px" runat="server" visible="false">

                                            <div class="col-lg-6" runat="server" visible="false">

                                                <div class="row">

                                                    <div class="col-lg-3" runat="server">

                                                        <label class="docconlabels">Male Endorsed:</label>

                                                        <asp:TextBox ID="tbViewMaleEndorsed" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                            TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-3" runat="server">

                                                        <label class="docconlabels">Female Endorsed:</label>

                                                        <asp:TextBox ID="tbViewFemaleEndorsed" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                            TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-3">

                                                        <label class="docconlabels">Male Balance:</label>

                                                        <asp:TextBox ID="tbViewMaleBalance" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-3">

                                                        <label class="docconlabels">Female Balance:</label>

                                                        <asp:TextBox ID="tbViewFemaleBalance" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-lg-6">

                                                <div class="row">

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Status:</label>

                                                        <asp:TextBox ID="tbViewApplicationStatus" runat="server"
                                                            ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control"
                                                            Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>
                                                    <div class="col-lg-4" runat="server" id="colViewServedDate" visible="true">

                                                        <label class="docconlabels">Served Date:</label>

                                                        <asp:TextBox ID="tbViewServedDate" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                            TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="row" id="rowServedHistory2" style="margin-top: 10px">

                                            <div class="col-lg-12">

                                                <asp:GridView ID="gridEndorsementLogs2" runat="server" Width="100%"
                                                    AutoGenerateColumns="false" HeaderStyle-BackColor="whitesmoke"
                                                    ShowFooter="false" AllowPaging="true" DataSourceID="SQLDSEndorsementLogs2"
                                                    PageSize="10" PagerSettings-Mode="NumericFirstLast"
                                                    EmptyDataText="No Endorsement Records Found">

                                                    <Columns>

                                                        <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Count">

                                                            <ItemTemplate>

                                                                <%# Eval("MaleCount") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Endorsed">

                                                            <ItemTemplate>

                                                                <%# Eval("MaleEndorsed") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Balance">

                                                            <ItemTemplate>

                                                                <%# Eval("MaleBalance") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Count">

                                                            <ItemTemplate>

                                                                <%# Eval("FemaleCount") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Endorsed">

                                                            <ItemTemplate>

                                                                <%# Eval("FemaleEndorsed") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Balance">

                                                            <ItemTemplate>

                                                                <%# Eval("FemaleBalance") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Served Date">

                                                            <ItemTemplate>

                                                                <%# Eval("ServedDate", "{0:yyyy-MMM-dd}") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                    </Columns>

                                                    <EmptyDataRowStyle CssClass="items2" />

                                                    <PagerStyle CssClass="gridview" HorizontalAlign="center" />

                                                </asp:GridView>

                                            </div>

                                        </div>

                                    </div>
                                </div>

                                <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip4"
                                    MultiPageID="RadMultiPage4" SelectedIndex="0" Font-Size="Smaller">

                                    <Tabs>

                                        <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                                        <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                                    </Tabs>

                                </telerik:RadTabStrip>

                                <telerik:RadMultiPage runat="server" ID="RadMultiPage4" SelectedIndex="0">

                                    <telerik:RadPageView runat="server" ID="RadPageView5">
                                        <div class="box">
                                            <div class="box-body">

                                                <div class="row">

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Control No:</label>

                                                        <asp:TextBox ID="tbViewControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Date Filed:</label>

                                                        <asp:TextBox ID="tbViewDateFiled" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Date Received:</label>

                                                        <asp:TextBox ID="tbViewDateReceived" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Requestor Name:</label>

                                                        <asp:TextBox ID="tbViewReqName" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Department:</label>

                                                        <asp:TextBox ID="tbViewDepartment" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Section:</label>

                                                        <asp:TextBox ID="tbViewSection" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-4">
                                                        <label class="docconlabels">Position:</label>

                                                        <asp:TextBox ID="tbViewPosition" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <div class="row">
                                                            <div class="col-lg-4">

                                                                <label class="docconlabels">Male:</label>

                                                                <asp:TextBox ID="tbViewMaleCount" runat="server" ReadOnly="true"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                                    TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                            </div>

                                                            <div class="col-lg-4">

                                                                <label class="docconlabels">Female:</label>

                                                                <asp:TextBox ID="tbViewFemaleCount" runat="server" ReadOnly="true"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                                    TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                            </div>

                                                            <div class="col-lg-4">

                                                                <label class="docconlabels">Total:</label>

                                                                <asp:TextBox ID="tbViewTotal" runat="server" ReadOnly="true"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="col-lg-4">

                                                        <label class="docconlabels">Date Needed:</label>

                                                        <asp:TextBox ID="tbViewDateNeeded" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                            TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Job Description</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">

                                                    <div class="col-lg-12">

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <label class="docconlabels">Brief Description of Duties:</label>

                                                                <asp:TextBox ID="tbViewBriefDesc" runat="server" ReadOnly="true" Visible="false"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridBriefDesc" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                    AutoGenerateColumns="False" OnItemDataBound="gridBriefDesc_ItemDataBound"
                                                                    DataSourceID="SqlDSBriefDescOfDuties" Skin="Metro" ShowHeader="false">

                                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false" />

                                                                        <SortExpressions>

                                                                            <telerik:GridSortExpression FieldName="ID" SortOrder="Ascending" />

                                                                        </SortExpressions>

                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px"
                                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="TemplateColumn" HeaderText="#"
                                                                                Display="false">

                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="numberLabel" runat="server" />

                                                                                </ItemTemplate>

                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-HorizontalAlign="Center" Display="false"
                                                                                SortExpression="ID" UniqueName="ID">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn DataField="BriefDesc" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-CssClass="wrapword maximize" ItemStyle-HorizontalAlign="Left" HeaderText="Brief Description "
                                                                                SortExpression="BriefDesc" UniqueName="BriefDesc" HeaderStyle-Font-Size="15">

                                                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                                                    <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                                                    </RequiredFieldValidator>
                                                                                </ColumnValidationSettings>
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                    </MasterTableView>

                                                                    <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                                                </telerik:RadGrid>

                                                            </div>

                                                        </div>

                                                        <br />

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                                                <asp:TextBox ID="tbViewSpecialSkills" ReadOnly="true" runat="server" Visible="false"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridSpecialSkills_QualificationsRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                    AutoGenerateColumns="False" OnItemDataBound="gridSpecialSkills_QualificationsRequired_ItemDataBound"
                                                                    Skin="Metro" ShowHeader="false" DataSourceID="SqlDSSpecialSkills_QualificationsReq">

                                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false" />

                                                                        <SortExpressions>

                                                                            <telerik:GridSortExpression FieldName="ID" SortOrder="Ascending" />

                                                                        </SortExpressions>

                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" Display="false"
                                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="TemplateColumn" HeaderText="#">

                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="numberLabel" runat="server" />

                                                                                </ItemTemplate>

                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-HorizontalAlign="Center" Display="false"
                                                                                SortExpression="ID" UniqueName="ID">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn DataField="Skills_Qualifications" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-CssClass="wrapword" ItemStyle-HorizontalAlign="Left" HeaderText="Skills_Qualifications"
                                                                                SortExpression="Skills_Qualifications" UniqueName="Skills_Qualifications" HeaderStyle-Font-Size="15">

                                                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                                                    <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                                                    </RequiredFieldValidator>
                                                                                </ColumnValidationSettings>
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                    </MasterTableView>

                                                                    <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                                                </telerik:RadGrid>

                                                            </div>

                                                        </div>

                                                        <br />

                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <label class="docconlabels">Education Required:</label>

                                                                <asp:TextBox ID="tbViewEducationRequired" runat="server" ReadOnly="true" Visible="false"
                                                                    placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                    AutoGenerateColumns="False" OnItemDataBound="gridEducationRequired_ItemDataBound"
                                                                    Skin="Metro" ShowHeader="false"
                                                                    DataSourceID="SQLDSEducationRequired">

                                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false" />

                                                                        <SortExpressions>

                                                                            <telerik:GridSortExpression FieldName="ID" SortOrder="Ascending" />

                                                                        </SortExpressions>

                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px"
                                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="TemplateColumn" HeaderText="#"
                                                                                Display="false">

                                                                                <ItemTemplate>

                                                                                    <asp:Label ID="numberLabel" runat="server" />

                                                                                </ItemTemplate>

                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-HorizontalAlign="Center" Display="false"
                                                                                SortExpression="ID" UniqueName="ID">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn DataField="EducationRequired" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                                                ItemStyle-CssClass="wrapword" ItemStyle-HorizontalAlign="Left" HeaderText="EducationRequired"
                                                                                SortExpression="EducationRequired" UniqueName="EducationRequired" HeaderStyle-Font-Size="15">

                                                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                                                    <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                                                    </RequiredFieldValidator>
                                                                                </ColumnValidationSettings>
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>

                                                                    <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                                                </telerik:RadGrid>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Employment/Work Status</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="tbViewWorkStatus" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                            TextMode="SingleLine"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Justification and History</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">

                                                    <div class="col-lg-4">

                                                        <asp:TextBox ID="tbViewJustification" runat="server" ReadOnly="true"
                                                            placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                    <div class="col-lg-8">

                                                        <asp:TextBox ID="tbViewHistory" runat="server" placeholder="Enter Text Here..."
                                                            Enabled="false" CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <br />

                                                <div class="row">

                                                    <div class="col-lg-12 bg-gray-light" style="color: black">

                                                        <h4 style="margin-top: 10px; font-weight: 400">Attachments</h4>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px">

                                                    <div class="col-lg-12">




                                                        <div class="row">

                                                            <div class="col-lg-12">

                                                                <asp:GridView ID="gridViewAttachments" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                    CssClass="table table-outline table-vcenter text-nowrap card-table"
                                                                    ShowFooter="false" EmptyDataText="No Records Found" OnPreRender="gridViewAttachments_PreRender">

                                                                    <Columns>

                                                                        <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="95%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                                            <ItemTemplate>

                                                                                <%# Eval("attachmentname") %>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20px">

                                                                            <ItemTemplate>

                                                                                <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                                                    CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" Text=" Download" Width="100%" />

                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>

                                                                    </Columns>

                                                                    <AlternatingRowStyle />

                                                                    <EmptyDataRowStyle CssClass="items2" />

                                                                </asp:GridView>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                    </telerik:RadPageView>

                                    <telerik:RadPageView runat="server" ID="RadPageView6">

                                        <%= GetCommentThreads()%>
                                    </telerik:RadPageView>

                                </telerik:RadMultiPage>

                            </div>

                        </div>

                    </div>

                </div>

            </section>

        </div>

    </div>

    <div class="modal fade" id="modalCancel">

        <div class="modal-dialog modal-lg">

            <div class="modal-content" style="width: 100%;">

                <div class="modal-header bg-blue-gradient">

                    <h4 class="modal-title">Reason Of Cancellation</h4>
                </div>

                <div class="modal-body alignleft">

                    <asp:TextBox runat="server" ID="tbreason" TextMode="MultiLine" CssClass="form-control" placeholder="Enter Text Here..." Height="300px" Width="100%"></asp:TextBox>

                </div>

                <div class="modal-footer">

                    <div class="row">

                        <div class="col-lg-6">
                        </div>

                        <div class="col-lg-6">

                            <div class="row">
                                <div class="col-lg-6">

                                    <telerik:RadButton ID="RadButton1" runat="server"
                                        Text="Cancel" SingleClick="true"
                                        Primary="true" RenderMode="Lightweight" Enabled="true"
                                        Style="width: 100%; height: 35px;"
                                        SingleClickText="Cancelling..." OnClick="btnCancel_Click">
                                    </telerik:RadButton>

                                </div>

                                <div class="col-lg-6">

                                    <button type="button" class="btn btn-default btn-flat" style="width: 100%" data-dismiss="modal">Close</button>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </div>



    <asp:SqlDataSource ID="SQLDSEndorsementLogs2" runat="server"
        SelectCommand="select * FROM [ManpowerMonitoringDB].[dbo].[tblEndorsementLogs] where ControlNo=@ControlNo" SelectCommandType="Text">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbViewControlNo" PropertyName="Text" Name="ControlNo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <%--<asp:ControlParameter ControlID="tbPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />--%>

            <asp:SessionParameter SessionField="SPosition" Name="PositionDesc" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:SessionParameter SessionField="SPosition" Name="PositionDesc" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:SessionParameter SessionField="SPosition" Name="PositionDesc" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="GetAllDepartment" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

</asp:Content>
