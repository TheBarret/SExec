Imports SExec.Interfaces
Imports System.Security.Policy
Imports System.Reflection

Namespace Domain
    Public Class Container
        Implements IContainer
        Public Property Options As Options
        Sub New(Optional Options As Options = Nothing)
            Me.Options = Options
            If (Options Is Nothing) Then Me.Options = New Options
        End Sub
        Private Function Create(Environment As IEnvironment) As IHost Implements IContainer.Create
            Dim Host As Host = Me.CreateInstance(
                                Me.CreateDomain(
                                    If(Me.Options.Name, Environment.Name),
                                    GetType(Confinement).Assembly.Evidence.GetHostEvidence(Of StrongName)()
                                    )
                                )
            Host.Load(GetType(Confinement).Assembly.GetName())
            For Each obj In Environment.ToList()
                If TypeOf obj Is AssemblyName Then
                    Host.Load(CType(obj, AssemblyName))
                End If
            Next
            Return Host
        End Function
        Private Function CreateDomain(Name As String, StrongName As StrongName) As AppDomain
            Return AppDomain.CreateDomain(Name, Nothing, Me.Options.Setup, Me.Options.Permissions, StrongName, GetType(Object).Assembly.Evidence.GetHostEvidence(Of StrongName)())
        End Function
        Private Function CreateInstance(Domain As AppDomain) As Host
            Return CType(Activator.CreateInstanceFrom(Domain, GetType(Host).Assembly.CodeBase, GetType(Host).FullName).Unwrap(), Host)
        End Function
    End Class
End Namespace