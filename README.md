# NetworkVisualizer
 +Network Visualizer in C# using Sigma.js
 **How to use this**
 *	Use windows
 *	Make sure WinPcap is installed
 *	Open the 'MyPacketCapturer' project in visual studio
 *	Check the JSONFileAddress variable in 'Program.cs' and make sure it is pointed at "HTMLClient\data\graphData.json"
 *	Check the file 'htmlClient\htmlClient.html' and make sure it is pointed at '\data\graphData.json'
 *	Run the project in visual studio for a bit and let it populate the list (like 30 seconds)
 *	Run zervit.exe in the htmlClient folder (it's a mini HTTP server.)
 *	open your browser to 127.0.0.1:80