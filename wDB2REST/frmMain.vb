Imports System
' splash timer
Imports System.Timers
' collections (allHosts, etc)
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
' certificate stuff
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
' console i/o
Imports System.IO
' web requests
Imports System.Net
' Encoding
Imports System.Text
' RemoteCertificateValidation
Imports System.Net.Security
' Registry stuff
Imports Microsoft.Win32
' JSON handling
Imports Newtonsoft.Json

Public Class frmMain
    Public Const __debug As Boolean = False
    Public Const __app = "wDB2REST"
    ' Registry key names
    Public Const regKeyBase = "Software\DB2Dinosaur\wDB2REST"
    Public Const regConfig = regKeyBase & "\Config"
    Public Const regHostList = regKeyBase & "\Hosts"
    Public Const regHostName = "AKA"
    Public Const regNetworkName = "IP"
    Public Const regNetworkPort = "PORT"
    Public Const regSecure = "SEC"
    Public Const regUseCertificate = "UCRT"
    Public Const regUserid = "USER"
    Public Const regPassword = "PWD"
    Public Const regCertIsFile = "CFLE"
    Public Const regCert = "CERT"
    Public Const regLastHost = "LHST"
    Private Const regFormWidth = "FRMW"
    Private Const regFormHeight = "FRMH"
    Private Const regDetailWidth = "DTLW"
    Private Const regDetailHeight = "DRLH"
    Private Const regConnectionsHeight = "CONH"
    Private Const regConnectionsWidth = "CONW"
    Public Const regCertFormHeight = "CRTH"
    Public Const regCertFormWidth = "CRTW"

    ' Some private globals
    Private _splashTimerRunning As Boolean = False
    Private _lastHost As String = ""
    ' Current connection 
    Private __baseurl As String = ""
    Private __basicauth As Boolean = True
    Private __certificate As X509Certificate2 = Nothing
    Private __b64authstr As String = ""

    Public Class Hosts
        Private nickname As String
        Private netaddr As String
        Private port As String
        Private secure As Boolean
        Private clientCert As Boolean
        Private certRefIsFile As Boolean
        Private certFile As String
        Private certSubj As String
        Private conUser As String
        Private conPwd As String
        ReadOnly Property name() As String           ' name that this ref will be known as
            Get
                Return nickname
            End Get
        End Property
        ReadOnly Property netaddress() As String         ' ip name or address
            Get
                Return netaddr
            End Get
        End Property
        ReadOnly Property netport() As String            ' ip port number
            Get
                Return port
            End Get
        End Property
        ReadOnly Property https As Boolean               ' if true, HTTPS, else HTTP
            Get
                Return secure
            End Get
        End Property
        ReadOnly Property use_certificate() As Boolean   ' if secure then if true use certificate
            Get
                Return clientCert
            End Get
        End Property
        ReadOnly Property userid() As String             ' userid for basic auth
            Get
                Return conUser
            End Get
        End Property
        ReadOnly Property password() As String           ' password for userid or file based certificate
            Get
                Return conPwd
            End Get
        End Property
        ReadOnly Property certificate_file() As String  ' if secure & use_certificate then if true certificate_ref = file path, else = Subject in user store
            Get
                Return certFile
            End Get
        End Property
        ReadOnly Property certificate_subject() As String    ' subject for certificate
            Get
                Return certSubj
            End Get
        End Property

        Public Sub New(name As String, networkAddr As String, networkPort As String, https As Boolean, certIsFile As Boolean, certSubjectFilePath As String, certPassword As String)
            nickname = name
            netaddr = networkAddr
            port = networkPort
            clientCert = True
            certFile = ""
            certSubj = ""
            conUser = ""
            conPwd = certPassword
            If certIsFile Then
                certFile = certSubjectFilePath
            Else
                certSubj = certSubjectFilePath
            End If
            certRefIsFile = certIsFile
            secure = https
        End Sub
        Public Sub New(name As String, networkAddr As String, networkPort As String, https As Boolean, userid As String, password As String)
            nickname = name
            netaddr = networkAddr
            port = networkPort
            certFile = ""
            certSubj = ""
            conUser = userid
            conPwd = password
            certRefIsFile = False
            secure = https
        End Sub

    End Class

    Public Class HostsCollection
        Inherits KeyedCollection(Of String, Hosts)
        Protected Overrides Function GetKeyForItem(ByVal item As Hosts) As String
            Return item.name()
        End Function
    End Class

    Public allHosts As HostsCollection

    Private Sub splashTimerExpired(sender As Object, e As ElapsedEventArgs)
        _splashTimerRunning = False
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Splash screen
        Dim fSplash As frmSplash = New frmSplash()
        fSplash.Show()
        ' Start splash screen timer
        Dim timerSplash As Timer = New Timer()
        timerSplash.Interval = 3000  ' 3 seconds
        timerSplash.AutoReset = False  ' stop after first interval
        AddHandler timerSplash.Elapsed, New ElapsedEventHandler(AddressOf splashTimerExpired)
        _splashTimerRunning = True
        timerSplash.Start()
        ' Fetch config from reg
        Dim rkey As RegistryKey
        Dim firstTime As Boolean = False
        Try
            rkey = Registry.CurrentUser.OpenSubKey(regKeyBase)
            Dim speculative As String() = rkey.GetSubKeyNames()
        Catch eregerr As System.NullReferenceException
            firstTime = True
        End Try
        If firstTime Then
            rkey = Registry.CurrentUser.CreateSubKey(regKeyBase)
            rkey = Registry.CurrentUser.CreateSubKey(regConfig)
            rkey = Registry.CurrentUser.CreateSubKey(regHostList)
        Else
            rkey = Registry.CurrentUser.OpenSubKey(regConfig)
            _lastHost = rkey.GetValue(regLastHost, "")
            Me.Width = rkey.GetValue(regFormWidth, 1021)
            Me.Height = rkey.GetValue(regFormHeight, 400)
        End If
        ' Fetch host list array from reg
        allHosts = New HostsCollection()
        rkey = Registry.CurrentUser.OpenSubKey(regHostList)
        Dim hostnames As String() = rkey.GetSubKeyNames()
        For Each host In hostnames
            If __debug Then
                Console.WriteLine("Host: {0}", host)
            End If
            Dim rkhost As RegistryKey = rkey.OpenSubKey(host)
            Dim hname As String = rkhost.GetValue(regHostName, "")
            If hname <> "" Then
                Dim hipad As String = rkhost.GetValue(regNetworkName)
                Dim hippt As String = rkhost.GetValue(regNetworkPort)
                Dim bsecu As Byte() = rkhost.GetValue(regSecure)
                Dim hsecu As Boolean = (bsecu(0) <> 0)
                Dim bucrt As Byte() = rkhost.GetValue(regUseCertificate)
                Dim hucrt As Boolean = (bucrt(0) <> 0)
                Dim huser As String = rkhost.GetValue(regUserid)
                Dim hpwd As String = rkhost.GetValue(regPassword)
                Dim bcfle As Byte() = rkhost.GetValue(regCertIsFile)
                Dim hcfle As Boolean = (bcfle(0) <> 0)
                Dim hcert As String = rkhost.GetValue(regCert)
                Dim nh As Hosts
                If hucrt Then ' using a certificate?
                    nh = New Hosts(hname, hipad, hippt, hsecu, hcfle, hcert, hpwd)
                Else ' or Basic authentication?
                    nh = New Hosts(hname, hipad, hippt, hsecu, huser, hpwd)
                End If
                allHosts.Add(nh)
                Dim index As Integer = Me.lbHosts.Items().Add(hname)
                If hname = _lastHost Then
                    Me.lbHosts.SelectedIndex = index
                End If
            End If
        Next
        ' Setup the service manager to talk AT-TLS to DB2 and manage certificates
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' Let the user decide if they want to accept a self-signed certificate from DB2
        ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf ValidateServerCertificate)
        ' Wait for the splash timer to complete
        While _splashTimerRunning
            Application.DoEvents()
        End While
        ' Drop splash screen
        Me.Show()
        ' Focus on Connect button
        btnConnect.Select()
        fSplash.Hide()
        fSplash.Close()
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Dim currentHost As String = lbHosts.SelectedItem
        If currentHost <> "" Then
            Dim thisHost As Hosts = allHosts.Item(currentHost)
            Dim urlpref = "http"
            __basicauth = True
            If thisHost.https Then
                urlpref = "https"
                If thisHost.use_certificate Then
                    __basicauth = False
                End If
            End If
            ' baseurl = http/https://ip:port/services/
            __baseurl = String.Format("{0}://{1}:{2}/services/", urlpref, thisHost.netaddress, thisHost.netport)
            ' basicauth = true / false
            If __basicauth Then
                ' b64authstr - user / pwd encoded
                Dim authstr As String = String.Format("{0}:{1}", thisHost.userid, thisHost.password)
                __b64authstr = Convert.ToBase64String(Encoding.ASCII.GetBytes(authstr))
                __certificate = Nothing
            Else
                ' certificate - load / ref it
                If thisHost.certificate_file <> "" Then
                    If thisHost.password <> "" Then
                        __certificate = New X509Certificate2(thisHost.certificate_file, thisHost.password)
                    Else
                        __certificate = New X509Certificate2(thisHost.certificate_file)
                    End If
                ElseIf thisHost.certificate_subject <> "" Then
                    ' find certificate in current user store:
                    Dim x509Store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
                    Try
                        ' create and open store for read-only access
                        x509Store.Open(OpenFlags.ReadOnly)
                        ' search store
                        Dim col As New X509Certificate2Collection
                        col = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, thisHost.certificate_subject, True)
                        If col.Count() > 0 Then
                            __certificate = col.Item(0)
                        End If
                    Catch ex As Exception
                        If __debug Then
                            Console.WriteLine("An error occurred: '{0}'", ex)
                        End If
                    Finally
                        x509Store.Close()
                    End Try
                End If
                __b64authstr = ""
            End If
            '
            ' set last host
            Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(regConfig, True)
            _lastHost = thisHost.name
            rkey.SetValue(regLastHost, thisHost.name)
            rkey.Close()
            ' Update the service list
            RefreshServiceList()
            ' Show and enable the New service button
            btnNewService.Visible = True
            btnNewService.Enabled = True
        End If
    End Sub

    Sub RefreshServiceList()
        Me.Cursor = Cursors.WaitCursor
        Me.Text = "DB2 REST Tool - " + _lastHost
        With lvServices
            .Items.Clear()
            .Columns.Clear()
            .View = View.Details
            .Columns.Add("Collection", 105)
            .Columns.Add("Name", 160)
            .Columns.Add("Description", 305)
            .Columns.Add("URL", 410)
            .FullRowSelect = True
            .Sorting = SortOrder.None
        End With
        ' issue HTTP GET request against .../services/ - i.e. drive DB2ServiceDiscover
        Dim respJson As String = HttpGet("")
        ' required to handle error conditions
        If respJson <> "" Then
            Dim rsp As RestResponse = JsonConvert.DeserializeObject(Of RestResponse)(respJson)
            If rsp.StatusCode <> 200 And rsp.StatusCode <> 0 Then
                MsgBox("Error retrieving services list : " & rsp.StatusDescription, MsgBoxStyle.OkOnly, __app)
            Else
                ' populate services listview
                Dim listServices As DB2Services = JsonConvert.DeserializeObject(Of DB2Services)(respJson)
                If listServices.DB2Services.Count > 0 Then
                    For idx As Integer = 0 To listServices.DB2Services.Count
                        If idx < listServices.DB2Services.Count Then
                            Dim serv As RESTServiceReport = listServices.DB2Services.Item(idx)
                            lvServices.Items.Add(New ListViewItem({serv.ServiceCollectionID, serv.ServiceName, serv.ServiceDescription, serv.ServiceURL}))
                        End If
                    Next
                End If
            End If
            ' add the following to ensure sorting is done over the first two columns, rather than the default (first one)
            lvServices.ListViewItemSorter = New ListViewServicesComparer()
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Function HttpGet(requestPath As String) As String
        Dim strurl As String = ""
        Dim prefix = requestPath.Split(":".ToCharArray())
        Dim lcprefix0 As String = prefix(0).ToLower()
        If lcprefix0 = "http" Or lcprefix0 = "https" Then
            strurl = requestPath
        Else
            strurl = __baseurl & requestPath
        End If
        Dim req As HttpWebRequest
        Dim resp As HttpWebResponse
        Dim enc As New System.Text.UTF8Encoding
        req = DirectCast(WebRequest.Create(strurl), HttpWebRequest)
        req.Accept = "application/json"
        req.ContentType = "application/json"
        req.Method = "GET"
        If __basicauth Then
            req.Headers.Add("Authorization", String.Format("Basic {0}", __b64authstr))
        Else
            req.ClientCertificates.Add(__certificate)
        End If
        Try
            resp = DirectCast(req.GetResponse(), HttpWebResponse)
        Catch e As System.Net.WebException
            resp = e.Response
        End Try
        If resp Is Nothing Then
            MsgBox(String.Format("Unable to connect to '{0}'", _lastHost), MsgBoxStyle.OkOnly, __app + " Connection Error")
            HttpGet = ""
        Else
            If __debug Then
                Console.WriteLine("Response headers:")
                Dim rsphdrs As WebHeaderCollection = resp.Headers()
                Dim i As Integer = 0
                While i < rsphdrs.Count()
                    Console.WriteLine("  {0}: {1}", rsphdrs.Keys(i), rsphdrs.Item(i))
                    i += 1
                End While
            End If
            Dim srJSON As String = ""
            If Not resp Is Nothing Then
                Dim sr As StreamReader = New StreamReader(resp.GetResponseStream())
                srJSON = sr.ReadToEnd()
                sr.Close()
                resp.Close()
            Else
                srJSON = "Error - no data returned"
            End If
            If __debug Then
                Console.WriteLine("Response (JSON) text: {0}", srJSON)
            End If
            HttpGet = srJSON
        End If
    End Function

    ' Collection of certificates that the user has accepted even though they're a bit iffy, so that we don't re-prompt for them each time they're used
    Dim selfSignedButAllowed As X509Certificate2Collection = New X509Certificate2Collection()

    ' function to prompt user if an invalid / self-signed certificate is returned by the host
    Function ValidateServerCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        If sslPolicyErrors = sslPolicyErrors.None Then
            Return True
        Else
            Dim srsn As String = ""
            Dim certFlaggedInError As X509Certificate2Collection = New X509Certificate2Collection()
            If sslPolicyErrors = Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch Then
                srsn = "Certificate name mismatch"
            ElseIf sslPolicyErrors = Net.Security.SslPolicyErrors.RemoteCertificateNotAvailable Then
                srsn = "Remote server certificate not available"
            ElseIf sslPolicyErrors = Net.Security.SslPolicyErrors.RemoteCertificateChainErrors Then
                Dim els As X509ChainElementCollection = chain.ChainElements()
                Dim i As Integer = 0
                For Each el In els
                    Dim thiscert As X509Certificate2 = el.Certificate()
                    ' if it's not in our list of already encountered certs that the user has said is okay...
                    If Not selfSignedButAllowed.Contains(thiscert) Then
                        Dim s As String = ""
                        For Each st In el.ChainElementStatus()
                            Dim status As String = st.Status()
                            Dim stinf As String = st.StatusInformation()
                            If status.Length() > 0 Then
                                s = s & String.Format("{0} - {1} ", st.Status(), st.StatusInformation())
                                certFlaggedInError.Add(thiscert)
                            End If
                        Next
                        If s.Length > 0 Then
                            srsn = String.Format("{0} : {1}", thiscert.GetNameInfo(X509NameType.SimpleName, False), s)
                        End If
                    End If
                    i += 1
                Next
            End If
            If srsn.Length() > 0 Then
                If MsgBox("Server certificate is not valid" & vbNewLine & vbNewLine & srsn & vbNewLine & "Accept?", MsgBoxStyle.OkCancel, __app) = MsgBoxResult.Ok Then
                    For Each errcert In certFlaggedInError
                        selfSignedButAllowed.Add(errcert)
                    Next
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        End If
        Return True
    End Function

    ' resize the services listview with the form
    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        lvServices.Height = Me.Height - 85
        lvServices.Width = Me.Width - 20
    End Sub

    ' save the sizes that the user has resized to
    Private Sub frmMain_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(regConfig, True)
        rkey.SetValue(regFormWidth, Me.Width)
        rkey.SetValue(regFormHeight, Me.Height)
        rkey.Close()
    End Sub

    Private Sub Details_ResizeEnd(sender As Object, e As EventArgs)
        Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(regConfig, True)
        rkey.SetValue(regDetailWidth, sender.Width)
        rkey.SetValue(regDetailHeight, sender.Height)
        rkey.Close()
    End Sub

    Private Sub Details_Resize(sender As Object, e As EventArgs)
        sender.Controls(0).Height = sender.Height - 40
        sender.Controls(0).Width = sender.Width - 15
    End Sub

    ' driven if the user double clicks on a service or right clicks on it and selects Details from the context menu
    ' present information about the parameters and response
    Private Sub lvServices_DoubleClick(sender As Object, e As EventArgs) Handles lvServices.DoubleClick
        If lvServices.SelectedItems().Count > 0 Then
            Dim collid As String = lvServices.SelectedItems(0).SubItems(0).Text
            Dim srvname As String = lvServices.SelectedItems(0).SubItems(1).Text
            Dim srvdesc As String = lvServices.SelectedItems(0).SubItems(2).Text
            Dim srvurl As String = lvServices.SelectedItems(0).SubItems(3).Text
            ' use HTTP GET vs service URL to retrieve service profile (parms and output format)
            Dim json As String = HttpGet(srvurl)
            Dim service = JsonConvert.DeserializeObject(json)
            Dim stcode As Integer = service("StatusCode")
            Dim stmsg As String = service("StatusDescription")
            If stcode <> 0 And (stcode < 200 Or stcode >= 300) Then
                MsgBox("Error retrieving service details:" + vbNewLine + vbNewLine + stmsg, MsgBoxStyle.OkOnly, __app)
                Exit Sub
            End If
            Dim reqSchema = service(srvname)("RequestSchema")
            Dim rspSchema = service(srvname)("ResponseSchema")
            Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(regConfig)
            Dim frm As Form = New Form()
            Dim fm As frmMain = CType(lvServices.Parent, frmMain)
            Dim lX As Integer = lvServices.SelectedItems(0).Position.X + 10 + fm.Location.X
            Dim lY As Integer = lvServices.SelectedItems(0).Position.Y + 90 + fm.Location.Y
            With frm
                .FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
                .Text = srvname + " Details"
                .Location = New Point(lX, lY)
                .StartPosition = FormStartPosition.Manual
                .Height = rkey.GetValue(regDetailHeight, 250)
                .Width = rkey.GetValue(regDetailWidth, 610)
            End With
            rkey.Close()
            AddHandler frm.Resize, AddressOf Details_Resize
            AddHandler frm.ResizeEnd, AddressOf Details_ResizeEnd
            ' add handler for resize end
            Dim lvDetails As MyListViewClass = New MyListViewClass()
            With lvDetails
                .FullRowSelect = True
                .View = View.Details
                .Columns.Add("Parm/Result", 80)
                .Columns.Add("Name", 110)
                .Columns.Add("Description", 380)
                .Height = frm.Height - 40
                .Width = frm.Width - 15
                .Location = New Point(0, 0)
                .Scrollable = True
            End With
            frm.Controls.Add(lvDetails)
            If reqSchema("properties") Is Nothing Then
                'lvDetails.Items.Add(New ListViewItem({"Parm", "<none>", "<none>"}))
            Else
                ' the following JSON layout makes more sense if you look at the GET response in a web browser...
                Dim idx As Integer = 0
                Dim parm = reqSchema("properties").First()
                While idx < reqSchema("properties").Count()
                    Dim strparm As String = parm.ToString()
                    Dim strparmparts As String() = strparm.Split(":")
                    Dim parmname As String = strparmparts(0).Trim("""".ToCharArray())
                    Dim parmdetails = JsonConvert.DeserializeObject(reqSchema("properties")(parmname).ToString())
                    lvDetails.Items.Add(New ListViewItem({"Parm", parmname, parmdetails("description").ToString()}))
                    If parm.Next Is Nothing Then
                        parm = parm.Last
                    Else
                        parm = parm.Next
                    End If
                    idx += 1
                End While
            End If
            If rspSchema("properties") Is Nothing Then
                'MsgBox("No output defined - if you see this then there's definitely a problem!!!", MsgBoxStyle.OkOnly)
            Else
                Dim respdetails As Collection(Of ListViewItem) = InterpretDetails(rspSchema, "properties", "Result")
                For Each rdet In respdetails
                    lvDetails.Items.Add(rdet)
                Next
            End If
            frm.ShowDialog()
        End If
    End Sub

    Function InterpretDetails(ByRef jsobj As Object, strLevel As String, strLabel As String) As Collection(Of ListViewItem)
        Dim rv As Collection(Of ListViewItem) = New Collection(Of ListViewItem)
        Dim ellv = jsobj(strLevel)
        Dim el = ellv.First()
        Dim i As Integer = 0
        While i < ellv.Count()
            Dim strel As String = el.ToString()
            Dim elparts As String() = strel.Split(":")
            Dim elname As String = elparts(0).Trim("""".ToCharArray())
            Dim eldetails = JsonConvert.DeserializeObject(jsobj(strLevel)(elname).ToString())
            rv.Add(New ListViewItem({strLabel, elname, eldetails("description")}))
            If elname = "ResultSet Output" Then
                Dim rsd As Collection(Of ListViewItem) = InterpretDetails(eldetails("items"), "properties", " ")
                For Each rsv In rsd
                    rv.Add(rsv)
                Next
            ElseIf elname = "Output Parameters" Then
                Dim opd As Collection(Of ListViewItem) = InterpretDetails(eldetails, "properties", " ")
                For Each opv In opd
                    rv.Add(opv)
                Next
            End If
            If el.Next Is Nothing Then
                el = el.Last
            Else
                el = el.Next
            End If
            i += 1
        End While
        InterpretDetails = rv
    End Function

    Private Sub Connections_ResizeEnd(sender As Object, e As EventArgs)
        Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(regConfig, True)
        rkey.SetValue(regConnectionsWidth, sender.Width)
        rkey.SetValue(regConnectionsHeight, sender.Height)
        rkey.Close()
    End Sub

    Private Sub Connections_Resize(sender As Object, e As EventArgs)
        If sender.Controls.Count() > 0 Then
            sender.Controls(0).Height = sender.Height - 50
            sender.Controls(0).Width = sender.Width - 15
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim lv As ListView = CType(sender, Button).Parent.Controls(0)
        Dim checklist As String = ""
        For Each item In lv.Items
            If item.Checked Then
                Dim conn As String = item.Text
                Dim rkey As RegistryKey
                checklist += "'" + item.Text + "' "
                allHosts.Remove(conn)
                ' if the requested host to delete is the currently connected one...
                If _lastHost = conn Then
                    ' unset _lastHost
                    _lastHost = ""
                    rkey = Registry.CurrentUser.OpenSubKey(regConfig, True)
                    rkey.SetValue(regLastHost, _lastHost)
                    rkey.Close()
                    ' hide and disable the New (service) button
                    Me.btnNewService.Enabled = False
                    Me.btnNewService.Visible = False
                    ' clear service list
                    Me.lvServices.Items.Clear()
                    ' reset title
                    Me.Text = "DB2 REST Tool"
                    ' unset current connection details
                    __b64authstr = ""
                    __baseurl = ""
                    __basicauth = True
                    __certificate = Nothing
                End If
                ' remove from rmfMain.lbConnections
                Me.lbHosts.Items.Remove(conn)
                ' remove from lv
                lv.Items.Remove(item)
                ' remove registry entry
                rkey = Registry.CurrentUser.OpenSubKey(regHostList, True)
                rkey.DeleteSubKey(conn)
                rkey.Close()
            End If
        Next
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs)
        Dim fnew As frmNewConn = New frmNewConn()
        Dim btn As Button = CType(sender, Button)
        fnew.SetParent(btn.Parent, Me)
        fnew.ShowDialog()
    End Sub

    Private Sub btnNewHost_Click(sender As Object, e As EventArgs) Handles btnNewHost.Click
        Dim frm As Form = New Form()
        Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey(regConfig)
        With frm
            .FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
            .Text = "Defined Connections"
            .Location = New Point(Me.Location.X + 40, Me.Location.Y + 90)
            .StartPosition = FormStartPosition.Manual
            .Height = rkey.GetValue(regConnectionsHeight, 250)
            .Width = rkey.GetValue(regConnectionsWidth, 610)
        End With
        rkey.Close()
        AddHandler frm.Resize, AddressOf Connections_Resize
        AddHandler frm.ResizeEnd, AddressOf Connections_ResizeEnd
        '        Dim lvDetails As ListView = New ListView()
        Dim lvDetails As MyListViewClass = New MyListViewClass()
        With lvDetails
            .FullRowSelect = True
            .View = View.Details
            .CheckBoxes = True
            .Columns.Add("Name", 140)
            .Columns.Add("Base URL", 225)
            .Columns.Add("Auth Type", 75)  ' Certificate / User/Pwd
            .Columns.Add("Auth", 310)
            .Height = frm.Height - 50
            .Width = frm.Width - 15
            .Location = New Point(0, 45)
            .Scrollable = True
        End With
        frm.Controls.Add(lvDetails)
        Dim btnNew As Button = New Button()
        Dim btnDelete As Button = New Button()
        btnNew.Text = "New"
        btnNew.Location = New Point(10, 10)
        btnDelete.Location = New Point(110, 10)
        btnDelete.Text = "Delete"
        AddHandler btnNew.Click, AddressOf btnNew_Click
        AddHandler btnDelete.Click, AddressOf btnDelete_Click
        frm.Controls.Add(btnNew)
        frm.Controls.Add(btnDelete)

        Dim hDets As ListViewItem
        For Each host In allHosts
            Dim thisurl As String = ""
            If host.https Then
                thisurl = "https://"
            Else
                thisurl = "http://"
            End If
            thisurl += host.netaddress + ":" + host.netport + "/services/"
            Dim thisauthtype As String = "User/Pwd"
            Dim thisauth = host.userid
            If host.use_certificate Then
                thisauthtype = "Certificate"
                If host.certificate_file <> "" Then
                    thisauth = host.certificate_file
                Else
                    thisauth = host.certificate_subject
                End If
            End If
            hDets = New ListViewItem({host.name, thisurl, thisauthtype, thisauth})
            lvDetails.Items.Add(hDets)
        Next
        frm.ShowDialog()
    End Sub

    Public Sub ListViewCopy_KeyDown(sender As Object, e As KeyEventArgs) Handles lvServices.KeyDown
        If e.KeyCode = Keys.C And e.Modifiers = Keys.Control Then
            Dim snd As ListView = CType(sender, ListView)
            If snd.SelectedItems.Count() > 0 Then
                Clipboard.Clear()
                Dim strClip As String = ""
                For Each item In snd.SelectedItems
                    Dim i As Integer = 0
                    While i < item.SubItems.Count()
                        strClip += item.subitems(i).text
                        i += 1
                        If i < item.subitems.count() Then
                            strClip += ", "
                        End If
                    End While
                    strClip += vbNewLine
                Next
                Clipboard.SetText(strClip)
            End If
        ElseIf e.KeyCode = Keys.Return And lvServices.SelectedItems.Count() > 0 Then
            lvServices_DoubleClick(lvServices, Nothing)
        ElseIf e.KeyCode = Keys.A And e.Modifiers = Keys.Control Then
            Dim snd As ListView = CType(sender, ListView)
            For i As Integer = 0 To snd.Items.Count() - 1
                snd.Items(i).Selected = True
            Next
        End If
    End Sub

    Private Sub lvServices_MouseDown(sender As Object, e As MouseEventArgs) Handles lvServices.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If lvServices.GetItemAt(e.X, e.Y) IsNot Nothing Then
                lvServices.SelectedItems.Clear()
                lvServices.GetItemAt(e.X, e.Y).Selected = True
                cmnServices.Show(lvServices, New Point(e.X, e.Y))
            End If
        End If
    End Sub

    Private Sub DetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailsToolStripMenuItem.Click
        lvServices_DoubleClick(lvServices, Nothing)
    End Sub

    Private Sub RunToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunToolStripMenuItem.Click
        ' execute a service by POSTing it's URL.
        ' 1. Get parameter and response details
        Dim servitem As ListViewItem = lvServices.SelectedItems(0)
        Dim servCollid As String = servitem.SubItems(0).Text
        Dim servName As String = servitem.SubItems(1).Text
        Dim servUrl As String = servitem.SubItems(3).Text
        RunService(servCollid, servName, servUrl)
    End Sub

    Private Sub RunService(servCollid As String, servName As String, servUrl As String)
        ' get defined parameter and response layout
        Dim resp As String = HttpGet(servUrl)
        If resp = "" Then
            MsgBox(String.Format("Unable to contact '{0}'", _lastHost), MsgBoxStyle.OkOnly, __app)
            Exit Sub
        End If
        Dim service = JsonConvert.DeserializeObject(resp)
        Dim stcode As Integer = service("StatusCode")
        If stcode <> 0 And (stcode < 200 Or stcode >= 300) Then
            MsgBox("ERROR: " + service("StatusDescription").ToString(), MsgBoxStyle.OkOnly, __app)
            Exit Sub
        End If
        Dim requestSchema = service(servName)("RequestSchema")
        Dim responseSchema = service(servName)("ResponseSchema")
        Dim parmCount As Integer = 0
        Dim parms As Collection(Of Parameter) = New Collection(Of Parameter)
        Dim resultset As Collection(Of Parameter) = New Collection(Of Parameter)
        Dim anonresultsets As Integer = 0
        Dim outputparms As Collection(Of Parameter) = New Collection(Of Parameter)
        Dim requiredParms As String = ""
        Dim requiredResponse As String = ""
        Try
            requiredParms = requestSchema("required").ToString
        Catch ex As Exception
        End Try
        Try
            requiredResponse = responseSchema("required").ToString()
        Catch ex As Exception
        End Try
        ' interpret parameters
        If requestSchema("properties") IsNot Nothing Then
            Dim idx As Integer = 0
            Dim parm = requestSchema("properties").First()
            While idx < requestSchema("properties").Count()
                Dim strparm As String = parm.ToString()
                Dim strparmparts As String() = strparm.Split(":")
                Dim parmname As String = strparmparts(0).Trim("""".ToCharArray())
                Dim parmdetails = JsonConvert.DeserializeObject(requestSchema("properties")(parmname).ToString())
                Dim p As Parameter = New Parameter()
                p.name = parmname
                p.nullable = False
                Dim tp As String = ""
                If parmdetails("type") IsNot Nothing Then
                    ' handle nullable and typed (string, integer, etc) parms
                    If parmdetails("type").ToString().Contains(",") Then
                        For Each ptp In parmdetails("type")
                            If ptp = "null" Then
                                p.nullable = True
                            Else
                                tp += ptp + " "
                            End If
                        Next
                    Else
                        tp = parmdetails("type").ToString()
                    End If
                Else
                    ' handle enum (listed value) parms
                    If parmdetails("enum") IsNot Nothing Then
                        For Each ptp In parmdetails("enum")
                            If tp <> "" Then
                                tp += " | "
                            Else
                                tp = "enum: "
                            End If
                            tp += ptp
                        Next
                    End If
                End If
                p.type = tp
                If parmdetails("maxLength") IsNot Nothing Then
                    p.length = parmdetails("maxLength")
                Else
                    p.length = 0
                End If
                parms.Add(p)
                If parm.Next Is Nothing Then
                    parm = parm.Last
                Else
                    parm = parm.Next
                End If
                idx += 1
            End While
        End If
        ' interpret defined response properties - NB doesn't cover dynamic result sets from stored procedures
        If responseSchema("properties") Is Nothing Then
            'MsgBox("No output defined - if you see this then there's definitely a problem!!!", MsgBoxStyle.OkOnly)
        Else
            ' catch output parameter profiles
            If responseSchema("properties")("Output Parameters") IsNot Nothing Then
                Dim oparms = responseSchema("properties")("Output Parameters")("properties")
                If oparms IsNot Nothing Then
                    Dim opcount As Integer = oparms.Count()
                    Dim opi As Integer = 0
                    Dim opitem = oparms.First
                    While opi < opcount
                        Dim opstr As String = opitem.ToString()
                        Dim opstrparts As String() = opstr.Split(":")
                        Dim opname As String = opstrparts(0).Trim("""".ToCharArray())
                        Dim opdets = JsonConvert.DeserializeObject(oparms(opname).ToString())
                        Dim op As Parameter = New Parameter()
                        op.name = opname
                        op.nullable = False
                        Dim optp As String = ""
                        For Each otp In opdets("type")
                            If otp = "null" Then
                                op.nullable = False
                            Else
                                optp += otp + " "
                            End If
                        Next
                        If optp = "" Then
                            op.type = opdets("type")
                        Else
                            op.type = optp
                        End If
                        If opdets("maxLength") IsNot Nothing Then
                            op.length = opdets("maxLength")
                        Else
                            op.length = 0
                        End If
                        outputparms.Add(op)
                        If opitem.Next Is Nothing Then
                            opitem = opitem.Last
                        Else
                            opitem = opitem.Next
                        End If
                        opi += 1
                    End While
                End If
            End If
            ' catch defined resultset profile
            Dim rscount = 0
            Try
                rscount = responseSchema("properties")("ResultSet Output")("items")("properties").Count()
            Catch ex As Exception
            End Try
            If rscount > 0 Then
                Dim idy As Integer = 0
                Dim rs = responseSchema("properties")("ResultSet Output")("items")("properties").First
                While idy < rscount
                    Dim strrs As String = rs.ToString()
                    Dim strrsparts As String() = strrs.Split(":")
                    Dim rsname As String = strrsparts(0).Trim("""".ToCharArray())
                    Dim rsdets = JsonConvert.DeserializeObject(responseSchema("properties")("ResultSet Output")("items")("properties")(rsname).ToString())
                    Dim r As Parameter = New Parameter()
                    r.name = rsname
                    r.nullable = False
                    Dim tp As String = ""
                    For Each rtp In rsdets("type")
                        If rtp = "null" Then
                            r.nullable = True
                        Else
                            tp += rtp + " "
                        End If
                    Next
                    If tp = "" Then
                        r.type = rsdets("type")
                    Else
                        r.type = tp
                    End If
                    If rsdets("maxLength") IsNot Nothing Then
                        r.length = rsdets("maxLength")
                    Else
                        r.length = 0
                    End If
                    resultset.Add(r)
                    If rs.Next Is Nothing Then
                        rs = rs.Last
                    Else
                        rs = rs.Next
                    End If
                    idy += 1
                End While
            Else
                ' if no defined ResulSet, there might be Anonymous ResultSets (undefined profile)
                Dim ars = responseSchema("properties")("Anonymous ResultSets")
                If ars IsNot Nothing Then
                    anonresultsets = 1
                End If
            End If
        End If
        Dim parameterJSON As String = ""
        ' prompt the user for parameters if any exist
        If parms.Count > 0 Then
            ' display a dynamic parameters form to allow the user to provide input
            Dim f As Form = New Form
            With f
                .Text = "Service Parameters for " + servName
                .Width = 600
                .Height = 400
                .AutoScroll = True
            End With
            Dim header As ParmHeader = New ParmHeader(New Point(10, 10), f)
            f.Width = header.width + 30
            Dim pcol As Collection(Of ParmLine) = New Collection(Of ParmLine)
            Dim curY As Integer = header.location.X + header.height + 5
            For Each parm In parms
                Dim notes As String = ""
                Dim nullable As String = ""
                If parm.nullable Then
                    nullable = "Nullable "
                End If
                If parm.length > 0 Then
                    notes = String.Format("{0}{1} max length {2}", nullable, parm.type, parm.length)
                Else
                    notes = String.Format("{0}{1}", nullable, parm.type)
                End If
                Dim inuse As Boolean = False
                If requiredParms.Contains(parm.name) Then
                    inuse = True
                End If
                Dim nrow As ParmLine = New ParmLine(parm.name, inuse, parm.nullable, notes, New Point(10, curY), f)
                curY += nrow.height + 3
                pcol.Add(nrow)
            Next
            Dim btnParmsOkay As Button = New Button
            btnParmsOkay.Text = "OK"
            btnParmsOkay.DialogResult = Windows.Forms.DialogResult.OK
            Dim btnParmsCancel As Button = New Button
            btnParmsCancel.Text = "Cancel"
            btnParmsCancel.DialogResult = Windows.Forms.DialogResult.Cancel
            ' location for the buttons is going to be either:
            ' * bottom of the screen
            ' * after all the parms if there are so many that we overflow (scroll bars)
            btnParmsOkay.Location = New Point((f.Width / 2) - (btnParmsOkay.Width + 5), curY)
            btnParmsCancel.Location = New Point((f.Width / 2) + 5, curY)
            If curY + btnParmsOkay.Height + 10 < f.Height Then
                Dim pan As Panel = New Panel()
                pan.Width = btnParmsOkay.Width + btnParmsCancel.Width + 10
                pan.Height = btnParmsOkay.Height + 5
                '                pan.Location = btnParmsOkay.Location
                btnParmsOkay.Location = New Point(0, 0)
                btnParmsCancel.Location = New Point(btnParmsOkay.Width + 5, 0)
                pan.Controls.Add(btnParmsOkay)
                pan.Controls.Add(btnParmsCancel)
                Dim py = f.Height - (btnParmsOkay.Height + 47)
                Dim px = (f.Width / 2) - (btnParmsOkay.Width + 5)
                If px < 0 Then px = 100
                pan.Location = New Point(px, py)
                f.Controls.Add(pan)
            Else
                f.Controls.Add(btnParmsOkay)
                f.Controls.Add(btnParmsCancel)
            End If
            f.AcceptButton = btnParmsOkay
            f.CancelButton = btnParmsCancel
            ' loop here to cover parameter verification
            Dim proceed = False
            Dim strparm As String = ""
            While Not proceed
                If f.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
                proceed = True
                For Each prow In pcol
                    If prow.enabled.Checked Then
                        Dim thisparm As String = prow.name.Text
                        Dim thisval As String = ""
                        If prow.notes.Text.StartsWith("enum:") Then
                            Dim cmb As ComboBox = CType(prow.valuef, ComboBox)
                            thisval = cmb.SelectedItem
                            If thisval = "" Then
                                proceed = False
                            End If
                        Else
                            thisval = prow.valuef.Text
                        End If
                        If prow.nullf.Checked Then
                            thisval = "null"
                        ElseIf prow.notes.Text.ToLower.Contains("string") Or prow.notes.Text.ToLower.StartsWith("enum:") Then
                            thisval = """" + thisval + """"
                        End If
                        If strparm <> "" Then
                            strparm += ","
                        End If
                        strparm += String.Format("""{0}"":{1}", thisparm, thisval)
                    End If
                Next
            End While
            parameterJSON = "{" + strparm + "}"
        End If
        ' 2. Make call
        Dim postResponse As String = HttpPost(servUrl, parameterJSON)
        If postResponse <> "" Then
            ' define form results form
            Dim fo As Form = New Form()
            fo.Width = 600
            fo.Height = 400
            fo.AutoScroll = True
            fo.Text = servName + " Response"
            ' add tab strip
            Dim tabs As TabControl = New TabControl()
            tabs.Dock = DockStyle.Fill
            fo.Controls.Add(tabs)
            ' Did we get anything back (status, etc)?
            Dim rjs = JsonConvert.DeserializeObject(postResponse)
            If rjs.ToString() = "" Then
                Exit Sub
            End If
            ' Bad status returned?
            Dim postStcode = rjs("StatusCode")
            If postStcode < 200 Or postStcode >= 300 Then
                MsgBox(rjs("StatusDescription").ToString(), MsgBoxStyle.OkOnly, __app)
                Exit Sub
            End If
            ' set heading width and padding
            Dim hdwidth As Integer = 100
            Dim hdpad As Integer = 5
            Dim hdheightpad As Integer = 5
            ' Add StatusCode and StatusDescription to "Return Status" tab
            Dim yp As Integer = 5
            Dim rstat As TabPage = New TabPage()
            rstat.Text = "Returned Status"
            Dim lbStat As Label = New Label()
            lbStat.Text = "Status code :"
            lbStat.Width = hdwidth
            Dim txtStatVal As TextBox = New TextBox()
            txtStatVal.Text = String.Format("{0}", stcode)
            txtStatVal.ReadOnly = True
            lbStat.Location = New Point(5, yp)
            txtStatVal.Location = New Point(5 + hdwidth + hdpad, yp)
            yp += txtStatVal.Height + hdheightpad
            rstat.Controls.Add(lbStat)
            rstat.Controls.Add(txtStatVal)
            Dim lbStatDesc As Label = New Label()
            lbStatDesc.Text = "Status description :"
            lbStat.Width = hdwidth
            Dim txtStatDescVal As TextBox = New TextBox()
            txtStatDescVal.ReadOnly = True
            txtStatDescVal.Multiline = True
            txtStatDescVal.Width = 300
            txtStatDescVal.Height = 50
            txtStatDescVal.Text = rjs("StatusDescription").ToString()
            lbStatDesc.Location = New Point(5, yp)
            txtStatDescVal.Location = New Point(5 + hdwidth + hdpad, yp)
            yp += txtStatDescVal.Height + hdheightpad
            rstat.Controls.Add(lbStatDesc)
            rstat.Controls.Add(txtStatDescVal)
            tabs.TabPages.Add(rstat)
            ' Add output parms (if any)
            If outputparms.Count() > 0 Then
                ' Add output parms
                Dim lbOpHr As Label = New Label
                lbOpHr.Text = "Output Parms :"
                lbOpHr.Width = hdwidth
                lbOpHr.Location = New Point(5, yp)
                rstat.Controls.Add(lbOpHr)
                yp += txtStatVal.Height + hdheightpad
                For Each opvar In outputparms
                    Dim opval As String = rjs("Output Parameters")(opvar.name).ToString()
                    Dim lbOpValHdr As Label = New Label()
                    lbOpValHdr.Text = opvar.name
                    lbOpValHdr.Width = hdwidth
                    Dim txtOpVal As TextBox = New TextBox()
                    txtOpVal.Text = opval
                    txtOpVal.ReadOnly = True
                    If opvar.type.Contains("string") And opvar.length > 30 Then
                        txtOpVal.Width = 300
                        txtOpVal.Height = 50
                        txtOpVal.Multiline = True
                    End If
                    lbOpValHdr.Location = New Point(5, yp)
                    txtOpVal.Location = New Point(5 + hdwidth + hdpad, yp)
                    yp += txtOpVal.Height + hdheightpad
                    rstat.Controls.Add(lbOpValHdr)
                    rstat.Controls.Add(txtOpVal)
                Next
            End If
            ' Add result sets
            Dim rscount As Integer = 0
            ' dynamic / anonymous result sets
            If anonresultsets > 0 Then
                Dim rsi As Integer = 1
                Dim rsstr As String = String.Format("ResultSet {0} Output", rsi)
                Dim ars = rjs(rsstr)
                While ars IsNot Nothing
                    Dim tpdrs As TabPage = New TabPage()
                    tpdrs.Text = rsstr
                    AddData(tpdrs, ars.ToString())
                    tabs.TabPages.Add(tpdrs)
                    rscount += 1
                    rsi += 1
                    rsstr = String.Format("ResultSet {0} Output", rsi)
                    ars = rjs(rsstr)
                End While
            End If
            ' defined result set
            If rjs("ResultSet Output") IsNot Nothing Then
                Dim tprs As TabPage = New TabPage()
                tprs.Text = "ResultSet Output"
                tabs.TabPages.Add(tprs)
                AddData(tprs, rjs("ResultSet Output").ToString())
                rscount += 1
            End If
            ' handle service create / drop
            If stcode = 201 Or (stcode = 200 And rscount = 0) Or (stcode = 0 And rscount = 0) Then
                RefreshServiceList()
                Exit Sub
            End If
            fo.ShowDialog()
        End If
    End Sub

    Private Sub AddData(parent As Control, data As String)
        Dim firsttime As Boolean = True
        ' define data grid view
        Dim dgv As DataGridView = New DataGridView()
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        ' parse string to JSON
        Dim rowset = JsonConvert.DeserializeObject(data)
        ' define columns collection
        Dim resultset As Collection(Of Parameter) = New Collection(Of Parameter)()
        ' process each row
        Dim rp As Integer = 0
        Dim row = rowset.First
        While rp < rowset.Count
            If firsttime Then
                ' get column names
                Dim col = row.First
                Dim colct As Integer = row.Count()
                Dim coli As Integer = 0
                While coli < colct
                    Dim colstr As String = col.ToString()
                    Dim colstrparts As String() = colstr.Split(":")
                    Dim colname As String = colstrparts(0).Trim("""")
                    ' build column collection used to process rows
                    Dim thiscol As Parameter = New Parameter()
                    thiscol.name = colname
                    resultset.Add(thiscol)
                    ' add column to data grid
                    Dim dgvc As DataGridViewColumn = New DataGridViewColumn()
                    dgvc.CellTemplate = New DataGridViewTextBoxCell()
                    dgvc.HeaderText = colname
                    dgv.Columns.Add(dgvc)
                    ' next column
                    coli += 1
                    If col.Next Is Nothing Then
                        col = col.Last
                    Else
                        col = col.Next
                    End If
                End While
                firsttime = False
            End If
            ' with each row
            Dim rowdata As String = ""
            Dim thisrow As Integer = dgv.Rows.Add()
            Dim cp As Integer = 0
            ' interpret data
            For Each rsval In resultset
                Dim rcval = row(rsval.name)
                If rowdata <> "" Then
                    rowdata += ","
                End If
                If rsval.type = "string" Then
                    rowdata += String.Format("""{0}""", rcval)
                Else
                    rowdata += String.Format("{0}", rcval)
                End If
                dgv.Rows(thisrow).Cells(cp).Value = rcval
                cp += 1
            Next
            If __debug Then
                Console.WriteLine(rowdata)
            End If
            If row.Next Is Nothing Then
                row = row.Last
            Else
                row = row.Next
            End If
            rp += 1
        End While
        For Each cw In dgv.Columns
            cw.width = cw.getpreferredwidth(DataGridViewAutoSizeColumnMode.AllCells, True)
        Next
        dgv.Dock = DockStyle.Fill
        parent.Controls.Add(dgv)
    End Sub

    ' HTTP POST - optionally with JSON data parms
    Private Function HttpPost(requestPath As String, parmData As String) As String
        Dim strurl As String = ""
        Dim prefix = requestPath.Split(":".ToCharArray())
        If prefix(0) = "http" Or prefix(0) = "https" Then
            strurl = requestPath
        Else
            strurl = __baseurl & requestPath
        End If
        Dim req As HttpWebRequest
        Dim resp As HttpWebResponse
        Dim enc As New System.Text.UTF8Encoding
        req = DirectCast(WebRequest.Create(strurl), HttpWebRequest)
        req.Accept = "application/json"
        req.ContentType = "application/json"
        req.Method = "POST"
        ' required to stop .Net adding header "Expect: 100-continue" which is not supported by DB2
        req.ServicePoint.Expect100Continue = False
        If __basicauth Then
            req.Headers.Add("Authorization", String.Format("Basic {0}", __b64authstr))
        Else
            req.ClientCertificates.Add(__certificate)
        End If
        Dim sw As Stream
        If parmData.Length > 0 Then
            Dim bdata As Byte() = enc.GetBytes(parmData)
            sw = req.GetRequestStream()
            sw.Write(bdata, 0, bdata.Length)
            sw.Close()
        End If
        Try
            resp = DirectCast(req.GetResponse(), HttpWebResponse)
        Catch e As System.Net.WebException
            resp = e.Response
        End Try
        If resp Is Nothing Then
            MsgBox(String.Format("Unable to connect to '{0}'", _lastHost), MsgBoxStyle.OkOnly, __app + " Connection Error")
            HttpPost = ""
        Else
            If __debug Then
                Console.WriteLine("Response headers:")
                Dim rsphdrs As WebHeaderCollection = resp.Headers()
                Dim i As Integer = 0
                While i < rsphdrs.Count()
                    Console.WriteLine("  {0}: {1}", rsphdrs.Keys(i), rsphdrs.Item(i))
                    i += 1
                End While
            End If
            Dim srJSON As String = ""
            If Not resp Is Nothing Then
                Dim sr As StreamReader = New StreamReader(resp.GetResponseStream())
                srJSON = sr.ReadToEnd()
                sr.Close()
                resp.Close()
            Else
                srJSON = "Error - no data returned"
            End If
            If __debug Then
                Console.WriteLine("Returned POST data : {0}", srJSON)
            End If
            HttpPost = srJSON
        End If
    End Function

    ' Drop a service by driving the DB2ServiceManager (HTTP POST)
    Private Sub DropToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DropToolStripMenuItem.Click
        Dim servitem As ListViewItem = lvServices.SelectedItems(0)
        Dim servCollid As String = servitem.SubItems(0).Text
        Dim servName As String = servitem.SubItems(1).Text
        Dim servUrl As String = servitem.SubItems(3).Text
        If servCollid = "" And servName.StartsWith("DB2Service") Then
            MsgBox("We do not support attempting to drop the IBM supplied services", MsgBoxStyle.OkOnly, __app)
            Exit Sub
        End If
        If MsgBox(String.Format("CONFIRM: Really drop service {0}.{1}?", servCollid, servName), MsgBoxStyle.OkCancel, __app + " Confirm Service Drop") = MsgBoxResult.Ok Then
            Dim jsonParms As String = String.Format("""collectionID"":""{0}"",""serviceName"":""{1}"",""requestType"":""dropService""", servCollid, servName)
            jsonParms = "{" + jsonParms + "}"
            Dim dropResponse = HttpPost(__baseurl + "DB2ServiceManager", jsonParms)
            'parse response
            Dim service = JsonConvert.DeserializeObject(dropResponse)
            Dim stcode As Integer = service("StatusCode")
            Dim stmsg As String = service("StatusDescription")
            If stcode = 200 Then
                'it dropped, so refresh the service list and put a message up
                RefreshServiceList()
                MsgBox(String.Format("Service {0}.{1} was successfully dropped", servCollid, servName), MsgBoxStyle.OkOnly, __app + " Successful Drop")
            Else
                'otherwise, put up a message and report StatusDescription
                MsgBox("Error - failed to drop service: " + stmsg, MsgBoxStyle.OkOnly, __app + " Failed Drop")
            End If
        End If
    End Sub

    ' Create a new service by driving the DB2ServiceManager
    Private Sub btnNewService_Click(sender As Object, e As EventArgs) Handles btnNewService.Click
        RunService("", "DB2ServiceManager", __baseurl + "DB2ServiceManager")
    End Sub
End Class

' Used in a collection to hold details about parameters (input and output)
Public Class Parameter
    Public name As String
    Public type As String
    Public length As Integer
    Public nullable As Boolean
End Class

' Dynamic panel input parameter line - used in a collection
Public Class ParmLine
    Public name As Label
    Public enabled As CheckBox
    Public nullf As CheckBox
    Public valuef As Control
    Public notes As Label
    Public parent As Form
    Public height As Integer
    Public width As Integer
    Public location As Point
    Sub New(strname As String, inuse As Boolean, nullable As Boolean, strnotes As String, xy As Point, ByRef owner As Form)
        name = New Label
        name.Text = strname
        name.Location = xy
        name.Width = 100
        enabled = New CheckBox
        enabled.CheckAlign = ContentAlignment.MiddleCenter
        enabled.Checked = inuse
        enabled.Location = New Point(xy.X + name.Width + 10, xy.Y)
        enabled.Width = 40
        If inuse Then ' if it's required, don't let the user disable it
            enabled.Enabled = False
        End If
        nullf = New CheckBox
        nullf.CheckAlign = ContentAlignment.MiddleCenter
        nullf.Checked = False
        nullf.Enabled = nullable
        nullf.Location = New Point(enabled.Location.X + enabled.Width + 10, xy.Y)
        nullf.Width = 40
        If strnotes.StartsWith("enum:") Then
            Dim cmbValue As ComboBox = New ComboBox()
            Dim enumVals As String = strnotes.Substring(6)
            Dim evals As String() = enumVals.Split("|")
            For Each xyz As String In evals
                cmbValue.Items.Add(xyz.Trim())
            Next
            valuef = cmbValue
        Else
            Dim txtValue As TextBox = New TextBox()
            txtValue.Text = ""
            valuef = txtValue
        End If
        valuef.Width = 280
        valuef.Location = New Point(nullf.Location.X + nullf.Width + 10, xy.Y)
        notes = New Label
        notes.Text = strnotes
        notes.Location = New Point(valuef.Location.X + valuef.Width + 10, xy.Y)
        notes.Width = 200
        height = notes.Height
        width = notes.Location.X + notes.Width - xy.X
        location = New Point(xy.X, xy.Y)
        owner.Controls.Add(name)
        owner.Controls.Add(enabled)
        owner.Controls.Add(nullf)
        owner.Controls.Add(valuef)
        owner.Controls.Add(notes)
    End Sub
End Class

' The headers that go above Parameter on the dynamic form
Public Class ParmHeader
    Public name As Label
    Public enabled As Label
    Public nullf As Label
    Public valuef As Label
    Public notes As Label
    Public parent As Form
    Public height As Integer
    Public width As Integer
    Public location As Point
    Sub New(xy As Point, ByRef owner As Form)
        Dim f As Font = New Font(owner.Font, FontStyle.Bold)
        name = New Label
        name.Font = f
        name.BorderStyle = BorderStyle.Fixed3D
        name.Text = "Name"
        name.Location = xy
        name.Width = 100
        enabled = New Label
        enabled.Font = f
        enabled.BorderStyle = BorderStyle.Fixed3D
        enabled.Text = "Used?"
        enabled.Location = New Point(xy.X + name.Width + 10, xy.Y)
        enabled.Width = 40
        nullf = New Label
        nullf.Font = f
        nullf.BorderStyle = BorderStyle.Fixed3D
        nullf.Text = "Null?"
        nullf.Location = New Point(enabled.Location.X + enabled.Width + 10, xy.Y)
        nullf.Width = 40
        valuef = New Label
        valuef.Font = f
        valuef.BorderStyle = BorderStyle.Fixed3D
        valuef.Text = "Value"
        valuef.Width = 280
        valuef.Location = New Point(nullf.Location.X + nullf.Width + 10, xy.Y)
        notes = New Label
        notes.Font = f
        notes.BorderStyle = BorderStyle.Fixed3D
        notes.Text = "Notes"
        notes.Location = New Point(valuef.Location.X + valuef.Width + 10, xy.Y)
        notes.Width = 200
        height = notes.Height
        width = notes.Location.X + notes.Width - xy.X
        location = New Point(xy.X, xy.Y)
        owner.Controls.Add(name)
        owner.Controls.Add(enabled)
        owner.Controls.Add(nullf)
        owner.Controls.Add(valuef)
        owner.Controls.Add(notes)
    End Sub
End Class

' An extension to the ListView class for dynamic (added by code) controls that allows us to add some additional behaviour:
' 1. Select all (control-A)
' 2. Copy selected (control-C) items to the clipboard
Public Class MyListViewClass
    Inherits ListView
    Protected Sub MyListViewClass_KeyDown(sender As Object, e As KeyEventArgs) Handles MyClass.KeyDown
        If e.KeyCode = Keys.C And e.Modifiers = Keys.Control Then
            Dim snd As MyListViewClass = CType(sender, MyListViewClass)
            If snd.SelectedItems.Count() > 0 Then
                Clipboard.Clear()
                Dim strClip As String = ""
                For Each item In snd.SelectedItems
                    Dim i As Integer = 0
                    While i < item.SubItems.Count()
                        strClip += item.subitems(i).text
                        i += 1
                        If i < item.subitems.count() Then
                            strClip += ", "
                        End If
                    End While
                    strClip += vbNewLine
                Next
                Clipboard.SetText(strClip)
            End If
        ElseIf e.KeyCode = Keys.A And e.Modifiers = Keys.Control Then
            Dim snd As ListView = CType(sender, ListView)
            For i As Integer = 0 To snd.Items.Count() - 1
                snd.Items(i).Selected = True
            Next
        End If
    End Sub
End Class

' Used to manage JSON text output. NB for this to work requires that the output be managed like this:
'     sr = New StreamReader(resp.GetResponseStream())
'     srJSON = sr.ReadToEnd()
'     srJSON = srJSON.Replace("ResultSet Output", "ResultSet_Output")    ' <-- required to map fields correctly
'     Dim rsp As RestResponse = JsonConvert.DeserializeObject(Of RestResponse)(srJSON)
'     rsp.report()
<Serializable> _
Public Class RestResponse
    Public ResultSet_Output As Object
    Public StatusCode As Integer
    Public StatusDescription As String
    ' debug support
    Function report() As String
        Dim rv As String = ""
        rv = String.Format("Status code = {0}", StatusCode)
        rv &= vbNewLine & String.Format("Description = {0}", StatusDescription)
        rv &= vbNewLine & String.Format("Result set  : {0}", ResultSet_Output.ToString())
        report = rv
    End Function
End Class

' Used to interpret the service list data returned by DB2 service manager
<Serializable> _
Public Class RESTServiceReport
    Public ServiceName As String
    Public ServiceCollectionID As String
    Public ServiceDescription As String
    Public ServiceProvider As String
    Public ServiceURL As String
End Class

<Serializable> _
Public Class DB2Services
    Public DB2Services As List(Of RESTServiceReport)
End Class

' Used by the frmMain lvServices list view so that sorting is by the first two columns (collid, name), rather than the
' default (just the first column)
Class ListViewServicesComparer
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
       Implements IComparer.Compare
        Dim strx As String = CType(x, ListViewItem).SubItems(0).Text + "@@@###@@@" + CType(x, ListViewItem).SubItems(1).Text
        Dim stry As String = CType(y, ListViewItem).SubItems(0).Text + "@@@###@@@" + CType(y, ListViewItem).SubItems(1).Text
        Return [String].Compare(strx, stry)
    End Function
End Class
