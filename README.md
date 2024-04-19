

## Acquisition and Configuration Document

*Yuheng Sun, Jingyuan Sun, Ruiyang Ji, Lucheng Wang, Meiqing Li, Yuting Yan*

#### ***1. Get applications:***
The client and server applications, alongside their respective source codes, are accessible via GitHub at [GitHub Repository](https://github.com/Jissee/Comp208). Each published version is prominently displayed in the "Release" section located on the right side of the main page.

To initiate the setup, download the `release.zip` file from the "Release" section and proceed to extract its contents. After extraction, both clinet and server programs will be generated.

#### ***2. Install .NET 8.0 Runtime***
It is requisite for all operational devices to have the .NET 8.0 runtime environment installed. This framework can be acquired and installed through Microsoft at [Download .NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

#### ***3. Deployment of Server Program***

***3.1. Local Network Execution (e.g., LAN play, local gaming):***
    **a.** Ensure that all devices within the LAN can communicate via IP addresses. Execute the server program on a computer. Note: direct execution of the executable file, i.e., `EoE.Server.exe`, is not recommended. Instead, utilize a startup script (`.bat` file) which can be edited by right-click on the file and choose "edit". We have provided a sample `bat` file, whose content should be like "EoE.Server.exe 0.0.0.0 25566". Among those values, `0.0.0.0` is the server ip, and `25566` is the server port. You can change them as you want. A successful server launch is indicated by the message "Server Started".
    **b.** Modify the firewall settings to open the designated port by adding an inport rule. Choose the TCP protocol, allow connections, and specify the port (as determined above).

***3.2. Public Network Execution:***
    For operation over the internet, employ a server with a public IP address, adhering to the setup described in section 1.

***3.3. Developer-Provided Testing Server:***
    The developers have also provided a server for testing purposes, which has been deployed on the Microsoft Azure cloud computing platform. The server's IP address and port number are as follows: `20.117.175.118:25566`.

#### ***4. Execution of Client Program:***

Execute the client program `EoE.Client.exe`. Enter the IP address and the port of the server, input the player name, review and accept the user agreement, and then click the connect button to log into the game.