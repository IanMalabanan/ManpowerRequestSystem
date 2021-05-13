<%@ Page Title="Personnel Requisition System" Language="C#" MasterPageFile="~/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="PersonnelRequisitionPage.aspx.cs" Inherits="PersonnelRequisitionSystem.PersonnelRequisitionPage"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../Bootstraps/AdminLTE_Bootstrap/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />--%>

    <style>
        .maximize input[type=text] {
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- <telerik:RadWindow ID="RadWindow1" runat="server" AutoSize="True" Animation="Resize" AnimationDuration="100">
    </telerik:RadWindow>--%>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100000" IconUrl="~/images/sohbiicon.ico">
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

            <!-- Main content -->
            <section class="content">

                <%--<asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>--%>

                <div class="row" id="divDetails" runat="server">

                    <div class="col-lg-12">

                        <div class="box-header bg-gray-light">

                            <h2 class=" pull-right" style="margin-top: 28px; margin-right: 2%;">

                                <b>MANPOWER REQUEST FORM</b>

                            </h2>

                            <table>

                                <tr>

                                    <td rowspan="2" style="padding-top: 3%;">

                                        <img src="../images/skpi%20logo%20pic.png" style="height: 80px; width: 90px;" />

                                    </td>

                                    <td>

                                        <div class="row">

                                            <div class="col-lg-12">

                                                <h3><b>SOHBI KOHGEI PHILIPPINES, INC.</b></h3>

                                            </div>

                                        </div>

                                        <div class="row">

                                            <div class="col-lg-12">

                                                <h5>LIMA Techology Center, Lipa City, Batangas</h5>

                                            </div>

                                        </div>


                                    </td>

                                </tr>

                            </table>

                        </div>

                        <br />

                        <div class="row">

                            <div class="col-lg-4">

                                <label class="docconlabels">Requestor Name:</label>

                                <asp:TextBox ID="tb_name" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                            </div>

                            <div class="col-lg-4">

                                <label class="docconlabels">Department:</label>

                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="radcboDepartment"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px"
                                    HighlightTemplatedItems="true" DataSourceID="SqlDSGetAllDepartment"
                                    DataTextField="Description" DataValueField="Code"
                                    AutoPostBack="true" CssClass="RadComboBoxDropDown"
                                    EmptyMessage="Select Department" Filter="Contains">
                                    <HeaderTemplate>
                                        <ul>
                                            <li class="col1">Code</li>
                                            <li class="col2">Department</li>
                                        </ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <ul>
                                            <li class="col1">
                                                <%# DataBinder.Eval(Container.DataItem, "Code") %></li>
                                            <li class="col2">
                                                <%# DataBinder.Eval(Container.DataItem, "Description") %></li>
                                        </ul>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        A total of
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                        items
                                    </FooterTemplate>
                                </telerik:RadComboBox>

                            </div>

                            <div class="col-lg-4">

                                <label class="docconlabels">Section:</label>

                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="radcboSection"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px"
                                    HighlightTemplatedItems="true" DataSourceID="SqlDSSKPI_GetSection" DataValueField="SectCode"
                                    DataTextField="Description" AutoPostBack="true" CssClass="RadComboBoxDropDown"
                                    EmptyMessage="Select Section" Filter="Contains">
                                    <HeaderTemplate>
                                        <ul>
                                            <li class="col1" style="width: 60px;">Code</li>
                                            <li class="col2">Section</li>
                                        </ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <ul>
                                            <li class="col1" style="width: 60px">
                                                <%# DataBinder.Eval(Container.DataItem, "SectCode") %></li>
                                            <li class="col2">
                                                <%# DataBinder.Eval(Container.DataItem, "Description") %></li>
                                        </ul>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        A total of
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                        items
                                    </FooterTemplate>
                                </telerik:RadComboBox>

                            </div>

                        </div>
                        <!-- end row 1 -->

                        <br />
                        <div class="row">

                            <div class="col-lg-4">

                                <label class="docconlabels">Position:</label>

                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="radcboPosition"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px"
                                    HighlightTemplatedItems="true" DataSourceID="SqlDSGetPosition"
                                    DataValueField="Description" DataTextField="Description"
                                    AutoPostBack="true" CssClass="RadComboBoxDropDown" OnSelectedIndexChanged="radcboPosition_SelectedIndexChanged"
                                    EmptyMessage="Select Position" Filter="Contains">
                                </telerik:RadComboBox>

                            </div>

                            <div class="col-lg-4">

                                <div class="row">
                                    <div class="col-lg-6">
                                        <label class="docconlabels">Male:</label>

                                        <asp:TextBox ID="tbMaleCount" runat="server" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                    </div>
                                    <div class="col-lg-6">
                                        <label class="docconlabels">Female:</label>

                                        <asp:TextBox ID="tbFemaleCount" runat="server" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-4 hide">
                                        <label class="docconlabels">Total:</label>

                                        <asp:TextBox ID="tbTotalCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-4">

                                <label class="docconlabels">Date Needed:</label>

                                <telerik:RadDatePicker RenderMode="Lightweight" DateInput-ReadOnly="true" DateInput-DateFormat="dd/MMM/yyyy"
                                    ID="dpDateNeeded" Width="100%" Height="38px" runat="server">
                                </telerik:RadDatePicker>

                            </div>


                        </div>
                        <!-- end row 1 -->

                        <br />


                        <!-- collapse 3 -->
                        <div class="box no-border" runat="server" visible="false" id="rowJobRequirements">
                            <div class="box-header with-border bg-gray-light">
                                <h4 class="box-title">
                                    <!-- collapse 3 <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree" style="color:black;"></a>-->
                                    <b>JOB REQUIREMENTS</b>
                                </h4>

                            </div>
                            <div class="box-body">

                                <div class="row">
                                    <div class="col-lg-12">

                                        <label class="docconlabels">Brief Description of Duties:</label>

                                        <asp:TextBox ID="tbBriefDescriptionofDuties" runat="server" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>


                                        <telerik:RadGrid RenderMode="Lightweight" ID="gridBriefDesc" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                            AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                            AutoGenerateColumns="False" OnItemDataBound="gridBriefDesc_ItemDataBound"
                                            OnItemUpdated="gridBriefDesc_ItemUpdated" Skin="Metro" ShowHeader="false"
                                            OnItemDeleted="gridBriefDesc_ItemDeleted"
                                            OnItemInserted="gridBriefDesc_ItemInserted"
                                            DataSourceID="SqlDSBriefDescOfDuties">

                                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID"
                                                HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                <BatchEditingSettings EditType="Cell" OpenEditingEvent="Click" />

                                                <CommandItemSettings ShowCancelChangesButton="false" />

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

                                                    <telerik:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                        ConfirmTitle="Delete" HeaderStyle-ForeColor="White"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                        ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="50px" ItemStyle-Width="50px"
                                                        CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                                                    </telerik:GridButtonColumn>
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

                                        <asp:TextBox ID="tbSpecialSkills_QualificationsRequired" runat="server" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                        <telerik:RadGrid RenderMode="Lightweight" ID="gridSpecialSkills_QualificationsRequired" GridLines="None" runat="server" 
                                            AllowAutomaticDeletes="True"
                                            AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                            AutoGenerateColumns="False" OnItemDataBound="gridSpecialSkills_QualificationsRequired_ItemDataBound"
                                            OnItemUpdated="gridSpecialSkills_QualificationsRequired_ItemUpdated" Skin="Metro" ShowHeader="false"
                                            OnItemDeleted="gridSpecialSkills_QualificationsRequired_ItemDeleted"
                                            OnItemInserted="gridSpecialSkills_QualificationsRequired_ItemInserted"
                                            DataSourceID="SqlDSSpecialSkills_QualificationsReq">

                                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID"
                                                HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                <BatchEditingSettings EditType="Cell" OpenEditingEvent="Click" />

                                                <CommandItemSettings ShowCancelChangesButton="false" />

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

                                                    <telerik:GridBoundColumn DataField="Skills_Qualifications" HeaderStyle-Width="100%" HeaderStyle-ForeColor="White"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                        ItemStyle-CssClass="wrapword" ItemStyle-HorizontalAlign="Left" HeaderText="Skills_Qualifications"
                                                        SortExpression="Skills_Qualifications" UniqueName="Skills_Qualifications" HeaderStyle-Font-Size="15">

                                                        <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                            <RequiredFieldValidator ForeColor="Red" Text="*This field is required" Display="Dynamic">
                                                            </RequiredFieldValidator>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                        ConfirmTitle="Delete" HeaderStyle-ForeColor="White"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                        ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="50px" ItemStyle-Width="50px"
                                                        CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                                                    </telerik:GridButtonColumn>
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

                                        <asp:TextBox ID="tbEducationRequired" runat="server" placeholder="Enter Text Here..." Visible="false" CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                        <telerik:RadGrid RenderMode="Lightweight" ID="gridEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                            AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                            AutoGenerateColumns="False" OnItemDataBound="gridEducationRequired_ItemDataBound"
                                            OnItemUpdated="gridEducationRequired_ItemUpdated" Skin="Metro" ShowHeader="false"
                                            OnItemDeleted="gridEducationRequired_ItemDeleted"
                                            OnItemInserted="gridEducationRequired_ItemInserted"
                                            DataSourceID="SQLDSEducationRequired">

                                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID"
                                                HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                <BatchEditingSettings EditType="Cell" OpenEditingEvent="Click" />

                                                <CommandItemSettings ShowCancelChangesButton="false" />

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

                                                    <telerik:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                        ConfirmTitle="Delete" HeaderStyle-ForeColor="White"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                        ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="50px" ItemStyle-Width="50px"
                                                        CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>

                                            <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>

                                        </telerik:RadGrid>

                                    </div>

                                </div>

                            </div>

                        </div>
                        <!-- end of collapse 3 -->


                        <!-- collapse 3 -->
                        <div class="box no-border">
                            <div class="box-header with-border bg-gray-light">
                                <h4 class="box-title">
                                    <b>EMPLOYMENT/WORK STATUS</b>
                                </h4>

                            </div>
                            <div class="box-body">
                                <div class="row" style="margin-top: 7px;">
                                    <div class="col-lg-12">
                                        <telerik:RadRadioButtonList runat="server" ID="rdolstWorkStat"
                                            Direction="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdolstWorkStat_SelectedIndexChanged">
                                            <DataBindings DataValueField="StatusCode" DataTextField="StatusDesc" />
                                        </telerik:RadRadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- collapse 3 -->
                        <div class="box no-border">
                            <div class="box-header with-border bg-gray-light">
                                <h4 class="box-title">
                                    <b>JUSTIFICATION and HISTORY</b>
                                </h4>

                            </div>
                            <div class="box-body">

                                <div class="row">

                                    <div class="col-lg-4">

                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="radcboJustification"
                                            MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px"
                                            HighlightTemplatedItems="true" DataSourceID="SqlDSGetJustification"
                                            DataTextField="JustificationDesc" DataValueField="JustificationCode"
                                            AutoPostBack="true" CssClass="RadComboBoxDropDown"
                                            EmptyMessage="Select Justification" Filter="Contains"
                                            OnSelectedIndexChanged="radcboJustification_SelectedIndexChanged">
                                        </telerik:RadComboBox>

                                    </div>

                                    <div class="col-lg-8">

                                        <asp:TextBox ID="tbHistory" runat="server" placeholder="Enter Text Here..." Enabled="false" CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- end of collapse 3 -->

                        <!-- collapse 4 -->
                        <div class="box no-border">
                            <div class="box-header with-border bg-gray-light">
                                <h4 class="box-title">
                                    <b>ATTACHMENTS</b>

                                </h4>
                            </div>

                            <div class="box-body" style="border-color: white; border-bottom-color: red;">

                                <div class="bs-callout bs-callout-info docconlabels">

                                    <b>Note:</b>

                                    <p>
                                        1.) Attachment Needed : Manpower Simulation, Job Description.
                                    </p>

                                    <p>
                                        2.) <b>NO ATTACHMENT, NO PROCESSING POLICY</b>
                                    </p>

                                </div>


                                <div class="row" id="divExisting" runat="server" visible="true">

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

                                        <%--<div class="row">
                                            <div class="col-lg-1" style="margin-top: 7px">
                                                <b>Select File:</b>
                                            </div>

                                            <div class="col-lg-4">

                                                <telerik:RadAsyncUpload ID="RadAsyncUpload1" Skin="Metro"
                                                    Style="padding-top: 8px" MultipleFileSelection="Disabled" runat="server" Width="100%">
                                                </telerik:RadAsyncUpload>

                                            </div>

                                        </div>--%>

                                        <%--<div class="row">
                                            <div class="col-lg-1" style="margin-top: 6px">
                                            </div>
                                            <div class="col-lg-4">
                                                <div class="row">
                                                    <div class="col-lg-8">
                                                        <asp:LinkButton ID="linkUpload" runat="server" Style="font-size: 10px;"
                                                            class="btn btn-primary btn-flat" OnClientClick="return confirm('Do you want to proceed?')"
                                                            OnClick="UploadFile"><i class="glyphicon glyphicon-upload"></i> Upload</asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>--%>

                                    </div>

                                </div>

                                <br />

                                <div class="row">

                                    <div class="col-lg-12">

                                        <asp:GridView ID="gridAttachment" runat="server" Width="100%" AutoGenerateColumns="false" ShowFooter="false" EmptyDataText="No Records Found">

                                            <Columns>

                                                <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="90%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                    <ItemTemplate>

                                                        <%# Eval("attachmentname") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="items2" HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-Width="25px" HeaderStyle-Width="25px">

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                            CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" />

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
                        <!-- end of collapse 4 -->


                        <div class="row" runat="server" id="rowIfManager">
                            <div class="col-lg-12 aligncenter">
                                <%--<asp:Button ID="btnSubmitIfManager" runat="server" class="btn btn-success btn-flat"
                                            Text="Submit Application" Height="38px"
                                            OnClientClick="return confirm('By clicking Yes/OK will submit the document. Do you want to proceed?')"
                                            OnClick="btnSubmit_Click" />--%>

                                <telerik:RadButton ID="btnSubmitIfManager" runat="server"
                                    Text="Submit Application" SingleClick="true" Skin="Material"
                                    Primary="true" Height="38px" RenderMode="Lightweight"
                                    OnClientClick="return confirm('By clicking Yes/OK will submit the request. Do you want to proceed?')"
                                    SingleClickText="Submitting..." OnClick="btnSubmit_Click">
                                </telerik:RadButton>
                            </div>
                        </div>


                        <div class="box no-border" runat="server" id="rowIfSupervisor" visible="false">

                            <div class="box-header with-border bg-gray-light">
                                <h4 class="box-title">

                                    <b>APPROVER</b>

                                </h4>
                            </div>

                            <div class="box-body" style="border-color: white; border-bottom-color: red;">

                                <div class="row">

                                    <div class="col-lg-4">

                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="RadcboApprover"
                                            MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px"
                                            HighlightTemplatedItems="true" OnDataBound="RadcboApprover_DataBound"
                                            OnClientItemsRequested="UpdateItemCountField" OnItemDataBound="RadcboApprover_ItemDataBound"
                                            AutoPostBack="true" CssClass="RadComboBoxDropDown" EmptyMessage="Select Approver"
                                            Filter="Contains" OnSelectedIndexChanged="RadcboApprover_SelectedIndexChanged">
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

                                    </div>

                                    <div class="col-lg-8">

                                        <%--<asp:Button ID="btnSubmit" runat="server" class="btn btn-success btn-flat" 
                                                    Text="Submit Application" Height="38px"
                                                    OnClientClick="return confirm('By clicking Yes/OK will submit the document. Do you want to proceed?')"
                                                    OnClick="btnSubmit_Click" />--%>
                                        <%-- CssClass="btn btn-success btn-flat"--%>

                                        <%--<telerik:RadButton ID="btnSubmit" runat="server"
                                            Text="Submit Application" SingleClick="true"
                                            Height="38px" Primary="true" RenderMode="Lightweight" Skin="Material"
                                            OnClientClick="return confirm('By clicking Yes/OK will submit the request. Do you want to proceed?')"
                                            SingleClickText="Submitting..." OnClick="btnSubmit_Click">
                                        </telerik:RadButton>--%>

                                        <telerik:RadButton ID="btnSubmit" runat="server"
                                            Text="Submit Application" SingleClick="true"
                                            Primary="true" RenderMode="Lightweight" Enabled="true"
                                            Style="width: 150px; height: 38px;"
                                            SingleClickText="Submitting..." OnClick="btnSubmit_Click">

                                            <ConfirmSettings ConfirmText="By clicking Yes/OK will submit the request. Do you want to proceed?" Title="Confirm Action" />
                                        </telerik:RadButton>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

                <div class="box no-border" runat="server" id="divSuccess" visible="false">

                    <div class="box-body">

                        <div class="row">

                            <div class="col-lg-12 aligncenter">

                                <img src="../images/thumb-up-smiley.png" style="width: 250px; height: 250px;" />

                                <br />

                                <h2><b>Request Successfully Sent</b></h2>

                            </div>

                        </div>

                    </div>

                    <div class="box-footer">

                        <div class="row">

                            <div class="col-lg-5"></div>

                            <div class="col-lg-2">
                                <%--<asp:Button ID="btnGoToMain" runat="server" class="btn btn-primary btn-flat bg-blue-gradient"
                                             Width="100%" Height="40px"
                                            Text="Go To Main" OnClick="btnGoToMain_Click" />--%>
                                <telerik:RadButton ID="btnGoToMain" runat="server"
                                    Text="Go To Main" SingleClick="true"
                                    Width="100%" Height="40px" Primary="true" RenderMode="Lightweight"
                                    SingleClickText="Loading..." OnClick="btnGoToMain_Click">
                                </telerik:RadButton>

                            </div>

                            <div class="col-lg-5"></div>

                        </div>

                    </div>

                </div>

                <%--</ContentTemplate>

                    <Triggers>
                        <asp:PostBackTrigger ControlID="linkUpload" />
                        <asp:PostBackTrigger ControlID="gridAttachment" />
                        
                    </Triggers>

                </asp:UpdatePanel>--%>
            </section>

        </div>

    </div>


    <asp:SqlDataSource ID="SqlDSGetJustification" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="GetJustification" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSGetAllDepartment" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="GetAllDepartment" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSSKPI_GetSection" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="SKPI_GetSection" SelectCommandType="StoredProcedure">

        <SelectParameters>
            <asp:ControlParameter Name="deptcode" ControlID="radcboDepartment" PropertyName="SelectedValue" />
        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSGetPosition" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="GetPosition" SelectCommandType="StoredProcedure"></asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDSBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text"
        DeleteCommand="Delete from tblBriefDescOfDuties where ID=@ID" DeleteCommandType="Text"
        UpdateCommand="Update tblBriefDescOfDuties set BriefDesc=@BriefDesc WHERE ID=@ID" UpdateCommandType="Text"
        InsertCommand="IF NOT EXISTS(SELECT * FROM tblBriefDescOfDuties WHERE BriefDesc = @BriefDesc AND PositionDesc = @PositionDesc) BEGIN INSERT INTO tblBriefDescOfDuties(BriefDesc,PositionDesc)VALUES(@BriefDesc,@PositionDesc) END" InsertCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="radcboPosition" Name="PositionDesc" PropertyName="SelectedValue" DbType="String" />

        </SelectParameters>

        <InsertParameters>

            <asp:Parameter Name="BriefDesc" Type="String"></asp:Parameter>

            <asp:ControlParameter ControlID="radcboPosition" Name="PositionDesc" PropertyName="SelectedValue" DbType="String" />

        </InsertParameters>

        <UpdateParameters>

            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>

            <asp:Parameter Name="BriefDesc" Type="String"></asp:Parameter>

        </UpdateParameters>

        <DeleteParameters>

            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>

        </DeleteParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text"
        DeleteCommand="Delete from tblSpecialSkills_QualificationsReq where ID=@ID" DeleteCommandType="Text"
        UpdateCommand="Update tblSpecialSkills_QualificationsReq set Skills_Qualifications=@Skills_Qualifications WHERE ID=@ID" UpdateCommandType="Text"
        InsertCommand="IF NOT EXISTS(SELECT * FROM tblSpecialSkills_QualificationsReq WHERE Skills_Qualifications = @Skills_Qualifications AND PositionDesc = @PositionDesc) 
                           BEGIN 
                            INSERT INTO tblSpecialSkills_QualificationsReq(Skills_Qualifications,PositionDesc)VALUES(@Skills_Qualifications,@PositionDesc) 
                           END"
        InsertCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="radcboPosition" Name="PositionDesc" PropertyName="SelectedValue" DbType="String" />

        </SelectParameters>

        <InsertParameters>

            <asp:Parameter Name="Skills_Qualifications" Type="String"></asp:Parameter>

            <asp:ControlParameter ControlID="radcboPosition" Name="PositionDesc" PropertyName="SelectedValue" DbType="String" />

        </InsertParameters>

        <UpdateParameters>

            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>

            <asp:Parameter Name="BriefDesc" Type="String"></asp:Parameter>

        </UpdateParameters>

        <DeleteParameters>

            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>

        </DeleteParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text"
        DeleteCommand="Delete from tblEducationRequired where ID=@ID" DeleteCommandType="Text"
        UpdateCommand="Update tblEducationRequired set EducationRequired=@EducationRequired WHERE ID=@ID" UpdateCommandType="Text"
        InsertCommand="IF NOT EXISTS(SELECT * FROM tblEducationRequired WHERE EducationRequired = @EducationRequired AND PositionDesc = @PositionDesc) 
                           BEGIN 
                            INSERT INTO tblEducationRequired(EducationRequired,PositionDesc)VALUES(@EducationRequired,@PositionDesc) 
                           END"
        InsertCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="radcboPosition" Name="PositionDesc" PropertyName="SelectedValue" DbType="String" />

        </SelectParameters>

        <InsertParameters>

            <asp:Parameter Name="EducationRequired" Type="String"></asp:Parameter>

            <asp:ControlParameter ControlID="radcboPosition" Name="PositionDesc" PropertyName="SelectedValue" DbType="String" />

        </InsertParameters>

        <UpdateParameters>

            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>

            <asp:Parameter Name="EducationRequired" Type="String"></asp:Parameter>

        </UpdateParameters>

        <DeleteParameters>

            <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>

        </DeleteParameters>

    </asp:SqlDataSource>



    <script type="text/javascript">
        function RedirectPageToMSN(arg) {
            window.location.href = '<%=ConfigurationManager.AppSettings["CheckerPage"] %>';
        }

    </script>

</asp:Content>
