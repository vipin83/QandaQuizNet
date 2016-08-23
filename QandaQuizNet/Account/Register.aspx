<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QandaQuizNet.Account.Register" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <script type="text/javascript">
        function CheckBoxRequired_ClientValidate(sender, e) {            
            e.IsValid = $("#<%= Over16AgeConfirm.ClientID %>").is(':checked');
            
        }

        $(function () {
            $(document).on('change', "#<%= Over16AgeConfirm.ClientID %>", function () {                
                CheckBoxRequired_ClientValidate();
                var validatorCB = <%= CheckBoxRequired.ClientID %>;
                validatorCB.isvalid = true;
                ValidatorUpdateIsValid();

            })

            $("#<%= Password.ClientID %>").popover(
                {                   
                    content:"Password must meet the following criteria:<br/><br/> is at least 6 characters;<br/> Uppercase characters (A - Z);<br/> Lowercase characters (a - z);<br/> Numerals (0 - 9);<br/> Non-alphabetic characters($, #, %, etc.)",
                    html:true
                }
                );
        });


    </script>

    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" DisplayMode="list"  Visible="false"/>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="firstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="firstName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="firstName"
                    CssClass="text-danger" ErrorMessage="The first name field is required." Display="Dynamic"/>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="lastName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="lastName"
                    CssClass="text-danger" ErrorMessage="The last name field is required." Display="Dynamic"/>
            </div>
        </div>
      
         

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="townCity" CssClass="col-md-2 control-label">Town/City</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="townCity" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="townCity"
                    CssClass="text-danger" ErrorMessage="Town/City field is required." Display="Dynamic"/>
            </div>
        </div>


        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlCountryList" CssClass="col-sm-2 control-label">Country</asp:Label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlCountryList" class="form-control" runat="server" EnableViewState="true" />                
                <asp:CompareValidator ID="cmpCountry"  runat="server" ControlToValidate="ddlCountryList" Operator="NotEqual" ValueToCompare="0" Type="Integer"
                    ErrorMessage="The country field is required." CssClass="text-danger" Display="Dynamic"/>
            </div>
            <div class="col-sm-7">&nbsp;</div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="This will be your username." />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." Display="Dynamic"/>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" data-toggle="popover" data-trigger="hover"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." Display="Dynamic"/>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>


        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Referral User</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="referralEmail" CssClass="form-control" TextMode="Email" placeholder="Email of referral Qanda user (optional)" />                
            </div>
        </div>

        <div class="form-group checkbox" style="margin-top:-15px;margin-bottom:15px;">            
            <div class="col-md-offset-2 col-md-10">                
                <asp:CheckBox ID="Over16AgeConfirm" CssClass="control-label OverrrideBootStrapLeftMarginForCheckBox" runat="server" 
                    Text="Confirm you are over 16 years of age." />      
                <br />
                <asp:CustomValidator runat="server" ID="CheckBoxRequired" EnableClientScript="true"    
                    ClientValidationFunction="CheckBoxRequired_ClientValidate" CssClass="text-danger" Display="Dynamic">You must confirm you are over 16 to register.</asp:CustomValidator>       
                         
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default btn-danger" />
            </div>
        </div>
    </div>
</asp:Content>
