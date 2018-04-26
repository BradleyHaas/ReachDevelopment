Imports System.Net.Mail
Imports System.Web
Imports System.Web.UI
Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class ReachApp

    Inherits System.Web.UI.MasterPage

    Public Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack() Then

            If Request.QueryString("NoAuth") = 1 Then

                BtnQuickMessage.Visible = False
                LnkMySettings.Visible = False
                LnkSignOut.Visible = False

            Else

                'GetMyAppsGrid()

            End If

            Page.Title = ConfigurationManager.AppSettings.Item("SiteTitle").ToString()

        End If

    End Sub

    Protected Sub GetMyAppsGrid()

        Dim mySQLConnection As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SQLContentment").ConnectionString)

        Dim myDataSet As DataSet = New DataSet

        Dim mySQLCommand As New Data.SqlClient.SqlCommand("GetMyAppsGrid", mySQLConnection)
        Dim mySQLAdapter As New Data.SqlClient.SqlDataAdapter

        mySQLCommand.CommandType = Data.CommandType.StoredProcedure
        mySQLCommand.Parameters.Add("@PersonId", SqlDbType.Int).Value = Int(Session("SessionUserId"))

        mySQLAdapter.SelectCommand = mySQLCommand

        mySQLConnection.Open()
        mySQLAdapter.Fill(myDataSet, "GetMyAppsGrid")
        mySQLConnection.Close()

        MyAppsList.ShowFooter = False
        MyAppsList.ShowHeader = False
        MyAppsList.RepeatDirection = RepeatDirection.Horizontal
        'MyAppsList.RepeatColumns = 5

        MyAppsList.DataSource = myDataSet
        MyAppsList.DataBind()

    End Sub

    Function BuildAppUrl(ByVal AppUrl As Object, ByVal ThisUniqueId As Object) As String

        Dim CompleteUrl As String = "~/Contentment/AppsGrid.aspx"
        Dim ThisGuid As Guid = New Guid(ThisUniqueId.ToString())

        If Not IsDBNull(AppUrl) Then

            If InStr(AppUrl.ToString(), "?") > 0 Then
                CompleteUrl = AppUrl & "&AppId=" & ThisGuid.ToString()
            Else
                CompleteUrl = AppUrl & "?AppId=" & ThisGuid.ToString()
            End If

        End If

        Return CompleteUrl

    End Function

    Function GetImageUrl(ByVal ImageName As Object) As String

        Dim CompleteUrl As String = ""

        If Not IsDBNull(ImageName) Then
            CompleteUrl = "~/Contentment/Images/Apps/" & ImageName.ToString()
        End If

        Return CompleteUrl

    End Function

    Protected Sub BtnQuickMessage_Click(ByVal sender As Object, ByVal e As EventArgs)

        DisplayQuickMessagePopUp.Show()

    End Sub

    Protected Sub BtnSendMessage_Click(ByVal sender As Object, ByVal e As EventArgs)

        SendNotificationEmail()
        TxtQuickMessage.Text = ""
        DisplayQuickMessagePopUp.Hide()

    End Sub

    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)

        DisplayQuickMessagePopUp.Hide()

    End Sub

    Protected Sub SendNotificationEmail()

        Dim EmailMessage As String = ""
        Dim Today As Date
        Dim myDataSet As DataSet = New DataSet

        Today = FormatDateTime(Now(), DateFormat.ShortDate)

        Dim FormMail As New MailMessage
        Dim Smtp As New SmtpClient(ConfigurationManager.AppSettings.Item("MandrillSMTP").ToString())

        Smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings.Item("MandrillUserName").ToString(), ConfigurationManager.AppSettings.Item("MandrillAPIKey").ToString())
        Smtp.Port = ConfigurationManager.AppSettings.Item("MandrillPort").ToString()

        Dim StrSubject As String = "System Quick Message"
        Dim StrFromAddr As String = ConfigurationManager.AppSettings.Item("SystemNotificationFromAddress").ToString()
        Dim StrReplyToAddr As String = Session("Email")
        Dim StrFromName As String = Session("FullName")

        FormMail.From = New MailAddress(StrFromAddr, StrFromName)
        FormMail.ReplyToList.Add(New MailAddress(StrReplyToAddr, "reply-to"))

        EmailMessage = EmailMessage & "<div style='width: 894px; text-align: left;'><div style='width: 894px; text-align: left;'><img src='" & ConfigurationManager.AppSettings("MasterUrl") & "/Contentment/Images/Header/EmailHeader.png' border='0px' /></div><div style='padding: 20px 0px 30px 6px;'>"

        EmailMessage = EmailMessage & "Message From: " & Session("FullName") & "<br /><br />"

        EmailMessage = EmailMessage & TxtQuickMessage.Text() & "<br /><br />"

        EmailMessage = EmailMessage & "Email submitted: " & Now() & "</div></div>"

        Try

            FormMail.From = New MailAddress(StrFromAddr, StrFromName)

            If QuickMessageType.SelectedValue = "Support" Then
                FormMail.To.Add("support@servantscout.com")
            Else
                FormMail.To.Add(ConfigurationManager.AppSettings.Item("AdminNotificationAddress").ToString())
            End If

            FormMail.Subject = StrSubject
            FormMail.Body = EmailMessage
            FormMail.IsBodyHtml = True

            Smtp.Send(FormMail)

        Catch



        End Try

        EmailMessage = ""

        FormMail.To.Clear()

    End Sub

End Class