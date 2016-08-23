<%@ Page Title="Confirm Topup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
        CodeBehind="ConfirmTopup.aspx.cs" Inherits="QandaQuizNet.Account.ConfirmTopup" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <h2><%: Title %>.</h2>
    <p>
        <asp:Literal runat="server" ID="lblSuccess" />
    </p>
    

</asp:Content>