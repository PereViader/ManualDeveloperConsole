# ManualCheats.Core

In this repository you will find the core part of the ManualCheats library, it will allow you to quickly add cheats to your game.

Visualize hidden/difficult to access state or change the state of the game in any way you want through a simple interface that will fit in any screen size.

Inspired by [SR Debugger](https://www.stompyrobot.uk/tools/srdebugger/) options panel, this project aims to provide a better experience for developers.

Features:
- Custom cheat widgets: You don't need to rely only on the cheats provided by the library, make your own custom ones if you need to.
- Dynamic cheat categories: No need to statically define the categories of your cheats. Data driven systems can add their own categories with their dynamic names.
- Dynamic cheat control: Register and unregister cheats as you move through the game. You are not restricted by a static set amount of cheats.

Planned Features:
- Embed in external canvas: Instead of having the cheat canvas be a standalone canvas, add the panel to another panel so it can be integrated with any other tool
- Search functionality: Enter any string on a search box and do a fuzzy search to find all the cheats with a similar name or categories

## Usage Overview

Cheats are composed of two parts

- Cheat: Contains the data and appropiate actions the cheat should take on the game
- CheatWidget: Visual representation the cheat on the cheat canvas

When the service is built, it is provided with al the Cheat types it can support. For example: in order to support having a cheat that is just a button, the service
is configured with both `ButtonCheat` and `ButtonCheatWidget`.

Once the service is built, you can provide it with instances of the registered Cheat and it will ready the registered widget so it can be used.

## Examples

![Image of how the cheat panel looks](Images/Example1.png?raw=true)

# Register a cheat so it can be used

```
var buttonFactory = new ButtonCheatWidgetFactory(defaultManualCheatsConfiguration.buttonCheatWidget);

var service = new ManualCheatsServiceBuilder()
    .AddCheat(new TypeCheatConfiguration(
        t => t == typeof(ButtonCheat),
        x => buttonFactory.Create((ButtonCheat)x),
        new DestroyGameObjectCheatWidgetDisposer().Dispose
        ))
    .Build();
```

In this example we can see the following 
- `ButtonCheatWidgetFactory`: Instantiates the button widget through its prefab and initializes with a `ButtonCheat`
- `ManualCheatsServiceBuilder`: Builder class the project provides to build the cheat service
- `t => t == typeof(ButtonCheat)`: Predicate used to match the cheat to the widget creation and disposal delegates
- `x => buttonFactory.Create((ButtonCheat)x)`: Delegate used to create the widget from the `ButtonCheat`
- `new DestroyGameObjectCheatWidgetDisposer().Dispose`: Has a Dispose function that will destroy a GameObject


# Add a cheat to the panel

Imagine on your game you just spawned a character and you want to expose some functionality it has to make it invincible on a cheat. Let's see how to do that.

```
interface ICharacter
{
    string Name { get; }
    void ToggleInvincibility();
}

void RegisterCharacterCheats(IManualCheatsService service, ICharacter character)
{
    service.AddCheat($"Character {character.Name}", new ButtonCheat("Toggle Invincibility", character.ToggleInvincibility));
}
```

Imagine a character called `Bob`, calling the previous `RegisterCharacterCheats` with `Bob` would create a new Cheat Category named `Character Bob`, and it would add a Button Cheat named `Toggle Invincibility` which would call the requested `ToggleInvincibility` method when the user presses the button.