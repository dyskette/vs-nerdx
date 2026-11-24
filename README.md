# VsNerdX
[NERDTree](https://github.com/scrooloose/nerdtree) inspired Solution Explorer for Visual Studio. It integrates VIM bindings for tree navigation and manipulation into Visual Studio's hierarchy windows.

# Install
Download [`VsNerdX.vsix`](https://github.com/dyskette/vs-nerdx/releases/latest/download/VsNerdX.vsix) from the latest release (its SHA256 is in `VsNerdX.vsix.sha256`) and open it, with Visual Studio closed.

# Build
`msbuild VsNerdX.sln /restore /p:Configuration=Release` builds `VSNerdX-26/bin/Release/VSNerdX-26.vsix`; it needs Visual Studio's MSBuild and the .NET Framework 4.7.2 targeting pack.

# Release
Bump `Version` in `VSNerdX-26/source.extension.vsixmanifest`, then push a matching tag (`v1.0.3` for `1.0.3`). The `Build` workflow builds every push and pull request, and publishes the VSIX for a tag; it fails if the tag and the manifest disagree.

# Keys

The map follows the [snacks.nvim](https://github.com/folke/snacks.nvim) explorer
in LazyVim, as bound on the VS Code explorer (`l h a A d r c m p y q`), so the same
fingers work in Neovim, VS Code and here. Where that map binds nothing, the
[nvim-tree](https://github.com/nvim-tree/nvim-tree.lua) default is used instead:
`P`, `W`, `gy`, `ge`, `H`. In the tables, "both" means snacks.nvim and nvim-tree agree.

`Enter` is not bound here. It is passed through unhandled so Visual Studio opens
the selection itself.

## Navigation

| Key | Action | From |
|-----|--------|------|
| `j` / `k` | down / up | both |
| `J` / `K` | last / first child | nvim-tree |
| `h` | collapse the containing folder | snacks.nvim |
| `P` | go to parent | nvim-tree |
| `gg` / `G` | top / bottom | both |

## Opening

| Key | Action | From |
|-----|--------|------|
| `l` | open | snacks.nvim |
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
| `m` | cut | snacks.nvim |
| `c` | copy | both |
| `p` | paste | both |
| `y` | copy path relative to the solution | snacks.nvim (VS Code's copy relative path) |
| `gy` | copy full path | nvim-tree |
| `ge` | copy visible text | nvim-tree (`ge` copies the basename) |
| `H` / `I` | toggle show all files | nvim-tree / VsNerdX |
| `e` | open in File Explorer | VsNerdX |
| `q` | close Solution Explorer | snacks.nvim |

## Find and help

| Key | Action |
|-----|--------|
| `/` | enter find mode |
| `Esc` | leave find mode, or clear a pending prefix |
| `?` / `g?` | toggle help |
