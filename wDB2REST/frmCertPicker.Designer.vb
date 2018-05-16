<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCertPicker
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
        Me.lvCerts = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSubject = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFrom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colIssuer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'lvCerts
        '
        Me.lvCerts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colSubject, Me.colFrom, Me.colTo, Me.colIssuer})
        Me.lvCerts.FullRowSelect = True
        Me.lvCerts.Location = New System.Drawing.Point(-2, -2)
        Me.lvCerts.MultiSelect = False
        Me.lvCerts.Name = "lvCerts"
        Me.lvCerts.Size = New System.Drawing.Size(634, 385)
        Me.lvCerts.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvCerts.TabIndex = 0
        Me.lvCerts.UseCompatibleStateImageBehavior = False
        Me.lvCerts.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        '
        'colSubject
        '
        Me.colSubject.Text = "Subject"
        '
        'colFrom
        '
        Me.colFrom.Text = "Valid From"
        '
        'colTo
        '
        Me.colTo.Text = "Valid To"
        '
        'colIssuer
        '
        Me.colIssuer.Text = "Issuer"
        '
        'frmCertPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 384)
        Me.Controls.Add(Me.lvCerts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmCertPicker"
        Me.Text = "Certificate Picker"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvCerts As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSubject As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFrom As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTo As System.Windows.Forms.ColumnHeader
    Friend WithEvents colIssuer As System.Windows.Forms.ColumnHeader
End Class
