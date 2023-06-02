Public Class Form1
    Dim namaFile As String
    Dim ygdicari As String
    Dim posisihasilcari As Integer
    Dim StringToPrint As String

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim dr As DialogResult
        If txtNotepad.Modified Then
            dr = MessageBox.Show("Do you want to save the changes?", "NotepadKu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If (dr = DialogResult.Cancel) Then
                'do nothing
            ElseIf (dr = DialogResult.No) Then
                Me.Text = "NotepadKu"
                namaFile = String.Empty
                txtNotepad.Clear()
                txtNotepad.SelectionStart = 0
            ElseIf dr = DialogResult.Yes Then
                saveToolStripMenuItem_Click(sender, e)
                Me.Text = "NotepadKu"
                namaFile = String.Empty
                txtNotepad.Clear()
                txtNotepad.SelectionStart = 0
            End If
        Else
            Me.Text = "NotepadKu"
            namaFile = String.Empty
            txtNotepad.Clear()
            txtNotepad.SelectionStart = 0
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim dr As DialogResult
        If txtNotepad.Modified Then
            dr = MessageBox.Show("Do you want to save the changes?", "NotepadKu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If dr = DialogResult.No Then
                dr = OpenFileDialog1.ShowDialog()
                If dr = DialogResult.OK Then
                    txtNotepad.Text = System.IO.File.ReadAllText(OpenFileDialog1.FileName)
                End If
            ElseIf dr = DialogResult.Cancel Then
                'do nothing
            ElseIf dr = DialogResult.Yes Then
                SaveToolStripMenuItem_Click(sender, e)
            End If
        Else
            dr = OpenFileDialog1.ShowDialog()
            If dr = DialogResult.OK Then
                txtNotepad.Text = System.IO.File.ReadAllText(OpenFileDialog1.FileName)
                namaFile = OpenFileDialog1.FileName
            End If
        End If
        Me.Text = "NotepadKu - (" & namaFile & ")"
        txtNotepad.SelectionStart = 0
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Dim dr As DialogResult
        If namaFile = String.Empty Then
            dr = SaveFileDialog1.ShowDialog()
            If dr = DialogResult.OK Then
                System.IO.File.WriteAllText(SaveFileDialog1.FileName, txtNotepad.Text)
                namaFile = SaveFileDialog1.FileName
            End If
        Else
            System.IO.File.WriteAllText(namaFile, txtNotepad.Text)
        End If
        Me.Text = "NotepadKu - (" & namaFile & ")"
        txtNotepad.SelectionStart = 0
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Dim dr As DialogResult
        dr = SaveFileDialog1.ShowDialog()
        If dr = DialogResult.OK Then
            System.IO.File.WriteAllText(SaveFileDialog1.FileName, txtNotepad.Text)
            namaFile = SaveFileDialog1.FileName
        End If
        Me.Text = "NotepadKu - (" & namaFile & ")"
        txtNotepad.SelectionStart = 0
    End Sub

    Private Sub PageSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PageSetupToolStripMenuItem.Click
        Dim dr As DialogResult
        PageSetupDialog1.Document = PrintDocument1
        PageSetupDialog1.Document.DefaultPageSettings.Color = False
        dr = PageSetupDialog1.ShowDialog()
        If dr = DialogResult.OK Then
            PrintDocument1.DefaultPageSettings = PageSetupDialog1.PageSettings
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        Dim dr As DialogResult
        StringToPrint = txtNotepad.Text
        PrintDocument1.DocumentName = "NotepadKu"
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        PrintDialog1.Document = PrintDocument1
        dr = PrintDialog1.ShowDialog()
        If dr = DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        NewToolStripMenuItem_Click(sender, e)
        txtNotepad.Clear()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Dim dr As DialogResult
        If txtNotepad.Modified Then
            dr = MessageBox.Show("Do you want to save the changes?", "NotepadKu", MessageBoxButtons.YesNoCancel)
            If dr = DialogResult.Yes Then
                SaveAsToolStripMenuItem_Click(sender, e)
                Application.Exit()
            ElseIf dr = DialogResult.Cancel Then
                'do nothing
            ElseIf dr = DialogResult.No Then
                Application.Exit()
            End If
        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        If txtNotepad.CanUndo Then
            txtNotepad.Undo()
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        Clipboard.Clear()
        If txtNotepad.SelectionLength > 0 Then
            txtNotepad.Cut()
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Clipboard.Clear()
        If txtNotepad.SelectionLength > 0 Then
            txtNotepad.Copy()
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        If Clipboard.ContainsText() Then
            txtNotepad.Paste()
        End If
    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        ygdicari = InputBox("Enter text to be found : ", "Find", "", 100, 100)
        posisihasilcari = InStr(1, txtNotepad.Text, ygdicari, CompareMethod.Text)
        If posisihasilcari > 0 Then
            txtNotepad.Focus()
            txtNotepad.SelectionStart = posisihasilcari - 1
            txtNotepad.SelectionLength = ygdicari.Length
        Else
            MessageBox.Show("Text is Not Found.")
        End If
    End Sub

    Private Sub GoToLine(ByVal tb As TextBox, ByVal Line As Integer)
        Dim idx As Integer = 0
        For x As Integer = 0 To Line - 1
            idx += 1
        Next
        idx += Environment.NewLine.Length
        tb.SelectionStart = idx
        tb.SelectionLength = 0
        tb.Select()
    End Sub

    Private Sub GoToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoToToolStripMenuItem.Click
        Dim baris As String = InputBox("Go To Line: ", "Go to Line", "", 100, 100)
        GoToLine(txtNotepad, CInt(baris))
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        txtNotepad.SelectAll()
    End Sub

    Private Sub InsertDateTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertDateTimeToolStripMenuItem.Click
        txtNotepad.SelectedText = Convert.ToString(Now())
    End Sub

    Private Sub WordWrapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WordWrapToolStripMenuItem.Click
        txtNotepad.WordWrap = Not txtNotepad.WordWrap
        WordWrapToolStripMenuItem.Checked = Not WordWrapToolStripMenuItem.Checked
    End Sub

    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        Dim dr As DialogResult
        dr = FontDialog1.ShowDialog()
        If dr = DialogResult.OK Then
            txtNotepad.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        StatusStrip1.Visible = Not StatusStrip1.Visible
        StatusBarToolStripMenuItem.Checked = Not StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        MessageBox.Show("NotepadKu dibuat oleh Eldad Rizaldi A (C) 2023")
    End Sub

    Private Sub Form1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.CapsLock Then
            capsStripStatus.Enabled = Not capsStripStatus.Enabled
        End If
        If e.KeyCode = Keys.NumLock Then
            numStripStatus.Enabled = Not numStripStatus.Enabled
        End If
        If (e.KeyCode = Keys.Insert) AndAlso (insertStripStatus.Text = "INS") Then
            insertStripStatus.Text = "OVR"
        Else
            insertStripStatus.Text = "INS"
        End If
    End Sub

    Private Sub txtNotepad_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtNotepad.KeyUp
        Dim line As Integer = txtNotepad.GetLineFromCharIndex(txtNotepad.SelectionStart) + 1
        toolStripStatusLine.Text = "Line: " & line
    End Sub

    Private Sub FindNextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindNextToolStripMenuItem.Click
        posisihasilcari = InStr(posisihasilcari + 1, txtNotepad.Text, ygdicari,
CompareMethod.Text)
        If posisihasilcari > 0 Then
            txtNotepad.Focus()
            txtNotepad.SelectionStart = posisihasilcari - 1
            txtNotepad.SelectionLength = ygdicari.Length
        Else
            MessageBox.Show("Text is not found.")
        End If

    End Sub
End Class
