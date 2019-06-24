# FontGen_4iOS Windows

[English](readme.md) [简体中文](readme-cn.md)

## What is this for？

This Windows tool is for creating .mobileconfig profiles for installing custom fonts on iOS devices. Currently you need the Apple Configurator 2 on the Mac for doing this. The custom fonts installed on iOS devices can be used within lots of productivity apps like Microsoft Office and Apple iWork.

![Font App](img/img0004.jpg)

![Image of Fonts 1](img/img0001.jpg)

![Image of Fonts 2](img/img0002.jpg)

## The requirement(s) of this tool

Windows 7 or higher with [.Net Framework 4.5](https://www.microsoft.com/en-us/download/details.aspx?id=30653) Support.

## Usage

1. Import .otf/.ttf font files. Drag and drop from Windows Explorer is supported.

2. Click “Start/开始”，and choose the output folder。

3. Transfer the generated .mobileconfig by some means.

## Attention!

- iOS ONLY supports .otf amd .ttf font files.

- If the generated .mobileconfig file is larger than 20MB, iOS won't recongnize the profile, so please be careful of your font size.

- You must install the profile as soon as the profile was imported to the System Settings. If you import another profile when there's a profile imported but not installed, the previous profile will be overwritten by the new one. (You must install them one by one, especiallly when using AirDrop.)

![Profile Installation](img/img0003.jpg)

## Tips：How to import .mobileconfig to iOS devices.

- Upload those files to your iCloud Drive and download them within the built-in Files App.

- Send an email with those files as attachments and open the attachments within the built-in Mail App.

- Upload those files to any http(s) servers or any cloud file services as long as Safari can download them.

- Use AirDrop by any other Apple Devices. ( One profile at a time only)

- Maybe there are other ways to be discovered..

## Is there a Mac version?

- I currently have no plan of doing so. Mac already has the Apple Configurator 2 after all. It is just not as efficient as my tool since this tool can create multiple profiles at one time.


