<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuizList.aspx.cs" Inherits="QandaQuizNet.QuizList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6 col-xs-6 text-left">
                <h2>Quiz List</h2>
                <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
            </div>
            <div class="col-md-6 col-xs-6 text-right" style="padding-top: 30px;">
                <asp:LinkButton ID="btnAddQuiz" runat="server" CssClass="btn btn-danger btn-md" PostBackUrl="~/Admin/AddQuiz.aspx">
                    <span class="glyphicon glyphicon-plus">
                        Add Quiz
                    </span>
                </asp:LinkButton>
            </div>
        </div>

        <br />

        <div class="table-responsive">
            <asp:GridView ID="gvQuizList" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No quiz added" DataKeyNames="Id"
                HeaderStyle-CssClass="bg-primary" RowStyle-CssClass="bg-success"
                OnRowCommand="gvQuizList_Command" OnRowDataBound="gvQuizList_RowDataBound">
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnQuizID" runat="server" Value='<%# Bind("Id") %>'></asp:HiddenField>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Id" Visible="True" ReadOnly="true" DataField="Id" />
                    <asp:BoundField HeaderText="Title" DataField="quizTitle" SortExpression="quizTitle" />
                    <asp:BoundField HeaderText="Active From" DataField="quizAvtiveDateTime" SortExpression="quizAvtiveDateTime" DataFormatString="{0:dd/MMM/yyyy HH:mm}" />
                    <asp:BoundField HeaderText="Prize" DataField="prize" SortExpression="prize" DataFormatString="{0:C}" />
                    <asp:BoundField HeaderText="Responses" DataField="responses" SortExpression="responses" />
                    <asp:BoundField HeaderText="Winner" DataField="winner" SortExpression="winner" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="rsbEdit" runat="server" Width="50px" Text="Edit" CommandName="Edit" CausesValidation="False"
                                CommandArgument='<%# Bind("Id") %>'></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>

    </div>


</asp:Content>
