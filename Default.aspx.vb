Imports System.Net.Mail
Imports System.Web
Imports System.Web.UI
Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Partial Class _Default

    Inherits System.Web.UI.Page

    Public Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim StrMasterUrl As String = ConfigurationManager.AppSettings.Item("MasterUrl").ToString()
        Page.Title = ConfigurationManager.AppSettings.Item("SiteTitle").ToString()

        LblSingInFooter.Text = "&copy;&nbsp;" & Year(Now()) & "&nbsp;" & ConfigurationManager.AppSettings.Item("OrganizationName").ToString() & "&nbsp;|&nbsp;" & ConfigurationManager.AppSettings.Item("MailingAddress").ToString() & ",&nbsp;" & ConfigurationManager.AppSettings.Item("MailingCity").ToString() & "&nbsp;" & ConfigurationManager.AppSettings.Item("MailingState").ToString() & "&nbsp;" & ConfigurationManager.AppSettings.Item("MailingZip").ToString()

        If Request.QueryString("Mobile") = "Off" Then

            'Load Desktop/Full Website

            Response.Redirect("~/Contentment/Default.aspx")

        Else

            Dim u As String = Request.ServerVariables("HTTP_USER_AGENT")

            Dim b As New Regex("android.+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase)

            Dim v As New Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|e\-|e\/|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\-|2|g)|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase)

            If b.IsMatch(u) Or v.IsMatch(Left(u, 4)) Then
                Response.Redirect("~/Contentment/Mobile/Default.aspx")
            Else
                'Response.Redirect("~/Contentment/Default.aspx")
            End If

        End If

        If Request.QueryString("AccountHelp") = 1 Then

            LoginPanel.Visible = False
            ResetAccountPanel.Visible = True

        End If

        'GetWelcomeMessage()

        TxtUserName.Focus()

    End Sub

    Protected Sub GetWelcomeMessage()

        Dim myDataSet As DataSet = New DataSet

        Dim mySQLConnection As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)

        Dim mySQLCommand As New Data.SqlClient.SqlCommand("GetSystemPreferences", mySQLConnection)
        Dim mySQLAdapter As New Data.SqlClient.SqlDataAdapter

        mySQLAdapter.SelectCommand = mySQLCommand

        mySQLConnection.Open()
        mySQLAdapter.Fill(myDataSet, "GetSystemPreferences")
        mySQLConnection.Close()

        If myDataSet.Tables(0).Rows.Count > 0 Then

            LblWelcomeMessage.Text = CheckIfNull(myDataSet.Tables(0).Rows(0).Item("WelcomeMessage"))

        End If

    End Sub

    Function CheckIfNull(ByVal RandomString As Object) As String

        Dim TempString As String = ""

        If Not (IsDBNull(RandomString)) Then
            TempString = RandomString
        End If

        Return TempString

    End Function

    Protected Sub BtnNeedHelp_Click(ByVal sender As Object, ByVal e As EventArgs)

        DisplayHelpPopUp.Show()

    End Sub

    Protected Sub BtnSendHelp_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim PersonId As Integer = 0
        Dim NewRandomPassword As String = ""

        If ReasonForHelp.SelectedValue = "LockedAccount" Then

            PersonId = LocateLockedAccount(TxtEmailAddress.Text())

            If PersonId <> 0 Then

                UpdatePersonAccountLock(PersonId)

                NewRandomPassword = GetRandomPasswordUsingGUID(10)

                UpdatePersonPassword(PersonId, NewRandomPassword)

                EmailPerson(TxtEmailAddress.Text(), NewRandomPassword)

                LblSuccess.Text = "We've located your locked account, reset your password and unlocked your account. You will recieve an email shortly with your new password."

            Else

                LblSuccess.Text = "Sorry. We couldn't locate any accounts in the Servant Scout system that match the email address supplied."

            End If

        ElseIf ReasonForHelp.SelectedValue = "ForgotPassword" Then

            PersonId = LocateActiveAccountByEmail(TxtEmailAddress.Text())

            If PersonId <> 0 Then

                NewRandomPassword = GetRandomPasswordUsingGUID(10)

                UpdatePersonPassword(PersonId, NewRandomPassword)

                EmailPerson(TxtEmailAddress.Text(), NewRandomPassword)

                LblSuccess.Text = "We've located your account and sent you a new password. Please check your email for further instructions."

            Else

                LblSuccess.Text = "Sorry. We couldn't locate any accounts in the Servant Scout system that match the email address supplied."

            End If

        Else

            LblSuccess.Text = "Sorry. We couldn't locate any accounts in the Servant Scout system that match the email address supplied."

        End If

        LblSuccess.Visible = True

        DisplayHelpPopUp.Show()

    End Sub

    Protected Sub UpdatePersonPassword(ByVal PersonId As Integer, ByVal NewPassword As String)

        Dim NewPassEncrypted As String = Sha256Encrypt(NewPassword)

        Dim mySQLConnection As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)

        mySQLConnection.Open()

        Dim mySQLCommand As New Data.SqlClient.SqlCommand("UpdatePersonPassword", mySQLConnection)
        Dim mySQLAdapter As New Data.SqlClient.SqlDataAdapter

        mySQLCommand.CommandType = Data.CommandType.StoredProcedure

        mySQLCommand.Parameters.AddWithValue("@int_id", PersonId)
        mySQLCommand.Parameters.AddWithValue("@txt_password", NewPassEncrypted)

        mySQLCommand.ExecuteNonQuery()

        mySQLConnection.Close()

    End Sub

    Protected Sub UpdatePersonAccountLock(ByVal PersonId As Integer)

        Dim mySQLConnection As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)

        mySQLConnection.Open()

        Dim mySQLCommand As New Data.SqlClient.SqlCommand("UpdateMemberLock", mySQLConnection)
        Dim mySQLAdapter As New Data.SqlClient.SqlDataAdapter

        mySQLCommand.CommandType = Data.CommandType.StoredProcedure

        mySQLCommand.Parameters.AddWithValue("@int_id", PersonId)
        mySQLCommand.Parameters.AddWithValue("@StrModifiedBy", "System - Reset Account Lock")

        mySQLCommand.ExecuteNonQuery()

        mySQLConnection.Close()

    End Sub

    Public Function GetRandomPasswordUsingGUID(ByVal length As Integer) As String

        'Get the GUID
        Dim guidResult As String = System.Guid.NewGuid().ToString()

        'Remove the hyphens
        guidResult = guidResult.Replace("-", String.Empty)

        'Make sure length is valid
        If length <= 0 OrElse length > guidResult.Length Then
            Throw New ArgumentException("Length must be between 1 and " & guidResult.Length)
        End If

        'Return the first length bytes
        Return guidResult.Substring(0, length)

    End Function

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)

        DisplayHelpPopUp.Hide()

    End Sub

    Protected Sub BtnResetAccount_Click(ByVal sender As Object, ByVal e As EventArgs)

        LoginPanel.Visible = True
        ResetAccountPanel.Visible = False

    End Sub

    Function LocateLockedAccount(ByVal EmailAddr As String) As Integer

        Dim AccountId As Integer = 0

        Dim mySQLConnection As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)

        Dim myDataSet As DataSet = New DataSet

        Dim mySQLCommand As New Data.SqlClient.SqlCommand("GetLockedAccountByEmail", mySQLConnection)
        Dim mySQLAdapter As New Data.SqlClient.SqlDataAdapter

        mySQLCommand.CommandType = Data.CommandType.StoredProcedure
        mySQLCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = EmailAddr
        mySQLAdapter.SelectCommand = mySQLCommand

        mySQLConnection.Open()
        mySQLAdapter.Fill(myDataSet, "GetLockedAccountByEmail")
        mySQLConnection.Close()

        If myDataSet.Tables(0).Rows.Count > 0 Then

            AccountId = myDataSet.Tables(0).Rows(0).Item("int_id")

        End If

        Return AccountId

    End Function

    Function LocateActiveAccountByEmail(ByVal EmailAddr As String) As Integer

        Dim AccountId As Integer = 0

        Dim mySQLConnection As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)

        Dim myDataSet As DataSet = New DataSet

        Dim mySQLCommand As New Data.SqlClient.SqlCommand("GetActiveAccountByEmail", mySQLConnection)
        Dim mySQLAdapter As New Data.SqlClient.SqlDataAdapter

        mySQLCommand.CommandType = Data.CommandType.StoredProcedure
        mySQLCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = Trim(EmailAddr)
        mySQLAdapter.SelectCommand = mySQLCommand

        mySQLConnection.Open()
        mySQLAdapter.Fill(myDataSet, "GetActiveAccountByEmail")
        mySQLConnection.Close()

        If myDataSet.Tables(0).Rows.Count > 0 Then

            AccountId = myDataSet.Tables(0).Rows(0).Item("int_id")

        End If

        Return AccountId

    End Function

    Protected Sub BtnSignIn_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim myDataSet As DataSet = New DataSet

        Dim mySQLConnection As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)
        Dim mySQLDataAdapter As New SqlClient.SqlDataAdapter

        Dim mySQLCommand As New MySql.Data.MySqlClient.MySqlCommand("GetActivePersonByEmail", mySQLConnection)
        Dim mySQLAdapter As New MySql.Data.MySqlClient.MySqlDataAdapter

        mySQLCommand.CommandType = Data.CommandType.StoredProcedure
        mySQLCommand.Parameters.AddWithValue("@ThisEmail", TxtUserName.Text())

        mySQLAdapter.SelectCommand = mySQLCommand

        mySQLConnection.Open()
        mySQLAdapter.Fill(myDataSet, "GetActivePersonByEmail")
        mySQLConnection.Close()

        If myDataSet.Tables(0).Rows.Count > 0 Then

            If myDataSet.Tables(0).Rows(0).Item("Password") = TxtPassword.Text() Then

                'If myDataSet.Tables(0).Rows(0).Item("int_status") = 0 Then

                '    LblErrorMessage.Text = "The account you're using is currently disabled.<br /><br />Please contact the system administrator to enable your account."
                '    LblErrorMessage.Visible = True

                'ElseIf myDataSet.Tables(0).Rows(0).Item("AccountLock") = 1 Then

                '    LblErrorMessage.Text = "The account you're using is currently locked because of too many failed attempts.<br /><br />Please contact the system administrator to enable your account."
                '    LblErrorMessage.Visible = True

                'ElseIf myDataSet.Tables(0).Rows(0).Item("AccountAccess") = 0 Then

                '    LblErrorMessage.Text = "The account you're using is NOT configured to access Servant Scout.<br /><br />Please contact the system administrator to enable your account."
                '    LblErrorMessage.Visible = True

                'Else

                Dim StrFullName As String = myDataSet.Tables(0).Rows(0).Item("FirstName").ToString() & " " & myDataSet.Tables(0).Rows(0).Item("LastName").ToString()
                Dim StrEmail As String = myDataSet.Tables(0).Rows(0).Item("Email").ToString()
                Dim StrPhone As String = myDataSet.Tables(0).Rows(0).Item("Phone").ToString()

                Dim StrPermission As String = myDataSet.Tables(0).Rows(0).Item("Role")
                Dim TxtHostName As String = Request.UserHostName.ToString()
                Dim TxtHostAddr As String = Request.UserHostAddress.ToString()

                Session.Remove("SessionUserId")
                Session.Add("SessionUserId", myDataSet.Tables(0).Rows(0).Item("PersonId").ToString())

                Session.Remove("ClinicId")
                Session.Add("ClinicId", myDataSet.Tables(0).Rows(0).Item("ClinicId").ToString())

                Session.Add("StrHostName", TxtHostName)
                Session.Add("StrHostAddr", TxtHostAddr)

                Session.Add("FirstName", myDataSet.Tables(0).Rows(0).Item("FirstName").ToString())
                Session.Add("LastName", myDataSet.Tables(0).Rows(0).Item("LastName").ToString())

                Session.Add("Email", StrEmail)
                Session.Add("Phone", StrPhone)

                InsertMemberAccessRecord()

                Response.Redirect("ReachHome.aspx")
                'Response.Redirect("BuildClassicASPSessionsMembers.asp?UserId=" & myDataSet.Tables(0).Rows(0).Item("int_id") & "&Email=" & TxtUserName.Text())

                'End If

            Else

                'Dim IntFailed As Integer = myDataSet.Tables(0).Rows(0).Item("FailedLoginAttempts") + 1
                'Dim IntLocked As Integer = 0

                'If IntFailed = 6 Then
                '    IntLocked = 1
                'End If

                'Dim StrUpdateUser As String = "UPDATE tbl_Members SET FailedLoginAttempts = " & IntFailed & ", AccountLock = " & IntLocked & " WHERE int_id = " & myDataSet.Tables(0).Rows(0).Item("int_id") & ""

                'Dim myDataCommandLock As SqlClient.SqlCommand

                'myDataCommandLock = New SqlClient.SqlCommand(StrUpdateUser, mySQLConnection)
                'mySQLConnection.Open()
                'myDataCommandLock.ExecuteNonQuery()
                'mySQLConnection.Close()

                LblErrorMessage.Text = "Either your username or password is incorrect."
                LblErrorMessage.Visible = True

                'If IntFailed = 6 Then

                '    LblErrorMessage.Text = "Your account has been locked.<br /><br />Please contact the system administrator to enable your account."
                '    LblErrorMessage.Visible = True

                'End If

            End If

        Else

            LblErrorMessage.Text = "The username you specified does not exist in our system.<br /><br />Please contact the system administrator to enable your account."
            LblErrorMessage.Visible = True

        End If

    End Sub

    Function CheckIfNullValueInt(ByVal RandomInt As Object) As Object

        Dim ThisVariable As Object = 0

        If Not IsDBNull(RandomInt) Then
            ThisVariable = RandomInt
        End If

        Return ThisVariable

    End Function

    Public Sub InsertMemberAccessRecord()

        Try

            Dim mySQLConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ReachMysql").ConnectionString)

            Dim mySQLCommand As New SqlCommand("RecordMemberTransaction", mySQLConnection)
            Dim mySQLAdapter As New SqlDataAdapter

            mySQLCommand.CommandType = Data.CommandType.StoredProcedure

            mySQLCommand.Parameters.AddWithValue("@MemberID", Session("MemberId"))
            mySQLCommand.Parameters.AddWithValue("@TransactionType", 8)
            mySQLCommand.Parameters.AddWithValue("@TransactionOwner", Session("FullName"))

            mySQLConnection.Open()
            mySQLCommand.ExecuteNonQuery()
            mySQLConnection.Close()

        Catch ex As Exception



        End Try

    End Sub

    Protected Sub EmailPerson(ByVal EmailAddress As String, ByVal NewPassword As String)

        Dim EmailMessage As String = ""
        Dim Today As Date = FormatDateTime(Now(), DateFormat.ShortDate)

        Dim FormMail As New MailMessage
        Dim Smtp As New SmtpClient(ConfigurationManager.AppSettings.Item("MandrillSMTP").ToString())

        Smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings.Item("MandrillUserName").ToString(), ConfigurationManager.AppSettings.Item("MandrillAPIKey").ToString())
        Smtp.Port = ConfigurationManager.AppSettings.Item("MandrillPort").ToString()

        EmailMessage = EmailMessage & "<div style='width: 894px; text-align: left;'><div style='width: 894px; text-align: left;'><img src='" & ConfigurationManager.AppSettings("MasterUrl") & "/Contentment/Images/Header/ChurchEmailHeader.png' border='0px' /></div><div style='padding: 20px 0px 30px 6px;'>"

        EmailMessage = EmailMessage & "Your account has been unlocked and a new password has been assigned to you. Your new password is below.<br /><br />"

        EmailMessage = EmailMessage & NewPassword & "<br /><br />"

        EmailMessage = EmailMessage & "<a href='" & ConfigurationManager.AppSettings.Item("MasterUrl").ToString() & "Contentment/Default.aspx'>Click here to access your account.</a>"

        EmailMessage = EmailMessage & "</div></div>"

        Try

            FormMail.From = New MailAddress(ConfigurationManager.AppSettings.Item("SystemNotificationFromAddress").ToString())
            FormMail.To.Add(EmailAddress)
            FormMail.Bcc.Add(ConfigurationManager.AppSettings.Item("ServantScoutNotificationAddress").ToString())
            FormMail.Subject = "Account Help"
            FormMail.Body = EmailMessage
            FormMail.IsBodyHtml = True

            Smtp.Send(FormMail)

        Catch

        End Try

    End Sub

    Function Sha256Encrypt(ByVal ClearString As String) As String

        Dim uEncode As New UnicodeEncoding()
        Dim bytClearString() As Byte = uEncode.GetBytes(ClearString)
        Dim sha As New System.Security.Cryptography.SHA256Managed()
        Dim hash() As Byte = sha.ComputeHash(bytClearString)
        Return Convert.ToBase64String(hash)

        Return ClearString

    End Function

    Protected Sub BtnCreateAccount_Click(sender As Object, e As EventArgs)



    End Sub

End Class
