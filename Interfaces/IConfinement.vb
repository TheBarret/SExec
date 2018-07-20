Namespace Interfaces
    Public Interface IConfinement
        Inherits IDisposable
        Function Resolve(Name As String) As Proxy
    End Interface
End Namespace