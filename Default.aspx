<%@ Page Title="" Language="VB" MasterPageFile="~/Reach.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContent" Runat="Server">

    <div style="padding: 20px 0px 0px 0px; text-align: center; width: 1000px; display: table; margin: 0 auto;">

    <div style="border: 1px dotted #808080; height: 500px; width: 1000px; text-align: left; background-color: #f3f3f3;">

        <div style="padding: 30px 0px 0px 60px;">

            <ajax:ModalPopupExtender ID="DisplayHelpPopUp" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="NeedHelpPanel" Enabled="True" TargetControlID="BtnNeedHelp" CancelControlID="BtnCancel"></ajax:ModalPopupExtender>

            <asp:Panel ID="NeedHelpPanel" style="border-radius: 10px;  display: none; padding: 18px; text-align: left; background-color: #FFFFFF; box-shadow:0 0 20px #444444; border: 2px solid #000000; width: 60%; height: 300px; overflow: auto;" runat="server">   

                <div style="text-align: left; padding: 10px 0px 0px 0px;">
            
                    <asp:Label ID="LblName" runat="server" Text="Need Help Signing In?" Font-Size="20px" Font-Bold="true" /><br /><br />

                    <asp:Label ID="LblMessageInstructions" runat="server" Text="Did you forget your password or is your account locked due to too many failed attempts? No problem! Provide your account email address below and we'll shoot you an email so you can get back into Servant Scout." /><br /><br />

                    <asp:DropDownList ID="ReasonForHelp" runat="server" CssClass="MediumDropDown" Width="250px">
                        <asp:ListItem Text="I forgot my password" Value="ForgotPassword" />
                        <asp:ListItem Text="My account is locked" Value="LockedAccount" />
                    </asp:DropDownList><br /><br />

                    <asp:TextBox ID="TxtEmailAddress" runat="server" CssClass="MediumTxtBox" Width="320px" placeholder="Email Address" /><br /><br />
            
                    <asp:Button Id="BtnSendHelp" runat="server" Text="Send Help" CssClass="StandardButton" OnClick="BtnSendHelp_Click" />&nbsp;&nbsp;
                    <asp:Button Id="BtnCancel" runat="server" Text="Cancel" CssClass="StandardButton" OnClick="BtnCancel_Click" /><br /><br />

                    <asp:Label ID="LblSuccess" runat="server" Visible="false" Text="" />

                </div>

            </asp:Panel>

            <asp:Panel ID="LoginPanel" runat="server" Visible="true" DefaultButton="BtnLogin">

                <div style="padding: 20px 0px 0px 0px; float: left; width: 35%;">

                    <div style="padding: 0px 0px 6px 0px; font-size: 28px;">Sign-In</div>

                    <div style="padding: 0px 0px 6px 0px;">Username:</div>
                    <asp:TextBox ID="TxtUserName" runat="server" Text="" CssClass="MediumTxtBox" Width="260px" ToolTip="Username" /><br />

		            <br />

                    <div style="padding: 0px 0px 6px 0px;">Password:</div>
                    <asp:TextBox ID="TxtPassword" runat="server" Text="password" TextMode="Password" CssClass="MediumTxtBox" Width="260px" ToolTip="Password" />

                    <br /><br />

                    <asp:Button ID="BtnLogin" runat="server" CssClass="StandardButton" Width="268px" Height="32px" Font-Size="16px" Text="Sign In" OnClick="BtnSignIn_Click" />

                    <br /><br />

                    <asp:Button ID="BtnCreateAccount" runat="server" CssClass="StandardButtonGreen" OnClick="BtnCreateAccount_Click" Text="Create Account" Font-Size="16px" Height="32px" Width="268px" />

                    <br /><br />

                    <div style="padding: 0px 0px 0px 0px; border-bottom: 1px dotted #808080; width: 82%;">&nbsp;</div>

                    <div style="padding: 20px 0px 0px 0px; font-size: 22px;">I Need Help</div>

                    <div style="padding: 0px 0px 6px 0px; font-size: 10px;">
                        <ul>
                            <li>Forgot your password?</li>
                            <li>Account locked?</li>
                        </ul>
                    </div>

                    <asp:Button ID="BtnNeedHelp" runat="server" CssClass="StandardButton" Width="268px" Height="32px" Font-Size="16px" Text="Send Help My Way" OnClick="BtnNeedHelp_Click" />

                    <br /><br />

                    <asp:Label ID="LblErrorMessage" runat="server" Text="" Font-Size="10px" Font-Names="Century Gothic" Font-Italic="true" ForeColor="Red" Visible="false" />

                </div>

            </asp:Panel>

            <asp:Panel ID="ResetAccountPanel" runat="server" Visible="false">

                <div style="padding: 20px 0px 0px 0px; float: left; width: 35%;">

                    <div style="padding: 0px 0px 6px 0px; font-size: 28px;">Reset My Account</div>

                    <div style="padding: 0px 0px 6px 0px;">Password:</div>
                    <asp:TextBox ID="TxtNewPassword" runat="server" Text="password" TextMode="Password" CssClass="MediumTxtBox" Width="260px" ToolTip="New Password" /><br />

		            <br />

                    <div style="padding: 0px 0px 6px 0px;">Password Confirm:</div>
                    <asp:TextBox ID="TxtNewPasswordConfirm" runat="server" Text="password" TextMode="Password" CssClass="MediumTxtBox" Width="260px" ToolTip="New Password Confirm" />

                    <br /><br />

                    <asp:Button ID="BtnResetAccount" runat="server" CssClass="StandardButton" Width="268px" Text="UPDATE ACCOUNT" OnClick="BtnResetAccount_Click" />

                    <br /><br />

                    <asp:Label ID="LblAccountHelpErrorMessage" runat="server" Text="" Font-Size="10px" Font-Names="Century Gothic" Font-Italic="true" ForeColor="Red" Visible="false" />

                </div>

            </asp:Panel>

            <div style="padding: 20px 0px 0px 0px; float: left; width: 60%; border-left: dotted 1px #808080; min-height: 300px;">

                <div style="padding: 0px 0px 0px 40px;">

                    <asp:Label ID="LblWelcomeMessage" runat="server" Visible="true" Text="" />

                </div>

            </div>

        </div>

    </div>

    </div>

    <div style="padding: 20px 0px 0px 0px; text-align: center;">

        <asp:Label ID="LblSingInFooter" runat="server" Text="" Visible="true" Font-Size="10px" ForeColor="#666666" />

    </div>

</asp:Content>