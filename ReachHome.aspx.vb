

Partial Class ReachHome

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            'Session.Abandon()

        End If

    End Sub

    Protected Sub BtnMyPatients_Click(sender As Object, e As EventArgs)



    End Sub

    Protected Sub BtnAllPatients_Click(sender As Object, e As EventArgs)



    End Sub

    Protected Sub BtnFilters_Click(sender As Object, e As EventArgs)



    End Sub

    Protected Sub BtnAddPatient_Click(sender As Object, e As EventArgs)



    End Sub

End Class
