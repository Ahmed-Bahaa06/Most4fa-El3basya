# Scene Management Implementation Plan

## Goal Description
Implement a robust, event-driven scene loading system that avoids using brittle `buildIndex` values, which can cause merge conflicts and break for team members when scenes are reordered in the Build Settings. 
The system will use a Bootstrapper to initialize the game, load the Main Menu, and then transition to Gameplay (or Cutscenes) via ScriptableObject events.

## User Review Required
> [!IMPORTANT]
> The proposed architecture introduces a `GameSceneSO` ScriptableObject to represent scenes instead of using build indices or hardcoded strings. I want to make sure you approve of this approach before we implement it.
> Note: According to the project rules, we will also copy this plan into `Assets/_Agents/_AntigravityArtifacts/` upon completion.

## Open Questions
> [!WARNING]
> Do you want the Scene Loader to also handle a Loading Screen (with a progress bar/fade transition), or should we start with a simple direct asynchronous load first?

## Proposed Changes

### Core Scene Management Data
We will create ScriptableObjects to represent scenes, so developers can assign them in the inspector without worrying about build indices.

#### [NEW] [GameSceneSO.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Scenes/GameSceneSO.cs)
A ScriptableObject that holds an editor-only reference to a `SceneAsset` and automatically caches its `sceneName` string. This string is then used at runtime.

#### [NEW] [LoadEventChannelSO.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Events/ScriptableObjects/LoadEventChannelSO.cs)
An event channel (following our SO architecture) used to request scene loads. It will pass the `GameSceneSO` to the loader.

### Scene Loading Logic
#### [NEW] [SceneLoader.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Scenes/SceneLoader.cs)
A persistent `MonoBehaviour` (lives in a Bootstrapper scene) that listens to the `LoadEventChannelSO`. When invoked, it loads the requested scene asynchronously via `SceneManager.LoadSceneAsync(sceneName)`.

### Bootstrapper
#### [NEW] [Bootstrapper.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/Scenes/Bootstrapper.cs)
A simple script that sits in an initial "Initialization" scene. It will immediately request to load the Main Menu scene using the `SceneLoader`.

### UI Integration
#### [MODIFY] [MainMenuUI.cs](file:///d:/Unity%20Projects/Most4fa-El3basya/Assets/Scripts/UI/MainMenuUI.cs)
Update the `PlayGame()` method to invoke the `LoadEventChannelSO` and pass the `GameplaySceneSO` rather than using `SceneManager.LoadScene(...)`.

## Verification Plan
### Manual Verification
1. Create a `Bootstrapper` scene, a `MainMenu` scene, and a `Gameplay` scene.
2. Create `GameSceneSO` assets for each.
3. Hook up the `MainMenuUI` play button to trigger the event channel.
4. Run from the Bootstrapper scene and verify we successfully transition to the Main Menu, and then to the Gameplay scene.
