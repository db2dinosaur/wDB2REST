' registry bits
Imports Microsoft.Win32
' certificate stuff
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Public Class frmCertPicker
    Public Function Selected() As X509Certificate2
        Selected = mySelectedCert
    End Function

    Private myCerts As X509Certificate2Collection
    Private mySelectedCert As X509Certificate2

    Public Sub New(ByRef certs As X509Certificate2Collection)
        Me.InitializeComponent()
        myCerts = certs
        mySelectedCert = Nothing
    End Sub

    Private Sub frmCertPicker_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' get sizes from registry
        Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(frmMain.regConfig)
        Me.Width = rkey.GetValue(frmMain.regCertFormWidth, 600)
        Me.Height = rkey.GetValue(frmMain.regCertFormHeight, 400)
        rkey.Close()
        For Each cert As X509Certificate2 In myCerts
            Dim lvi As ListViewItem = New ListViewItem({cert.GetNameInfo(X509NameType.SimpleName, False), cert.Subject, cert.NotBefore.ToLocalTime.ToString(), cert.NotAfter.ToLocalTime.ToString(), cert.Issuer})
            lvCerts.Items.Add(lvi)
        Next
    End Sub

    Private Sub frmCertPicker_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        lvCerts.Width = Me.Width - 10
        lvCerts.Height = Me.Height - 20
    End Sub

    Private Sub frmCertPicker_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        ' save width and height
        Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(frmMain.regConfig, True)
        rkey.SetValue(frmMain.regCertFormHeight, Me.Height)
        rkey.SetValue(frmMain.regCertFormWidth, Me.Width)
        rkey.Close()
    End Sub

    Private Sub lvCerts_DoubleClick(sender As Object, e As EventArgs) Handles lvCerts.DoubleClick
        If lvCerts.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = lvCerts.SelectedItems(0)
            Dim name As String = lvi.SubItems(0).Text
            Dim subject As String = lvi.SubItems(1).Text
            Dim found As Boolean = False
            Dim curCert As X509Certificate2
            For Each curCert In myCerts
                If curCert.Subject = subject Then
                    mySelectedCert = curCert
                    Exit For
                End If
            Next
            If Not mySelectedCert Is Nothing Then
                Me.Close()
            End If
        End If
    End Sub
End Class