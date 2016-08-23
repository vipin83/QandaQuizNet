<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenAuthProviders.ascx.cs" Inherits="QandaQuizNet.Account.OpenAuthProviders" %>

<div id="socialLoginList">
    <h4>Use your existing account to log in.</h4>
    <hr />
    <asp:ListView runat="server" ID="providerDetails" ItemType="System.String"
        SelectMethod="GetProviderNames" ViewStateMode="Disabled">
        <ItemTemplate>

            <span id="parent">               
                <button type="submit" name="provider" class="externalLoginButton" value="<%#: Item %>" title="Log in using your <%#: Item %> account.">
                    <img class="socialLoginButton" src="../images/Login<%#: Item.ToLower() %>.png" alt="Log in using your <%#: Item %> account."/>
                </button>
            </span>

        </ItemTemplate>
        <EmptyDataTemplate>
            <div>
                <p>There are no external authentication services configured. </p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</div>
