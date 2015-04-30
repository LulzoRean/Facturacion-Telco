Imports Finisar.SQLite

Module Globals
    Public DBRoute As String = ""
    Public GlobNomEmpresa As String = ""
    Public GlobNitEmpresa As String = ""
    Public GlobDirEmpresa As String = ""
    Public GlobTelEmpresa As String = ""

    Public cadena_conexion As String = "Data Source=" & DBRoute & ";Version=3;"

    Public GlobFacCliente As String = ""
    Public GlobfacNit As String = ""
    Public GlobFacDir As String = ""
    Public GlobFacTel As String = ""
    Public GlobFactFpago As String = ""
    Public GlobFactOC As String = ""
    Public GlobFactDate As String = ""

    Public Sub CargarEmpresa()
        Dim objConn As SQLiteConnection
        Dim objcommand As SQLiteCommand
        Dim objReader As SQLiteDataReader
        Dim list As New ArrayList

        Try
            objConn = New SQLiteConnection("Data Source=" & DBRoute & ";Version=3;")
            objConn.Open()
            objcommand = objConn.CreateCommand()
            objcommand.CommandText = "SELECT * from empresa"
            objReader = objcommand.ExecuteReader()


            While (objReader.Read())
                list.Add(objReader("nombre"))
                list.Add(objReader("nit"))
                list.Add(objReader("direccion"))
                list.Add(objReader("telefono"))
            End While


            GlobNomEmpresa = list.Item(0).ToString

            GlobNitEmpresa = list(1).ToString

            GlobDirEmpresa = list(2).ToString

            GlobTelEmpresa = list(3).ToString


        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not IsNothing(objConn) Then
                objConn.Close()
            End If
        End Try
    End Sub
End Module
