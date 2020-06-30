# Reload

"Reload" is a basic multiplatform game/graphics engine template
written in C#/.net5 and I use it for building
the "Reload" game.This is a small personal project, licenced under the MIT
licence, and is provided "AS IS". At the moment I have NO plan, NOR knowledge to
maintain it as a serious project, so use it at your own risk.

## Get Reload

with SSH
`git clone git@gitlab.com:reload_group/reloadsharp.git`

or with HHTPS
`git clone https://gitlab.com/reload_group/reloadsharp.git`

then run `dotnet restore` to install the required NuGet packages.

## Dependencies:

### Open AL

For cross platform audio using OpenAl you have to install it first:

Windows: [link](https://www.openal.org/downloads/) - download installer.

Linux:
    Debian/Ububtu: `sudo apt install libopenal-dev`
    Fedora: `sudo dnf install openal-soft-devel`
MacOS: ???

### SPIR-V

For cross compilation of shaders
