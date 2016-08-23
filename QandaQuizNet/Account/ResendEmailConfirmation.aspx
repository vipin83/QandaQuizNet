<%@ Page Title="Email Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResendEmailConfirmation.aspx.cs" Inherits="QandaQuizNet.Account.ResendEmailConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>

    <div class="row">
        <div class="col-md-8">
            <asp:PlaceHolder id="loginForm" runat="server">
                <div class="form-horizontal">
                    <h4>Re-send Email Confirmation</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-5 input-group input-group-lg">
                             <span class="input-group-addon">
                                <span class="glyphicon glyphicon-envelope"></span>
                            </span>
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="Email Address"/>                           
                        </div>

                         <div class="col-md-offset-2 col-md-5">
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." Display="Dynamic"/>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="ResendEmailConfirmationMethod" Text="Resend" CssClass="btn btn-default btn-danger" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
                <p class="text-info">
                    We have send email to your registered address, please check your email and verify your address. 
                    <br />
                    If you accidently deleted or can't find your verification email, please check your spam folder and filter. If you still can't find it, click <a href="/Account/ResendEmailConfirmation.aspx"> here</a> and we'll send you a new one.
                </p>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
