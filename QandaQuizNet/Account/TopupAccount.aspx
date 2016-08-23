<%@ Page Title="Topup Account Balance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="TopupAccount.aspx.cs" Inherits="QandaQuizNet.Account.TopupAccountPage" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

     <div class="col-md-12" runat="server" id="EmailConfirmPendingDialog" visible="false">
            <div class="alert alert-info fade in">
                <a href="#" class="close  fa-close icon" data-dismiss="alert" aria-label="close"></a>
                <p>
                    <strong>
                        Welcome to QandA Quiz. Your account has been opened successfully and you are now seconds away from your first quiz.
                   </strong>                  
                </p>
                <p class="freePlayText">
                    To welcome you to our quiz family, we have offered you first play free.
                </p>
                <p>
                    We've sent an email confirming your account details to <%= user.Email %>. <br />
                    Please check your email and verify your address. If you can't find your verification email, please check your spam folder and filter. If you still can't find it, click <a href="/Account/ResendEmailConfirmation.aspx"> here</a> and we'll send you a new one.<br /> 
                    Please deposit funds into your account using the form below so you can start playing and winning.
                </p>
            </div>
        </div>

    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" DisplayMode="list"  Visible="false"/>

       
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlTopupAmount" CssClass="col-md-2 control-label">Amount</asp:Label>

            <div class="col-md-2" >
                <asp:DropDownList ID="ddlTopupAmount" class="form-control input-sm" runat="server" EnableViewState="true" />
                
                <asp:CompareValidator ID="cmpCountry"  runat="server" ControlToValidate="ddlTopupAmount" Operator="NotEqual" ValueToCompare="0" Type="Integer"
                    ErrorMessage="The amount field is required." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>
        <div class="form-group" style="margin-top:-15px;">
            <div class="col-md-offset-2 col-md-10" >
                <p class="form-control-static">The minimum amount that can be topped up is 5.</p>
                </div>
            </div>
        
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="TopupAccount_Click" Text="Submit" CssClass="btn btn-default btn-danger" />
            </div>
        </div>

    </div>
</asp:Content>