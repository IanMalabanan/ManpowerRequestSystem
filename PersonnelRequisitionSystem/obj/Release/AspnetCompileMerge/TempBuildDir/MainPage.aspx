<%@ Page Title="Personnel Requisition System" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="PersonnelRequisitionSystem.MainPage" %>

<%@ MasterType VirtualPath="~/MasterApproverPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper aligncenter" style="background-image: url(../images/sohbi.jpg); background-size: cover; background-repeat: no-repeat;">

        <!-- Content Header (Page header) -->
        <section class="content-header aligncenter hide" style="background-image: url(../images/mdm-banner.jpg); background-size: cover; background-repeat: no-repeat;">
            <img src="../images/skpilogopic.png" style="width: 200px; height: 150px;" />

            <p style="color: white; font-size: 50px"><b>Sohbi Kohgei (Phils.), Inc.</b></p>

            <p style="color: white; font-size: 20px">Special Economic Zone, Lima Technology Center, Lipa City, Batangas</p>

            <br />
        </section>

        <section class="content">

            <div style="margin-top: 5%; margin-bottom: 5%;">
                <img src="../images/skpilogopic.png" style="width: 300px; height: 250px;" />

                <p style="color: white; font-size: 60px"><b>Sohbi Kohgei (Phils.), Inc.</b></p>

                <p style="color: white; font-size: 30px">Special Economic Zone, Lima Technology Center, Lipa City, Batangas</p>
            </div>

        </section>

    </div>
    <!-- /.content-wrapper -->
</asp:Content>
