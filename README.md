[![Windows Build](https://ci.appveyor.com/api/projects/status/github/crisfervil/tinychat?svg=true)](https://ci.appveyor.com/project/crisfervil/tinychat)

# TinyChat
Tiny chat application that works on a LAN infrastructure

# Description
What's the minimun number of lines of code do you need to write a messenger application that works on a LAN network?

That's a question I've asked my colleagues the other day, and this is my proposed solution. 

It's a **108** lines long, but I think it can be reduced even further. 

The program creates a temporary .txt file containing the chat lines and monitors its changes to show other users messages.

# How to use it?

Download the **TinyChat.exe** file from the [releases section](https://github.com/crisfervil/TinyChat/releases) in GitHub, copy the file to a network shared folder, and run it from there.

The application first asks for a nick name, and then enters in display mode, to show the conversation lines. 

In order to enter in the write mode, press any key and start typing. Hit enter to send the message. 

![Demo image](img/demo.gif "Demo image")