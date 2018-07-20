Imports SExec.Interfaces
Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions

Namespace Domain
    Public NotInheritable Class Host
        Inherits MarshalByRefObject
        Implements IHost, IDisposable
        Sub New()
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf Me.ResolveEvent
        End Sub
        Public Sub Load(Name As AssemblyName)
            Call New FileIOPermission(PermissionState.Unrestricted).Assert()
            Assembly.Load(Name)
            CodeAccessPermission.RevertAll()
        End Sub
        Public Function Resolve(Name As String) As ITarget Implements IHost.Resolve
            Return New Target(Name)
        End Function
        Private Function ResolveEvent(Sender As Object, Parameters As ResolveEventArgs) As Assembly
            Return (From assembly In AppDomain.CurrentDomain.GetAssemblies
                    Where assembly.FullName = Parameters.Name
                    Select assembly).FirstOrDefault()
        End Function
#Region "Disposable"
        Private disposedValue As Boolean
        Protected Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf Me.ResolveEvent
                End If
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace