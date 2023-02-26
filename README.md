# BeatSaberCustomSongInfo
A tool that you can use to find what folders your songs are in without having to open beat saber or trawl through files.

## Prerequisites
Only works on Windows.

Requires .NET 6.0 which can be downloaded here: [.NET 6.0 Runtime Installer Download](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.5-windows-x64-installer)

### Automatically detects custom song path if you have beat saber installed with steam
![image](https://user-images.githubusercontent.com/12065481/169604060-0bbd4b2a-8b3e-4b55-a5df-e6d1ed7f868b.png)

### Results page lets you filter results (works on song name, artist, folder, and mapper)
![image](https://user-images.githubusercontent.com/12065481/169604396-27f70f55-49e6-4a69-84af-3d20857589b9.png)

You can also open a song's folder by clicking on it and pressing "Open Selected", clicking it without anything selected opens the custom songs folder.

"Refresh" reloads the song list (useful if you added or removed songs while the program is open).

Feel free to submit pull requests if you want to add features or improve existing functionality.

If you want to do something basic, like add the BPM to the list, I would recommend downloading the repo and making the change yourself, it's nice and simple.

As for bugs, if you find any, let me know, I'll fix crashes and major issues, but stuff like UI getting squashed when the window is small, I won't fix.

Enjoy.

### New functionality
Links: A link to the song is displayed in the rightmost column if it looks like the folder contains the song id. NB: It can get it wrong sometimes. Right clicking anywhere on the song entry (name, artist, mapper, link, etc.) will copy that link to the clipboard and show a message letting you know. The message automatically closes itself.

Back button: Self-explanatory

Play button: Plays the currently selected song. Songs can be stopped or started, they cannot be paused. They always start from the start. Songs will automatically be stopped if you press the back button. They will also stop when you press the button again. You cannot play more than one song at once. You must stop the current one then start the next one. Songs are played at 33% volume based on the assumption that they are made loud for the game. Songs will automatically stop when they finish; obviously.