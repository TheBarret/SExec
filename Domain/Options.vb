Imports System.IO
Imports System.Security
Imports System.Security.Permissions

Namespace Domain
    Public NotInheritable Class Options
        Const DomainPath As String = ".\Root\"
        Public Property Name As String
        Public Property Filename As String
        Public Property Setup As AppDomainSetup
        Public Property Permissions As PermissionSet
        Public Sub New()
            Me.Filename = New FileInfo(Options.DomainPath).FullName
            If Not Directory.Exists(Filename) Then Directory.CreateDirectory(Me.Filename)
            Me.Setup = New AppDomainSetup With {.ApplicationBase = filename}
            Me.Permissions = New PermissionSet(PermissionState.None)
            Me.Permissions.AddPermission(New SecurityPermission(SecurityPermissionFlag.Execution))
        End Sub
    End Class
End Namespace