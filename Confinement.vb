Imports SExec.Interfaces

Public NotInheritable Class Confinement
    Implements IConfinement
    Public Property Host As IHost
    Sub New(Host As IHost)
        Me.Host = Host
    End Sub
    Public Function Resolve(Name As String) As Proxy Implements IConfinement.Resolve
        Return New Proxy(Me.Host.Resolve(Name))
    End Function
    Public Shared Function Create(Container As IContainer, environment As IEnvironment) As IConfinement
        Return New Confinement(Container.Create(environment))
    End Function
#Region "Disposable"
    Private disposedValue As Boolean
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Me.Host = Nothing
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
