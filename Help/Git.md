# Git
Git is used as source control for the development of this project and the repository is hosted on GitHub. 

## Install
There are multiple ways to install Git on Windows. For example if you want to use Visual Studio, Git is already part of this IDE and doesn't require any additional tooling. That being said, it's usually a good idea to have access to the Git CLI as it is more robust.

[Git for Windows](https://gitforwindows.org/) is the easiest and most common way to use Git on Windows as it provides simple GUI and CLI alternatives. 

When installing, make sure that `Windows Explorer Integration` and `Git LFS` are checked.

Once installed you can now open Git Bash (or GUI) session in any directory by simply right clicking and choosing `Git Bash here`.

## Git LFS
Since GitHub limits the size of files allowed in repositories we need a way to track files beyond this limit. The solution is `Git Large File Storage` (LFS). It replaces large files such as audio samples, videos, datasets, and graphics with text pointers inside Git, while storing the file contents on a remote server.

Since we checked Git LFS during install it should be already installed. To check if that is the case open Git Bash and type in the command to check the current version of LFS.
```
$ git lfs --version
git-lfs/3.1.4 (GitHub; windows amd64; go 1.17.8)
``` 

_If `git lfs` is not recognized as a command refer to [this guide](https://docs.github.com/en/repositories/working-with-files/managing-large-files/installing-git-large-file-storage) for Git LFS installation._

Once it is working properly, we need to initialize it by typing in:
```
$ git lfs install
> Git LFS initialized.
```

## Hooks
Hooks are a custom scripts that get executed before or after certain git actions like `push`, `pull` or `commit`.

Currently in this project we only use pre-commit hooks. 
All the configuration is in the `.pre-commit-config.yaml`. 
This simple setup is possible thanks to a framework for managing and maintaining multi-language pre-commit hooks, which is simply called `pre-commit`. 
Its setup has been already described in [Python section](/Help/Python.md), now all is left to initialize it by typing in:
```
$ pre-commit install
```

If you wish to add a new pre-commit hook, refer to the [pre-commit documentation](https://pre-commit.com/#install).

### Pre-commit hooks in use:
- `dotnet-format` - C# code file formatter

## Additional materials

- New to Git CLI? Check out this small [cheat sheet](https://rogerdudler.github.io/git-guide/index.html).
- [More on Git Hooks](https://git-scm.com/book/en/v2/Customizing-Git-Git-Hooks)


[**Home**](/README.md) | [◀️ **Back:** Python](/Help/Python.md) | [**Next:** Visual Studio ▶️](/Help/VS.md)
