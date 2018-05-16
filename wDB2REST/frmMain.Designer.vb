<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Me.lbHosts = New System.Windows.Forms.ComboBox()
        Me.btnNewHost = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lvServices = New System.Windows.Forms.ListView()
        Me.cmnServices = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DropToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnNewService = New System.Windows.Forms.Button()
        Me.cmnServices.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbHosts
        '
        Me.lbHosts.FormattingEnabled = True
        Me.lbHosts.Location = New System.Drawing.Point(95, 15)
        Me.lbHosts.Name = "lbHosts"
        Me.lbHosts.Size = New System.Drawing.Size(175, 21)
        Me.lbHosts.Sorted = True
        Me.lbHosts.TabIndex = 2
        '
        'btnNewHost
        '
        Me.btnNewHost.Location = New System.Drawing.Point(276, 15)
        Me.btnNewHost.Name = "btnNewHost"
        Me.btnNewHost.Size = New System.Drawing.Size(27, 21)
        Me.btnNewHost.TabIndex = 3
        Me.btnNewHost.Text = "..."
        Me.btnNewHost.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(14, 15)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lvServices
        '
        Me.lvServices.Location = New System.Drawing.Point(2, 44)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.Size = New System.Drawing.Size(629, 335)
        Me.lvServices.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvServices.TabIndex = 5
        Me.lvServices.UseCompatibleStateImageBehavior = False
        '
        'cmnServices
        '
        Me.cmnServices.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailsToolStripMenuItem, Me.RunToolStripMenuItem, Me.ToolStripSeparator1, Me.DropToolStripMenuItem})
        Me.cmnServices.Name = "cmnServices"
        Me.cmnServices.Size = New System.Drawing.Size(110, 76)
        '
        'DetailsToolStripMenuItem
        '
        Me.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem"
        Me.DetailsToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.DetailsToolStripMenuItem.Text = "&Details"
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.RunToolStripMenuItem.Text = "&Run"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(106, 6)
        '
        'DropToolStripMenuItem
        '
        Me.DropToolStripMenuItem.Name = "DropToolStripMenuItem"
        Me.DropToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.DropToolStripMenuItem.Text = "Drop"
        '
        'btnNewService
        '
        Me.btnNewService.Enabled = False
        Me.btnNewService.Location = New System.Drawing.Point(364, 14)
        Me.btnNewService.Name = "btnNewService"
        Me.btnNewService.Size = New System.Drawing.Size(75, 23)
        Me.btnNewService.TabIndex = 4
        Me.btnNewService.Text = "New"
        Me.btnNewService.UseVisualStyleBackColor = True
        Me.btnNewService.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 381)
        Me.Controls.Add(Me.btnNewService)
        Me.Controls.Add(Me.lvServices)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnNewHost)
        Me.Controls.Add(Me.lbHosts)
        Me.Name = "frmMain"
        Me.Text = "DB2 REST Tool"
        Me.cmnServices.ResumeLayout(False)
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents lbHosts As System.Windows.Forms.ComboBox
    Friend WithEvents btnNewHost As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents lvServices As System.Windows.Forms.ListView
    Friend WithEvents cmnServices As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DropToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnNewService As System.Windows.Forms.Button

End Class
