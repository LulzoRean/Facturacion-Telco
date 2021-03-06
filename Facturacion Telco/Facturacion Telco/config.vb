﻿Imports Finisar.SQLite

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
        Button3.Enabled = True
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Nombre.Text.Length = 0 Or Nit.Text.Length = 0 Or Direccion.Text.Length = 0 _
            Or Tel.Text.Length = 0 Then
            MsgBox("La informacion de la empresa debe realizarse")
        Else
            'Guardar DB datos de la empresa
            Dim objCon As SQLiteConnection
            Dim objCoomand As SQLiteCommand
            Dim objReader As SQLiteDataReader
            Try
                objCon = New SQLiteConnection("Data Source=" & DBRoute & ";Version=3;")
                objCon.Open()
                objCoomand = objCon.CreateCommand()

                objCoomand.CommandText = "SELECT * from empresa"
                objReader = objCoomand.ExecuteReader()
                objReader.Read()

                If objReader.IsDBNull(0) Then
                    'Si no existe la crea
                    objReader.Close()
                    objCoomand.CommandText = "Insert into empresa(nombre, nit, direccion, telefono)" & _
                   "values('" & Nombre.Text & "','" & Nit.Text & "','" & Direccion.Text & "','" & Tel.Text & "');"
                    objCoomand.ExecuteNonQuery()
                Else
                    'si existe la actualiza
                    objReader.Close()
                    objCoomand.CommandText = "UPDATE empresa " & _
                                            "SET nombre='" & Nombre.Text & "', nit='" & Nit.Text & _
                                            "', direccion='" & Direccion.Text & "', telefono='" & Tel.Text & "';"
                    objCoomand.ExecuteNonQuery()
                End If

                Try
                    Dim FILE_NAME As String = "config.ini"
                    Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
                    objWriter.WriteLine(TextBox1.Text)
                    objWriter.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If Not IsNothing(objCon) Then
                    objCon.Close()
                End If
                Me.Close()
            End Try
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
            objCoomand.CommandText = "CREATE TABLE empresa (nombre VARCHAR(30) Primary KEY, nit VARCHAR(15), " & _
                                    "direccion VARCHAR(40), telefono NUMERIC);"
            objCoomand.ExecuteNonQuery()

            'Clientes
            objCoomand.CommandText = "CREATE TABLE clientes (id INTEGER Primary KEY, nombre VARCHAR(30), nit VARCHAR(15)," & _
                                   "direccion VARCHAR(40), telefono NUMERIC);"
            objCoomand.ExecuteNonQuery()

            'Facturas
            objCoomand.CommandText = "CREATE TABLE facturas (consecutivo INTEGER Primary KEY, clienteid INTEGER," & _
                                   "fecha DATE, fpago INTEGER, ocompra NUMERIC, item1 INTEGER, garantia TEXT, " & _
                                   "iva BOOLEAN, vtotal NUMERIC, estado INTEGER);"
            objCoomand.ExecuteNonQuery()

            'Items Facturas
            objCoomand.CommandText = "CREATE TABLE itemsfactura (id INTEGER Primary KEY, factura INTEGER FOREIGN KEY REFERENCES facturas(consecutivo), cantidad INTEGER," & _
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
        TextBox1.Text = DBRoute
    End Sub

    Private Sub Nombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Nombre.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Nit.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub Nit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Nit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Direccion.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub Direccion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Direccion.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Tel.Select()
            e.Handled = True
        End If
    End Sub

    Private Sub Tel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tel.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Button3.Select()
            e.Handled = True
        End If
    End Sub
End Class