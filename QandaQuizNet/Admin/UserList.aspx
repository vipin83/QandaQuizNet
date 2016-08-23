<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="QandaQuizNet.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6 col-xs-6 text-left">
                <h2>Registered User List</h2>
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
            </div>
           
        </div>

        <br />

        <div class="table-responsive">
            <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No User added" DataKeyNames="userEmail"
                HeaderStyle-CssClass="bg-primary" RowStyle-CssClass="bg-success"
                OnRowCommand="gvUserList_Command" OnRowDataBound="gvUserList_RowDataBound">
                <Columns>
                    <%--<asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Bind("Id") %>'></asp:HiddenField>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    
                    <asp:BoundField HeaderText="Name" DataField="userFullName" SortExpression="userFullName" />
                    <asp:BoundField HeaderText="Email" DataField="userEmail" SortExpression="userEmail" />
                    <asp:BoundField HeaderText="Location" DataField="userLocation" SortExpression="userLocation" />
                    <asp:BoundField HeaderText="Account Balance" DataField="userAccountBalance" SortExpression="userAccountBalance" DataFormatString="{0:C}" />
                    <asp:BoundField HeaderText="Registered Date" DataField="userRegisteredDate" SortExpression="userRegisteredDate" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
                    <asp:BoundField HeaderText="Referral" DataField="referralEmail" SortExpression="referralEmail" />

                  <%--  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="rsbEdit" runat="server" Width="50px" Text="Edit" CommandName="Edit" CausesValidation="False"
                                CommandArgument='<%# Bind("Id") %>'></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>

        </div>

    </div>


</asp:Content>
