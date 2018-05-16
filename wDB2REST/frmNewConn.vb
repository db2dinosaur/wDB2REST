Imports System
Imports Microsoft.Win32
' certificate stuff
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
' Also reference to System.Security

Public Class frmNewConn
    Public myParent As frmMain
    Public myCaller As Form

    Public Sub SetParent(ByRef caller As Form, ByRef creator As frmMain)
        myCaller = caller
        myParent = creator
    End Sub

    Private Sub rbBasicHTTP_CheckedChanged(sender As Object, e As EventArgs) Handles rbBasicHTTP.CheckedChanged
        If rbBasicHTTP.Checked Then
            rbBasicHTTPS.Checked = False
        End If
    End Sub

    Private Sub rbBasicHTTPS_CheckedChanged(sender As Object, e As EventArgs) Handles rbBasicHTTPS.CheckedChanged
        If rbBasicHTTPS.Checked Then
            rbBasicHTTP.Checked = False
        End If
    End Sub

    Private Sub rbHTTPCertFile_CheckedChanged(sender As Object, e As EventArgs) Handles rbHTTPCertFile.CheckedChanged
        If rbHTTPCertFile.Checked Then
            rbHTTPSCertFile.Checked = False
        End If
    End Sub

    Private Sub rbHTTPSCertFile_CheckedChanged(sender As Object, e As EventArgs) Handles rbHTTPSCertFile.CheckedChanged
        If rbHTTPSCertFile.Checked Then
            rbHTTPCertFile.Checked = False
        End If
    End Sub

    Private Sub btnOkayBasic_Click(sender As Object, e As EventArgs) Handles btnOkayBasic.Click
        ' verify that name is not already in use
        Dim testHost As frmMain.Hosts = Nothing
        Try
            testHost = myParent.allHosts.Item(txtName.Text)
        Catch err As System.Collections.Generic.KeyNotFoundException
        End Try
        If Not testHost Is Nothing Then
            MsgBox(String.Format("Connection {0} is already defined", txtName.Text), MsgBoxStyle.OkOnly, "wDB2REST New Connection")
        Else
            Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(frmMain.regHostList, True)
            ' define key
            rkey.CreateSubKey(txtName.Text)
            Dim rhost As RegistryKey = rkey.OpenSubKey(txtName.Text, True)
            ' define values within key
            rhost.SetValue(frmMain.regHostName, txtName.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regNetworkName, txtHost.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regNetworkPort, txtPort.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regUserid, txtUserid.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regPassword, txtPassword.Text, RegistryValueKind.String)
            Dim https As Byte() = {0}
            If rbBasicHTTPS.Checked Then
                https(0) += 1
            End If
            rhost.SetValue(frmMain.regSecure, https, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regUseCertificate, New Byte() {0}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regCertIsFile, New Byte() {0}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regCert, "", RegistryValueKind.String)
            rhost.Close()
            rkey.Close()
            Dim nh As frmMain.Hosts = New frmMain.Hosts(txtName.Text, txtHost.Text, txtPort.Text, rbBasicHTTPS.Checked, txtUserid.Text, txtPassword.Text)
            myParent.allHosts.Add(nh)
            Dim newlb As Integer = myParent.lbHosts.Items.Add(txtName.Text)
            myParent.lbHosts.SelectedIndex = newlb
            Dim lv As ListView = CType(myCaller.Controls(0), ListView)
            Dim nurl As String = "http"
            If rbBasicHTTPS.Checked Then
                nurl = "https"
            End If
            nurl += "://" + txtHost.Text + ":" + txtPort.Text + "/services/"
            Dim nlv As ListViewItem = New ListViewItem({txtName.Text, nurl, "User/Pwd", txtUserid.Text})
            lv.Items.Add(nlv)
            Me.Close()
        End If
    End Sub

    Private Sub btnCancelBasic_Click(sender As Object, e As EventArgs) Handles btnCancelBasic.Click
        Me.Close()
    End Sub

    Private Sub btnCancelCertFile_Click(sender As Object, e As EventArgs) Handles btnCancelCertFile.Click
        Me.Close()
    End Sub

    Private Sub btnOkayCertFile_Click(sender As Object, e As EventArgs) Handles btnOkayCertFile.Click
        ' verify that the name is not already in use:
        Dim testHost As frmMain.Hosts = Nothing
        Try
            testHost = myParent.allHosts.Item(txtNameCertFile.Text)
        Catch err As System.Collections.Generic.KeyNotFoundException
        End Try
        If Not testHost Is Nothing Then
            MsgBox(String.Format("Connection{0} is already defined", txtNameCertFile.Text), MsgBoxStyle.OkOnly, "wDB2REST New Connection")
        Else
            Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(frmMain.regHostList, True)
            rkey.CreateSubKey(txtNameCertFile.Text)
            Dim rhost As RegistryKey = rkey.OpenSubKey(txtNameCertFile.Text, True)
            ' define values within key
            rhost.SetValue(frmMain.regHostName, txtNameCertFile.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regNetworkName, txtHostCertFile.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regNetworkPort, txtPortCertFile.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regSecure, New Byte() {1}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regUseCertificate, New Byte() {1}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regCertIsFile, New Byte() {1}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regCert, txtPathCertFile.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regPassword, txtPwdCertFile.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regUserid, "", RegistryValueKind.String)
            rhost.Close()
            rkey.Close()
            ' update controls
            Dim nh As frmMain.Hosts = New frmMain.Hosts(txtNameCertFile.Text, txtHostCertFile.Text, txtPortCertFile.Text, True, True, txtPathCertFile.Text, txtPwdCertFile.Text)
            myParent.allHosts.Add(nh)
            Dim newlb As Integer = myParent.lbHosts.Items.Add(txtNameCertFile.Text)
            myParent.lbHosts.SelectedIndex = newlb
            Dim lv As ListView = CType(myCaller.Controls(0), ListView)
            Dim nurl As String = String.Format("https://{0}:{1}/services/", txtHostCertFile.Text, txtPortCertFile.Text)
            Dim nlv As ListViewItem = New ListViewItem({txtNameCertFile.Text, nurl, "Certificate", txtPathCertFile.Text})
            lv.Items.Add(nlv)
            Me.Close()
        End If
    End Sub

    Private Sub btnFilePickerCertFile_Click(sender As Object, e As EventArgs) Handles btnFilePickerCertFile.Click
        Dim f As OpenFileDialog = New OpenFileDialog()
        f.Filter = "PKCS12 Certificates (*.p12)|*.p12|All files (*.*)|*.*"
        f.Title = "Certificate File"
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If f.FileName <> "" Then
                txtPathCertFile.Text = f.FileName
            End If
        End If
    End Sub

    Private Sub btnCertPicker_Click(sender As Object, e As EventArgs) Handles btnCertPicker.Click
        Dim myClientCert As X509Certificate2 = Nothing
        Dim x509Store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
        Try
            ' create and open store for read-only access
            x509Store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)
            ' search store
            Dim col As New X509Certificate2Collection
            col = x509Store.Certificates()
            ' create shortlist of those CurrentUser certificates with a private key (i.e. can be client certs)
            Dim ccol As New X509Certificate2Collection
            For Each cert In col
                If cert.HasPrivateKey() Then
                    ccol.Add(cert)
                End If
            Next
            Dim f As frmCertPicker = New frmCertPicker(ccol)
            f.ShowDialog()
            myClientCert = f.Selected()
            If Not myClientCert Is Nothing Then
                txtSubjectCertStore.Text = myClientCert.Subject
            End If
        Catch err As Exception
        End Try
        x509Store.Close()
    End Sub

    Private Sub btnCancelCertStore_Click(sender As Object, e As EventArgs) Handles btnCancelCertStore.Click
        Me.Close()
    End Sub

    Private Sub btnOkayCertStore_Click(sender As Object, e As EventArgs) Handles btnOkayCertStore.Click
        ' verify that the name is not already in use:
        Dim testHost As frmMain.Hosts = Nothing
        Try
            testHost = myParent.allHosts.Item(txtNameCertStore.Text)
        Catch err As System.Collections.Generic.KeyNotFoundException
        End Try
        If Not testHost Is Nothing Then
            MsgBox(String.Format("Connection{0} is already defined", txtNameCertStore.Text), MsgBoxStyle.OkOnly, "wDB2REST New Connection")
        Else
            Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(frmMain.regHostList, True)
            rkey.CreateSubKey(txtNameCertStore.Text)
            Dim rhost As RegistryKey = rkey.OpenSubKey(txtNameCertStore.Text, True)
            ' define values within key
            rhost.SetValue(frmMain.regHostName, txtNameCertStore.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regNetworkName, txtHostNameCertStore.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regNetworkPort, txtHostPortCertStore.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regSecure, New Byte() {1}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regUseCertificate, New Byte() {1}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regCertIsFile, New Byte() {0}, RegistryValueKind.Binary)
            rhost.SetValue(frmMain.regCert, txtSubjectCertStore.Text, RegistryValueKind.String)
            rhost.SetValue(frmMain.regPassword, "", RegistryValueKind.String)
            rhost.SetValue(frmMain.regUserid, "", RegistryValueKind.String)
            rhost.Close()
            rkey.Close()
            ' update controls
            Dim nh As frmMain.Hosts = New frmMain.Hosts(txtNameCertStore.Text, txtHostNameCertStore.Text, txtHostPortCertStore.Text, True, False, txtSubjectCertStore.Text, "")
            myParent.allHosts.Add(nh)
            Dim newlb As Integer = myParent.lbHosts.Items.Add(txtNameCertStore.Text)
            myParent.lbHosts.SelectedIndex = newlb
            Dim lv As ListView = CType(myCaller.Controls(0), ListView)
            Dim nurl As String = String.Format("https://{0}:{1}/services/", txtHostCertFile.Text, txtPortCertFile.Text)
            Dim nlv As ListViewItem = New ListViewItem({txtNameCertStore.Text, nurl, "Certificate", txtSubjectCertStore.Text})
            lv.Items.Add(nlv)
            Me.Close()
        End If
    End Sub
End Class