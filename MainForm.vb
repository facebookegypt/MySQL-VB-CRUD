Imports MySql.Data.MySqlClient
Public Class MainForm
    Private connectionString As String = "Server=localhost;port=3307;Database=ProductDB;User Id=root;Password=AhmedSamir1_;"
    Private Sub LoadProducts()
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT * FROM Products"
            Dim adapter As New MySqlDataAdapter(query, connection)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            DataGridView1.DataSource = dataTable
        End Using
    End Sub
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        LoadProducts()
    End Sub
    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@Name", TxtName.Text)
                command.Parameters.AddWithValue("@Price", Decimal.Parse(TxtPrice.Text))
                command.Parameters.AddWithValue("@Stock", Integer.Parse(TxtStock.Text))
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
        LoadProducts()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim productId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("ProductID").Value)
            Using connection As New MySqlConnection(connectionString)
                Dim query As String = "UPDATE Products SET Name=@Name, Price=@Price, Stock=@Stock WHERE ProductID=@ProductID"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Name", TxtName.Text)
                    command.Parameters.AddWithValue("@Price", Decimal.Parse(TxtPrice.Text))
                    command.Parameters.AddWithValue("@Stock", Integer.Parse(TxtStock.Text))
                    command.Parameters.AddWithValue("@ProductID", productId)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
            LoadProducts()
        Else
            MessageBox.Show("Please select a product to update.")
        End If
    End Sub
    Private Sub dataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            TxtName.Text = DataGridView1.SelectedRows(0).Cells("Name").Value.ToString()
            TxtPrice.Text = DataGridView1.SelectedRows(0).Cells("Price").Value.ToString()
            TxtStock.Text = DataGridView1.SelectedRows(0).Cells("Stock").Value.ToString()
        End If
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim productId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("ProductID").Value)
            Using connection As New MySqlConnection(connectionString)
                Dim query As String = "DELETE FROM Products WHERE ProductID=@ProductID"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ProductID", productId)
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
            LoadProducts()
        Else
            MessageBox.Show("Please select a product to delete.")
        End If
    End Sub
End Class
