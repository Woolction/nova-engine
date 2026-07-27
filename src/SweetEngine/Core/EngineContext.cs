// this is a temporary measure

using SweetEngine.Resources;
using SweetEngine.Editor;
using SweetLib.Intents;
using SweetLib.Devices;
using SweetEngine.MDI;

namespace SweetEngine.Core;

public unsafe readonly struct EngineContext
{
    public readonly Device* Device;
    public readonly Intent* Intent;

    public readonly KitchenManager* Kitchen;
    public readonly MixerManager* Mixer;
    public readonly ResourceManager* Resource;
    public readonly EditorManager* Editor;

    public EngineContext(Device* device, Intent* intent, KitchenManager* kitchen, MixerManager* mixer, ResourceManager* resource, EditorManager* editor)
    {
        Device = device;
        Intent = intent;
        Kitchen = kitchen;
        Mixer = mixer;
        Resource = resource;
        Editor = editor;
    }

}