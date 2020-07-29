# Reload

"Reload" is a basic multiplatform game/graphics engine template
written in C#/.net5 and I use it for building
the "Reload" game.This is a small personal project, licenced under the MIT
licence, and is provided "AS IS". At the moment I have NO plan, NOR knowledge to
maintain it as a serious project, so use it at your own risk.


## Get Reload

Clone with SSH:

``` bash
git clone git@gitlab.com:reload_group/reloadsharp.git
```

Or alternatively clone with HHTPS:

```bash
git clone https://gitlab.com/reload_group/reloadsharp.git
```

then go to the repo directory and run: 

``` bash
dotnet restore
```

to install the required NuGet packages.

## Dependencies:

### Glfw

#### Linux:

Glfw varsion 3.3 or greater is required for the Cross platform GUI. 
If you are running Debian (or other distro with glfw 3.2 available in the package manager), you first have to remove the old version using

```bash
sudo apt remove libglfw3 libglfw3-dev
```

To clone the Glwf repo run:

```bash
git clone https://github.com/glfw/glfw.git
```

then go to the repo directory and run:

```bash
cmake -DBUILD_SHARED_LIBS=ON .
make
sudo make install
```

if you need different configuration or stumble upon some errors,
follow the instructions on this [link](https://www.glfw.org/docs/latest/compile.html).

#### Windows:
A glfw.dll file is included so there is no need to do anything.

#### Mac OSX:
???

### Open AL

For cross platform audio using OpenAl you have to install it first:

#### Linux:
Debian/Ububtu:

```bash
sudo apt install libopenal-dev
```

Fedora:

```bash
sudo dnf install openal-soft-devel
```

#### Windows:
Download the installer from this [link](https://www.openal.org/downloads/).

#### Mac OSX:
???

### SPIR-V

For cross compilation of shaders
