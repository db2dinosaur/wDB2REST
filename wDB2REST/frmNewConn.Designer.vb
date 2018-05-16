<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewConn
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tabNewConnection = New System.Windows.Forms.TabControl()
        Me.tpgBasic = New System.Windows.Forms.TabPage()
        Me.btnCancelBasic = New System.Windows.Forms.Button()
        Me.btnOkayBasic = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lbPwd = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lbUserid = New System.Windows.Forms.Label()
        Me.txtUserid = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.rbBasicHTTPS = New System.Windows.Forms.RadioButton()
        Me.rbBasicHTTP = New System.Windows.Forms.RadioButton()
        Me.tpgCertFile = New System.Windows.Forms.TabPage()
        Me.btnFilePickerCertFile = New System.Windows.Forms.Button()
        Me.btnCancelCertFile = New System.Windows.Forms.Button()
        Me.btnOkayCertFile = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNameCertFile = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPwdCertFile = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPathCertFile = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPortCertFile = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtHostCertFile = New System.Windows.Forms.TextBox()
        Me.rbHTTPSCertFile = New System.Windows.Forms.RadioButton()
        Me.rbHTTPCertFile = New System.Windows.Forms.RadioButton()
        Me.tpgCertStore = New System.Windows.Forms.TabPage()
        Me.btnCertPicker = New System.Windows.Forms.Button()
        Me.btnCancelCertStore = New System.Windows.Forms.Button()
        Me.btnOkayCertStore = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtNameCertStore = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtSubjectCertStore = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtHostPortCertStore = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtHostNameCertStore = New System.Windows.Forms.TextBox()
        Me.rbHTTPSCertStore = New System.Windows.Forms.RadioButton()
        Me.rbHTTPCertStore = New System.Windows.Forms.RadioButton()
        Me.tabNewConnection.SuspendLayout()
        Me.tpgBasic.SuspendLayout()
        Me.tpgCertFile.SuspendLayout()
        Me.tpgCertStore.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabNewConnection
        '
        Me.tabNewConnection.Controls.Add(Me.tpgBasic)
        Me.tabNewConnection.Controls.Add(Me.tpgCertFile)
        Me.tabNewConnection.Controls.Add(Me.tpgCertStore)
        Me.tabNewConnection.Location = New System.Drawing.Point(0, 0)
        Me.tabNewConnection.Name = "tabNewConnection"
        Me.tabNewConnection.SelectedIndex = 0
        Me.tabNewConnection.Size = New System.Drawing.Size(288, 262)
        Me.tabNewConnection.TabIndex = 0
        '
        'tpgBasic
        '
        Me.tpgBasic.Controls.Add(Me.btnCancelBasic)
        Me.tpgBasic.Controls.Add(Me.btnOkayBasic)
        Me.tpgBasic.Controls.Add(Me.Label4)
        Me.tpgBasic.Controls.Add(Me.Label3)
        Me.tpgBasic.Controls.Add(Me.Label5)
        Me.tpgBasic.Controls.Add(Me.txtName)
        Me.tpgBasic.Controls.Add(Me.lbPwd)
        Me.tpgBasic.Controls.Add(Me.txtPassword)
        Me.tpgBasic.Controls.Add(Me.lbUserid)
        Me.tpgBasic.Controls.Add(Me.txtUserid)
        Me.tpgBasic.Controls.Add(Me.Label2)
        Me.tpgBasic.Controls.Add(Me.txtPort)
        Me.tpgBasic.Controls.Add(Me.Label1)
        Me.tpgBasic.Controls.Add(Me.txtHost)
        Me.tpgBasic.Controls.Add(Me.rbBasicHTTPS)
        Me.tpgBasic.Controls.Add(Me.rbBasicHTTP)
        Me.tpgBasic.Location = New System.Drawing.Point(4, 22)
        Me.tpgBasic.Name = "tpgBasic"
        Me.tpgBasic.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgBasic.Size = New System.Drawing.Size(280, 236)
        Me.tpgBasic.TabIndex = 0
        Me.tpgBasic.Text = "Basic Auth"
        Me.tpgBasic.UseVisualStyleBackColor = True
        '
        'btnCancelBasic
        '
        Me.btnCancelBasic.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelBasic.Location = New System.Drawing.Point(141, 204)
        Me.btnCancelBasic.Name = "btnCancelBasic"
        Me.btnCancelBasic.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelBasic.TabIndex = 15
        Me.btnCancelBasic.Text = "Cancel"
        Me.btnCancelBasic.UseVisualStyleBackColor = True
        '
        'btnOkayBasic
        '
        Me.btnOkayBasic.Location = New System.Drawing.Point(60, 204)
        Me.btnOkayBasic.Name = "btnOkayBasic"
        Me.btnOkayBasic.Size = New System.Drawing.Size(75, 23)
        Me.btnOkayBasic.TabIndex = 14
        Me.btnOkayBasic.Text = "OK"
        Me.btnOkayBasic.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "HTTPS"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "HTTP"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Entry name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(80, 12)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(188, 20)
        Me.txtName.TabIndex = 1
        '
        'lbPwd
        '
        Me.lbPwd.AutoSize = True
        Me.lbPwd.Location = New System.Drawing.Point(17, 166)
        Me.lbPwd.Name = "lbPwd"
        Me.lbPwd.Size = New System.Drawing.Size(53, 13)
        Me.lbPwd.TabIndex = 9
        Me.lbPwd.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(80, 163)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(92, 20)
        Me.txtPassword.TabIndex = 8
        '
        'lbUserid
        '
        Me.lbUserid.AutoSize = True
        Me.lbUserid.Location = New System.Drawing.Point(17, 140)
        Me.lbUserid.Name = "lbUserid"
        Me.lbUserid.Size = New System.Drawing.Size(37, 13)
        Me.lbUserid.TabIndex = 7
        Me.lbUserid.Text = "Userid"
        '
        'txtUserid
        '
        Me.txtUserid.Location = New System.Drawing.Point(80, 137)
        Me.txtUserid.Name = "txtUserid"
        Me.txtUserid.Size = New System.Drawing.Size(92, 20)
        Me.txtUserid.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Host port"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(80, 111)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(50, 20)
        Me.txtPort.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Host name"
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(80, 85)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.Size = New System.Drawing.Size(188, 20)
        Me.txtHost.TabIndex = 4
        '
        'rbBasicHTTPS
        '
        Me.rbBasicHTTPS.AutoSize = True
        Me.rbBasicHTTPS.Location = New System.Drawing.Point(80, 62)
        Me.rbBasicHTTPS.Name = "rbBasicHTTPS"
        Me.rbBasicHTTPS.Size = New System.Drawing.Size(14, 13)
        Me.rbBasicHTTPS.TabIndex = 3
        Me.rbBasicHTTPS.UseVisualStyleBackColor = True
        '
        'rbBasicHTTP
        '
        Me.rbBasicHTTP.AutoSize = True
        Me.rbBasicHTTP.Checked = True
        Me.rbBasicHTTP.Location = New System.Drawing.Point(80, 39)
        Me.rbBasicHTTP.Name = "rbBasicHTTP"
        Me.rbBasicHTTP.Size = New System.Drawing.Size(14, 13)
        Me.rbBasicHTTP.TabIndex = 2
        Me.rbBasicHTTP.TabStop = True
        Me.rbBasicHTTP.UseVisualStyleBackColor = True
        '
        'tpgCertFile
        '
        Me.tpgCertFile.Controls.Add(Me.btnFilePickerCertFile)
        Me.tpgCertFile.Controls.Add(Me.btnCancelCertFile)
        Me.tpgCertFile.Controls.Add(Me.btnOkayCertFile)
        Me.tpgCertFile.Controls.Add(Me.Label6)
        Me.tpgCertFile.Controls.Add(Me.Label7)
        Me.tpgCertFile.Controls.Add(Me.Label8)
        Me.tpgCertFile.Controls.Add(Me.txtNameCertFile)
        Me.tpgCertFile.Controls.Add(Me.Label9)
        Me.tpgCertFile.Controls.Add(Me.txtPwdCertFile)
        Me.tpgCertFile.Controls.Add(Me.Label10)
        Me.tpgCertFile.Controls.Add(Me.txtPathCertFile)
        Me.tpgCertFile.Controls.Add(Me.Label11)
        Me.tpgCertFile.Controls.Add(Me.txtPortCertFile)
        Me.tpgCertFile.Controls.Add(Me.Label12)
        Me.tpgCertFile.Controls.Add(Me.txtHostCertFile)
        Me.tpgCertFile.Controls.Add(Me.rbHTTPSCertFile)
        Me.tpgCertFile.Controls.Add(Me.rbHTTPCertFile)
        Me.tpgCertFile.Location = New System.Drawing.Point(4, 22)
        Me.tpgCertFile.Name = "tpgCertFile"
        Me.tpgCertFile.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgCertFile.Size = New System.Drawing.Size(280, 236)
        Me.tpgCertFile.TabIndex = 1
        Me.tpgCertFile.Text = "Certificate File"
        Me.tpgCertFile.UseVisualStyleBackColor = True
        '
        'btnFilePickerCertFile
        '
        Me.btnFilePickerCertFile.Location = New System.Drawing.Point(240, 136)
        Me.btnFilePickerCertFile.Name = "btnFilePickerCertFile"
        Me.btnFilePickerCertFile.Size = New System.Drawing.Size(26, 21)
        Me.btnFilePickerCertFile.TabIndex = 24
        Me.btnFilePickerCertFile.Text = "..."
        Me.btnFilePickerCertFile.UseVisualStyleBackColor = True
        '
        'btnCancelCertFile
        '
        Me.btnCancelCertFile.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelCertFile.Location = New System.Drawing.Point(141, 204)
        Me.btnCancelCertFile.Name = "btnCancelCertFile"
        Me.btnCancelCertFile.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelCertFile.TabIndex = 31
        Me.btnCancelCertFile.Text = "Cancel"
        Me.btnCancelCertFile.UseVisualStyleBackColor = True
        '
        'btnOkayCertFile
        '
        Me.btnOkayCertFile.Location = New System.Drawing.Point(60, 204)
        Me.btnOkayCertFile.Name = "btnOkayCertFile"
        Me.btnOkayCertFile.Size = New System.Drawing.Size(75, 23)
        Me.btnOkayCertFile.TabIndex = 30
        Me.btnOkayCertFile.Text = "OK"
        Me.btnOkayCertFile.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Enabled = False
        Me.Label6.Location = New System.Drawing.Point(17, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "HTTPS"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Enabled = False
        Me.Label7.Location = New System.Drawing.Point(17, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "HTTP"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Entry name"
        '
        'txtNameCertFile
        '
        Me.txtNameCertFile.Location = New System.Drawing.Point(80, 12)
        Me.txtNameCertFile.Name = "txtNameCertFile"
        Me.txtNameCertFile.Size = New System.Drawing.Size(188, 20)
        Me.txtNameCertFile.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 166)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Password"
        '
        'txtPwdCertFile
        '
        Me.txtPwdCertFile.Location = New System.Drawing.Point(80, 163)
        Me.txtPwdCertFile.Name = "txtPwdCertFile"
        Me.txtPwdCertFile.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwdCertFile.Size = New System.Drawing.Size(92, 20)
        Me.txtPwdCertFile.TabIndex = 25
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 140)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Certificate"
        '
        'txtPathCertFile
        '
        Me.txtPathCertFile.Location = New System.Drawing.Point(80, 137)
        Me.txtPathCertFile.Name = "txtPathCertFile"
        Me.txtPathCertFile.Size = New System.Drawing.Size(154, 20)
        Me.txtPathCertFile.TabIndex = 23
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 114)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Host port"
        '
        'txtPortCertFile
        '
        Me.txtPortCertFile.Location = New System.Drawing.Point(80, 111)
        Me.txtPortCertFile.Name = "txtPortCertFile"
        Me.txtPortCertFile.Size = New System.Drawing.Size(50, 20)
        Me.txtPortCertFile.TabIndex = 22
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(17, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "Host name"
        '
        'txtHostCertFile
        '
        Me.txtHostCertFile.Location = New System.Drawing.Point(80, 85)
        Me.txtHostCertFile.Name = "txtHostCertFile"
        Me.txtHostCertFile.Size = New System.Drawing.Size(188, 20)
        Me.txtHostCertFile.TabIndex = 20
        '
        'rbHTTPSCertFile
        '
        Me.rbHTTPSCertFile.AutoSize = True
        Me.rbHTTPSCertFile.Checked = True
        Me.rbHTTPSCertFile.Enabled = False
        Me.rbHTTPSCertFile.Location = New System.Drawing.Point(80, 62)
        Me.rbHTTPSCertFile.Name = "rbHTTPSCertFile"
        Me.rbHTTPSCertFile.Size = New System.Drawing.Size(14, 13)
        Me.rbHTTPSCertFile.TabIndex = 19
        Me.rbHTTPSCertFile.TabStop = True
        Me.rbHTTPSCertFile.UseVisualStyleBackColor = True
        '
        'rbHTTPCertFile
        '
        Me.rbHTTPCertFile.AutoSize = True
        Me.rbHTTPCertFile.Enabled = False
        Me.rbHTTPCertFile.Location = New System.Drawing.Point(80, 39)
        Me.rbHTTPCertFile.Name = "rbHTTPCertFile"
        Me.rbHTTPCertFile.Size = New System.Drawing.Size(14, 13)
        Me.rbHTTPCertFile.TabIndex = 17
        Me.rbHTTPCertFile.UseVisualStyleBackColor = True
        '
        'tpgCertStore
        '
        Me.tpgCertStore.Controls.Add(Me.btnCertPicker)
        Me.tpgCertStore.Controls.Add(Me.btnCancelCertStore)
        Me.tpgCertStore.Controls.Add(Me.btnOkayCertStore)
        Me.tpgCertStore.Controls.Add(Me.Label13)
        Me.tpgCertStore.Controls.Add(Me.Label14)
        Me.tpgCertStore.Controls.Add(Me.Label15)
        Me.tpgCertStore.Controls.Add(Me.txtNameCertStore)
        Me.tpgCertStore.Controls.Add(Me.Label17)
        Me.tpgCertStore.Controls.Add(Me.txtSubjectCertStore)
        Me.tpgCertStore.Controls.Add(Me.Label18)
        Me.tpgCertStore.Controls.Add(Me.txtHostPortCertStore)
        Me.tpgCertStore.Controls.Add(Me.Label19)
        Me.tpgCertStore.Controls.Add(Me.txtHostNameCertStore)
        Me.tpgCertStore.Controls.Add(Me.rbHTTPSCertStore)
        Me.tpgCertStore.Controls.Add(Me.rbHTTPCertStore)
        Me.tpgCertStore.Location = New System.Drawing.Point(4, 22)
        Me.tpgCertStore.Name = "tpgCertStore"
        Me.tpgCertStore.Size = New System.Drawing.Size(280, 236)
        Me.tpgCertStore.TabIndex = 2
        Me.tpgCertStore.Text = "Certificate Store"
        Me.tpgCertStore.UseVisualStyleBackColor = True
        '
        'btnCertPicker
        '
        Me.btnCertPicker.Location = New System.Drawing.Point(240, 138)
        Me.btnCertPicker.Name = "btnCertPicker"
        Me.btnCertPicker.Size = New System.Drawing.Size(26, 21)
        Me.btnCertPicker.TabIndex = 40
        Me.btnCertPicker.Text = "..."
        Me.btnCertPicker.UseVisualStyleBackColor = True
        '
        'btnCancelCertStore
        '
        Me.btnCancelCertStore.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelCertStore.Location = New System.Drawing.Point(141, 205)
        Me.btnCancelCertStore.Name = "btnCancelCertStore"
        Me.btnCancelCertStore.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelCertStore.TabIndex = 47
        Me.btnCancelCertStore.Text = "Cancel"
        Me.btnCancelCertStore.UseVisualStyleBackColor = True
        '
        'btnOkayCertStore
        '
        Me.btnOkayCertStore.Location = New System.Drawing.Point(60, 205)
        Me.btnOkayCertStore.Name = "btnOkayCertStore"
        Me.btnOkayCertStore.Size = New System.Drawing.Size(75, 23)
        Me.btnOkayCertStore.TabIndex = 46
        Me.btnOkayCertStore.Text = "OK"
        Me.btnOkayCertStore.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Enabled = False
        Me.Label13.Location = New System.Drawing.Point(17, 65)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "HTTPS"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Enabled = False
        Me.Label14.Location = New System.Drawing.Point(17, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(36, 13)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "HTTP"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(17, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(60, 13)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "Entry name"
        '
        'txtNameCertStore
        '
        Me.txtNameCertStore.Location = New System.Drawing.Point(80, 13)
        Me.txtNameCertStore.Name = "txtNameCertStore"
        Me.txtNameCertStore.Size = New System.Drawing.Size(188, 20)
        Me.txtNameCertStore.TabIndex = 32
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(17, 141)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(54, 13)
        Me.Label17.TabIndex = 40
        Me.Label17.Text = "Certificate"
        '
        'txtSubjectCertStore
        '
        Me.txtSubjectCertStore.Location = New System.Drawing.Point(80, 138)
        Me.txtSubjectCertStore.Name = "txtSubjectCertStore"
        Me.txtSubjectCertStore.Size = New System.Drawing.Size(154, 20)
        Me.txtSubjectCertStore.TabIndex = 39
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(17, 115)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 13)
        Me.Label18.TabIndex = 37
        Me.Label18.Text = "Host port"
        '
        'txtHostPortCertStore
        '
        Me.txtHostPortCertStore.Location = New System.Drawing.Point(80, 112)
        Me.txtHostPortCertStore.Name = "txtHostPortCertStore"
        Me.txtHostPortCertStore.Size = New System.Drawing.Size(50, 20)
        Me.txtHostPortCertStore.TabIndex = 38
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 89)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(58, 13)
        Me.Label19.TabIndex = 34
        Me.Label19.Text = "Host name"
        '
        'txtHostNameCertStore
        '
        Me.txtHostNameCertStore.Location = New System.Drawing.Point(80, 86)
        Me.txtHostNameCertStore.Name = "txtHostNameCertStore"
        Me.txtHostNameCertStore.Size = New System.Drawing.Size(188, 20)
        Me.txtHostNameCertStore.TabIndex = 36
        '
        'rbHTTPSCertStore
        '
        Me.rbHTTPSCertStore.AutoSize = True
        Me.rbHTTPSCertStore.Checked = True
        Me.rbHTTPSCertStore.Enabled = False
        Me.rbHTTPSCertStore.Location = New System.Drawing.Point(80, 63)
        Me.rbHTTPSCertStore.Name = "rbHTTPSCertStore"
        Me.rbHTTPSCertStore.Size = New System.Drawing.Size(14, 13)
        Me.rbHTTPSCertStore.TabIndex = 35
        Me.rbHTTPSCertStore.TabStop = True
        Me.rbHTTPSCertStore.UseVisualStyleBackColor = True
        '
        'rbHTTPCertStore
        '
        Me.rbHTTPCertStore.AutoSize = True
        Me.rbHTTPCertStore.Enabled = False
        Me.rbHTTPCertStore.Location = New System.Drawing.Point(80, 40)
        Me.rbHTTPCertStore.Name = "rbHTTPCertStore"
        Me.rbHTTPCertStore.Size = New System.Drawing.Size(14, 13)
        Me.rbHTTPCertStore.TabIndex = 33
        Me.rbHTTPCertStore.UseVisualStyleBackColor = True
        '
        'frmNewConn
        '
        Me.AcceptButton = Me.btnOkayBasic
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelBasic
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.tabNewConnection)
        Me.Name = "frmNewConn"
        Me.Text = "New Connection"
        Me.tabNewConnection.ResumeLayout(False)
        Me.tpgBasic.ResumeLayout(False)
        Me.tpgBasic.PerformLayout()
        Me.tpgCertFile.ResumeLayout(False)
        Me.tpgCertFile.PerformLayout()
        Me.tpgCertStore.ResumeLayout(False)
        Me.tpgCertStore.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabNewConnection As System.Windows.Forms.TabControl
    Friend WithEvents tpgBasic As System.Windows.Forms.TabPage
    Friend WithEvents rbBasicHTTPS As System.Windows.Forms.RadioButton
    Friend WithEvents rbBasicHTTP As System.Windows.Forms.RadioButton
    Friend WithEvents tpgCertFile As System.Windows.Forms.TabPage
    Friend WithEvents lbPwd As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lbUserid As System.Windows.Forms.Label
    Friend WithEvents txtUserid As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtHost As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelBasic As System.Windows.Forms.Button
    Friend WithEvents btnOkayBasic As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCancelCertFile As System.Windows.Forms.Button
    Friend WithEvents btnOkayCertFile As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNameCertFile As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPwdCertFile As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPathCertFile As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPortCertFile As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtHostCertFile As System.Windows.Forms.TextBox
    Friend WithEvents rbHTTPSCertFile As System.Windows.Forms.RadioButton
    Friend WithEvents rbHTTPCertFile As System.Windows.Forms.RadioButton
    Friend WithEvents tpgCertStore As System.Windows.Forms.TabPage
    Friend WithEvents btnCancelCertStore As System.Windows.Forms.Button
    Friend WithEvents btnOkayCertStore As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtNameCertStore As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtSubjectCertStore As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtHostPortCertStore As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtHostNameCertStore As System.Windows.Forms.TextBox
    Friend WithEvents rbHTTPSCertStore As System.Windows.Forms.RadioButton
    Friend WithEvents rbHTTPCertStore As System.Windows.Forms.RadioButton
    Friend WithEvents btnCertPicker As System.Windows.Forms.Button
    Friend WithEvents btnFilePickerCertFile As System.Windows.Forms.Button
End Class
