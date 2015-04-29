Imports Finisar.SQLite

Public Class config

    Private Sub config_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Carga de configuracion desde las variables locales
        TextBox1.Text = DBRoute
        Nombre.Text = GlobNomEmpresa
        Nit.Text = GlobNitEmpresa
        Direccion.Text = GlobDirEmpresa
        Tel.Text = GlobTelEmpresa
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text.Length = 0 Or DBRoute.Length = 0 Then
            MsgBox("La configuración de la base de datos debe realizarse")
        ElseIf Nombre.Text.Length = 0 Or Nit.Text.Length = 0 Or Direccion.Text.Length = 0 _
            Or Tel.Text.Length = 0 Then
            MsgBox("La informacion de la empresa debe realizarse")
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExaminarDB.ShowDialog()
        If ExaminarDB.FileName.Length > 0 Then
            TextBox1.Text = ExaminarDB.FileName
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Nombre.Enabled = True
        Nit.Enabled = True
        Direccion.Enabled = True
        Tel.Enabled = True
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Nombre.Text.Length = 0 Or Nit.Text.Length = 0 Or Direccion.Text.Length = 0 _
            Or Tel.Text.Length = 0 Then
            MsgBox("La informacion de la empresa debe realizarse")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DBRoute.Length > 0 Then
            If MsgBox("¿Desea crear una nueva base de datos?" & vbNewLine & _
                      "Esta reemplazara a la existente si se encuentra en la ruta de la aplicacion.", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                DBRoute = "FacturacionDB.tlc"
                crearDB()
            End If
        Else
            DBRoute = "FacturacionDB.tlc"
            crearDB()
        End If
    End Sub
    Sub crearDB()
        Dim objCon As SQLiteConnection
        Dim objCoomand As SQLiteCommand
        Try
            'Crear e iniciar DB
            objCon = New SQLiteConnection("Data Source=" & DBRoute & ";Version=3;" & "New=True;")
            objCon.Open()
            objCoomand = objCon.CreateCommand()

            '••Crear Tablas de DB••
            'Empresa
            objCoomand.CommandText = "CREATE TABLE empresa (nombre VARCHAR(25) Primary KEY, nit VARCHAR(15), " & _
                                    "direccion VARCHAR(30), telefono NUMERIC);"
            objCoomand.ExecuteNonQuery()

            'Clientes
            objCoomand.CommandText = "CREATE TABLE clientes (id INTEGER Primary KEY, nombre VARCHAR(25), nit VARCHAR(15)," & _
                                   "direccion VARCHAR(30), telefono NUMERIC);"
            objCoomand.ExecuteNonQuery()

            'Facturas
            objCoomand.CommandText = "CREATE TABLE facturas (consecutivo INTEGER Primary KEY, clienteid INTEGER," & _
                                   "fecha DATE, fpago INTEGER, ocompra NUMERIC, " & _
                                   "item1 INTEGER, item2 INTEGER, item3 INTEGER, item4 INTEGER, item5 INTEGER, " & _
                                   "item6 INTEGER, item7 INTEGER, item8 INTEGER, item9 INTEGER, item10 INTEGER, " & _
                                   "item11 INTEGER, item12 INTEGER, item13 INTEGER, item14 INTEGER, item15 INTEGER, " & _
                                   "item16 INTEGER, item17 INTEGER, item18 INTEGER, item19 INTEGER, item20 INTEGER, " & _
                                   "item21 INTEGER, item22 INTEGER, item23 INTEGER, item24 INTEGER, item25 INTEGER, " & _
                                   "item26 INTEGER, item27 INTEGER, item28 INTEGER, item29 INTEGER, item30 INTEGER, " & _
                                   "iva BOOLEAN, vtotal NUMERIC, estado INTEGER);"
            objCoomand.ExecuteNonQuery()

            'Items Facturas
            objCoomand.CommandText = "CREATE TABLE itemsfactura (id INTEGER Primary KEY, cantidad INTEGER," & _
                                   "vunitario NUMERIC, descripcion INTEGER);"
            objCoomand.ExecuteNonQuery()

            'Productos y servicios
            objCoomand.CommandText = "CREATE TABLE productoservicio (id INTEGER Primary KEY, nombre TEXT," & _
                                   "precio NUMERIC);"
            objCoomand.ExecuteNonQuery()



            
        Catch ex As Exception
            DBRoute = ""
            MsgBox(ex.Message)
            Exit Sub
        Finally
            If Not IsNothing(objCon) Then
                objCon.Close()
            End If
        End Try
        MsgBox("Base de datos creada")
    End Sub
End Class