Public Class Principal

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        config.ShowDialog()
    End Sub

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim FILE_NAME As String = "Config.ini"
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objReader As New System.IO.StreamReader(FILE_NAME)
                DBRoute = objReader.ReadToEnd
                objReader.Close()
                CargarEmpresa()
                GroupBox1.Enabled = True
                Button1.Enabled = True
                Button2.Enabled = True
            Else
                config.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
