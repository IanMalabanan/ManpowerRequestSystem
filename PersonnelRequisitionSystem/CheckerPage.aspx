<%@ Page Title="Personnel Requisition System" Language="C#" MasterPageFile="~/MasterApproverPage.Master" AutoEventWireup="true" CodeBehind="CheckerPage.aspx.cs" Inherits="PersonnelRequisitionSystem.CheckerPage" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<%@ MasterType VirtualPath="~/MasterApproverPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
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

    <%--Details and Status--%>
    <div class="content-wrapper" style="margin-top: 50px;">

        <div class="container">

            <section class="content">

                <div class="row" runat="server" id="rowDetails" visible="true">

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

                        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip2"
                            MultiPageID="RadMultiPage2" SelectedIndex="0">

                            <Tabs>

                                <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                                <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                            </Tabs>

                        </telerik:RadTabStrip>

                        <telerik:RadMultiPage runat="server" ID="RadMultiPage2" SelectedIndex="0">

                            <telerik:RadPageView runat="server" ID="RadPageView1">

                                <div class="row" style="margin-top: 10px">
                                    <div class="col-lg-4">
                                        <label class="docconlabels">Control No:</label>

                                        <asp:TextBox ID="tbControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-4">
                                        <label class="docconlabels">Date Filed:</label>

                                        <asp:TextBox ID="tbDateFiled" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>
                                </div>

                                <br />

                                <div class="row">
                                    <div class="col-lg-4">

                                        <label class="docconlabels">Requestor Name:</label>

                                        <asp:TextBox ID="tbName" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-4">

                                        <label class="docconlabels">Requesting Department:</label>

                                        <asp:TextBox ID="tbDepartment" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-4">

                                        <label class="docconlabels">Requesting Section:</label>

                                        <asp:TextBox ID="tbSection" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>
                                </div>

                                <br />

                                <div class="row">
                                    <div class="col-lg-4">

                                        <label class="docconlabels">Requesting Position:</label>

                                        <asp:TextBox ID="tbPosition" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>

                                    <div class="col-lg-4">

                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label class="docconlabels">Male:</label>

                                                <asp:TextBox ID="tbMaleCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                            </div>
                                            <div class="col-lg-4">
                                                <label class="docconlabels">Female:</label>

                                                <asp:TextBox ID="tbFemaleCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                            </div>

                                            <div class="col-lg-4">
                                                <label class="docconlabels">Total:</label>

                                                <asp:TextBox ID="tbTotalCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                            </div>
                                        </div>



                                    </div>

                                    <div class="col-lg-4">

                                        <label class="docconlabels">Date Needed:</label>

                                        <asp:TextBox ID="tbDateNeeded" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                    </div>
                                </div>

                                <br />

                                <!-- collapse 3 -->
                                <div class="box no-border">
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

                                                <asp:TextBox ID="tbBriefDescriptionofDuties" runat="server" Visible="false" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

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

                                                <asp:TextBox ID="tbSpecialSkills_QualificationsRequired" runat="server" Visible="false" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

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

                                                <asp:TextBox ID="tbEducationRequired" runat="server" ReadOnly="true" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                <telerik:RadGrid RenderMode="Lightweight" ID="gridEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                    AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                    AutoGenerateColumns="False" OnItemDataBound="gridEducationRequired_ItemDataBound"
                                                    Skin="Metro" ShowHeader="false"
                                                    DataSourceID="SQLDSEducationRequired">

                                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                        <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                        <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowSaveChangesButton="false"/>

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
                                <!-- end of collapse 3 -->

                                <!-- collapse 3 -->
                                <div class="box no-border">
                                    <div class="box-header with-border bg-gray-light">
                                        <h4 class="box-title">
                                            <b>EMPLOYMENT/WORK STATUS</b>
                                        </h4>

                                    </div>
                                    <div class="box-body">

                                        <div class="col-lg-12">

                                            <asp:TextBox ID="tbWorkStatus" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                TextMode="SingleLine"></asp:TextBox>


                                        </div>

                                    </div>

                                </div>
                                <!-- end of collapse 3 -->

                                <!-- collapse 3 -->
                                <div class="box no-border">
                                    <div class="box-header with-border bg-gray-light">
                                        <h4 class="box-title">
                                            <b>JUSTIFICATION and HISTORY</b>
                                        </h4>

                                    </div>
                                    <div class="box-body">

                                        <div class="col-lg-4">

                                            <asp:TextBox ID="tbJustification" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                TextMode="SingleLine"></asp:TextBox>


                                        </div>

                                        <div class="col-lg-8">

                                            <asp:TextBox ID="tbHistory" runat="server" ReadOnly="true" placeholder="Enter Text Here..."
                                                Enabled="false" CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

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


                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:GridView ID="gridAttachment" runat="server" Width="100%" AutoGenerateColumns="false" ShowFooter="false" EmptyDataText="No Records Found">

                                                    <Columns>

                                                        <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="90%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                            <ItemTemplate>

                                                                <%# Eval("AttachmentName") %>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-CssClass="items2" HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-Width="25px" HeaderStyle-Width="25px">

                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                                    CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" />

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


                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageView2">

                                <div class="row" style="margin-top: 10px">
                                    <div class="col-lg-12">

                                        <%=GetCommentThreads()%>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>

                        <hr />

                        <div class="row" runat="server" visible="false" id="rowCommandButtonsOnly">

                            <div class="col-lg-12 aligncenter">
                                <button type="button" id="btnShowReject" runat="server" class="btn btn-danger"
                                    style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    data-toggle="modal" data-target="#modalReject">
                                    Reject Request</button>

                                <button type="button" id="btnShowHold" runat="server" class="btn btn-warning "
                                    style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    data-toggle="modal" data-target="#modalPendingRemarks">
                                    Hold and Leave Message</button>

                                <%--<asp:Button ID="btnApproved" runat="server" class="btn btn-success btn-flat" Text="Sign & Approve"
                                    Style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    OnClientClick="return confirm('By clicking Yes/OK will submit the request. Do you want to proceed?')"
                                    OnClick="btnApprove_Click" />--%>


                                <telerik:RadButton ID="btnApproved" runat="server"
                                    Text="Sign & Approve" SingleClick="true"
                                    Primary="true" RenderMode="Lightweight"
                                    Style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    OnClientClick="return confirm('By clicking Yes/OK will submit the Request. Do you want to proceed?')"
                                    SingleClickText="Processing..." OnClick="btnApprove_Click">
                                </telerik:RadButton>

                            </div>


                        </div>

                        <div class="row" runat="server" id="rowShowApproversForHRManager" visible="false">

                            <div class="col-lg-4">

                                <label class="docconlabels">Select Approver:</label>

                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="RadcboAppover"
                                    MarkFirstMatch="true" EnableLoadOnDemand="true" Width="100%" Height="400px"
                                    HighlightTemplatedItems="true" OnClientItemsRequested="UpdateItemCountField"
                                    OnSelectedIndexChanged="RadcboAppover_SelectedIndexChanged" Filter="Contains"
                                    OnDataBound="RadcboAppover_DataBound" OnItemDataBound="RadcboAppover_ItemDataBound"
                                    AutoPostBack="true" CssClass="RadComboBoxDropDown" EmptyMessage="Select Approver">

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

                            <div class="col-lg-8 alignleft" style="margin-top: 28px;">

                                <button type="button" id="Button1" runat="server" class="btn btn-danger btn-flat"
                                    style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    data-toggle="modal" data-target="#modalReject">
                                    Reject Request</button>

                                <button type="button" id="Button2" runat="server" class="btn btn-warning btn-flat" visible="false"
                                    style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    data-toggle="modal" data-target="#modalPendingRemarks">
                                    Hold and Leave Message</button>

                                <%--<asp:Button ID="Button3" runat="server" class="btn btn-success btn-flat" Text="Sign & Approve"
                                    Style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;" Visible="false"
                                    OnClientClick="return confirm('By clicking Yes/OK will submit the Request. Do you want to proceed?')"
                                    OnClick="btnApprove_Click" />--%>

                                <%-- <telerik:RadButton ID="Button3" runat="server"
                                    Text="Sign & Approve" SingleClick="true"
                                    Visible="false" Primary="true" RenderMode="Lightweight"
                                    Style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    OnClientClick="return confirm('By clicking Yes/OK will submit the Request. Do you want to proceed?')"
                                    SingleClickText="Processing..." OnClick="btnApprove_Click">
                                </telerik:RadButton>--%>


                                <telerik:RadButton ID="Button3" runat="server"
                                    Text="Sign & Approve" SingleClick="true" Visible="false"
                                    Primary="true" RenderMode="Lightweight" Enabled="true"
                                    Style="width: 200px; height: 38px; margin-left: 10px; margin-right: 10px;"
                                    SingleClickText="Processing..." OnClick="btnApprove_Click">

                                    <ConfirmSettings ConfirmText="Are you sure you want to submit the request?" Title="Confirm Action" />
                                </telerik:RadButton>


                            </div>

                        </div>


                        <br />

                        <div class="box no-border hide">
                            <div class="box-header with-border bg-gray-light bg-blue">
                                <h4 class="box-title">
                                    <b>COMMUNICATION THREAD</b>

                                </h4>
                            </div>

                            <div class="box-body" style="border-color: white; border-bottom-color: red;">
                            </div>
                        </div>

                        <div class="modal fade" id="modalPendingRemarks">

                            <div class="modal-dialog modal-lg">

                                <div class="modal-content" style="width: 100%;">

                                    <div class="modal-header bg-blue">

                                        <h4 class="modal-title"><b>SEND MESSAGE TO REQUESTOR</b></h4>
                                    </div>

                                    <div class="modal-body">
                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:TextBox runat="server" ID="tbPendingRemarks" TextMode="MultiLine" placeholder="Type Message Here..."
                                                    Height="150px" Width="100%"></asp:TextBox>

                                            </div>

                                        </div>

                                    </div>

                                    <div class="modal-footer">

                                        <%--<asp:Button runat="server" ID="btnSendPending" 
                                            CssClass="btn btn-primary bg-blue-gradient btn-flat pull-right" 
                                            Text="Send" Width="100px" OnClientClick="return confirm('Do you want to proceed?')" 
                                            OnClick="btnHold_Click" />--%>

                                        <%--<telerik:RadButton ID="btnSendPending" runat="server"
                                            Text="Send" SingleClick="true" Width="100px" Style="height: 35px;"
                                            CssClass="pull-right" Primary="true" RenderMode="Lightweight"
                                            OnClientClick="return confirm('Do you want to proceed?')"
                                            SingleClickText="Sending..." OnClick="btnHold_Click">
                                        </telerik:RadButton>--%>

                                        <telerik:RadButton ID="btnSendPending" runat="server"
                                            Text="Send" SingleClick="true"
                                            Primary="true" RenderMode="Lightweight" Enabled="true"
                                            Style="width: 100%; height: 36px; margin-left: 10px; margin-right: 10px; border: none"
                                            SingleClickText="Sending..." OnClick="btnHold_Click">
                                            <ConfirmSettings ConfirmText="Are you sure you want to continue?" Title="Confirm Action" />
                                        </telerik:RadButton>


                                        <button type="button" class="btn btn-default btn-flat pull-right" style="width: 100px; margin-right: 10px;" data-dismiss="modal">Close</button>


                                    </div>

                                </div>

                            </div>

                        </div>

                        <div class="modal fade" id="modalReject">

                            <div class="modal-dialog modal-lg">

                                <div class="modal-content" style="width: 100%;">

                                    <div class="modal-header bg-blue-gradient">

                                        <h4 class="modal-title">Reason Of Rejection</h4>
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

                                                        <%--<asp:Button runat="server" ID="btnReject" CssClass="btn btn-danger" Text="Reject"
                                                            OnClientClick="return confirm('This process may reject the Request. Do you want to proceed?')"
                                                            Width="100%" OnClick="btnReject_Click" />--%>

                                                        <telerik:RadButton ID="btnReject" runat="server" Style="height: 35px;"
                                                            Text="Reject" SingleClick="true" Width="100%" Primary="true" RenderMode="Lightweight"
                                                            OnClientClick="return confirm('This process may reject the request. Do you want to proceed?')"
                                                            SingleClickText="Processing..." OnClick="btnReject_Click">
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

                    </div>

                </div>

                <div class="box no-border" runat="server" id="rowSuccess" visible="false">

                    <div class="box-body">

                        <div class="row">

                            <div class="col-lg-12 aligncenter">

                                <img src="../images/thumb-up-smiley.png" style="width: 250px; height: 250px;" />

                                <br />

                                <h2><b>Checked and Reviewed</b></h2>

                            </div>

                        </div>

                    </div>

                </div>

                <div class="box no-border" runat="server" id="rowRejected" visible="false">

                    <div class="box-body">

                        <div class="row">

                            <div class="col-lg-12 aligncenter">
                                <%--style="width: 550px; height: 250px;"--%>
                                <img src="../images/rejected.png" style="width: 650px; height: 250px;" />

                                <br />

                                <h2><b>This Application Has Been Rejected By The Approver</b></h2>

                            </div>

                        </div>

                    </div>

                </div>

            </section>

        </div>

    </div>

    <asp:SqlDataSource ID="SqlDSBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>


</asp:Content>
