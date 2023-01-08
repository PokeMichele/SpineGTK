# SpineGTK
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![Linux](https://img.shields.io/badge/Linux-FCC624?style=for-the-badge&logo=linux&logoColor=black) [![License: CC BY-NC 4.0](https://img.shields.io/badge/License-CC_BY--NC_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc/4.0/)
SpineGTK is a simple GUI Launcher for Spine made with GTKSharp and Mono.

## Platforms
|Platform|Status                                               |
|--------|-----------------------------------------------------|
|Linux   |Working                                              |
|Windows |Some Functions may work, but you don't need to run it|
|MacOS   |Not Working                                          |

## Installation
- ### Mono
    - First of all you need to install Mono (6.12.0.182+):
        - On Debian-Based Distros:
            ```
            sudo apt-get install mono-complete
            ```
        - On RedHat-Based Distros:
            ```
            sudo dnf install mono-devel
            ```
        - On Arch-Based Distros
            ```
            sudo pacman -S mono
            ```
    - Make sure you have all the permissions needed to run the file
        ```
        chmod +x /your/directory/SpineGTK_v1.exe
        ```
    - Install the dependencies (only if needed - [See How](https://github.com/GtkSharp/GtkSharp/blob/develop/README.md))
    - Execute the Software with all its dependencies:
        ```
        mono SpineGTK_v1.exe -r:adk-sharp.dll -r:gdk-sharp.dll -r:gio-sharp.dll -r:glade-sharp.dll -r:glib-sharp.dll -r:gtk-sharp.dll -r:Mono.Posix.dll -r:pango-sharp -r:System.dll -r:System.IO.Compression.dll -r:System.IO.Compression.FileSystem.dll -r:System.Net.dll -r:System.Xml.dll -r:System.Xml.Linq.dll
        ```
        If you prefer you can create a shell script.
## Building from Source
- The simplest way to build this project is installing MonoDevelop and make it compile the project by itself:
    - Install MonoDevelop (See the official [Installation Instructions](https://www.monodevelop.com/download/))
    - Open the project
    - Compile it (with the button)
## FAQ
- Why C#?
    - I wanted to practice with C# and I think it's one of the best Object-Oriented Languages to use for this project.
- Why Mono and GTKSharp?
    - I used Mono and GTKSharp because they are simpler to learn than other Cross-Platform Frameworks, like [Avalonia](https://avaloniaui.net/) + You can run the software with all your beautiful GTK Themes.
## Credits & License
 - SpineGTK is made using [Mono](https://www.mono-project.com/) and [GTKSharp](https://www.mono-project.com/docs/gui/gtksharp/) and it's released under [CC BY-NC 4.0 License](https://creativecommons.org/licenses/by-nc/4.0/).
