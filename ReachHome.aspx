<%@ Page Title="" Language="VB" MasterPageFile="~/ReachApp.master" AutoEventWireup="false" CodeFile="ReachHome.aspx.vb" Inherits="ReachHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppLeftColumn" Runat="Server">

    <asp:Panel ID="PatientNavPanel" runat="server" Visible="true">
        <div style="padding: 20px;">
            <asp:Button ID="BtnMyPatients" runat="server" OnClick="BtnMyPatients_Click" CssClass="StandardButton" Text="My Patients" Width="90%" Height="34px" />
            <br /><br />
            <asp:Button ID="BtnAllPatients" runat="server" OnClick="BtnAllPatients_Click" CssClass="StandardButton" Text="All Patients" Width="90%" Height="34px" />
            <br /><br />
            <asp:Button ID="BtnFilters" runat="server" OnClick="BtnFilters_Click" CssClass="StandardButton" Text="Filters" Width="90%" Height="34px" />
            <br /><br />
            <asp:Button ID="BtnAddPatient" runat="server" OnClick="BtnAddPatient_Click" CssClass="StandardButton" Text="Add Patients" Width="90%" Height="34px" />
        </div>
    </asp:Panel>

    <asp:Panel ID="PatientDetailPanel" runat="server" Visible="false">
        <div style="padding: 20px;">

        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AppCenterColumn" Runat="Server">



</asp:Content>