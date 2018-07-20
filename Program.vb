Imports SExec.Domain
Imports SExec.Interfaces
Imports System.Security.Permissions

Module Program

    Sub Main()

        Try
            Dim options As New Options

            '// We deny these privileges 
            options.Permissions.AddPermission(New FileIOPermission(PermissionState.None))
            options.Permissions.AddPermission(New RegistryPermission(PermissionState.None))
            options.Permissions.AddPermission(New EnvironmentPermission(PermissionState.None))

            Console.WriteLine("Loading assembly...")

            Using sandbox As IConfinement = Confinement.Create(New Container(options), Environment.Create("Sandbox", "Tester.dll"))

                '// Creating our proxy gateway
                Dim target As Proxy = sandbox.Resolve("Tester.Demo")

                '// These test methods attempt to access al the above permissions
                '// But the sandbox will not allow it and will throw an execption
                '// We capture these exceptions and expand their info

                target.Invoke("SecurityTest1")
                target.Invoke("SecurityTest2")
                target.Invoke("SecurityTest3")

            End Using

        Catch ex As Exception
            Console.WriteLine("Exception Caught: {0}", ex.Message)
        End Try
        Console.Read()
    End Sub
End Module
