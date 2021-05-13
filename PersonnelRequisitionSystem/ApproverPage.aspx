<%@ Page Title="" Language="C#" MasterPageFile="~/MasterApproverPage.Master" AutoEventWireup="true" CodeBehind="ApproverPage.aspx.cs" Inherits="PersonnelRequisitionSystem.ApproverPage" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterApproverPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Details and Status--%>
    <div class="content-wrapper" runat="server" id="divDetailsandStatus" visible="true">

        <div class="container">

            <section class="content">

                <div class="box-header bg-gray-light">

                    <h2 class=" pull-right" style="margin-top: 28px; margin-right: 2%;">

                        <b>PERSONNEL REQUISITION FORM</b>

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

                        <asp:TextBox ID="tbName" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                    </div>

                    <div class="col-lg-4">

                        <label class="docconlabels">Department:</label>

                        <asp:TextBox ID="tbDepartment" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                    </div>

                    <div class="col-lg-4">

                        <label class="docconlabels">Section:</label>

                        <asp:TextBox ID="tbSection" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-lg-4">

                        <label class="docconlabels">Position:</label>

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

                                <asp:TextBox ID="tbBriefDescriptionofDuties" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                            </div>

                        </div>

                        <br />

                        <div class="row">

                            <div class="col-lg-12">

                                <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                <asp:TextBox ID="tbSpecialSkills_QualificationsRequired" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                            </div>

                        </div>

                        <br />

                        <div class="row">
                            <div class="col-lg-12">

                                <label class="docconlabels">Education Required:</label>

                                <asp:TextBox ID="tbEducationRequired" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                            </div>
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

                                                <%# Eval("attachmentname") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="items2" HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-Width="25px" HeaderStyle-Width="25px">

                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                    CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" />

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

                <hr />

                <div class="row">
                    <div class="col-lg-12 alignright">

                        <button type="button" id="btnShowReject" runat="server" class="btn btn-danger btn-flat"
                            style="width: 200px; height: 50px; margin-left: 10px; margin-right: 10px;"
                            data-toggle="modal" data-target="#modalReject">
                            Reject Document</button>

                        <asp:Button ID="btnApproved" runat="server" class="btn btn-success btn-flat" Text="Sign & Approve: No Remarks"
                            Style="height: 50px; margin-left: 10px; margin-right: 10px;"
                            OnClientClick="return confirm('By clicking Yes/OK will submit the document. Do you want to proceed?')" />

                        <button type="button" id="Button2" runat="server" class="btn btn-success btn-flat"
                            style="height: 50px; margin-left: 10px; margin-right: 10px;"
                            data-toggle="modal" data-target="#modalApproveWithRemarks">
                            Sign & Approve: With Remarks</button>

                        <button type="button" id="Button1" visible="false" runat="server" class="btn btn-success btn-flat"
                            style="height: 50px; margin-left: 10px; margin-right: 10px;"
                            data-toggle="modal" data-target="#modalPendingRemarks">
                            Set As Pending</button>
                    </div>
                </div>


            </section>
        </div>
    </div>


    <div class="box no-border" runat="server" id="divSuccess" visible="false">

        <div class="box-body">

            <div class="row">

                <div class="col-lg-12 aligncenter">

                    <img src="../images/thumb-up-smiley.png" style="width: 250px; height: 250px;" />

                    <br />

                    <h2><b>Reviewed</b></h2>

                </div>

            </div>

        </div>

        <div class="box-footer hide">

            <div class="row">

                <div class="col-lg-5"></div>

                <div class="col-lg-2">
                    <asp:Button ID="btnGoToMain" runat="server" class="btn btn-primary btn-flat bg-blue-gradient"
                        Width="100%" Height="40px" Text="View Details" />
                </div>

                <div class="col-lg-5"></div>

            </div>

        </div>

    </div>

</asp:Content>
