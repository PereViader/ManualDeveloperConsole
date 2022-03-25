# ManualCheats.Core

Quickly add cheats to your game with ManualCheats

Inspired by [SR Debugger](https://www.stompyrobot.uk/tools/srdebugger/) options panel, this project aims to provide a better experience for developers.

Features:
- Runtime Interface: Access the cheats you make on runtime just as you would interact with any other canvas.
- Runtime Cheat Search: Enter the name of a cheat you are looking for to find it quickly or discover new ones you might not know about.
- Editor Interface: Access the cheats you make on the Unity Editor interface for easy access.
- Custom cheat widgets: You don't need to rely only on the cheats provided by this library, make your own custom ones if you need to.
- Dynamic cheat categories: No need to statically define the categories of your cheats. Data driven systems can add their own categories with their dynamic names.
- Dynamic cheat control: Register and unregister cheats as you move through the game. You are not restricted by a static set amount of cheats.

## Usage Overview

Cheats are composed of 3 parts

- Cheat: Contains the data and appropiate actions the cheat should take on the game
- CheatRuntimeWidget: Handles the representation of the cheat on the runtime cheat interface
- CheatEditorWidget: Handles the representation of the cheat on the editor cheat interface

When the service is built, it is provided with al the Cheat types it can support. For example: in order to support having a cheat that is just a button, the service
is configured with `ButtonCheat`, `ButtonCheatRuntimeWidget` and `ButtonCheatEditorWidget`.

Once the service is built, you can provide it with instances of the registered Cheat and it will ready the registered widget so it can be used.

## Examples

![The cheat panel showing cheats both on the game and on a editor window](Images/Example1.png?raw=true)

# How to use it

## Install the package

(Repository still does not have a UPM branch, for now you can download the repository locally and reference it as a package from disk)

## Setup default configurations

Go to your project and create the default editor configuration by right clicking on the hierarchy and going to `Create/Manual Cheats/Default Editor Configuration`
The project needs to have just one instance of this asset. It is used to setup the editor cheat widgets.

## Add it to your codebase

```c#
interface ICharacter
{
    string Name { get; }
    void ToggleInvincibility();

    ...
}

...
var buttonFactory = new ButtonCheatWidgetFactory(defaultManualCheatsConfiguration.buttonCheatWidget);

IManualCheatsService service = new ManualCheatsServiceBuilder()
    .AddDefaultCheats(configuration)
    .Build();

service.AddCheat($"Character {character.Name}", new ButtonCheat("Toggle Invincibility", character.ToggleInvincibility));
...
service.Show();
```

Here you can see the simplified interface for a character. For this example, the interface allows us to query the name of the character and provides the functionlity we want to expose as a cheat.

For a character called `Bob`, the last line in the previous example would add a new Cheat on a Category named `Character Bob`, and it would add a Button Cheat named `Toggle Invincibility` which would call the requested `ToggleInvincibility` method when the user presses the button either on the runtime or the editor interfaces.

Calling `Show` will then enable the canvas so you can interact with it.

# Included cheats with the library

- `ButtonCheat`: Button to activate any action
```c#
new ButtonCheat(name: "The name", action: () => Debug.Log("Button Pressed"))
```

- `DropdownButtonCheat`: Dropdown with any amount of options and a button to confirm the selection of the active dropdown element
```c#
new DropdownButtonCheat<int>(
    name: "The name", 
    optionValues: new[] { 1, 3, 5 }, 
    optionNames: new[] { "1", "3", "5" }, 
    onActivate: x => Debug.Log(x)
    );
```

- `NextPreviousCheat`: Given the current status, there are buttons to move to the next and previous values based on previous and next values 
```c#
int value = 0;

new NextPreviousCheat<int>(
    name: "The name",
    setValue: x => value = x,
    getValue: () => value,
    getNextValue: x => x + 1,
    getPreviousValue: x => x - 1,
    convertStringToValue: int.Parse,
    convertValueToString: Convert.ToString
    );
```

- `ToggleCheat`: Allows to toggle something on and off
```c#
bool value = false;

new ToggleCheat(
    name: "The name",
    getValue: () => value,
    setValue: x => value = x
    );
```


# How you can add your custom cheat types and widgets

If you want to create your own custom cheats, you will need to register your custom types both on the Runtime and Editor systems. Both work very similarly internally.

## Register Runtime Cheats

In order to register the new runtime widget, you will need to register each one during the build process on `ManualCheatsServiceBuilder` using the `AddCheat` method.

```c#
var factory = new SomeCustomCheatFactory();

var service = new ManualCheatsServiceBuilder()
    .AddDefaultCheats(configuration)
    .AddCheat(new TypeCheatConfiguration(
        typeof(SomeCustomCheat).IsAssignableFrom,
        x => factory.Create((SomeCustomCheat)x),
        new DestroyGameObjectCheatWidgetDisposer().Dispose
        ))
    .Build();

...
```

Adding each cheat requests 3 delegates:
- The first, defines the type of the cheat. IsAssignableFrom allows us to match the `SomeCustomCheat` type to itself and all types that inherit from it
- The second, is a delegate that will create the widget from the cheat itself
- The third, is a delegate that will dispose of the cheat once it is no longer beeing used

Note: Feel free to customize the creation and disposal delegates with whatever Pooling strategy you deem necesary. The default implementation does not provide any pooling at all. If your project needs it, you can reimplement the `AddDefaultCheats` with your pooling strategy.

## Register Editor Cheats

In order to register custom cheats to be displayed on the Manual Cheats editor window you will need to delete the previously created asset when you created the asset at `Create/Manual Cheats/Default Editor Configuration`. This default asset is of type `DefaultEditorConfiguration`.

After you delete it, you will need to create a new scriptable object that inherits from it and add the custom cheats by overriding the `Create` function like this.


```c#
public class CustomManualCheatsConfiguration : ManualCheats.Core.DefaultEditorConfiguration
{
    public override EditorWidgetConfiguration Create()
    {
        var configuration = base.Create();

        configuration.WidgetEntry.Add(CreateWidgetEntry<SomeCustomCheat>(x => new SomeCustomCheatEditorWidget(x)));

        return configuration;
    }
}
```