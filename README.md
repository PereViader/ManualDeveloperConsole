# Unity Developer Console
Powerful developer console for your unity game

Inspired by [SR Debugger](https://www.stompyrobot.uk/tools/srdebugger/) options panel, this project aims to provide a better experience for developers.

Features:
- Runtime Interface: Access the options you make on runtime just as you would interact with any other canvas.
- Runtime Option Search: Enter the name of an option you are looking for to find it quickly or discover new ones you might not know about.
- Editor Interface: Access the options you make on the Unity Editor interface for easy access.
- Custom option widgets: You don't need to rely only on the options provided by this library, make your own custom ones if you need to.
- Dynamic option categories: No need to statically define the categories of your options. Data driven systems can add their own categories with their dynamic names.
- Dynamic option control: Register and unregister options as you move through the game. You are not restricted by a static set amount of options.

## Overview

Options are composed of 3 parts

- Option: Contains the data and appropiate actions the option should take on the game
- OptionRuntimeWidget: Handles the representation of the option on the runtime option interface
- OptionEditorWidget: Handles the representation of the option on the editor option interface

When the service is built, it is provided with al the Option types it can support. For example: in order to support having a option that is just a button, the service
is configured with `ButtonOption`, `ButtonOptionRuntimeWidget` and `ButtonOptionEditorWidget`.

Once the service is built, you can provide it with instances of the registered Option and it will ready the registered widget so it can be used.

![The option panel showing options both on the game and on a editor window](Images/Example1.png?raw=true)

# How to use it

## Install the package

- Check which is the latest commit id on the `main` branch
    - For example at the time of writing this it was `a660f635012b95f5e7b037b77622b940f01ae578`
- Paste the id after the `#` symbol `https://github.com/PereViader/UnityDeveloperConsole.git?path=Assets/UnityDeveloperConsole#`
    - It should look like `https://github.com/PereViader/UnityDeveloperConsole.git?path=Assets/UnityDeveloperConsole#a660f635012b95f5e7b037b77622b940f01ae578`
- Go to `Window/Package Manager/+/Add package from git URL...` in your project and use the URL 

If you have any doubts on the GIT url format [this is the documentation](https://docs.unity3d.com/Manual/upm-git.html)

## Setup default configurations

Go to your project and create the default editor configuration by right clicking on the hierarchy and going to `Create/Unity Developer Console/Default Editor Configuration`
The project needs to have just one instance of this asset. It is used to setup the editor option widgets.

## Add it to your codebase

```c#
interface ICharacter
{
    string Name { get; }
    void ToggleInvincibility();

    ...
}

...
IUnityDeveloperConsole console = new UnityDeveloperConsoleBuilder()
    .AddDefaultOptions(configuration)
    .Build();

console.AddOption($"Character {character.Name}", new ButtonOption("Toggle Invincibility", character.ToggleInvincibility));
...
console.Show();
```

Here you can see the simplified interface for a character. For this example, the interface allows us to query the name of the character and provides the functionlity we want to expose as an option.

For a character called `Bob`, the last line in the previous example would add a new Option on a Category named `Character Bob`, and it would add a Button Option named `Toggle Invincibility` which would call the requested `ToggleInvincibility` method when the user presses the button either on the runtime or the editor interfaces.

Calling `Show` will then enable the canvas so you can interact with it.

# Included options with the library

- `ButtonOption`: Button to activate any action
```c#
new ButtonOption(name: "The name", action: () => Debug.Log("Button Pressed"))
```

- `DropdownButtonOption`: Dropdown with any amount of options and a button to confirm the selection of the active dropdown element
```c#
new DropdownButtonOption<int>(
    name: "The name", 
    optionValues: new[] { 1, 3, 5 }, 
    optionNames: new[] { "1", "3", "5" }, 
    onActivate: x => Debug.Log(x)
    );
```

- `NextPreviousOption`: Given the current status, there are buttons to move to the next and previous values based on previous and next values 
```c#
int value = 0;

new NextPreviousOption<int>(
    name: "The name",
    setValue: x => value = x,
    getValue: () => value,
    getNextValue: x => x + 1,
    getPreviousValue: x => x - 1,
    convertStringToValue: int.Parse,
    convertValueToString: Convert.ToString
    );
```

- `ToggleOption`: Allows to toggle something on and off
```c#
bool value = false;

new ToggleOption(
    name: "The name",
    getValue: () => value,
    setValue: x => value = x
    );
```


# How you can add your custom option types and widgets

If you want to create your own custom options, you will need to register your custom types both on the Runtime and Editor systems. Both work very similarly internally.

## Register Runtime Options

In order to register the new runtime widget, you will need to register each one during the build process on `UnityDeveloperConsoleBuilder` using the `AddOption` method.

```c#
var factory = new SomeCustomOptionFactory();

var service = new UnityDeveloperConsoleBuilder()
    .AddDefaultOptions(configuration)
    .AddOption(new TypeOptionConfiguration(
        typeof(SomeCustomOption).IsAssignableFrom,
        x => factory.Create((SomeCustomOption)x),
        DestroyGameObjectOptionWidgetDisposer.Dispose
        ))
    .Build();

...
```

Adding each option requests 3 delegates:
- The first, defines the type of the option. IsAssignableFrom allows us to match the `SomeCustomOption` type to itself and all types that inherit from it
- The second, is a delegate that will create the widget from the option itself
- The third, is a delegate that will dispose of the option once it is no longer beeing used

Note: Feel free to customize the creation and disposal delegates with whatever Pooling strategy you deem necesary. The default implementation does not provide any pooling at all. If your project needs it, you can reimplement the `AddDefaultOptions` with your pooling strategy.

## Register Editor Options

In order to register custom options to be displayed on the Manual Options editor window you will need to delete the previously created asset when you created the asset at `Create/Unity Developer Console/Default Editor Configuration`. This default asset is of type `DefaultEditorConfiguration`.

After you delete it, you will need to create a new scriptable object that inherits from it and add the custom options by overriding the `Create` function like this.


```c#
public class CustomUnityDeveloperConsoleEditorConfiguration : UnityDeveloperConsole.EditorPlugin.DefaultEditorConfiguration
{
    public override EditorWidgetConfiguration Create()
    {
        var configuration = base.Create();

        configuration.WidgetEntry.Add(CreateWidgetEntry<SomeCustomOption>(x => new SomeCustomOptionEditorWidget(x)));

        return configuration;
    }
}
```
