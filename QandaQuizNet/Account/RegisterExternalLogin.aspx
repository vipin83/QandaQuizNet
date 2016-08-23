<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterExternalLogin.aspx.cs" Inherits="QandaQuizNet.Account.RegisterExternalLogin" Async="true" %>

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
        });


    </script>



<h3>Register with your <%: ProviderName %> account</h3>

    <asp:PlaceHolder runat="server">

        <div class="form-horizontal">
            <h4>Association Form</h4>
            <hr />
            
            
            
            <div class="col-md-12" runat="server" id="ExternalAuthenticationPass" visible="true">
                <div class="alert alert-info fade in">
                    <a href="#" class="close  fa-close icon" data-dismiss="alert" aria-label="close"></a>
                    <p>
                        <strong>
                            Welcome to QandA Quiz. You've successfully autheticated with <strong><%: ProviderName %></strong>. 
                            <br />Please fill below details and click on Log in.
                       </strong>                  
                    </p>                
                </div>
            </div>
            
            <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" Visible="false" id="ValSummary"/>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-2 control-label">Email</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                        Display="Dynamic" CssClass="text-danger" ErrorMessage="Email is required" />                    
                </div>
            </div>




            <!--additional fields -->
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
                    <asp:Button runat="server" Text="Log in" CssClass="btn btn-default" OnClick="LogIn_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
