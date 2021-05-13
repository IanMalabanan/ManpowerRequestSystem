<%@ Page Title="All Requests" Language="C#" MasterPageFile="~/HRMasterPage.Master" AutoEventWireup="true" CodeBehind="HRRequestsPage.aspx.cs"
    Inherits="PersonnelRequisitionSystem.HRRequestsPage" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>

    <div class="row" runat="server" id="rowRecords" visible="false">

        <div class="col-12">

            <div class="row" id="rowLists">
                <div class="col-lg-12">
                    <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip1"
                        MultiPageID="RadMultiPage1" SelectedIndex="0" Font-Size="Smaller" OnTabClick="RadTabStrip1_TabClick">

                        <Tabs>

                            <telerik:RadTab Text="For Approver Signatorial" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="For Receive" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="For Status Update" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="Finished Requests" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="Summary" CssClass="aligncenter"></telerik:RadTab>

                        </Tabs>

                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">

                        <telerik:RadPageView runat="server" ID="tabNewRequests">

                            <div class="card">

                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-lg-12">

                                            <telerik:RadGrid RenderMode="Lightweight" ID="gridNewRequests" AllowFilteringByColumn="True" Height="600px"
                                                AllowSorting="True" AllowPaging="True" PageSize="30" runat="server" AutoGenerateColumns="False"
                                                EnableLinqExpressions="false" OnItemDataBound="gridNewRequests_ItemDataBound"
                                                ShowStatusBar="true" ShowGroupPanel="false">

                                                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="true">

                                                    <Columns>

                                                        <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" AllowFiltering="false"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="85%"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkViewDetailsNew" runat="server" CommandArgument='<%# Eval("UniqueCode")%>' Width="100%"
                                                                    Text="View" CssClass="btn btn-primary btn-flat bg-blue-gradient" Style="color: white" OnClick="ViewDetailsNew"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" AllowFiltering="false"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                            ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="CounterColumn" HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="numberLabel" runat="server" />
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
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Department" HeaderText="Department" SortExpression="Dept_Desc"
                                                            UniqueName="Dept_Desc" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">


                                                            <FilterTemplate>
                                                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxTitle" DataSourceID="SqlDataSource1"
                                                                    DataTextField="Description"
                                                                    DataValueField="Description" Width="100%" AppendDataBoundItems="true" Skin="Metro"
                                                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Dept_Desc").CurrentFilterValue %>'
                                                                    runat="server" OnClientSelectedIndexChanged="CodeIndexChanged6">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="All" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                                    <script type="text/javascript">
                                                                        function CodeIndexChanged6(sender, args) {
                                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                            tableView.filter("Dept_Desc", args.get_item().get_value(), "EqualTo");
                                                                        }
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                            </FilterTemplate>

                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="MaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" AllowFiltering="false"
                                                            FilterControlAltText="Filter Male" HeaderText="Male" SortExpression="MaleCount"
                                                            UniqueName="MaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="FemaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" AllowFiltering="false"
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

                                </div>

                            </div>

                        </telerik:RadPageView>

                        <telerik:RadPageView runat="server" ID="tabForReceive">

                            <div class="card">

                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-lg-12">

                                            <telerik:RadGrid RenderMode="Lightweight" ID="gridForReceive" AllowFilteringByColumn="True" Height="600px"
                                                AllowSorting="True" AllowPaging="True" PageSize="30" runat="server" AutoGenerateColumns="False"
                                                EnableLinqExpressions="false" ShowStatusBar="true" ShowGroupPanel="false" OnItemDataBound="gridForReceive_ItemDataBound">

                                                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="true">

                                                    <Columns>

                                                        <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" AllowFiltering="false"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="85%"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkViewDetailsForReceive" runat="server" CommandArgument='<%# Eval("UniqueCode")%>' Width="100%"
                                                                    Text="View" CssClass="btn btn-primary btn-flat bg-blue-gradient" Style="color: white" OnClick="ViewDetailsForReceive"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" AllowFiltering="false"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                            ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="CounterColumn" HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="numberLabel" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ControlNo"
                                                            HeaderStyle-Width="150px" ItemStyle-Width="150px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Control No" HeaderText="Control No" SortExpression="ControlNo"
                                                            UniqueName="ControlNo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Dept_Desc"
                                                            HeaderStyle-Width="250px" ItemStyle-Width="250px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Department" HeaderText="Department" SortExpression="Dept_Desc"
                                                            UniqueName="Dept_Desc" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">

                                                            <FilterTemplate>
                                                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxds" DataSourceID="SqlDataSource1"
                                                                    DataTextField="Description"
                                                                    DataValueField="Description" Width="100%" AppendDataBoundItems="true" Skin="Metro"
                                                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Dept_Desc").CurrentFilterValue %>'
                                                                    runat="server" OnClientSelectedIndexChanged="CodeIndexChangedRec">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="All" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                                    <script type="text/javascript">
                                                                        function CodeIndexChangedRec(sender, args) {
                                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                            tableView.filter("Dept_Desc", args.get_item().get_value(), "EqualTo");
                                                                        }
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                            </FilterTemplate>
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="MaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Male" HeaderText="Male" SortExpression="MaleCount"
                                                            UniqueName="MaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="FemaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Female" HeaderText="Female" SortExpression="FemaleCount"
                                                            UniqueName="FemaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridDateTimeColumn DataField="DateNeeded" HeaderText="Date Needed" FilterDateFormat="dd/MMM/yyyy"
                                                            SortExpression="DateNeeded" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
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

                                </div>

                            </div>

                        </telerik:RadPageView>

                        <telerik:RadPageView runat="server" ID="tabForStatusUpdate">

                            <div class="card">

                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-12">

                                            <telerik:RadGrid RenderMode="Lightweight" ID="gridForStatusUpdate" AllowFilteringByColumn="True" Height="600px"
                                                AllowSorting="True" AllowPaging="True" PageSize="30" runat="server" AutoGenerateColumns="False"
                                                EnableLinqExpressions="false" ShowStatusBar="true" ShowGroupPanel="false" OnItemDataBound="gridForStatusUpdate_ItemDataBound">

                                                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="true">

                                                    <Columns>

                                                        <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" AllowFiltering="false"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                            ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="CounterColumn" HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="numberLabel" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" AllowFiltering="false"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="85%"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkViewDetailsForStatUpdate" runat="server" CommandArgument='<%# Eval("UniqueCode")%>' Width="100%"
                                                                    Text="View" CssClass="btn btn-primary btn-flat bg-blue-gradient" Style="color: white" OnClick="ViewDetailsForStatUpdate"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ControlNo"
                                                            HeaderStyle-Width="150px" ItemStyle-Width="150px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Control No" HeaderText="Control No" SortExpression="ControlNo"
                                                            UniqueName="ControlNo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Dept_Desc"
                                                            HeaderStyle-Width="250px" ItemStyle-Width="250px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Department" HeaderText="Department" SortExpression="Dept_Desc"
                                                            UniqueName="Dept_Desc" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">

                                                            <FilterTemplate>
                                                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxstat" DataSourceID="SqlDataSource1"
                                                                    DataTextField="Description"
                                                                    DataValueField="Description" Width="100%" AppendDataBoundItems="true" Skin="Metro"
                                                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Dept_Desc").CurrentFilterValue %>'
                                                                    runat="server" OnClientSelectedIndexChanged="CodeIndexChangedStat">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="All" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                                <telerik:RadScriptBlock ID="RadScriptBlock4" runat="server">
                                                                    <script type="text/javascript">
                                                                        function CodeIndexChangedStat(sender, args) {
                                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                                            tableView.filter("Dept_Desc", args.get_item().get_value(), "EqualTo");
                                                                        }
                                                                    </script>
                                                                </telerik:RadScriptBlock>
                                                            </FilterTemplate>


                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="MaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Male" HeaderText="Male" SortExpression="MaleCount" AllowFiltering="false"
                                                            UniqueName="MaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="FemaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Female" HeaderText="Female" SortExpression="FemaleCount" AllowFiltering="false"
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

                                </div>

                            </div>

                        </telerik:RadPageView>

                        <telerik:RadPageView runat="server" ID="tabFinishedRequests">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-lg-12">

                                            <telerik:RadGrid RenderMode="Lightweight" ID="gridFinishedRequests" AllowFilteringByColumn="True" Height="600px"
                                                AllowSorting="True" AllowPaging="True" PageSize="30" runat="server" AutoGenerateColumns="False"
                                                EnableLinqExpressions="false" ShowStatusBar="true" ShowGroupPanel="false" OnItemDataBound="gridFinishedRequests_ItemDataBound">

                                                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="true">

                                                    <Columns>

                                                        <telerik:GridTemplateColumn HeaderStyle-ForeColor="White" HeaderStyle-Width="50px" AllowFiltering="false"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers2 bg-blue-gradient"
                                                            ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" UniqueName="CounterColumn" HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="numberLabel" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn FilterCheckListEnableLoadOnDemand="true" AllowFiltering="false"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="85%"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("UniqueCode")%>' Width="100%"
                                                                    Text="View" CssClass="btn btn-primary btn-flat bg-blue-gradient" Style="color: white" OnClick="ViewRequestDetails"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="ControlNo"
                                                            HeaderStyle-Width="150px" ItemStyle-Width="150px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Control No" HeaderText="Control No" SortExpression="ControlNo"
                                                            UniqueName="ControlNo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="Dept_Desc"
                                                            HeaderStyle-Width="250px" ItemStyle-Width="250px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
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

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="MaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Male" HeaderText="Male" SortExpression="MaleCount" AllowFiltering="false"
                                                            UniqueName="MaleCount" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="FemaleCount"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Female" HeaderText="Female" SortExpression="FemaleCount" AllowFiltering="false"
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
                                </div>
                            </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView runat="server" ID="tabSummary">
                            <div class="card">
                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <table style="width: 100%">
                                                <tr style="height: 50px;">
                                                    <td style="padding: 10px; width: 140px;">
                                                        <h4 style="margin-top: 18px">Select Year:</h4>
                                                    </td>
                                                    <td style="padding: 1px; width: 400px">

                                                        <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="RadDDLYear"
                                                            Width="100%" Font-Size="18px" Skin="Metro" AutoPostBack="true">
                                                        </telerik:RadDropDownList>
                                                    </td>

                                                    <td></td>

                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="col-lg-12">

                                            <telerik:RadGrid RenderMode="Lightweight" ID="gridSummary" AllowFilteringByColumn="false" Height="600px"
                                                AllowSorting="True" AllowPaging="True" PageSize="30" runat="server" AutoGenerateColumns="False"
                                                EnableLinqExpressions="false" ShowStatusBar="true" ShowGroupPanel="false" DataSourceID="SqlDSDisplaySummaryOfRecords">

                                                <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="false">

                                                    <ColumnGroups>

                                                        <telerik:GridColumnGroup HeaderText="MALE" HeaderStyle-ForeColor="White"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers bg-blue-gradient" Name="colMale">
                                                        </telerik:GridColumnGroup>
                                                        <telerik:GridColumnGroup HeaderText="FEMALE" HeaderStyle-ForeColor="White"
                                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headers bg-blue-gradient" Name="colFemale">
                                                        </telerik:GridColumnGroup>
                                                    </ColumnGroups>

                                                    <Columns>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="DeptName"
                                                            HeaderStyle-Width="250px" ItemStyle-Width="250px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items"
                                                            FilterControlAltText="Filter Department" HeaderText="Department" SortExpression="DeptName"
                                                            UniqueName="DeptName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="TotalMaleNeeded"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Male" HeaderText="Needed" SortExpression="TotalMaleNeeded"
                                                            UniqueName="TotalMaleNeeded" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ColumnGroupName="colMale">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="TotalMaleEndorsed"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Female" HeaderText="Endorsed" SortExpression="TotalMaleEndorsed"
                                                            UniqueName="TotalMaleEndorsed" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ColumnGroupName="colMale">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="TotalMaleBalance"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Male" HeaderText="Balance" SortExpression="TotalMaleBalance"
                                                            UniqueName="TotalMaleBalance" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ColumnGroupName="colMale">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="TotalFemaleNeeded"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Female" HeaderText="Needed" SortExpression="TotalFemaleNeeded"
                                                            UniqueName="TotalFemaleNeeded" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ColumnGroupName="colFemale">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="TotalFemaleEndorsed"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Male" HeaderText="Endorsed" SortExpression="TotalFemaleEndorsed"
                                                            UniqueName="TotalFemaleEndorsed" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ColumnGroupName="colFemale">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" DataField="TotalFemaleBalance"
                                                            HeaderStyle-Width="100px" ItemStyle-Width="100px" FilterControlWidth="100%" ShowFilterIcon="false"
                                                            HeaderStyle-CssClass="headers bg-blue-gradient" ItemStyle-CssClass="items2" ItemStyle-HorizontalAlign="Center"
                                                            FilterControlAltText="Filter Female" HeaderText="Balance" SortExpression="TotalFemaleBalance"
                                                            UniqueName="TotalFemaleBalance" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                            ColumnGroupName="colFemale">
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
                                </div>
                            </div>
                        </telerik:RadPageView>

                    </telerik:RadMultiPage>
                </div>
            </div>

            <%--<div class="row" id="rowChangeApprover">
                <div class="col-lg-12">

                </div>
            </div>--%>
        </div>
    </div>

    <div class="row" runat="server" id="rowDetails" visible="true">

        <div class="col-12">

            <div class="card" runat="server" id="cardViewOngoingRequest" visible="false">
                <div class="card-header" style="background-color: rgb(11, 163, 217);">
                    <h3 class="card-title" style="color: white">Request Details</h3>
                    <div class="card-options">
                        <a runat="server" class="card-options-remove" style="color: white" onserverclick="CloseForm"><i class="fe fe-x"></i></a>
                    </div>
                </div>
                <div class="card-body">

                    <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip5"
                        MultiPageID="RadMultiPage5" SelectedIndex="0" Font-Size="Smaller">

                        <Tabs>

                            <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                        </Tabs>

                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage runat="server" ID="RadMultiPage5" SelectedIndex="0">

                        <telerik:RadPageView runat="server" ID="RadPageView7">

                            <div class="card">

                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-12">

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Prepared by</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblNewPreparedBy" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Noted by(Dept. Manager)</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblNewCheckedBy" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Noted by(HRGA. Manager)</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblNewNotedBy" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Noted by(GM/Factory Manager)</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblNewSecondNotedBy" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Approver - VP</b> :
                                                </div>

                                                <div class="col-9">
                                                    <label runat="server" id="lblNewApprovedBy" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Received by</b> :
                                                </div>

                                                <div class="col-9">
                                                    <label runat="server" style="font-weight: normal;">HRGA</label>
                                                </div>
                                            </div>


                                        </div>

                                    </div>

                                    <ul class="steps" runat="server" visible="false">

                                        <li id="liNewPreparedBy" runat="server"><a href="#">
                                            <label style="font-weight: normal;">1. Requesting</label><br />
                                            <label id="lblNewPreparedByStat" runat="server" style="font-weight: normal;"></label>
                                        </a></li>

                                        <li id="liNewCheckedBy" runat="server"><a href="#">
                                            <label style="font-weight: normal;">2. Note By Dept. Manager)</label><br />
                                            <label id="lblNewCheckedByStat" runat="server" style="font-weight: normal;"></label>
                                        </a></li>

                                        <li id="liNewNotedBy" runat="server"><a href="#">
                                            <label style="font-weight: normal;">3. Note By HRGA Manager</label><br />
                                            <label id="lblNewNotedByStat" runat="server" style="font-weight: normal;"></label>
                                        </a></li>

                                        <li id="liNewSecondNotedBy" runat="server"><a href="#">
                                            <label style="font-weight: normal;">4. Note By GM/Factory Manager</label><br />
                                            <label id="lblNewSecondNotedByStat" runat="server" style="font-weight: normal;"></label>
                                        </a></li>

                                        <li id="liNewApprovedBy" runat="server"><a href="#">
                                            <label style="font-weight: normal;">5. Approval - VP</label><br />
                                            <label id="lblNewApprovedByStat" runat="server" style="font-weight: normal;"></label>
                                        </a></li>

                                        <li id="liNewReceived" runat="server"><a href="#">
                                            <label style="font-weight: normal;">6. Received(HRGA)</label><br />
                                            <label id="lblNewReceivedStat" runat="server" style="font-weight: normal;"></label>
                                        </a></li>


                                    </ul>

                                    <br />

                                    <div class="row">

                                        <div class="col-4">
                                            <label class="docconlabels">Control No:</label>

                                            <asp:TextBox ID="tbNewControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">
                                            <label class="docconlabels">Date Filed:</label>

                                            <asp:TextBox ID="tbNewDateFiled" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-lg-4">

                                            <label class="docconlabels">Requestor Name:</label>

                                            <asp:TextBox ID="tbNewReqName" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-4">

                                            <label class="docconlabels">Department:</label>

                                            <asp:TextBox ID="tbNewDepartment" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-4">

                                            <label class="docconlabels">Section:</label>

                                            <asp:TextBox ID="tbNewSection" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-lg-4">
                                            <label class="docconlabels">Position:</label>

                                            <asp:TextBox ID="tbNewPosition" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">

                                            <div class="row">
                                                <div class="col-4">

                                                    <label class="docconlabels">Male:</label>

                                                    <asp:TextBox ID="tbNewMale" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Female:</label>

                                                    <asp:TextBox ID="tbNewFemale" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Total:</label>

                                                    <asp:TextBox ID="tbNewTotal" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-4">

                                            <label class="docconlabels">Date Needed:</label>

                                            <asp:TextBox ID="tbNewDateNeeded" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Job Description</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

                                                    <label class="docconlabels">Brief Description of Duties:</label>

                                                    <asp:TextBox ID="TextBox11" runat="server" ReadOnly="true" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridNewBriefDescOfDuties" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridNewBriefDescOfDuties_ItemDataBound"
                                                        DataSourceID="SqlDSNewBriefDescOfDuties" Skin="Metro" ShowHeader="false">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                                    <asp:TextBox ID="TextBox12" ReadOnly="true" Visible="false" runat="server" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridNewSpecialSkills_QualificationsReq" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridNewSpecialSkills_QualificationsReq_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" DataSourceID="SqlDSNewSpecialSkills_QualificationsReq">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Education Required:</label>

                                                    <asp:TextBox ID="TextBox13" runat="server" ReadOnly="true" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridNewEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridNewEducationRequired_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" DataSourceID="SQLDSNewEducationRequired">

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

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Employment/Work Status</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="tbNewWorkStatus" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                TextMode="SingleLine"></asp:TextBox>

                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Justification and History</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-lg-4">

                                            <asp:TextBox ID="tbNewJustification" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-8">

                                            <asp:TextBox ID="tbNewHistory" runat="server" placeholder="Enter Text Here..." Enabled="false" CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Attachment</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px;">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

                                                    <asp:GridView ID="gridNewAttachment" runat="server" Width="100%"
                                                        CssClass="table table-outline table-vcenter text-nowrap card-table"
                                                        AutoGenerateColumns="false" ShowFooter="false" EmptyDataText="No Records Found"
                                                        OnPreRender="gridNewAttachment_PreRender">

                                                        <Columns>

                                                            <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="95%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                                <ItemTemplate>

                                                                    <%# Eval("attachmentname") %>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20px">

                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                                        CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" Text="Download" Width="100%" />

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

                        <telerik:RadPageView runat="server" ID="RadPageView8">

                            <div class="card">

                                <div class="card-body">

                                    <%= NewCommentThreads(uc)%>
                                </div>

                            </div>

                        </telerik:RadPageView>

                    </telerik:RadMultiPage>

                </div>

            </div>

            <div class="card" runat="server" id="cardReceive" visible="false">
                <div class="card-header" style="background-color: rgb(11, 163, 217);">
                    <h3 class="card-title" style="color: white">For Receive</h3>
                    <div class="card-options">
                        <a runat="server" class="card-options-remove" style="color: white" onserverclick="CloseForm"><i class="fe fe-x"></i></a>
                    </div>
                </div>
                <div class="card-body">

                    <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip2"
                        MultiPageID="RadMultiPage2" SelectedIndex="0" Font-Size="Smaller">

                        <Tabs>

                            <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                        </Tabs>

                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage runat="server" ID="RadMultiPage2" SelectedIndex="0">

                        <telerik:RadPageView runat="server" ID="RadPageView1">

                            <div class="card">

                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-12">

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Prepared by</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblSignRequestor" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Noted by(Dept. Manager)</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblSignDeptManager" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Noted by(HRGA. Manager)</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblSignHRManager" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Noted by(GM/Factory Manager)</b> :
                                                </div>
                                                <div class="col-9">
                                                    <label id="lblSignGM_FactoryManager" runat="server" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Approver - VP</b> :
                                                </div>

                                                <div class="col-9">
                                                    <label runat="server" id="lblSignVP" style="font-weight: normal;"></label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3 alignright">
                                                    <b>Received by</b> :
                                                </div>

                                                <div class="col-9">
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

                                        <div class="col-4">
                                            <label class="docconlabels">Control No:</label>

                                            <asp:TextBox ID="tbRecControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">
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

                                        <div class="col-4">

                                            <div class="row">
                                                <div class="col-4">

                                                    <label class="docconlabels">Male:</label>

                                                    <asp:TextBox ID="tbRecMaleCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Female:</label>

                                                    <asp:TextBox ID="tbRecFemaleCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Total:</label>

                                                    <asp:TextBox ID="tbRecTotalCount" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-4">

                                            <label class="docconlabels">Date Needed:</label>

                                            <asp:TextBox ID="tbRecDateNeeded" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Job Description</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

                                                    <label class="docconlabels">Brief Description of Duties:</label>

                                                    <asp:TextBox ID="tbRecBriefDescriptionofDuties" runat="server" ReadOnly="true" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridRecBriefDesc" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridRecBriefDesc_ItemDataBound"
                                                        DataSourceID="SqlDSRecBriefDescOfDuties" Skin="Metro" ShowHeader="false">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                                    <asp:TextBox ID="tbRecSpecialSkills_QualificationsRequired" ReadOnly="true" Visible="false" runat="server" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridRecSpecialSkills_QualificationsRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridRecSpecialSkills_QualificationsRequired_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" DataSourceID="SqlDSRecSpecialSkills_QualificationsReq">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Education Required:</label>

                                                    <asp:TextBox ID="tbRecEducationRequired" runat="server" ReadOnly="true" Visible="false" placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridRecEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridRecEducationRequired_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" DataSourceID="SQLDSRecEducationRequired">

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

                                        <div class="col-12 bg-gray-dark-lightest">

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

                                        <div class="col-12 bg-gray-dark-lightest">

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

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Attachment</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px;">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

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
                                                                        CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" Text="Download" Width="100%" />

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

                        <telerik:RadPageView runat="server" ID="RadPageView2">

                            <div class="card">

                                <div class="card-body">

                                    <%= NewCommentThreads(uc)%>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <hr style="margin-top: 10px" />
                                            <div class="input-group" style="margin-top: -10px">
                                                <input runat="server" id="tbPendingHR" type="text" name="message" placeholder="Type Message ..." class="form-control" />
                                                <span class="input-group-btn">
                                                    <button type="button" class="btn btn-primary btn-flat" runat="server" onserverclick="btnRecHold_Click">Send</button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                        </telerik:RadPageView>

                    </telerik:RadMultiPage>

                </div>

                <div class="card-footer">

                    <div class="row">

                        <div class="col-12 aligncenter">

                            <asp:Button runat="server" ID="btnMarkasReceived" CssClass="btn btn-azure btn-flickr"
                                Text="Mark as Receive" OnClientClick="return confirm('Do you want to proceed?')"
                                OnClick="btnMarkasReceived_Click" />

                        </div>

                    </div>

                </div>

            </div>

            <div class="card" runat="server" id="cardForStatusUpdate" visible="false">

                <div class="card-header" style="background-color: rgb(11, 163, 217);">

                    <h3 class="card-title" style="color: white">For Status Update</h3>

                    <div class="card-options">
                        <a runat="server" class="card-options-remove" style="color: white" onserverclick="CloseForm"><i class="fe fe-x"></i></a>
                    </div>

                </div>

                <div class="card-body">

                    <div class="row">

                        <div class="col-12 bg-gray-dark-lightest">

                            <h4 style="margin-top: 10px; font-weight: 400">Application Status</h4>

                        </div>
                    </div>

                    <div class="row" runat="server" id="AlertOnHold" visible="false" style="margin-top: 5px;">

                        <div class="col-12">

                            <%=HoldAlert(Session["ApplicationRemarks"].ToString().Trim()) %>
                        </div>

                    </div>

                    <div class="row" style="margin-top: 10px">

                        <div class="col-6">

                            <div class="row">

                                <div class="col-3">

                                    <label class="docconlabels">Male Endorsed:</label>

                                    <asp:TextBox ID="tbStatMaleEndorsed" runat="server" ReadOnly="false"
                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                        TextMode="Number" AutoPostBack="true" OnTextChanged="Compute_TextChanged"></asp:TextBox>

                                </div>

                                <div class="col-3">

                                    <label class="docconlabels">Female Endorsed:</label>

                                    <asp:TextBox ID="tbStatFemaleEndorsed" runat="server" ReadOnly="false"
                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                        TextMode="Number" AutoPostBack="true" OnTextChanged="Compute_TextChanged"></asp:TextBox>

                                </div>

                                <div class="col-3">

                                    <label class="docconlabels">Male Balance:</label>

                                    <asp:TextBox ID="tbStatMaleBalance" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                </div>

                                <div class="col-3">

                                    <label class="docconlabels">Female Balance:</label>

                                    <asp:TextBox ID="tbStatFemaleBalance" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                </div>

                            </div>

                        </div>

                        <div class="col-6">
                            <div class="row">
                                <div class="col-4">

                                    <label class="docconlabels">Status:</label>

                                    <asp:DropDownList ID="ddlAppStatus" runat="server" CssClass="form-control" AutoPostBack="true" Height="39px" OnSelectedIndexChanged="ddlAppStatus_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-4" runat="server" id="colServedDate" visible="false">

                                    <label class="docconlabels">Served Date:</label>

                                    <telerik:RadDatePicker RenderMode="Lightweight" DateInput-ReadOnly="true" DateInput-DateFormat="dd/MMM/yyyy"
                                        ID="dpDateServed" Width="100%" Height="38px" runat="server">
                                    </telerik:RadDatePicker>

                                </div>
                                <div class="col-4" style="margin-top: 30px">
                                    <button type="button" id="btnShowCancel" runat="server" class="btn btn-danger btn-flat"
                                        style="width: 200px; height: 38px;"
                                        data-toggle="modal" data-target="#modalRejectRemarks" visible="false">
                                        Cancel Application</button>

                                    <button type="button" id="btnShowHold" runat="server" class="btn btn-warning btn-flat" visible="false"
                                        style="width: 200px; height: 38px;"
                                        data-toggle="modal" data-target="#modalPendingRemarks">
                                        Hold and Leave Message</button>

                                    <asp:Button ID="btnUpdate" runat="server" class="btn btn-primary btn-flat" Text="Update"
                                        Style="width: 200px; height: 38px;" Visible="false"
                                        OnClientClick="return confirm('Do you want to proceed?')" OnClick="btnUpdateStatus_Click" />
                                </div>

                            </div>

                        </div>

                    </div>

                    <div class="row" id="rowServedHistory" style="margin-top: 10px">

                        <div class="col-lg-12">

                            <asp:GridView ID="gridEndorsementLogs1" runat="server" Width="100%"
                                AutoGenerateColumns="false" HeaderStyle-BackColor="whitesmoke"
                                ShowFooter="false" AllowPaging="true" DataSourceID="SQLDSEndorsementLogs"
                                PageSize="10" PagerSettings-Mode="NumericFirstLast"
                                EmptyDataText="No Endorsement Records Found">

                                <Columns>

                                    <%--<asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Count">

                                        <ItemTemplate>

                                            <%# Eval("MaleCount") %>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Endorsed">

                                        <ItemTemplate>

                                            <%# Eval("MaleEndorsed") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Endorsed">

                                        <ItemTemplate>

                                            <%# Eval("FemaleEndorsed") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Balance">

                                        <ItemTemplate>

                                            <%# Eval("MaleBalance") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <%--<asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Count">

                                        <ItemTemplate>

                                            <%# Eval("FemaleCount") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>



                                    <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Balance">

                                        <ItemTemplate>

                                            <%# Eval("FemaleBalance") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Served Date">

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

                    <br />

                    <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip3"
                        MultiPageID="RadMultiPage3" SelectedIndex="0" Font-Size="Smaller">

                        <Tabs>

                            <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                        </Tabs>

                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage runat="server" ID="RadMultiPage3" SelectedIndex="0">

                        <telerik:RadPageView runat="server" ID="RadPageView3">
                            <div class="card">
                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-4">
                                            <label class="docconlabels">Control No:</label>

                                            <asp:TextBox ID="tbStatControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">
                                            <label class="docconlabels">Date Filed:</label>

                                            <asp:TextBox ID="tbStatDateFiled" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>
                                        <div class="col-4">
                                            <label class="docconlabels">Date Received:</label>

                                            <asp:TextBox ID="tbStatDateReceived" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-lg-4">

                                            <label class="docconlabels">Requestor Name:</label>

                                            <asp:TextBox ID="tbStatReqName" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-4">

                                            <label class="docconlabels">Department:</label>

                                            <asp:TextBox ID="tbStatDepartment" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-4">

                                            <label class="docconlabels">Section:</label>

                                            <asp:TextBox ID="tbStatSection" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-lg-4">
                                            <label class="docconlabels">Position:</label>

                                            <asp:TextBox ID="tbStatPosition" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">

                                            <div class="row">
                                                <div class="col-4">

                                                    <label class="docconlabels">Male:</label>

                                                    <asp:TextBox ID="tbStatMaleCount" runat="server" ReadOnly="true"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                        TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Female:</label>

                                                    <asp:TextBox ID="tbStatFemaleCount" runat="server" ReadOnly="true"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                        TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Total:</label>

                                                    <asp:TextBox ID="tbStatTotalCount" runat="server" ReadOnly="true"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-4">

                                            <label class="docconlabels">Date Needed:</label>

                                            <asp:TextBox ID="tbStatDateNeeded" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Job Description</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

                                                    <label class="docconlabels">Brief Description of Duties:</label>

                                                    <asp:TextBox ID="tbStatBriefDescOfDuties" runat="server" ReadOnly="true" Visible="false"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridStatBriefDesc" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridStatBriefDesc_ItemDataBound" MasterTableView-CommandItemDisplay="None"
                                                        DataSourceID="SqlDSStatBriefDescOfDuties" Skin="Metro" ShowHeader="false">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                                    <asp:TextBox ID="tbStatSpecialSkills" ReadOnly="true" runat="server" Visible="false"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridStatSpecialSkills_QualificationsRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True" MasterTableView-CommandItemDisplay="None"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridStatSpecialSkills_QualificationsRequired_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" DataSourceID="SqlDSStatSpecialSkills_QualificationsReq">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Education Required:</label>

                                                    <asp:TextBox ID="tbStatEducationRequired" runat="server" ReadOnly="true" Visible="false"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridStatEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridStatEducationRequired_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" MasterTableView-CommandItemDisplay="None"
                                                        DataSourceID="SQLDSStatEducationRequired">

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

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Employment/Work Status</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="tbStatWorkStatus" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                TextMode="SingleLine"></asp:TextBox>

                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Justification and History</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-lg-4">

                                            <asp:TextBox ID="tbStatJustification" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-lg-8">

                                            <asp:TextBox ID="tbStatHistory" runat="server" placeholder="Enter Text Here..."
                                                Enabled="false" CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Attachment</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">
                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">
                                                    <%--CssClass="table table-outline table-vcenter text-nowrap card-table"--%>
                                                    <asp:GridView ID="gridStatAttachment" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        ShowFooter="false" EmptyDataText="No Records Found" OnPreRender="gridStatAttachment_PreRender">

                                                        <Columns>

                                                            <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="95%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                                <ItemTemplate>

                                                                    <%# Eval("attachmentname") %>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20px">

                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                                        CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" Text="Download" Width="100%" />

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

                        <telerik:RadPageView runat="server" ID="RadPageView4">
                            <div class="card">

                                <div class="card-body">
                                    <%--Session["StatUC"].ToString()--%>
                                    <%= NewCommentThreads(uc)%>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <hr style="margin-top: 10px" />
                                        <div class="input-group" style="margin-top: -10px">
                                            <input runat="server" id="tbPendingMessage" type="text" name="message" placeholder="Type Message ..." class="form-control" />
                                            <span class="input-group-btn">
                                                <button type="button" class="btn btn-primary btn-flat" runat="server" onserverclick="btnStatHold_Click">Send</button>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </telerik:RadPageView>

                    </telerik:RadMultiPage>

                </div>

            </div>

            <div class="card" runat="server" id="cardViewRequestDetails" visible="true">

                <div class="card-header" style="background-color: rgb(11, 163, 217);">

                    <h3 class="card-title" style="color: white">Accomplished Record Details</h3>

                    <div class="card-options">
                        <a runat="server" class="card-options-remove" style="color: white" onserverclick="CloseForm"><i class="fe fe-x"></i></a>
                    </div>

                </div>

                <div class="card-body">

                    <div class="row">

                        <div class="col-12 bg-gray-dark-lightest">

                            <h4 style="margin-top: 10px; font-weight: 400">Application Status</h4>

                        </div>
                    </div>

                    <div class="row" runat="server" id="AlertCancelled" visible="false" style="margin-top: 5px;">

                        <div class="col-12">

                            <%=CancelAlert(Session["ApplicationRemarks"].ToString().Trim()) %>
                        </div>

                    </div>

                    <div class="row" style="margin-top: 10px">

                        <div class="col-6" runat="server" visible="false">

                            <div class="row">

                                <div class="col-3">

                                    <label class="docconlabels">Male Endorsed:</label>

                                    <asp:TextBox ID="tbViewMaleEndorsed" runat="server" ReadOnly="true"
                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                        TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                </div>

                                <div class="col-3">

                                    <label class="docconlabels">Female Endorsed:</label>

                                    <asp:TextBox ID="tbViewFemaleEndorsed" runat="server" ReadOnly="true"
                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                        TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                </div>

                                <div class="col-3">

                                    <label class="docconlabels">Male Balance:</label>

                                    <asp:TextBox ID="tbViewMaleBalance" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                </div>

                                <div class="col-3">

                                    <label class="docconlabels">Female Balance:</label>

                                    <asp:TextBox ID="tbViewFemaleBalance" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                </div>

                            </div>

                        </div>

                        <div class="col-6">

                            <div class="row">
                                <div class="col-4">

                                    <label class="docconlabels">Status:</label>

                                    <asp:TextBox ID="tbViewApplicationStatus" runat="server"
                                        ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control"
                                        Height="38px" TextMode="SingleLine"></asp:TextBox>

                                </div>
                                <div class="col-4" runat="server" id="colViewServedDate" visible="false">

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

                                    <%--<asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Count">

                                        <ItemTemplate>

                                            <%# Eval("MaleCount") %>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Endorsed">

                                        <ItemTemplate>

                                            <%# Eval("MaleEndorsed") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Endorsed">

                                        <ItemTemplate>

                                            <%# Eval("FemaleEndorsed") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Male Balance">

                                        <ItemTemplate>

                                            <%# Eval("MaleBalance") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <%--<asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="14%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Count">

                                        <ItemTemplate>

                                            <%# Eval("FemaleCount") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>



                                    <asp:TemplateField ItemStyle-Wrap="true" ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Female Balance">

                                        <ItemTemplate>

                                            <%# Eval("FemaleBalance") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="Served Date">

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


                    <br />

                    <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip4"
                        MultiPageID="RadMultiPage4" SelectedIndex="0" Font-Size="Smaller">

                        <Tabs>

                            <telerik:RadTab Text="Details" CssClass="aligncenter"></telerik:RadTab>

                            <telerik:RadTab Text="Communication Thread" CssClass="aligncenter"></telerik:RadTab>

                        </Tabs>

                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage runat="server" ID="RadMultiPage4" SelectedIndex="0">

                        <telerik:RadPageView runat="server" ID="RadPageView5">
                            <div class="card">
                                <div class="card-body">

                                    <div class="row">

                                        <div class="col-4">
                                            <label class="docconlabels">Control No:</label>

                                            <asp:TextBox ID="tbViewControlNo" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">
                                            <label class="docconlabels">Date Filed:</label>

                                            <asp:TextBox ID="tbViewDateFiled" runat="server" ReadOnly="true" placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                        <div class="col-4">
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

                                        <div class="col-4">

                                            <div class="row">
                                                <div class="col-4">

                                                    <label class="docconlabels">Male:</label>

                                                    <asp:TextBox ID="tbViewMaleCount" runat="server" ReadOnly="true"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                        TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Female:</label>

                                                    <asp:TextBox ID="tbViewFemaleCount" runat="server" ReadOnly="true"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                        TextMode="Number" AutoPostBack="true"></asp:TextBox>

                                                </div>

                                                <div class="col-4">

                                                    <label class="docconlabels">Total:</label>

                                                    <asp:TextBox ID="tbViewTotal" runat="server" ReadOnly="true"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="38px" TextMode="Number"></asp:TextBox>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-4">

                                            <label class="docconlabels">Date Needed:</label>

                                            <asp:TextBox ID="tbViewDateNeeded" runat="server" ReadOnly="true"
                                                placeholder="Enter Text Here..." CssClass="form-control" Height="38px"
                                                TextMode="SingleLine"></asp:TextBox>

                                        </div>

                                    </div>

                                    <br />

                                    <div class="row">

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Job Description</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

                                                    <label class="docconlabels">Brief Description of Duties:</label>

                                                    <asp:TextBox ID="tbViewBriefDesc" runat="server" ReadOnly="true" Visible="false"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridViewBriefDesc" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridViewBriefDesc_ItemDataBound"
                                                        DataSourceID="SqlDSViewBriefDescOfDuties" Skin="Metro" ShowHeader="false" MasterTableView-CommandItemDisplay="None">

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Special Skills / Qualifications Required:</label>

                                                    <asp:TextBox ID="tbViewSpecialSkills" ReadOnly="true" runat="server" Visible="false"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridViewSpecialSkills_QualificationsReq" GridLines="None" runat="server"
                                                        AllowAutomaticDeletes="True" MasterTableView-CommandItemDisplay="None"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridViewSpecialSkills_QualificationsReq_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" DataSourceID="SqlDSViewSpecialSkills_QualificationsReq">

                                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ID"
                                                            HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="False">

                                                            <BatchEditingSettings EditType="Cell" OpenEditingEvent="None" />

                                                            <CommandItemSettings ShowCancelChangesButton="false" ShowAddNewRecordButton="false"
                                                                ShowRefreshButton="false" ShowSaveChangesButton="false" />

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

                                                <div class="col-12">

                                                    <label class="docconlabels">Education Required:</label>

                                                    <asp:TextBox ID="tbViewEducationRequired" runat="server" ReadOnly="true" Visible="false"
                                                        placeholder="Enter Text Here..." CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>

                                                    <telerik:RadGrid RenderMode="Lightweight" ID="gridViewEducationRequired" GridLines="None" runat="server" AllowAutomaticDeletes="True"
                                                        AllowAutomaticInserts="True" PageSize="10" AllowAutomaticUpdates="True" AllowPaging="True"
                                                        AutoGenerateColumns="False" OnItemDataBound="gridViewEducationRequired_ItemDataBound"
                                                        Skin="Metro" ShowHeader="false" MasterTableView-CommandItemDisplay="None"
                                                        DataSourceID="SQLDSViewEducationRequired">

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

                                        <div class="col-12 bg-gray-dark-lightest">

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

                                        <div class="col-12 bg-gray-dark-lightest">

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

                                        <div class="col-12 bg-gray-dark-lightest">

                                            <h4 style="margin-top: 10px; font-weight: 400">Attachment</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-12">

                                            <div class="row">

                                                <div class="col-12">

                                                    <asp:GridView ID="gridViewAttachments" runat="server" Width="100%" AutoGenerateColumns="false"
                                                        CssClass="table table-outline table-vcenter text-nowrap card-table"
                                                        ShowFooter="false" EmptyDataText="No Records Found"
                                                        OnPreRender="gridViewAttachments_PreRender">

                                                        <Columns>

                                                            <asp:TemplateField ItemStyle-CssClass="items" ItemStyle-Width="95%" HeaderStyle-CssClass="headers bg-blue-gradient" HeaderText="FILE NAME">

                                                                <ItemTemplate>

                                                                    <%# Eval("attachmentname") %>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-CssClass="items2" ItemStyle-Width="20px">

                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lnkDownloadAttachment" runat="server" class="btn btn-primary btn-flat glyphicon glyphicon-download-alt"
                                                                        CommandArgument='<%# Eval("id")%>' OnClientClick="return confirm('Do you want to download the file?')" OnClick="DownloadFile" Text="Download" Width="100%" />

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
                            <div class="card">

                                <div class="card-body">

                                    <%= NewCommentThreads(uc)%>
                                </div>

                            </div>
                        </telerik:RadPageView>

                    </telerik:RadMultiPage>

                </div>

            </div>

        </div>

    </div>


    <div class="modal fade" id="modalPendingRemarks">

        <div class="modal-dialog modal-lg">

            <div class="modal-content" style="width: 100%;">

                <div class="modal-header bg-blue">

                    <h4 class="modal-title" style="color: white"><b>HOLD REMARKS</b></h4>
                </div>

                <div class="modal-body">
                    <div class="row">

                        <div class="col-lg-12">

                            <asp:TextBox runat="server" ID="tbHoldRemarks" TextMode="MultiLine" placeholder="Type Message Here..."
                                Height="150px" Width="100%"></asp:TextBox>

                        </div>

                    </div>

                </div>

                <div class="modal-footer">

                    <asp:Button runat="server" ID="btnSendPending" CssClass="btn btn-primary bg-blue-gradient btn-flat pull-right" Text="Update"
                        Width="100px" OnClientClick="return confirm('Do you want to proceed?')" OnClick="btnUpdateStatus_Click" />

                    <button type="button" class="btn btn-default btn-flat pull-right" style="width: 100px; margin-right: 10px;" data-dismiss="modal">Close</button>


                </div>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modalRejectRemarks">

        <div class="modal-dialog modal-lg">

            <div class="modal-content" style="width: 100%;">

                <div class="modal-header bg-blue">

                    <h4 class="modal-title" style="color: white"><b>CANCEL REMARKS</b></h4>
                </div>

                <div class="modal-body">
                    <div class="row">

                        <div class="col-lg-12">

                            <asp:TextBox runat="server" ID="tbRejectRemarks" TextMode="MultiLine" placeholder="Type Message Here..."
                                Height="150px" Width="100%"></asp:TextBox>

                        </div>

                    </div>

                </div>

                <div class="modal-footer">

                    <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary bg-blue-gradient btn-flat pull-right" Text="Update"
                        Width="100px" OnClientClick="return confirm('Do you want to proceed?')" OnClick="btnUpdateStatus_Click" />

                    <button type="button" class="btn btn-default btn-flat pull-right" style="width: 100px; margin-right: 10px;" data-dismiss="modal">Close</button>


                </div>

            </div>

        </div>

    </div>


    <asp:SqlDataSource ID="SQLDSEndorsementLogs" runat="server"
        SelectCommand="select * FROM [ManpowerMonitoringDB].[dbo].[tblEndorsementLogs] where ControlNo=@ControlNo" SelectCommandType="Text">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbStatControlNo" PropertyName="Text" Name="ControlNo" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SQLDSEndorsementLogs2" runat="server"
        SelectCommand="select * FROM [ManpowerMonitoringDB].[dbo].[tblEndorsementLogs] where ControlNo=@ControlNo" SelectCommandType="Text">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbViewControlNo" PropertyName="Text" Name="ControlNo" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDSStatBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbStatPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSStatSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbStatPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSStatEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbStatPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSNewBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbNewPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDSRecBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbRecPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSRecSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbRecPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSNewSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbNewPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSRecEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbRecPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSNewEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbNewPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDSViewBriefDescOfDuties" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblBriefDescOfDuties where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbViewPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSViewSpecialSkills_QualificationsReq" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblSpecialSkills_QualificationsReq where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbViewPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDSViewEducationRequired" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="Select * from tblEducationRequired where PositionDesc=@PositionDesc" SelectCommandType="Text">

        <SelectParameters>

            <asp:ControlParameter ControlID="tbViewPosition" Name="PositionDesc" PropertyName="Text" DbType="String" />

        </SelectParameters>

    </asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="GetAllDepartment" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="GetAllDepartment" SelectCommandType="StoredProcedure"></asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDSDisplaySummaryOfRecords" runat="server" ProviderName="System.Data.SqlClient"
        SelectCommand="DisplaySummaryOfRecords" SelectCommandType="StoredProcedure">

        <SelectParameters>

            <asp:ControlParameter ControlID="RadDDLYear" Name="Year" PropertyName="SelectedValue" DbType="Int32" />

        </SelectParameters>

    </asp:SqlDataSource>


</asp:Content>
