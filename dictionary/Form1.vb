Imports System.ComponentModel
Imports System.Threading.Thread
Public Class Form1
    Dim search, searchnew, index As String
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchnew = Clipboard.GetText
        search = searchnew
        Me.Location = New System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - Me.Width,
Screen.PrimaryScreen.Bounds.Height - Me.Height - 100)
        WebBrowser1.ScriptErrorsSuppressed = True
        Timer1.Enabled = Enabled
        ' Me.Visible = False
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Me.Visible = False
        Timer1.Enabled = True
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Visible = False
        'Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        On Error Resume Next
        ' On Error Resume Next
        searchnew = Clipboard.GetText
        Label1.Text = searchnew
        If Clipboard.GetText = "Xclose" Then
            Application.Exit()
        End If
        If searchnew <> search Then
            search = searchnew
            Me.WebBrowser1.Navigate("http://tureng.com/tr/turkce-ingilizce/" + Replace(search, " ", "%20"))
            Do Until WebBrowser1.ReadyState = WebBrowserReadyState.Complete Or Clipboard.GetText = "Xclose"
                Application.DoEvents()
            Loop
            index = WebBrowser1.DocumentText
            If index.Contains("Sanırız yanlış oldu, doğrusu şunlar olabilir mi?") Then
                ' MessageBox.Show("yes")
            Else
                Timer1.Enabled = False
                Me.Visible = True
                Me.Refresh()
                WebBrowser1.Document.Window.ScrollTo(0, 220)
                Timer2.Enabled = Enabled

            End If
        End If


    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Clipboard.GetText = "Xclose" Then
            'Application.Exit()
            ' Me.Close()
        Else
            e.Cancel = True
            Timer2.Enabled = False
            Timer1.Enabled = True
            Me.Visible = False
        End If
    End Sub
End Class
