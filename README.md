# VsNerdX
[NERDTree](https://github.com/scrooloose/nerdtree) inspired Solution Explorer for Visual Studio. It integrates VIM bindings for tree navigation and manipulation into Visual Studio's hierarchy windows.

# Install
Download [`VsNerdX.vsix`](https://github.com/dyskette/vs-nerdx/releases/latest/download/VsNerdX.vsix) from the latest release (its SHA256 is in `VsNerdX.vsix.sha256`) and open it, with Visual Studio closed.

# Build
`msbuild VsNerdX.sln /restore /p:Configuration=Release` builds `VSNerdX-26/bin/Release/VSNerdX-26.vsix`; it needs Visual Studio's MSBuild and the .NET Framework 4.7.2 targeting pack.

# Release
Bump `Version` in `VSNerdX-26/source.extension.vsixmanifest`, then push a matching tag (`v1.0.3` for `1.0.3`). The `Build` workflow builds every push and pull request, and publishes the VSIX for a tag; it fails if the tag and the manifest disagree.

# Keys

The map follows the VS Code explorer bindings that
[vscode-neovim](https://github.com/vscode-neovim/vscode-neovim) installs, so the
same fingers work in both editors. Where vscode-neovim binds nothing, the
[nvim-tree](https://github.com/nvim-tree/nvim-tree.lua) default is used instead:
`P`, `W`, `c`, `gy`, `ge`, `H`.

`Enter` is not bound here. It is passed through unhandled so Visual Studio opens
the selection itself.

## Navigation

| Key | Action | From |
|-----|--------|------|
| `j` / `k` | down / up | both |
| `J` / `K` | last / first child | nvim-tree |
| `h` | collapse the containing folder | vscode-neovim (`list.collapse`) |
| `P` | go to parent | nvim-tree |
| `gg` / `G` | top / bottom | both |

## Opening

| Key | Action | From |
|-----|--------|------|
| `l` | open | vscode-neovim (`list.select`) |
| `o` / `O` | toggle node / open recursively | both |
| `go` | preview | VsNerdX |
| `v` | open in vertical split | vscode-neovim (`explorer.openToSide`) |
| `s` | open in horizontal split | VsNerdX |
| `C` | close node recursively | VsNerdX |
| `W` / `M` | collapse everything | nvim-tree / vscode-neovim (`zM`) |

## Files

| Key | Action | From |
|-----|--------|------|
| `a` / `A` | new file / new folder | both |
| `r` | rename | both |
| `d` | delete | both |
| `x` | cut | both |
| `y` | copy | vscode-neovim |
| `c` | copy | nvim-tree |
| `p` | paste | both |
| `gy` | copy full path | nvim-tree |
| `ge` | copy visible text | nvim-tree (`ge` copies the basename) |
| `H` / `I` | toggle show all files | nvim-tree / VsNerdX |
| `e` | open in File Explorer | VsNerdX |

## Find and help

| Key | Action |
|-----|--------|
| `/` | enter find mode |
| `Esc` | leave find mode, or clear a pending prefix |
| `?` / `g?` | toggle help |
