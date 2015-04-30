Imports Finisar.SQLite

Public Class Principal
    Dim col As New AutoCompleteStringCollection()

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        config.ShowDialog()
    End Sub

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
1:          Dim FILE_NAME As String = "Config.ini"
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objReader As New System.IO.StreamReader(FILE_NAME)
                DBRoute = objReader.ReadToEnd
                objReader.Close()
                CargarEmpresa()
                GroupBox1.Enabled = True
                Button1.Enabled = True
                Button2.Enabled = True
                LeerNombre()
            Else
                config.ShowDialog()
                GoTo 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub LeerNombre()
        Dim objConn As SQLiteConnection
        Dim objcommand As SQLiteCommand
        Dim objReader As SQLiteDataReader

        Try
            objConn = New SQLiteConnection("Data Source=" & DBRoute & ";Version=3;")
            objConn.Open()
            objcommand = objConn.CreateCommand()
            objcommand.CommandText = "SELECT nombre FROM clientes"
            objReader = objcommand.ExecuteReader()


            While (objReader.Read())
                col.Add(objReader("nombre").ToString)
            End While


            If col.Count > 0 Then
                ClientTextNm.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                ClientTextNm.AutoCompleteCustomSource = col
                ClientTextNm.AutoCompleteSource = AutoCompleteSource.CustomSource
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not IsNothing(objConn) Then
                objConn.Close()
            End If
        End Try
    End Sub
  
    Private Sub ClientTextNm_KeyUp(sender As Object, e As KeyEventArgs) Handles ClientTextNm.KeyUp
        If e.KeyCode = (Keys.Return) Then
            Dim objConn As SQLiteConnection
            Dim objcommand As SQLiteCommand
            Dim objReader As SQLiteDataReader
            Dim list As New ArrayList
            Try
                objConn = New SQLiteConnection("Data Source=" & DBRoute & ";Version=3;")
                objConn.Open()
                objcommand = objConn.CreateCommand()
                objcommand.CommandText = "SELECT * FROM clientes WHERE nombre='" & _
                                        ClientTextNm.Text & "';"
                objReader = objcommand.ExecuteReader()


                While (objReader.Read())
                    list.Add(objReader("nit").ToString)
                    list.Add(objReader("direccion").ToString)
                    list.Add(objReader("telefono").ToString)
                End While
                If list.Count > 0 Then
                    ClientNit.Text = list(0)
                    ClientDireccion.Text = list(1)
                    ClientTel.Text = list(2)
                End If
                ClientCondi.Select()
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                If Not IsNothing(objConn) Then
                    objConn.Close()
                End If
            End Try
            e.Handled = True
        End If
    End Sub

    Private Sub ClientNit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ClientNit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            ClientDireccion.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub ClientDireccion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ClientDireccion.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            ClientTel.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub ClientTel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ClientTel.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            ClientCondi.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub ClientCondi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ClientCondi.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            ClientOc.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub ClientOc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ClientOc.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Nextbt.Select()
            e.Handled = True
        End If
    End Sub
End Class
